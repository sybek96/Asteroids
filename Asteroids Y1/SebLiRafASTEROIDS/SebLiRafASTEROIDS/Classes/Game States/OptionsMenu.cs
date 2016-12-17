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
 *  Liam Hickey
 */
/* Class Description:
 *  -   Options selector for the controls and options of the game
 */

namespace Asteroids
{
    class OptionsMenu
    {
        #region VARIABLES & PROPERTIES
        /* This region will contain,
         * all variables and,
         * their respective properties,
         * for this class
         */

        // Background texture
        Texture2D moBackground;
        Texture2D controlsSprite;
        SpriteFont writeFont;
        Vector2 controlsDiagramLocation;
        Vector2 topLeftCorner = new Vector2(0, 0);
        Vector2 muteButtonLocation = new Vector2(100, 450);
        Vector2 controlsSouthPawLocation = new Vector2(500, 450);
        Button muteButton;       
        Button controlsSouthPaw;
        Button backButton;
        bool defaultControls = true;
        bool muted = false;
        bool mdButtonPressed = false;

        /**** End of VARIABLES & PROPERTIES region ****/
        #endregion

        #region CONSTANT DECLARATIONS
        /* This region will contain,
         * all constants for this class
         */

        const string muteText = "Mute";
        const string southPawText = "Set to Southpaw";
        const string backText = "Back";
        readonly Vector2 buttonDimensions = new Vector2(200, 100);
        readonly Vector2 BackBtnPosition = new Vector2(600, 00);

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
        public OptionsMenu()
        {

        }

        /**** End of CONSTRUCTORS region ****/
        #endregion

        #region PUBLIC METHODS
        /* This region will contain,
         * all public methods for this class
         */

        /// <summary>
        /// Will load all external content
        /// </summary>
        /// <param name="aoAssetsManager"></param>
        public void LoadContent(ContentManager aoAssetsManager)
        {
            moBackground = aoAssetsManager.Load<Texture2D>(FilePaths.OptionMenuPath+ "BackgroundOptions"); // Needs to be pathed
            writeFont = aoAssetsManager.Load<SpriteFont>(FilePaths.OptionMenuPath + "BtnFont");
            controlsSprite = aoAssetsManager.Load<Texture2D>(FilePaths.OptionMenuPath+"ControlsDiagram");
            muteButton = new Button(muteButtonLocation, buttonDimensions, writeFont, Color.White, muteText);
            controlsSouthPaw = new Button(controlsSouthPawLocation, buttonDimensions, writeFont, Color.White, southPawText);
            backButton = new Button(BackBtnPosition, buttonDimensions, writeFont, Color.White, backText);
            muteButton.LoadContent(aoAssetsManager);
            controlsSouthPaw.LoadContent(aoAssetsManager);
            backButton.LoadContent(aoAssetsManager);
            controlsDiagramLocation = new Vector2(800 - controlsSprite.Width - 80, 600 - controlsSprite.Height - 180);
        }
        /// <summary>
        /// Will handle the Screen's Update Logic
        /// </summary>
        public void Update(Keys[] controls, ref Game.GameState aeGameState)
        {
            muteButton.Update();
            controlsSouthPaw.Update();
            backButton.Update();

            if (mdButtonPressed == false)
            {
                // Checks for mouse over buttons
                RefreshMouse();
                ButtonCollisions(muteButton);
                ButtonCollisions(controlsSouthPaw);
                ButtonCollisions(backButton);
            }
            else
            {
                // Execute actions based on what the button states are
                if (muteButton.IsPressed() == true)
                {
                    if (muted == false)
                    {
                        SoundEffect.MasterVolume = 0f; // If the mute button is pressed set the master volume to zero
                        MediaPlayer.Volume = 0f;
                        muteButton.SetText("Unmute");
                    }
                    else
                    {
                        SoundEffect.MasterVolume = 1f; // Otherwise the master volume is maxed
                        MediaPlayer.Volume = 1f;
                        muteButton.SetText(muteText);
                    }
                    muted = !muted;
                    mdButtonPressed = false;
                }
                else if (controlsSouthPaw.IsPressed() == true)
                {
                    if (defaultControls == true)
                    {
                        SouthPaw(ref controls); // Alternate the controls layout to southpaw
                    }
                    else
                    {
                        Default(ref controls); // Alternate the controls layout to the default state
                    }
                    defaultControls = !defaultControls;
                    mdButtonPressed = false;
                }
                else if (backButton.IsPressed() == true)
                {
                    aeGameState = Game.GameState.MainMenu;
                    mdButtonPressed = false;
                }
            }
        }
        /// <summary>
        /// Will draw Screen components
        /// </summary>
        /// <param name="aoSpriteBatch"></param>
        public void Draw(SpriteBatch aoSpriteBatch, Rectangle aoMainFrame)
        {
            aoSpriteBatch.Draw(moBackground, aoMainFrame, Color.White);
            aoSpriteBatch.Draw(controlsSprite, controlsDiagramLocation, Color.White);
            muteButton.Draw(aoSpriteBatch);
            controlsSouthPaw.Draw(aoSpriteBatch);
            backButton.Draw(aoSpriteBatch);
        }

        /**** End of PUBLIC METHODS region ****/
        #endregion

        #region PRIVATE METHODS
        /* This region will contain,
         * all private methods for this class
         */

        /// <summary>
        /// will update the current mouse state
        /// </summary>
        private void RefreshMouse()
        {
            Input.UpdateMouse();
        }
        /// <summary>
        /// Event for the button actually being hovered over
        /// </summary>
        /// <param name="aoBtn"></param>
        private void ButtonCollisions(Button aoBtn)
        {
            if (RectangleCollision(Input.MouseState.X, Input.MouseState.Y, aoBtn.Bounds) == true)
            {
                if (Input.MouseState.LeftButton == ButtonState.Pressed &&
                    Input.MouseOldState.LeftButton == ButtonState.Released)
                {
                    aoBtn.Pressed();
                    mdButtonPressed = true;
                }
                else
                {
                    aoBtn.Hovered();
                }
            }
            else
            {
                aoBtn.SetActive();
            }
        }
        /// <summary>
        /// Rectangular collision checking if mouse inside a rectangle
        /// </summary>
        /// <param name="aoXCoord"></param>
        /// <param name="aoYCoord"></param>
        /// <param name="aoRectangle"></param>
        /// <returns></returns>
        private bool RectangleCollision(int aoXCoord, int aoYCoord, Rectangle aoRectangle)
        {
            bool adCollided = false;
            if (aoXCoord > aoRectangle.X &&
                aoXCoord < aoRectangle.X + aoRectangle.Width &&
                aoYCoord > aoRectangle.Y &&
                aoYCoord < aoRectangle.Y + aoRectangle.Height)
            {
                adCollided = true;
            }
            return adCollided;
        }

        private void SouthPaw(ref Keys[] controls)
        {
            controls[0] = Keys.W;
            controls[1] = Keys.D;
            controls[2] = Keys.S;
            controls[3] = Keys.A;
            controls[4] = Keys.Space;
            controls[5] = Keys.RightControl;
            controlsSouthPaw.SetText("Set to Default");
        }

        private void Default(ref Keys[] controls)
        {
            controls[0] = Keys.Up;
            controls[1] = Keys.Right;
            controls[2] = Keys.Down;
            controls[3] = Keys.Left;
            controls[4] = Keys.Space;
            controls[5] = Keys.LeftControl;
            controlsSouthPaw.SetText(southPawText);
        }

        /**** End of PRIVATE METHODS region ****/
        #endregion

    }
} // End of namespace