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
 *  -   Describe class here
 */

namespace Asteroids
{
    class Asteroid
    {
        #region VARIABLES & PROPERTIES
        /* This region will contain,
         * all variables and,
         * their respective properties,
         * for this class
         */

        // ENUM for the asteroid size
        public enum Size { Large, Medium, Small };

        // Objects
        Size asteroidSize;
        public Size AsteroidSize
        {
            get { return asteroidSize; }
        }
        public enum Type
	    {
            Brown, Grey1, Grey2
	    }
        Type asteroidType;
        public Type AsteroidType
        { get { return asteroidType; } }
        Vector2 mPosition;
        public Vector2 Position
        {
            get { return mPosition; }
        }
        Vector2 mOriginPoint;
        Vector2 mVelocity;
        public Vector2 Velocity
        {
            get { return mVelocity; }
        }
        Texture2D mSpriteTexture;
        Random aGen = new Random();

        // Get Property for the width of the asteroid texture
        float textureWidth;
        /// <summary>
        /// Get Property for the width of the asteroid texture
        /// </summary>
        public float TextureWidth
        {
            get { return textureWidth; }
        }
        
        // Get Property for the height
        float textureHeight;
        /// <summary>
        /// Get Property for the height of the asteroid texture
        /// </summary>
        public float TextureHeight
        {
            get { return textureHeight; }
        }

        // Other variables
        Rectangle boundingRectangle = new Rectangle();
        /// <summary>
        /// Gets the bounding rectangle around the asteroid
        /// </summary>
        public Rectangle HitBox
        { get { return boundingRectangle; } }
        float currentAngle;
        float rotationAngle;
        int screenWidth;
        int screenHeight;
        bool active;
        /// <summary>
        /// Gets whether or not the asteroid is active.
        /// </summary>
        public bool Active
        { get { return active; } }

        /**** End of VARIABLES & PROPERTIES region ****/
        #endregion

