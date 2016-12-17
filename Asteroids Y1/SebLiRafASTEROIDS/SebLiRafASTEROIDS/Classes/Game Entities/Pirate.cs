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
 *  -   Pirate class moves the pirate and aims towards the player
 */

namespace Asteroids
{
    class Pirate
    {
        #region VARIABLES & PROPERTIES
        /* This region will contain,
         * all variables and,
         * their respective properties,
         * for this class
         */

        //Variables and objects
        Texture2D mSpriteTexture; // Texture to be drawn
        Texture2D movingTexture; // Texture while moving
        Texture2D stationaryTeture; // Texture while stationary
        Vector2 mPosition; // Position of the sprite onscreen
        Vector2 mOriginPoint; // Where on the sprite is the origin
        Vector2 mVelocity = new Vector2(0, -0.5f); // We do not have a speed variable as this is equivalent to the y component of the velocity variable as the initial magnitude is equal to this
        /// <summary>
        /// Gets the Pirates current velocity
        /// </summary>
        public Vector2 Velocity
        { get { return mVelocity; } }
        Rectangle moBoundaryBox;
        Rectangle moHitBox;
        /// <summary>
        /// Gets the pirates Bounding box
        /// </summary>
        public Rectangle HitBox
        { get { return moHitBox; } }
        Random aGen = new Random();
        Bullet bullet; // Bullets used by the object
        public Bullet Bullet//Property
        {
            get { return bullet; }
        }
        float currentAngle = 0; // Keep track of what the pirate's angle relative to the normal is
        int screenWidth;
        int screenHeight;
        bool active;
        public bool Active // Property
        {
            get{ return active; }
        }

        /**** End of VARIABLES & PROPERTIES region ****/
        #endregion

        #region CONSTANT DECLARATIONS
        /* This region will contain,
         * all constants for this class
         */

        const int rotationRate = 5; // What the pirate can rotate by in each frame in degrees
        const double ShootRange = Math.PI / 12; // Half the field of view of the pirate in radians

        // File Name Constants
        const string PirateShipTextureOffFile = "PirateShipOff";
        const string PirateShipTextureOnFile = "PirateShipOn";
        const string PirateShipBulletTextureFile = "PirateLaser";
        // Bullets Constants
        const int BulletPosX = -300;
        const int BulletPosY = -300;
        const int BulletWidth = 5;
        const int BulletHeight = 50;
        readonly Rectangle BulletDimensions = new Rectangle(
                BulletPosX,
                BulletPosY,
                BulletWidth,
                BulletHeight
                );
        const float BulletSpeed = 2f;
        const int BulletDamage = 5;

        const int PirateWidth = 40;
        const int PirateHeight = 60;

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
        public Pirate(int screenWidthIn, int screenHeightIn)
        {
            // Set the screen width and height
            screenWidth = screenWidthIn;
            screenHeight = screenHeightIn;

            //Randomise the location and set mdActive to true
            int initialPosX; //= aGen.Next(50, 750);
            int initialPosY; //= aGen.Next(50, 550);
            double adWhereSpawn = aGen.NextDouble();
            if (adWhereSpawn > 0.5)
            {
                initialPosX = aGen.Next(600, 800);
            }
            else
            {
                initialPosX = aGen.Next(0, 200);
            }
            adWhereSpawn = aGen.NextDouble();
            if (adWhereSpawn > 0.5)
            {
                initialPosY = aGen.Next(400, 550);
            }
            else
            {
                initialPosY = aGen.Next(0, 200);
            }
            mPosition = new Vector2(initialPosX, initialPosY);
            bullet = new Bullet(BulletDimensions, BulletSpeed, BulletDamage);
            moBoundaryBox = new Rectangle(initialPosX, initialPosY, PirateWidth, PirateHeight);
            moHitBox = moBoundaryBox;
            active = false;
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
            bullet.LoadContent(aoAssetsManager, FilePaths.PirateShipPath + PirateShipBulletTextureFile);
            movingTexture = aoAssetsManager.Load<Texture2D>(FilePaths.PirateShipPath + PirateShipTextureOnFile);
            stationaryTeture = aoAssetsManager.Load<Texture2D>(FilePaths.PirateShipPath + PirateShipTextureOffFile);
            mSpriteTexture = movingTexture;

            //Set the sprite origin
            mOriginPoint.X = mSpriteTexture.Width / 2;
            mOriginPoint.Y = mSpriteTexture.Height / 2;
        }
        /// <summary>
        /// Will handle the Screen's Update Logic
        /// </summary>
        public void Update(Vector2 playerPos)
        {
            bullet.Update(mPosition); // Update the bullet associated with this class
            if (active == true)
            {
                UpdateLocation();
                Vector2 pirateToPlayer = playerPos - mPosition; // Get the vector from the pirate to the player
                double angleBetween = AngleBetween(mVelocity, pirateToPlayer); //Check the angle between the pirate and the player (smallest angle)
                if (angleBetween < ShootRange && angleBetween > -ShootRange && bullet.Alive == false)
                {
                    Shoot(); // We check if the pirate is facing close enough t the player before shooting, this allows the pirate to miss sometime while remaining relatively accurate
                }
                if (mVelocity.Y * pirateToPlayer.X <= mVelocity.X * pirateToPlayer.Y && angleBetween > DegreesToRadians(6)) // Check if the current velocity i to the right of the pirate to player vector
                {
                    TurnRight();
                }
                else if (mVelocity.Y * pirateToPlayer.X > mVelocity.X * pirateToPlayer.Y && angleBetween > DegreesToRadians(6)) // Check if the current velocity i to the left of the pirate to player vector
                {
                    TurnLeft();
                }
                else
                {
                    mPosition += mVelocity; // Only move when not turning
                }
            }
        }
        /// <summary>
        /// Will draw Screen components
        /// </summary>
        /// <param name="aoSpriteBatch"></param>
        public void Draw(SpriteBatch aoSpriteBatch)
        {
            if (active == true)
            {
                aoSpriteBatch.Draw(mSpriteTexture, moBoundaryBox, null, Color.White, DegreesToRadians(currentAngle), mOriginPoint, SpriteEffects.None, 0);
            }
            bullet.Draw(aoSpriteBatch);
        }

