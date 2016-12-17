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
 *  -   Rafael Girao
 */
/* Class Description:
 *  -   Will display a series of buttons that will take us to several menus or ingame
 */

namespace Asteroids
{
    class MainMenu
    {
        #region VARIABLES & PROPERTIES
        /* This region will contain,
         * all variables and,
         * their respective properties,
         * for this class
         */

        /* MAIN MENU VARIABLE DECLARATION */
        private Texture2D moBackground;
        private Button
            moBtnWorkshop,
            moBtnMap,
            moBtnHelp,
            moBtnOption,
            moBtnExit;
        private SpriteFont moMainMenuFont;
        private bool mdBtnPressed;

        /**** End of VARIABLES & PROPERTIES region ****/
        #endregion

        #region CONSTANT DECLARATIONS
        /* This region will contain,
         * all constants for this class
         */

        // NoFilter Colour for our SpriteBatch.Draw() methods
        private readonly Color NoFilter = Color.White;
        // File Name Constants
        private const string BackgroundTextureFileName = "MainMenu Background";
        private const string ButtonSpriteFontFileName = "MainMenuBtnFont";
        /* Button Constants */
        // All Buttons
        private const int BtnWidth = 200;
        private const int BtnHeight = 100;
        private readonly Vector2 BtnDimensions = new Vector2(BtnWidth, BtnHeight);
        private readonly Color BtnTextColour = Color.WhiteSmoke;
        // Map Button
        private const int BtnMapX = 300;
        private const int BtnMapY = 300;
        private readonly Vector2 BtnMapPosition = new Vector2(BtnMapX, BtnMapY);
        private const string BtnMapText = "Enter Game";
        // Workshop button
        private const int BtnWorkshopX = 200;
        private const int BtnWorkshopY = 400;
        private readonly Vector2 BtnWorkshopPosition = new Vector2(BtnWorkshopX, BtnWorkshopY);
        private const string BtnWorkshopText = "Workshop";
        // Help button
        private const int BtnHelpX = 600;
        private const int BtnHelpY = 500;
        private readonly Vector2 BtnHelpPosition = new Vector2(BtnHelpX, BtnHelpY);
        private const string BtnHelpText = "Help";
        // Exit button
        private const int BtnExitX = 400;
        private const int BtnExitY = 400;
        private readonly Vector2 BtnExitPosition = new Vector2(BtnExitX, BtnExitY);
        private const string BtnExitText = "Exit Game";
        // Options Button
        private const int BtnOptionX = 600;
        private const int BtnOptionY = 400;
        private readonly Vector2 BtnOptionPosition = new Vector2(BtnOptionX, BtnOptionY);
        private const string BtnOptionText = "Options";

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
        public MainMenu()
        {
            mdBtnPressed = false;
        }

        /**** End of CONSTRUCTORS region ****/
        #endregion

        #region PUBLIC METHODS
        /* This region will contain,
         * all public methods for this class
         */

        /// <summary>
        /// Will load all external content,
        /// and initialize any externally dependant classes.
        /// </summary>
        /// <param name="aoAssetsManager">
        /// Passed ContentManager for our Content Pipeline.
        /// </param>
        public void LoadContent(ContentManager aoAssetsManager)
        {
            moBackground = aoAssetsManager.Load<Texture2D>(FilePaths.MainMenuPath + BackgroundTextureFileName);
            moMainMenuFont = aoAssetsManager.Load<SpriteFont>(FilePaths.MainMenuPath + ButtonSpriteFontFileName);
            moBtnMap = new Button(
                BtnMapPosition,
                BtnDimensions,
                moMainMenuFont,
                BtnTextColour,
                BtnMapText
                );
            moBtnExit = new Button(
                BtnExitPosition,
                BtnDimensions,
                moMainMenuFont,
                BtnTextColour,
                BtnExitText
                );
            moBtnWorkshop = new Button(
                BtnWorkshopPosition,
                BtnDimensions,
                moMainMenuFont,
                BtnTextColour,
                BtnWorkshopText
                );
            moBtnHelp = new Button(
                BtnHelpPosition,
                BtnDimensions,
                moMainMenuFont,
                BtnTextColour,
                BtnHelpText
                );
            moBtnOption = new Button(
                BtnOptionPosition,
                BtnDimensions,
                moMainMenuFont,
                BtnTextColour,
                BtnOptionText
                );
            moBtnExit.LoadContent(aoAssetsManager);
            moBtnHelp.LoadContent(aoAssetsManager);
            moBtnMap.LoadContent(aoAssetsManager);
            moBtnWorkshop.LoadContent(aoAssetsManager);
            moBtnOption.LoadContent(aoAssetsManager);
        }
        /// <summary>
        /// Will handle the Screen's Update Logic
        /// </summary>
        public void Update(ref Game.GameState aeGameState)
        {
            moBtnExit.Update();
            moBtnHelp.Update();
            moBtnMap.Update();
            moBtnWorkshop.Update();
            moBtnOption.Update();

            if (mdBtnPressed == false)
            {
                RefreshMouse();
                ButtonCollisions(moBtnExit);
                ButtonCollisions(moBtnHelp);
                ButtonCollisions(moBtnMap);
                ButtonCollisions(moBtnWorkshop);
                ButtonCollisions(moBtnOption);
            }
            else
            {
                if (moBtnExit.IsPressed() == true)
                {
                    ChangeState(ref aeGameState, Game.GameState.Exit);
                }
                else if (moBtnHelp.IsPressed() == true)
                {
                    ChangeState(ref aeGameState, Game.GameState.HelpMenu);
                }
                else if (moBtnMap.IsPressed() == true)
                {
                    ChangeState(ref aeGameState, Game.GameState.OuterMap);
                }
                else if (moBtnWorkshop.IsPressed() == true)
                {
                    ChangeState(ref aeGameState, Game.GameState.Workshop);
                }
                else if (moBtnOption.IsPressed() == true)
                {
                    ChangeState(ref aeGameState, Game.GameState.OptionsMenu);
                }
            }
        }
        /// <summary>
        /// Will draw Screen components
        /// </summary>
        /// <param name="aoSpriteBatch"></param>
        public void Draw(SpriteBatch aoSpriteBatch, Rectangle aoMainFrame)
        {
            aoSpriteBatch.Draw(moBackground, aoMainFrame, NoFilter);
            moBtnExit.Draw(aoSpriteBatch);
            moBtnHelp.Draw(aoSpriteBatch);
            moBtnMap.Draw(aoSpriteBatch);
            moBtnWorkshop.Draw(aoSpriteBatch);
            moBtnOption.Draw(aoSpriteBatch);
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
                    mdBtnPressed = true;
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
        /// <summary>
        /// Will change the Passed Reference Game.GameState to the
        /// passed Game.GameState.
        /// </summary>
        /// <param name="aeState"></param>
        /// <param name="aeStateChange"></param>
        private void ChangeState(ref Game.GameState aeState, Game.GameState aeStateChange)
        {
            aeState = aeStateChange;
            mdBtnPressed = false;
        }

        /**** End of PRIVATE METHODS region ****/
        #endregion

    }
} // End of namespace