        #region CONSTANT DECLARATIONS
        /* This region will contain,
         * all constants for this class
         */
        const int minVelocityComponent = 1;
        const int maxVelocityComponent = 9;
        const int MaxSplitAngle = 31;
        const int MinSplitAngle = 10;
        const int LargeDimensions = 150;
        const int MediumDimensions = 100;
        const int SmallDimensions = 50;
        const float TwoPI = (float)(Math.PI * 2);

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
        public Asteroid(int screenWidthIn, int screenHeightIn)
        {
            int rotationDivisor = aGen.Next(6, 25); // Randomise the rotation speed of the asteroid
            rotationAngle = (float)(Math.PI / rotationDivisor); // This will create our constant add on to rotate the asteroid
            mVelocity = new Vector2(aGen.Next(minVelocityComponent, maxVelocityComponent), aGen.Next(minVelocityComponent, maxVelocityComponent)); // Gives the asteroid a random velcoty vector with randomly generated components
            screenWidth = screenWidthIn;
            screenHeight = screenHeightIn;
            asteroidSize = Size.Large;
            boundingRectangle.Width = LargeDimensions;
            boundingRectangle.Height = LargeDimensions;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionIn"></param>
        /// <param name="velocityIn"></param>
        /// <param name="screenWidthIn"></param>
        /// <param name="screenHeightIn"></param>
        /// <param name="asteroidSizeIn"></param>
        public Asteroid(Vector2 positionIn, Vector2 velocityIn, int screenWidthIn, int screenHeightIn, Size asteroidSizeIn)
        {
            int rotationDivisor = aGen.Next(1, 4); // Randomise the rotation speed of the asteroid
            rotationAngle = DegreesToRadians(rotationDivisor); // This will create our constant add on to rotate the asteroid
            screenWidth = screenWidthIn;
            screenHeight = screenHeightIn;
            mPosition = positionIn;
            mVelocity = velocityIn;
            int randomChange = aGen.Next();
            asteroidSize = asteroidSizeIn;
            if (asteroidSize == Size.Large)
            {
                boundingRectangle.Width = LargeDimensions;
                boundingRectangle.Height = LargeDimensions;
            }
            else if(asteroidSize == Size.Medium)
            {
                boundingRectangle.Width = MediumDimensions;
                boundingRectangle.Height = MediumDimensions;
            }
            else
            {
                boundingRectangle.Width = SmallDimensions;
                boundingRectangle.Height = SmallDimensions;
            }
            active = true;
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
            // Use a constant for the string
            string desiredSprite = "";
            int rand = aGen.Next(1, 4);
            switch(rand)
            {
                case 1:
                    desiredSprite = FilePaths.AsteroidBrownTexture;
                    asteroidType = Type.Brown;
                    break;
                case 2:
                    desiredSprite = FilePaths.Asteroid1stGrayTextureFile;
                    asteroidType = Type.Grey1;
                    break;
                case 3:
                    desiredSprite = FilePaths.Asteroid2ndGreyTextureFile;
                    asteroidType = Type.Grey2;
                    break;
            }
            mSpriteTexture = aoAssetsManager.Load<Texture2D>(desiredSprite);
            mOriginPoint = new Vector2(mSpriteTexture.Width / 2f, mSpriteTexture.Height / 2f);
        }
        /// <summary>
        /// Will load all external content,
        /// with the desired asteroid type.
        /// </summary>
        /// <param name="aoAssetsManager"></param>
        /// <param name="aeType"></param>
        public void LoadContent(ContentManager aoAssetsManager, Type aeType)
        {
            string desiredSprite = "";
            switch (aeType)
            {
                case Type.Brown:
                    desiredSprite = FilePaths.AsteroidBrownTexture;
                    break;
                case Type.Grey1:
                    desiredSprite = FilePaths.Asteroid1stGrayTextureFile;
                    break;
                case Type.Grey2:
                    desiredSprite = FilePaths.Asteroid2ndGreyTextureFile;
                    break;
            }
            asteroidType = aeType;
            mSpriteTexture = aoAssetsManager.Load<Texture2D>(desiredSprite);
            mOriginPoint = new Vector2(mSpriteTexture.Width / 2f, mSpriteTexture.Height / 2f);
        }
        /// <summary>
        /// Will handle the Screen's Update Logic
        /// </summary>
        public void Update()
        {
            if(active == true) // Do not update the asteroid if it has been destroyed
            {
                mPosition += mVelocity;
                currentAngle += rotationAngle;
                if (currentAngle >= TwoPI)
                {
                    currentAngle -= TwoPI;
                }
                ScreenWarp();
                boundingRectangle.X = (int)mPosition.X;
                boundingRectangle.Y = (int)mPosition.Y;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="neutronGems"></param>
        /// <param name="adGemIndex"></param>
        public Asteroid Destroyed(Vector2 aoVelocity, int gemPosition, ContentManager aoAssetsManager)
        {
            aoVelocity.X = -aoVelocity.X;
            aoVelocity.Y = -aoVelocity.Y;
            return Split(aoVelocity, gemPosition, aoAssetsManager);
        }
        /// <summary>
        /// Will draw Screen components
        /// </summary>
        /// <param name="aoSpriteBatch"></param>
        public void Draw(SpriteBatch aoSpriteBatch)
        {
            if (active == true)
            {
                aoSpriteBatch.Draw(mSpriteTexture, boundingRectangle, null, Color.White, currentAngle, mOriginPoint, SpriteEffects.None, 0);
            }
        }

        /**** End of PUBLIC METHODS region ****/
        #endregion

        #region PRIVATE METHODS
        /* This region will contain,
         * all private methods for this class
         */

        /// <summary>
        /// Method creates an array of two asteroids resulting from a destroyed asteroid
        /// </summary>
        /// <param name="neutronGems"></param>
        /// <param name="adGemIndex"></param>
        /// <returns></returns>
        private Asteroid Split(Vector2 aoVelocity, int adGemIndex, ContentManager aoAssetsManager)
        {
            if(asteroidSize != Size.Small) // If the asteroid isn't small we split it
            {
                Vector2 newVelocityOne;
                Vector2 newVelocityTwo;
                Vector2 currentVelocity = mVelocity - aoVelocity;
                float angleOne = aGen.Next(MinSplitAngle, MaxSplitAngle);
                float angleTwo = -aGen.Next(MinSplitAngle, MaxSplitAngle);
                newVelocityOne = Rotate(angleOne, currentVelocity);
                newVelocityTwo = Rotate(angleTwo, currentVelocity);
                Asteroid secondAsteroid;
                if (asteroidSize == Size.Large)
                {
                    mVelocity = newVelocityOne;
                    asteroidSize = Size.Medium;
                    boundingRectangle.Width = MediumDimensions;
                    boundingRectangle.Height = MediumDimensions;
                    secondAsteroid = new Asteroid(mPosition, newVelocityTwo, screenWidth, screenHeight, Size.Medium);
                }
                else
                {
                    mVelocity = newVelocityOne;
                    asteroidSize = Size.Small;
                    boundingRectangle.Width = SmallDimensions;
                    boundingRectangle.Height = SmallDimensions;
                    secondAsteroid = new Asteroid(mPosition, newVelocityTwo, screenWidth, screenHeight, Size.Small);
                }
                secondAsteroid.LoadContent(aoAssetsManager, asteroidType);
                return secondAsteroid;
            }
            else // We don't perform any operations if the size is small
            {
                active = false;
                return null;
            }
        }

        /// <summary>
        /// Method to rotate about a vector about a point by an angle
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="rotateVector"></param>
        /// <returns></returns>
        private Vector2 Rotate(float angle, Vector2 rotateVector)
        {
            /* 
             * Method uses the principles of a rotation matrix
             * |Cos(0) ,-Sin(0)|
             * |Sin(0) , Cos(0)|
             */

            // Set variavles
            float x = rotateVector.X; 
            float y = rotateVector.Y;
            float theta = DegreesToRadians(angle);

            // Perform the equivalent of a matrix operation withou the class
            float xCoord = (float)((Math.Cos(theta) * x) - (Math.Sin(theta) * y));
            float yCoord = (float)((Math.Sin(theta) * x) + (Math.Cos(theta) * y));

            // Return the resulatant vector
            return new Vector2(xCoord, yCoord);
        }

        /// <summary>
        /// Method to convert degrees to radians
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        private float DegreesToRadians(float angle)
        {
            return (float)((angle / 180) * Math.PI);
        }
        /// <summary>
        /// Will deal with warping coordinates.
        /// </summary>
        private void ScreenWarp()
        {
            // Deal with warping X coordinates
            if (mPosition.X + boundingRectangle.Width < 0)
            {
                mPosition.X = screenWidth;
            }
            else if (mPosition.X > screenWidth)
            {
                mPosition.X = -boundingRectangle.Width + 10;
            }

            //Deal with warping Y coordinates
            if (mPosition.Y + boundingRectangle.Height < 0)
            {
                mPosition.Y = screenHeight;
            }
            else if(mPosition.Y > screenWidth)
            {
                mPosition.Y = -boundingRectangle.Height + 10;
            }
        }

        /**** End of PRIVATE METHODS region ****/
        #endregion

    }
} // End of namespace