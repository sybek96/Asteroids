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
 *  -   This class holds the planet
 */

namespace Asteroids
{
    class Planet
    {
        #region VARIABLES & PROPERTIES
        /* This region will contain,
         * all variables and,
         * their respective properties,
         * for this class
         */
        public enum PlanetState { Active, Inactive, Pressed}
        private PlanetState mePlanetState;
        public PlanetState State
        {
            get
            {
                return mePlanetState;
            }
            set
            {
                mePlanetState = value;
            }
        }
        float mdGemPercentage;
        public float GemPercentage
        {
            get
            {
                return mdGemPercentage;
            }
            set
            {
                mdGemPercentage = value;
            }
        }
        float mdPiratePercentage;
        public float PiratePercentage
        {
            get
            {
                return mdPiratePercentage;
            }
            set
            {
                mdPiratePercentage = value;
            }
        }
        int mdAsteroidNo;
        public int AsteroidNo
        {
            get
            {
                return mdAsteroidNo;
            }
            set
            {
                mdAsteroidNo = value;
            }
        }
        bool mdActive = false;
        public bool Active
        {
            get
            {
                return mdActive;
            }
            set
            {
                mdActive = value;
            }
        }
        Vector2 moPosition;
        string mdPlanetName;
        int moX;
        int moY;
        int moWidth;
        int moHeight;
        NeutronGem.GemColor meGemColour;
        /// <summary>
        /// Gets the gem colour that this planets spawns
        /// </summary>
        public NeutronGem.GemColor GemColour
        { get { return meGemColour; } }
        private Button moPlanetBtn;
        /// <summary>
        /// Gets the buttons bounding rectangle
        /// </summary>
        public Button Button
        { get { return moPlanetBtn; } }
        private Texture2D
            moPlanetTexture,
            moPlanetInactiveTexture,
            moPlanetBackgroundTexture;
        private SpriteFont
            moBtnFont;
        /// <summary>
        /// Gets the background for level drawing
        /// </summary>
        public Texture2D Background
        { get { return moPlanetBackgroundTexture; } }

        /**** End of VARIABLES & PROPERTIES region ****/
        #endregion

        #region CONSTANT DECLARATIONS
        /* This region will contain,
         * all constants for this class
         */

        //

        /**** End of CONSTANT DECLARATIONS **/
        #endregion

        #region CONSTRUCTORS
        /* This region will contain,
         * all constructors for this class
         */

        /// <summary>
        /// Default Constructor,
        /// Creates our planet,
        /// initializing all variables
        /// </summary>
        public Planet()
        {}
        /// <summary>
        /// Constructor:
        /// Creates our planet container,
        /// for a level using all the passed parameters.
        /// </summary>
        /// <param name="aoDimensions">Sets the planets location/dimensions</param>
        /// <param name="aePlanetState">Sets the planets state</param>
        /// <param name="adGemPercent">Sets the chance of getting a gem</param>
        /// <param name="adPiratePercent">Sets the chance of getting a pirate spawning</param>
        /// <param name="adAsteroidNo">Sets the no. of asteroids in that level</param>
        public Planet(
            Rectangle aoDimensions,
            PlanetState aePlanetState,
            float adGemPercent,
            float adPiratePercent,
            int adAsteroidNo,
            string adPlanetName,
            NeutronGem.GemColor aeGemColour)
        {
            moX = aoDimensions.X;
            moY = aoDimensions.Y;
            moWidth = aoDimensions.Width;
            moHeight = aoDimensions.Height;
            mePlanetState = aePlanetState;
            mdGemPercentage = adGemPercent;
            mdPiratePercentage = adPiratePercent;
            mdAsteroidNo = adAsteroidNo;
            mdPlanetName = adPlanetName;
            if (mePlanetState == PlanetState.Active)
            {
                mdActive = true;
            }
            else
            {
                mdActive = false;
            }
            meGemColour = aeGemColour;
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
        /// <param name="planetName"></param>
        public void LoadContent(ContentManager aoAssetsManager,string planetName)
        {
            moPlanetTexture = aoAssetsManager.Load<Texture2D>(planetName);
            moPlanetInactiveTexture = aoAssetsManager.Load<Texture2D>(planetName + "_inactive");
            moPlanetBackgroundTexture = aoAssetsManager.Load<Texture2D>(planetName + "_Background");
            moBtnFont = aoAssetsManager.Load<SpriteFont>(FilePaths.OuterMapPath + "OuterBtnFont");
            moPlanetBtn = new Button(
                new Vector2(moX, moY),
                new Vector2(moWidth, moHeight),
                moBtnFont,
                Color.White,
                mdPlanetName);
            moPlanetBtn.LoadContent(aoAssetsManager);
        }


        /**** End of PUBLIC METHODS region ****/
        #endregion

    }
} // End of namespace
