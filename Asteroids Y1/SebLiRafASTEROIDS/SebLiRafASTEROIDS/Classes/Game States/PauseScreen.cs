using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

/* Worked on by:
 *  Rafael Girao
 */
/* Class Description:
 *  -   Represents the pause screen
 */

namespace Asteroids
{
    class PauseScreen
    {
        #region VARIABLES & PROPERTIES
        /* This region will contain,
         * all variables and,
         * their respective properties,
         * for this class
         */

        // Buttons
        private Button
            moBtnContinue,
            moBtnReturn,
            moBtnExit;
        // To check if a button was pressed
        private bool mdButtonPressed;
        // To track mouse moPosition and states
        private MouseState moMouseState, moMouseOldState;
        // Pause screen font
        private SpriteFont moPauseBtnFont;

        /**** End of VARIABLES & PROPERTIES region ****/
        #endregion

        #region CONSTANT DECLARATIONS
        /* This region will contain,
         * all constants for this class
         */

        // All Buttons
        private const int ButtonWidth = 200;
        private const int ButtonHeight = 100;
        private const int ButtonX = 300;
        private readonly Vector2 ButtonDimensions = new Vector2(ButtonWidth, ButtonHeight);
        private readonly Color ButtonTextColour = Color.White;
        // Continue Button
        private const int ButtonContinueY = 200;
        private readonly Vector2 ButtonContinuePosition = new Vector2(ButtonX, ButtonContinueY);
        private const string ButtonContinueText = "Continue";
        // Return Button
        private const int ButtonReturnY = 300;
        private readonly Vector2 ButtonReturnPosition = new Vector2(ButtonX, ButtonReturnY);
        private const string ButtonReturnText = "Return to Menu";
        // Exit Button
        private const int ButtonExitY = 400;
        private readonly Vector2 ButtonExitPosition = new Vector2(ButtonX, ButtonExitY);
        private const string ButtonExitText = "Exit Game";

        /**** End of CONSTANT DECLARATIONS **/
        #endregion

        #region CONSTRUCTORS
        /* This region will contain,
         * all constructors for this class
         */

        /// <summary>
        /// Default Constructor,
        /// Creates our screen,
        /// initializing all variables
        /// </summary>
        public PauseScreen()
        {
            mdButtonPressed = false;
        }

        /**** End of CONSTRUCTORS region ****/
        #endregion

        #region PUBLIC METHODS
        /* This region will contain,
         * all public methods for this class
         */

        /// <summary>
        /// Will load all external content for our Pause Screen,
        /// and initialize all externally dependant classes.
        /// </summary>
        /// <param name="aoAssetsManager">
        /// Passed ContentManager for our Content Pipeline.
        /// </param>
        public void LoadContent(ContentManager aoAssetsManager)
        {
            // Use a constant for the string
            moPauseBtnFont = aoAssetsManager.Load<SpriteFont>(FilePaths.PauseButtonFont);
            moBtnContinue = new Button(
                ButtonContinuePosition,
                ButtonDimensions,
                moPauseBtnFont,
                ButtonTextColour,
                ButtonContinueText
                );
            moBtnReturn = new Button(
                ButtonReturnPosition,
                ButtonDimensions,
                moPauseBtnFont,
                ButtonTextColour,
                ButtonReturnText
                );
            moBtnExit = new Button(
                ButtonExitPosition,
                ButtonDimensions,
                moPauseBtnFont,
                ButtonTextColour,
                ButtonExitText
                );
            moBtnContinue.LoadContent(aoAssetsManager);
            moBtnReturn.LoadContent(aoAssetsManager);
            moBtnExit.LoadContent(aoAssetsManager);
        }
        /// <summary>
        /// Will handle the Screen's Update Logic
        /// </summary>
        public void Update(ref Game.GameState aeGameState)
        {
            moBtnContinue.Update();
            moBtnReturn.Update();
            moBtnExit.Update();
            if (mdButtonPressed == false)
            {
                RefreshMouse();
                ComputeCollisions(moBtnContinue);
                ComputeCollisions(moBtnReturn);
                ComputeCollisions(moBtnExit);
            }
            else // mdButtonPressed == true
            {
                if (moBtnContinue.IsPressed() == true)
                {
                    aeGameState = Game.GameState.Level;
                    mdButtonPressed = false;
                }
                else if (moBtnReturn.IsPressed() == true)
                {
                    aeGameState = Game.GameState.MainMenu;
                    mdButtonPressed = false;
                }
                else if (moBtnExit.IsPressed() == true)
                {
                    aeGameState = Game.GameState.Exit;
                    mdButtonPressed = false;
                }
            }
        }
        /// <summary>
        /// Will draw Screen components
        /// </summary>
        /// <param name="aoSpriteBatch"></param>
        public void Draw(SpriteBatch aoSpriteBatch)
        {
            moBtnContinue.Draw(aoSpriteBatch);
            moBtnReturn.Draw(aoSpriteBatch);
            moBtnExit.Draw(aoSpriteBatch);
        }

        /**** End of PUBLIC METHODS region ****/
        #endregion

        #region PRIVATE METHODS
        /* This region will contain,
         * all private methods for this class
         */

        /// <summary>
        /// Will update the Mouse States.
        /// </summary>
        private void RefreshMouse()
        {
            Input.UpdateMouse();
            moMouseOldState = Input.MouseOldState;
            moMouseState = Input.MouseState;
        }
        /// <summary>
        /// Will handle the mouse collisions with the Passed Button.
        /// </summary>
        /// <param name="loButton">
        /// Passed Button will be changed based on mouses location.
        /// </param>
        private void ComputeCollisions(Button loButton)
        {
            if (loButton.IsBeingPressed() == false)
            {
                if (CheckIfIn(moMouseState.X, moMouseState.Y, loButton.Bounds) == true)
                {
                    if (moMouseState.LeftButton == ButtonState.Pressed &&
                        moMouseOldState.LeftButton == ButtonState.Released)
                    {
                        loButton.Pressed();

                        // This is so that no other buttons may be pressed, after one button has been pressed.
                        mdButtonPressed = true;
                    }
                    else if (loButton.State == Button.ButtonState.Active)
                    {
                        loButton.Hovered();
                    }
                }
                else
                {
                    loButton.SetActive();
                }
            }
        }
        /// <summary>
        /// Will check,
        /// using the two Passed integer's as coordinates (x and y, respectively),
        /// if they are inside the Passed Rectangle.
        /// </summary>
        /// <param name="ldXCoord">
        /// Passed integer to be used as a X coordinate.
        /// </param>
        /// <param name="ldYCoord">
        /// Passed integer to be used as a Y coordinate.
        /// </param>
        /// <param name="loBoundingRect">
        /// Passed Rectangle to be checked if other two int's are inside Passed Rectangle.
        /// </param>
        /// <returns>
        /// Returns true if the Passed coordinates are inside the Rectangle,
        /// else false.
        /// </returns>
        private bool CheckIfIn(int ldXCoord, int ldYCoord, Rectangle loBoundingRect)
        {
            bool ldInside = false;
            if (ldXCoord > loBoundingRect.X &&
                ldXCoord < loBoundingRect.X + loBoundingRect.Width &&
                ldYCoord > loBoundingRect.Y &&
                ldYCoord < loBoundingRect.Y + loBoundingRect.Height)
            {
                ldInside = true;
            }
            return ldInside;
        }

        /**** End of PRIVATE METHODS region ****/
        #endregion

    }
} // End of namespace