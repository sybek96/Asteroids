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
 *  Date: 30/03/16
 *  Time Worked On:     [14:00 - 15:00]
 */
/* Class Description:
 *  -   This class loads up the splash image which fades in, the user will have to press the space key in order to continue
 */

namespace Asteroids
{
    class SplashScreen
    {
        #region VARIABLES & PROPERTIES
        /* This region will contain,
         * all variables and,
         * their respective properties,
         * for this class
         */

        // Background texture
        Texture2D moBackground;
        double mFadeDelay = 0.5;
        Color moColour;
        /**** End of VARIABLES & PROPERTIES region ****/
        #endregion

        #region CONSTANT DECLARATIONS
        /* This region will contain,
         * all constants for this class
         */

        private const int FadeIncrement = 10;


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
        public SplashScreen()
        {
            moColour = new Color(255, 255, 255, 0);
        }

        /**** End of CONSTRUCTORS region ****/
        #endregion

        #region PUBLIC METHODS
        /* This region will contain,
         * all public methods for this class
         */

        /// <summary>
        /// Will load all external content for this screen.
        /// </summary>
        /// <param name="aoAssetsManager"></param>
        public void LoadContent(ContentManager aoAssetsManager)
        {
            // Use a constant for the string
            moBackground = aoAssetsManager.Load<Texture2D>(FilePaths.SplashArtPath + "screen_SplashScreen");
        }
        /// <summary>
        /// Will handle the Screen's Update Logic
        /// </summary>
        public void Update(GameTime gameTime, ref Game.GameState aeGameState)
        {
            //Decrement the delay by the number of seconds that have elapsed since
            //the last time that the Update method was called
            mFadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;
 
            //If the Fade delays has dropped below zero, then it is time to 
            //fade in/fade out the image a little bit more.
            if (mFadeDelay <= 0)
            {
                //Reset the Fade delay
                mFadeDelay = 0.035;
                if (moColour.A + FadeIncrement > 255)
                {
                    moColour.A = 255;
                    if (Input.KeyboardState.GetPressedKeys().Length > 0)
                    {
                        aeGameState = Game.GameState.MainMenu;
                    }
                }
                else
                {
                    //Increment/Decrement the fade value for the image
                    moColour.A += FadeIncrement;
                }
 
            }
            
        }
        /// <summary>
        /// Will draw Screen components
        /// </summary>
        /// <param name="aoSpriteBatch"></param>
        public void Draw(SpriteBatch aoSpriteBatch, Rectangle aoMainFrame)
        {
            aoSpriteBatch.Draw(moBackground, aoMainFrame, moColour);
        }

        /**** End of PUBLIC METHODS region ****/
        #endregion

        #region PRIVATE METHODS
        /* This region will contain,
         * all private methods for this class
         */

        /**** End of PRIVATE METHODS region ****/
        #endregion

    }
} // End of namespace