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
 *  -   Liam Hickey
 */
/* Class Description:
 *  -   Class for any instance of neutron gems in the game
 *      Neutron Gmes have the abilty to be rotated and collected
 *      by the player
 */

namespace Asteroids
{
    class NeutronGem
    {
        #region VARIABLES AND PROPERTIES
        /* This region will contain,
         * all variables and,
         * their respective properties,
         * for this class
         */
        public enum GemColor { Black, Blue, Green, LightBlue, Purple, Red, Yellow }
        GemColor gemColor;
        Vector2 mPosition;
        /// <summary>
        /// Gets the gem's current position
        /// </summary>
        public Vector2 Position
        { get { return mPosition; } }
        Vector2 mOriginPoint; // Used for rotating the sprite about a point
        Vector2 mVelocity;
        Rectangle mBoundaryBox;
        /// <summary>
        /// Gets the hit box
        /// </summary>
        public Rectangle HitBox
        { get { return mBoundaryBox; } }
        Texture2D mSpriteTexture;
        float textureWidth;
        float textureHeight;
        float currentAngle;
        bool active;
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        /// <summary>
        /// Gets the gem's current colour.
        /// </summary>
        public GemColor Colour
        { get { return gemColor; } }

        /**** End of VARIABLES & PROPERTIES region ****/
        #endregion

        #region CONSTANT DECLARATIONS
        /* This region will contain,
         * all constants for this class
         */

        const float RotationAngle = (float)(Math.PI / 96);

        /**** End of CONSTANT DECLARATIONS **/
        #endregion

        #region CONSTRUCTORS
        /* This region will contain,
         * all constructors for this class
         */

        //Default constructor method
        public NeutronGem(GemColor inputColor, Vector2 initialPosition)
        {
            gemColor = inputColor;
            mPosition = initialPosition;
            active = false;
        }

        public NeutronGem(GemColor inputColor, Rectangle initialRectangle)
        {
            gemColor = inputColor;
            mPosition.X = initialRectangle.X;
            mPosition.Y = initialRectangle.Y;
            mBoundaryBox = initialRectangle;
            active = false;
        }
        // Overloaded constructor
        public NeutronGem(Vector2 initialPosition, Color[] colorArray)
        {
            active = false;
            mPosition = initialPosition;
        }

        /**** End of CONSTRUCTORS **/
        #endregion

        #region PUBLIC METHODS
        /* This region will contain,
         * all public methods for this class
         */

        /// <summary>
        /// Will load external content
        /// </summary>
        /// <param name="aoAssetsManager"></param>
        public void LoadContent(ContentManager aoAssetsManager)
        {
            string adGemFilePath = FilePaths.NeutronGemsPath;
            switch (gemColor)
            {
                case GemColor.Black:
                    adGemFilePath += "neutron_gem_black";
                    break;
                case GemColor.Blue:
                    adGemFilePath += "neutron_gem_blue";
                    break;
                case GemColor.Green:
                    adGemFilePath += "neutron_gem_green";
                    break;
                case GemColor.LightBlue:
                    adGemFilePath += "neutron_gem_lightblue";
                    break;
                case GemColor.Purple:
                    adGemFilePath += "neutron_gem_purple";
                    break;
                case GemColor.Red:
                    adGemFilePath += "neutron_gem_red";
                    break;
                case GemColor.Yellow:
                    adGemFilePath += "neutron_gem_yellow";
                    break;
                default:
                    break;
            }
            mSpriteTexture = aoAssetsManager.Load<Texture2D>(adGemFilePath);
            
            /* Assign the texture width and height variables in
             * the LoadContent method because these variables
             * need the sprite texture to be assigned
             */
            mOriginPoint.X = mSpriteTexture.Width / 2;
            mOriginPoint.Y = mSpriteTexture.Height / 2;
        }
        /// <summary>
        /// Will handle the Screen's Update Logic
        /// </summary>
        public void Update(Vector2 playerOrigin, float tractorRadius)
        {
            if(active == true)
            {
                Vector2 gemToPlayer = playerOrigin - mPosition;
                float gemToPlayerMagnitude = gemToPlayer.Length();
                if(gemToPlayerMagnitude < tractorRadius)
                {
                    FollowBeam(gemToPlayer);
                }
                else
                {
                    mVelocity = new Vector2(0, 0);
                }
                currentAngle += RotationAngle;
                mPosition += mVelocity;
                mBoundaryBox.X = (int)mPosition.X;
                mBoundaryBox.Y = (int)mPosition.Y;
            }
        }

        /// <summary>
        /// The reset method allows us to re use the same object repeatedly (best used in conjunction with a list)
        /// </summary>
        /// <param name="inputColor"></param>
        /// <param name="initialPosition"></param>
        public void Reset(GemColor inputColor, Vector2 initialPosition)
        {
            active = true;
            gemColor = inputColor;
            mPosition = initialPosition;
            mVelocity = new Vector2(0,0);
        }
        /// <summary>
        /// 
        /// </summary>
        public NeutronGem Collect()
        {
            active = false;
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Activate(Vector2 spawnLocation)
        {
            active = true;
            mPosition = spawnLocation;
            mBoundaryBox.X = (int)mPosition.X;
            mBoundaryBox.Y = (int)mPosition.Y;
        }
        /// <summary>
        /// Will draw Screen components
        /// </summary>
        /// <param name="aoSpriteBatch"></param>
        public void Draw(SpriteBatch aoSpriteBatch)
        {
            if(active == true)
            {
                if (mBoundaryBox != null)
                {
                    aoSpriteBatch.Draw(mSpriteTexture, mBoundaryBox, null, Color.White, currentAngle, mOriginPoint, SpriteEffects.None, 0);
                }
                else
                {
                    aoSpriteBatch.Draw(mSpriteTexture, mPosition, null, Color.White, currentAngle, mOriginPoint, 1, SpriteEffects.None, 0);
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
        /// Method allow the gem to follow the player if they are within a certain distance
        /// </summary>
        /// <param name="betweenGemShip"></param>
        /// <param name="vectorMagnitude"></param>
        private void FollowBeam(Vector2 betweenGemShip)
        {
            // Normalise the vector and multiply by three to get an equivalent speed of three
            mVelocity.X = betweenGemShip.X * 0.01f;
            mVelocity.Y = betweenGemShip.Y * 0.01f;
        }

        /**** End of PRIVATE METHODS region ****/
        #endregion
    }
} // End of namespace
