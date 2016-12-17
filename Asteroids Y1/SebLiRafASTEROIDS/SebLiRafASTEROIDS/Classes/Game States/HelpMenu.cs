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
 *  -   Sebastian Kruzel
 */
/* Class Description: 
 * This is the help menu, it allows the user to navigate through different
 * screens that will explain certain parts of the game.
 */

namespace Asteroids
{
    class HelpMenu
    {
        #region VARIABLES & PROPERTIES
        /* This region will contain,
         * all variables and,
         * their respective properties,
         * for this class
         */

        //enum that determines the current menu page
        enum HelpMenuPage
        { Main, Controls, Enemies, Asteroids, Boss, SpacePirates, Goals, Levels };
        HelpMenuPage meCurrentPage = HelpMenuPage.Main;
        Texture2D moBackground;
        Texture2D moControlsScreen;
        Texture2D moGoalsScreen;
        Texture2D moLevelsScreen;
        Texture2D moAsteroidsScreen;
        Texture2D moBossScreen;
        Texture2D moPirateScreen;
        Button moBackButton;
        Button moControlsBtn;
        Button moEnemiesBtn;
        Button moLevelsBtn;
        Button moGoalsBtn;
        Button moAsteroidsBtn;
        Button moBossBtn;
        Button moPirateBtn;
        SpriteFont moFont;
        bool mdIsButtonPressed = false;

        /**** End of VARIABLES & PROPERTIES region ****/
        #endregion

        #region CONSTANT DECLARATIONS
        /* This region will contain,
         * all constants for this class
         */
        const int ButtonWidth = 350;
        const int ButtonHeight = 100;
        readonly Vector2 BackBtnPosition = new Vector2(600, 00);
        readonly Vector2 ControlsBtnPosition = new Vector2(200, 100);
        readonly Vector2 EnemiesBtnPosition = new Vector2(200, 200);
        readonly Vector2 LevelsBtnPosition = new Vector2(200, 300);
        readonly Vector2 GoalsBtnPosition = new Vector2(200, 400);
        readonly Vector2 AsteroidsBtnPosition = new Vector2(200, 150);
        readonly Vector2 BossBtnPosition = new Vector2(200, 250);
        readonly Vector2 PiratesBtnPosition = new Vector2(200, 350);
        readonly Vector2 ButtonSize = new Vector2(ButtonWidth,ButtonHeight);
        readonly Vector2 BackButtonSize = new Vector2(200,100);
        readonly Vector2 RootHeaderPos = new Vector2(310,50);
        readonly Vector2 EnemiesHeaderPos = new Vector2(330, 80);
        readonly Color ButtonTextColour = Color.White;
        readonly Color NoFilter = Color.White;
        const string RootHeader = "HELP MENU";
        const string EnemiesHeader = "ENEMIES";

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
        public HelpMenu()
        {

        }

        /**** End of CONSTRUCTORS region ****/
        #endregion

        #region PUBLIC METHODS
        /* This region will contain,
         * all public methods for this class
         */

