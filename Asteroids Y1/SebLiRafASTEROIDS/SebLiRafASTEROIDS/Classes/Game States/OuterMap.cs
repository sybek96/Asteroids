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
 *  -   Will represent the galactic map of the game.
 */

namespace Asteroids
{
    class OuterMap
    {
        #region VARIABLES & PROPERTIES
        /* This region will contain,
         * all variables and,
         * their respective properties,
         * for this class
         */

        // Background texture
        Texture2D moBackground;
        SpriteFont moFont;
        NeutronGem moGem;
        Vector2
            moPlanetStatPosition;
        Rectangle
            moGemRectangle;
        // represents what level was selected
        private int mdLevelSelected;
        /// <summary>
        /// Gets the level selected in the outer map (0 to 7)
        /// </summary>
        public int LevelSelected
        { get { return mdLevelSelected; } }
        private string mdPlanetStats;
        private bool mdPlanetSelected;

        /**** End of VARIABLES & PROPERTIES region ****/
        #endregion

        #region CONSTANT DECLARATIONS
        /* This region will contain,
         * all constants for this class
         */

        readonly Color NoFilter = Color.White;
        readonly Color TextColour = Color.White;

        private const int GemPosX = 700;
        private const int GemPosY = 520;
        private const int GemSize = 100;

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
        public OuterMap()
        {
            moPlanetStatPosition = new Vector2(400f, 480f);
            moGemRectangle = new Rectangle(GemPosX, GemPosY, GemSize, GemSize);
            mdPlanetStats = "";
            mdPlanetSelected = false;
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
        public void LoadContent(ContentManager aoAssetsManager)
        {
            moBackground = aoAssetsManager.Load<Texture2D>(FilePaths.OuterMapPath + "Outer_Map_Background");
            moFont = aoAssetsManager.Load<SpriteFont>(FilePaths.OuterMapPath + "OuterMapFont");
        }
        /// <summary>
        /// Will handle the Screen's Update Logic
        /// </summary>
        public void Update(Planet[] planets, ref Game.GameState aeGameState, ContentManager aoAssetsManager)
        {
            Input.UpdateMouse();
            mdPlanetStats = "";
            for (int i = 0; i < planets.Length; i++)
            {
                planets[i].Button.Update();
                if (mdPlanetSelected == false)
                {
                    if (planets[i].State == Planet.PlanetState.Active)
                    {
                        if (ProcessCollisions(Input.MouseState.X, Input.MouseState.Y, planets[i].Button.Bounds) == true) //check if mouse over the current planet
                        {
                            planets[i].Button.Hovered();
                            MouseState
                                aoMouseState = Input.MouseState,
                                aoMouseOldState = Input.MouseOldState;
                            if (aoMouseState.LeftButton == ButtonState.Pressed &&
                                aoMouseOldState.LeftButton == ButtonState.Released) //once clicked change to pressed state
                            {
                                planets[i].Button.Pressed();
                                mdPlanetSelected = true;
                            }
                            UpdatePlanetStats(planets, i, aoAssetsManager);
                        }
                        else
                        {
                            planets[i].Button.SetActive();
                        }
                    }
                }
                else // mdPlanetSelected == true
                {
                    if (planets[i].Button.IsPressed() == true)
                    {
                        aeGameState = Game.GameState.ContractsMenu;
                        mdLevelSelected = i;
                        mdPlanetSelected = false;
                    }
                }
            }
        }
        /// <summary>
        /// Will draw Screen components
        /// </summary>
        public void Draw(SpriteBatch aoSpriteBatch, Planet[] planets, Rectangle mainFrame)
        {
            aoSpriteBatch.Draw(moBackground,mainFrame,NoFilter);
            aoSpriteBatch.DrawString(moFont, mdPlanetStats, moPlanetStatPosition, TextColour);
            for (int i = 0; i < planets.Length; i++)
            {
                if (planets[i].State == Planet.PlanetState.Active)
                {
                    planets[i].Button.Draw(aoSpriteBatch);
                    if (planets[i].Button.IsHovered() == true)
                    {
                        if (moGem != null && i != 7)
                        {
                            moGem.Draw(aoSpriteBatch);
                        }
                    }
                }
            }
        }

        /**** End of PUBLIC METHODS region ****/
        #endregion

        #region PRIVATE METHODS
        /* This region will contain,
         * all private methods for this class
         */

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aoMouseX"></param>
        /// <param name="aoMouseY"></param>
        /// <param name="aoRectangle"></param>
        /// <returns></returns>
        private bool ProcessCollisions(int aoMouseX, int aoMouseY, Rectangle aoRectangle)
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
        /// 
        /// </summary>
        /// <param name="aoPlanets"></param>
        /// <param name="adPlanetLevel"></param>
        private void UpdatePlanetStats(Planet[] aoPlanets, int adPlanetLevel, ContentManager aoAssetsManager)
        {
            mdPlanetStats =
                "Level: " +
                (adPlanetLevel + 1).ToString("n0") + "\n";
                
            if (aoPlanets[adPlanetLevel].Active == true)
	        {
                mdPlanetStats +=
                    "Available\n";
	        }
            else
            {
                mdPlanetStats +=
                    "Unavailable\n";
            }
            mdPlanetStats +=
                "Number of asteroids: " +
                aoPlanets[adPlanetLevel].AsteroidNo.ToString("n0") + "\n" +
                "Chance of Pirates: " +
                (aoPlanets[adPlanetLevel].PiratePercentage * 100).ToString() + "%\n" +
                "Chance of Gems: " +
                (aoPlanets[adPlanetLevel].GemPercentage * 100).ToString() + "%\n";
            moGem = new NeutronGem(aoPlanets[adPlanetLevel].GemColour, moGemRectangle);
            moGem.LoadContent(aoAssetsManager);
            moGem.Activate(moGem.Position);
        }

        /**** End of PRIVATE METHODS region ****/
        #endregion

    }
} // End of namespace