        // Method allows the Pirate to be destroyed
        public void Die()
        {
            active = false;
        }
        // Method repawns the Pirate
        public void Spawn()
        {
            float adX, adY;
            double adWhereSpawn = aGen.NextDouble();
            if (adWhereSpawn > 0.5)
            {
                adX = aGen.Next(600, 800);
            }
            else
            {
                adX = aGen.Next(0, 200);
            }
            adWhereSpawn = aGen.NextDouble();
            if (adWhereSpawn > 0.5)
            {
                adY = aGen.Next(400, 550);
            }
            else
            {
                adY = aGen.Next(0, 200);
            }

            mPosition.X = adX;
            mPosition.Y = adY;
            moBoundaryBox.X = (int)adX;
            moBoundaryBox.Y = (int)adY;
            active = true;
        }

        /**** End of PUBLIC METHODS region ****/
        #endregion

        #region PRIVATE METHODS
        /* This region will contain,
         * all private methods for this class
         */

        private void Shoot()
        {
            bullet.Fire(mPosition, currentAngle); // Fire the bullet associated with this class
        }

        // Returns the angle between two vectors in radians
        private double AngleBetween(Vector2 u, Vector2 v)
        {
            // Uses the follwing formula to get the angle beteen two vectors

            /* 
             * Cos0 = (u).(v) / |u|.|v|
             * 0 = angle
             */

            float dotProduct = Vector2.Dot(u, v);
            float lengthOne = u.Length();
            float lengthTwo = v.Length();
            return Math.Acos(dotProduct / (lengthOne * lengthTwo)); // Perform some of the calculations in the return statement
        }

        // Method to turn the pirate right
        private void TurnRight()
        {
            mVelocity = Rotate(rotationRate, mVelocity); // Rotate by the positive sense of the rotation value
        }

        // Method to turn the pirate left
        private void TurnLeft()
        {
            mVelocity = Rotate(-rotationRate, mVelocity); // Rotate by the negative sense of the rotation value
        }

        // Method to rotate a vector by a certain angle
        private Vector2 Rotate(double angle, Vector2 vectorToRotate)
        {
            /* 
             * Method uses the principles of a rotation matrix
             * |Cos(0) ,-Sin(0)|
             * |Sin(0) , Cos(0)|
             */

            // Set variables
            float x = vectorToRotate.X;
            float y = vectorToRotate.Y;
            float theta = DegreesToRadians((float)angle);
            currentAngle += (float)angle;

            // Perform calculations
            float xCoord = (float)((Math.Cos(theta) * x) - (Math.Sin(theta) * y));
            float yCoord = (float)((Math.Sin(theta) * x) + (Math.Cos(theta) * y));

            // Return the resultant vector
            return new Vector2(xCoord, yCoord);
        }

        //Method to convert degrees to radians
        private float DegreesToRadians(float angle)
        {
            return (float)((angle / 180) * Math.PI);
        }
        /// <summary>
        /// Will update the rectangle to the pirate's moPosition
        /// </summary>
        private void UpdateLocation()
        {
            moBoundaryBox.X = (int)mPosition.X;
            moBoundaryBox.Y = (int)mPosition.Y;
            moHitBox.X = (int)(moBoundaryBox.X - (moBoundaryBox.Width / 2f));
            moHitBox.Y = (int)(moBoundaryBox.Y - (moBoundaryBox.Height / 2f));
        }

        /**** End of PRIVATE METHODS region ****/
        #endregion

    }
} // End of namespace