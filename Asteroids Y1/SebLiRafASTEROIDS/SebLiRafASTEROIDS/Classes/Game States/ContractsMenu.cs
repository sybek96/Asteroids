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
 *  Sebastian Kruzel
 */
/* Class Description:
 *  -   Will represent the contract menu
 */

namespace Asteroids
{
    class ContractsMenu
    {
        #region VARIABLES & PROPERTIES
        /* This region will contain,
         * all variables and,
         * their respective properties,
         * for this class
         */

        Texture2D contract1Texture;
        Texture2D contract2Texture;
        Texture2D contract3Texture;
        Texture2D contract4Texture;
        Texture2D contract5Texture;
        Texture2D contract6Texture;
        Texture2D contract7Texture;
        Texture2D contract8Texture;
        Texture2D currentContract;
        Button acceptBtn;
        Button declineBtn;
        SpriteFont moFont;
        MouseState
            mouseState, mouseOldState;
        bool mdIsButtonPressed = false;
        /**** End of VARIABLES & PROPERTIES region ****/
        #endregion

        #region CONSTANT DECLARATIONS
        /* This region will contain,
         * all constants for this class
         */
        readonly Color NoFilter = Color.White;
        const int ButtonWidth = 150;
        const int ButtonHeight = 70;
        readonly Vector2 AcceptBtnPosition = new Vector2(50, 520);
        readonly Vector2 DeclineBtnPosition = new Vector2(280, 520);
        readonly Vector2 ButtonSize = new Vector2(ButtonWidth, ButtonHeight);
        readonly Color ButtonTextColour = Color.White;
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
        public ContractsMenu()
        {

        }

        /**** End of CONSTRUCTORS region ****/
        #endregion

        #region PUBLIC METHODS
        /* This region will contain,
         * all public methods for this class
         */

        public void LoadContent(ContentManager aoAssetsManager)
        {
            contract1Texture = aoAssetsManager.Load<Texture2D>(FilePaths.ContractsPath + "screen_contract01");
            contract2Texture = aoAssetsManager.Load<Texture2D>(FilePaths.ContractsPath + "screen_contract02");
            contract3Texture = aoAssetsManager.Load<Texture2D>(FilePaths.ContractsPath + "screen_contract03");
            contract4Texture = aoAssetsManager.Load<Texture2D>(FilePaths.ContractsPath + "screen_contract04");
            contract5Texture = aoAssetsManager.Load<Texture2D>(FilePaths.ContractsPath + "screen_contract05");
            contract6Texture = aoAssetsManager.Load<Texture2D>(FilePaths.ContractsPath + "screen_contract06");
            contract7Texture = aoAssetsManager.Load<Texture2D>(FilePaths.ContractsPath + "screen_contract07");
            contract8Texture = aoAssetsManager.Load<Texture2D>(FilePaths.ContractsPath + "screen_contract08");
            moFont = aoAssetsManager.Load<SpriteFont>(FilePaths.HelpScreenPath + "Font");
            acceptBtn = new Button(AcceptBtnPosition, ButtonSize, moFont, ButtonTextColour, "Accept");
            declineBtn = new Button(DeclineBtnPosition, ButtonSize, moFont, ButtonTextColour, "Decline");
            acceptBtn.LoadContent(aoAssetsManager);
            declineBtn.LoadContent(aoAssetsManager);
            currentContract = contract1Texture;
        }
        /// <summary>
        /// Will handle the Screen's Update Logic
        /// </summary>
        public void Update(int adCurrentLevel, ref Game.GameState aeGameState)
        {
            switch (adCurrentLevel)
            {
                case 0:
                    currentContract = contract1Texture;
                    break;
                case 1:
                    currentContract = contract2Texture;
                    break;
                case 2:
                    currentContract = contract3Texture;
                    break;
                case 3:
                    currentContract = contract4Texture;
                    break;
                case 4:
                    currentContract = contract5Texture;
                    break;
                case 5:
                    currentContract = contract6Texture;
                    break;
                case 6:
                    currentContract = contract7Texture;
                    break;
                case 7:
                    currentContract = contract8Texture;
                    break;
                default:
                    break;
            }
            acceptBtn.Update();
            declineBtn.Update();
            BtnPress(ref aeGameState);
        }
        /// <summary>
        /// Will draw Screen components
        /// </summary>
        /// <param name="aoSpriteBatch"></param>
        public void Draw(SpriteBatch aoSpriteBatch, Rectangle mainFrame)
        {
            aoSpriteBatch.Draw(currentContract, mainFrame, NoFilter);
            acceptBtn.Draw(aoSpriteBatch);
            declineBtn.Draw(aoSpriteBatch);
        }

        /**** End of PUBLIC METHODS region ****/
        #endregion

        #region PRIVATE METHODS
        /* This region will contain,
         * all private methods for this class
         */

        /* This region will contain,
         * all private methods for this class
         */
        /// <summary>
        /// will update the current mouse state
        /// </summary>
        private void RefreshMouse()
        {
            mouseOldState = mouseState;
            mouseState = Mouse.GetState();
        }
        /// <summary>
        /// Event for the button actually being hovered over
        /// </summary>
        /// <param name="aoBtn"></param>
        private void ButtonCollisions(Button aoBtn)
        {
            if (RectangleCollision(mouseState.X, mouseState.Y, aoBtn.Bounds) == true)
            {
                aoBtn.Hovered();
                if (mouseState.LeftButton == ButtonState.Pressed &&
                    mouseOldState.LeftButton == ButtonState.Released)
                {
                    aoBtn.Pressed();
                    mdIsButtonPressed = true;
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
        /// <param name="aoMouseX"></param>
        /// <param name="aoMouseY"></param>
        /// <param name="aoRectangle"></param>
        /// <returns></returns>
        private bool RectangleCollision(int aoMouseX, int aoMouseY, Rectangle aoRectangle)
        {
            bool adCollided = false;
            if (aoMouseX > aoRectangle.X &&
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
        private void BtnPress(ref Game.GameState aeGameState)
        {
            if (mdIsButtonPressed == false)
            {
                RefreshMouse();
                ButtonCollisions(acceptBtn);
                ButtonCollisions(declineBtn);
            }
            else
            {
                if (acceptBtn.IsPressed() == true)
                {
                    aeGameState = Game.GameState.Level;
                    mdIsButtonPressed = false;
                }
                if (declineBtn.IsPressed() == true)
                {
                    aeGameState = Game.GameState.OuterMap;
                    mdIsButtonPressed = false;
                }
            }
        }

        /**** End of PRIVATE METHODS region ****/
        #endregion
    }
} // End of namespace