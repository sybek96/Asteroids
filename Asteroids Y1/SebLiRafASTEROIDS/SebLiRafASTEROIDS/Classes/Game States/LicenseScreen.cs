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
 *  -   Will represent the license screen
 */

namespace Asteroids
{
    class LicenseScreen
    {
        #region VARIABLES & PROPERTIES
        /* This region will contain,
         * all variables and,
         * their respective properties,
         * for this class
         */

        // Background texture
        Texture2D moBackground;
        double fadeDelay = 0.1;
        int alphaValue = 0;
        Color mColor;
        bool fadeIn = true;
        /**** End of VARIABLES & PROPERTIES region ****/
        #endregion

        #region CONSTANT DECLARATIONS
        /* This region will contain,
         * all constants for this class
         */

        private const int FadeIncrement = 10;

        private const string BackgroundTextureFile = "screen_Licenses";

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
        public LicenseScreen()
        {
            mColor = new Color(255, 255, 255, 0);
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
            moBackground = aoAssetsManager.Load<Texture2D>(FilePaths.LicenseFolderPath + BackgroundTextureFile);
        }
        /// <summary>
        /// Will handle the Screen's Update Logic
        /// </summary>
        public void Update(GameTime gameTime, ref Game.GameState aeGameState)
        {
            fadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;
            if(fadeDelay <= 0)
            {
                fadeDelay = 0.05;
                if (fadeIn == true)
                {
                    mColor.A += FadeIncrement;
                    if(mColor.A >= 255)
                    {
                        mColor.A = 255;
                        fadeIn = false;
                    }
                }
                else if (fadeIn == false)
                {
                    mColor.A -= FadeIncrement;
                    if(mColor.A == 0)
                    {
                        aeGameState = Game.GameState.SplashScreen;
                    }
                }
            }
        }
        /// <summary>
        /// Will draw Screen components
        /// </summary>
        /// <param name="aoSpriteBatch"></param>
        public void Draw(SpriteBatch aoSpriteBatch, Rectangle aoMainFrame)
        {
            aoSpriteBatch.Draw(moBackground, aoMainFrame, mColor);// new Color(255, 255, 255, alphaValue));
        }

        /**** End of PUBLIC METHODS region ****/
        #endregion

    }
} // End of namespace