using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace InputExample
{
    public class InputManager
    {

        KeyboardState currentKBState;
        KeyboardState previousKBState;
        MouseState currentMState;
        MouseState previousMState;
        GamePadState currentGPState;
        GamePadState previousGPState;

        

        /// <summary>
        /// Requested Direciton
        /// </summary>
        public Vector2 Direction { get; private set; }

        /// <summary>
        /// Requested Warp
        /// </summary>
        public bool Warp { get; private set; }

        /// <summary>
        /// Input for exiting
        /// </summary>
        public bool Exit { get; private set; }

        /// <summary>
        /// Updates each frame
        /// </summary>
        /// <param name="gameTime">information</param>
        public void Update(GameTime gameTime)
        {
            #region Input State Updating

            previousKBState = currentKBState;
            previousMState = currentMState;
            previousGPState = currentGPState;
            currentKBState = Keyboard.GetState();
            currentMState = Mouse.GetState();
            currentGPState = GamePad.GetState(0);

            #endregion

            #region Direction Input
            //Get Mouse Pos
            Direction = currentGPState.ThumbSticks.Right * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;


            //Get Postion from KB
            if (currentKBState.IsKeyDown(Keys.Left) || currentKBState.IsKeyDown(Keys.A))
            {
                Direction += new Vector2(-100 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
            }
            if (currentKBState.IsKeyDown(Keys.Up) || currentKBState.IsKeyDown(Keys.W))
            {
                Direction += new Vector2(0, -100 * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (currentKBState.IsKeyDown(Keys.Down) || currentKBState.IsKeyDown(Keys.S))
            {
                Direction += new Vector2(0, 100 * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (currentKBState.IsKeyDown(Keys.Right) || currentKBState.IsKeyDown(Keys.D))
            {
                Direction += new Vector2(100 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
            }

            #endregion

            #region Warp Input

            Warp = false;
            if (currentKBState.IsKeyDown(Keys.Space) && !previousKBState.IsKeyDown(Keys.Space))
            {
                Warp = true;
            }
            if (currentGPState.IsButtonDown(Buttons.A) && previousGPState.IsButtonUp(Buttons.A))
            {
                Warp = true;
            }

            #endregion

            #region Exit Input

            if (currentGPState.Buttons.Back == ButtonState.Pressed || currentKBState.IsKeyDown(Keys.Escape))
                Exit = true;

            #endregion

        }

    }
}