        /// <summary>
        /// Will Load all external content,
        /// and initialize any externally dependant classes.
        /// </summary>
        /// <param name="aoAssetsManager"></param>
        public void LoadContent(ContentManager aoAssetsManager)
        {
            moBackground = aoAssetsManager.Load<Texture2D>(FilePaths.HelpScreenPath + "screen_HelpMenu");
            moControlsScreen = aoAssetsManager.Load<Texture2D>(FilePaths.HelpScreenPath + "screen_HelpMenu_Controls");
            moGoalsScreen = aoAssetsManager.Load<Texture2D>(FilePaths.HelpScreenPath + "screen_HelpMenu_Goals");
            moLevelsScreen = aoAssetsManager.Load<Texture2D>(FilePaths.HelpScreenPath + "screen_HelpMenu_Levels");
            moAsteroidsScreen = aoAssetsManager.Load<Texture2D>(FilePaths.HelpScreenPath + "screen_HelpMenu_Enemies_Asteroids");
            moBossScreen = aoAssetsManager.Load<Texture2D>(FilePaths.HelpScreenPath + "screen_HelpMenu_Enemies_FinalBoss");
            moPirateScreen = aoAssetsManager.Load<Texture2D>(FilePaths.HelpScreenPath + "screen_HelpMenu_Enemies_SpacePirates");
            moFont = aoAssetsManager.Load<SpriteFont>(FilePaths.HelpScreenPath + "Font");
            moControlsBtn = new Button(ControlsBtnPosition, ButtonSize, moFont, ButtonTextColour, "Controls");
            moEnemiesBtn = new Button(EnemiesBtnPosition, ButtonSize, moFont, ButtonTextColour, "Enemies");
            moLevelsBtn = new Button(LevelsBtnPosition, ButtonSize, moFont, ButtonTextColour, "Levels");
            moGoalsBtn = new Button(GoalsBtnPosition, ButtonSize, moFont, ButtonTextColour, "Goals");
            moAsteroidsBtn = new Button(AsteroidsBtnPosition, ButtonSize, moFont, ButtonTextColour, "Asteroids");
            moBossBtn = new Button(BossBtnPosition, ButtonSize, moFont, ButtonTextColour, "Boss");
            moPirateBtn = new Button(PiratesBtnPosition, ButtonSize, moFont, ButtonTextColour, "Pirates");
            moBackButton = new Button(BackBtnPosition, BackButtonSize, moFont,ButtonTextColour, "Back");
            moBackButton.LoadContent(aoAssetsManager);
            moControlsBtn.LoadContent(aoAssetsManager);
            moEnemiesBtn.LoadContent(aoAssetsManager);
            moLevelsBtn.LoadContent(aoAssetsManager);
            moGoalsBtn.LoadContent(aoAssetsManager);
            moAsteroidsBtn.LoadContent(aoAssetsManager);
            moBossBtn.LoadContent(aoAssetsManager);
            moPirateBtn.LoadContent(aoAssetsManager);
        }
        /// <summary>
        /// Will handle the Screen's Update Logic
        /// </summary>
        public void Update(ref Game.GameState aeGameState)
        {
            RefreshMouse();
            switch (meCurrentPage)
            {
                case HelpMenuPage.Main:
                    moBackButton.Update();
                    moControlsBtn.Update();
                    moEnemiesBtn.Update();
                    moLevelsBtn.Update();
                    moGoalsBtn.Update();
                    BtnPressBack(ref aeGameState);
                    BtnPressMain();
                    break;
                case HelpMenuPage.Controls:
                    moBackButton.Update();
                    BtnPressBack(ref aeGameState);
                    break;
                case HelpMenuPage.Enemies:
                    moBackButton.Update();
                    moAsteroidsBtn.Update();
                    moBossBtn.Update();
                    moPirateBtn.Update();
                    BtnPressBack(ref aeGameState);
                    BtnPressEnemies();
                    break;
                case HelpMenuPage.Asteroids:
                    moBackButton.Update();
                    BtnPressBack(ref aeGameState);
                    break;
                case HelpMenuPage.Boss:
                    moBackButton.Update();
                    BtnPressBack(ref aeGameState);
                    break;
                case HelpMenuPage.SpacePirates:
                    moBackButton.Update();
                    BtnPressBack(ref aeGameState);
                    break;
                case HelpMenuPage.Goals:
                    moBackButton.Update();
                    BtnPressBack(ref aeGameState);
                    break;
                case HelpMenuPage.Levels:
                    moBackButton.Update();
                    BtnPressBack(ref aeGameState);
                    break;
                default:
                    meCurrentPage = HelpMenuPage.Main;
                    break;
            }

        }
        /// <summary>
        /// Will draw Screen components
        /// </summary>
        /// <param name="aoSpriteBatch"></param>
        public void Draw(SpriteBatch aoSpriteBatch, Rectangle aoMainFrame)
        {
            switch (meCurrentPage)
            {
                case HelpMenuPage.Main:
                    aoSpriteBatch.Draw(moBackground, aoMainFrame, NoFilter);
                    aoSpriteBatch.DrawString(moFont, RootHeader, RootHeaderPos, NoFilter);
                    moControlsBtn.Draw(aoSpriteBatch);
                    moEnemiesBtn.Draw(aoSpriteBatch);
                    moLevelsBtn.Draw(aoSpriteBatch);
                    moGoalsBtn.Draw(aoSpriteBatch);
                    moBackButton.Draw(aoSpriteBatch);
                    break;
                case HelpMenuPage.Controls:
                    aoSpriteBatch.Draw(moControlsScreen, aoMainFrame, NoFilter);
                    moBackButton.Draw(aoSpriteBatch);
                    break;
                case HelpMenuPage.Enemies:
                    aoSpriteBatch.Draw(moBackground, aoMainFrame, NoFilter);
                    aoSpriteBatch.DrawString(moFont, EnemiesHeader, EnemiesHeaderPos, NoFilter);
                    moAsteroidsBtn.Draw(aoSpriteBatch);
                    moBossBtn.Draw(aoSpriteBatch);
                    moPirateBtn.Draw(aoSpriteBatch);
                    moBackButton.Draw(aoSpriteBatch);
                    break;
                case HelpMenuPage.Asteroids:
                    aoSpriteBatch.Draw(moAsteroidsScreen, aoMainFrame, NoFilter);
                    moBackButton.Draw(aoSpriteBatch);
                    break;
                case HelpMenuPage.Boss:
                    aoSpriteBatch.Draw(moBossScreen, aoMainFrame, NoFilter);
                    moBackButton.Draw(aoSpriteBatch);
                    break;
                case HelpMenuPage.SpacePirates:
                    aoSpriteBatch.Draw(moPirateScreen, aoMainFrame, NoFilter);
                    moBackButton.Draw(aoSpriteBatch);
                    break;
                case HelpMenuPage.Goals:
                    aoSpriteBatch.Draw(moGoalsScreen, aoMainFrame, NoFilter);
                    moBackButton.Draw(aoSpriteBatch);
                    break;
                case HelpMenuPage.Levels:
                    aoSpriteBatch.Draw(moLevelsScreen, aoMainFrame, NoFilter);
                    moBackButton.Draw(aoSpriteBatch);
                    break;
                default:
                    break;
            }

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
                    mdIsButtonPressed = true;
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
        private bool RectangleCollision(int aoMouseX,int aoMouseY,Rectangle aoRectangle)
        {
            bool adCollided = false;
            if(aoMouseX > aoRectangle.X &&
                aoMouseX < aoRectangle.X + aoRectangle.Width && 
                aoMouseY > aoRectangle.Y &&
                aoMouseY < aoRectangle.Y + aoRectangle.Height)
            {
                adCollided = true;
            }
            return adCollided;
        }
        /// <summary>
        /// checks if any of the buttons pressed and does the event accordingly
        /// </summary>
        private void BtnPressMain()
        {
            if (mdIsButtonPressed == false)
            {
                ButtonCollisions(moControlsBtn);
                ButtonCollisions(moEnemiesBtn);
                ButtonCollisions(moGoalsBtn);
                ButtonCollisions(moLevelsBtn);
            }
            else
            {
                if (moControlsBtn.IsPressed() == true)
                {
                    ChangePageTo(HelpMenuPage.Controls);
                    moControlsBtn.SetActive();
                }
                if (moEnemiesBtn.IsPressed() == true)
                {
                    ChangePageTo(HelpMenuPage.Enemies);
                    moEnemiesBtn.SetActive();
                }
                if (moGoalsBtn.IsPressed() == true)
                {
                    ChangePageTo(HelpMenuPage.Goals);
                    moGoalsBtn.SetActive();
                }
                if (moLevelsBtn.IsPressed() == true)
                {
                    ChangePageTo(HelpMenuPage.Levels);
                    moLevelsBtn.SetActive();
                }
            }
        }
        /// <summary>
        /// Event for button clicks in the enemy page of the help menu
        /// </summary>
        private void BtnPressEnemies()
        {
            if (mdIsButtonPressed == false)
            {
                ButtonCollisions(moAsteroidsBtn);
                ButtonCollisions(moBossBtn);
                ButtonCollisions(moPirateBtn);
            }
            else
            {
                if (moAsteroidsBtn.IsPressed() == true)
                {
                    ChangePageTo(HelpMenuPage.Asteroids);
                    moAsteroidsBtn.SetActive();
                }
                if (moBossBtn.IsPressed() == true)
                {
                    ChangePageTo(HelpMenuPage.Boss);
                    moBossBtn.SetActive();
                }
                if (moPirateBtn.IsPressed() == true)
                {
                    ChangePageTo(HelpMenuPage.SpacePirates);
                    moPirateBtn.SetActive();
                }
            }
        }
        /// <summary>
        /// Event for the back button being pressed
        /// </summary>
        private void BtnPressBack(ref Game.GameState aeGameState)
        {
            if (mdIsButtonPressed == false)
            {
                ButtonCollisions(moBackButton);
            }
            else
            {
                if (moBackButton.IsPressed() == true)
                {
                    ChangePage(meCurrentPage, ref aeGameState);
                    moBackButton.SetActive();
                }
            }
        }
        /// <summary>
        /// Will change the HelpMenuPage to the Passed one.
        /// </summary>
        /// <param name="aeHelpMenuPage"></param>
        private void ChangePageTo(HelpMenuPage aeHelpMenuPage)
        {
            mdIsButtonPressed = false;
            meCurrentPage = aeHelpMenuPage;
        }
        /// <summary>
        /// Will change the HelpMenuPage based what HelpMenuPage is passed.
        /// </summary>
        /// <param name="aeHelpMenuPage"></param>
        /// <param name="aeGameState"></param>
        private void ChangePage(HelpMenuPage aeHelpMenuPage, ref Game.GameState aeGameState)
        {   
            switch (aeHelpMenuPage)
            {
                case HelpMenuPage.Asteroids:
                case HelpMenuPage.Boss:
                case HelpMenuPage.SpacePirates:
                    ChangePageTo(HelpMenuPage.Enemies);
                    break;
                case HelpMenuPage.Main:
                    mdIsButtonPressed = false;
                    aeGameState = Game.GameState.MainMenu;
                    break;
                case HelpMenuPage.Controls:
                case HelpMenuPage.Enemies:
                case HelpMenuPage.Goals:
                case HelpMenuPage.Levels:
                    ChangePageTo(HelpMenuPage.Main);
                    break;
                default:
                    break;
            }
        }

        /**** End of PRIVATE METHODS region ****/
        #endregion

    }
} // End of namespace