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
 *  Rafael Girao
 */
/* Class Description:
 *  -   Will represent the bullets fired,
 *  by the ship, pirate & boss classes.
 */

namespace Asteroids
{
    class Bullet
    {
        #region VARIABLES & PROPERTIES
        /* This region will contain,
         * all variables and,
         * their respective properties,
         * for this class
         */

        // Sprite texture
        private Texture2D moSpriteTexture;
        // Center of Bullets Textre which Draw() uses
        private Vector2 moOriginPosition;
        // Position
        private Vector2 moPosition;
        // Center of Bullets
        private Vector2 moCenterPosition;
        // Velocity
        private Vector2 moVelocity;
        /// <summary>
        /// Will get the bullet's current velocity
        /// </summary>
        public Vector2 Velocity
        { get { return moVelocity; } }
        // Bounding Box
        private Rectangle moBoundaryBox;
        /// <summary>
        /// Will get bullets bounding box,
        /// used for collision detection
        /// </summary>
        public Rectangle BoundaryBox
        { get { return moBoundaryBox; } }
        // Drawing Angle
        private float mdAngle;
        // Bullets Speed
        private float mdSpeed;
        // Bullets alive boolean
        private bool mdAlive;
        /// <summary>
        /// Will get bullets alive,
        /// used to determine whether to draw bullet or not
        /// </summary>
        public bool Alive
        { get { return mdAlive; } }

        private int mdDamage;
        /// <summary>
        /// Gets the Damage value for this bullet
        /// </summary>
        public int Damage
        { get { return mdDamage; } }
        // Bullets State
        enum BulletState
        { Alive, Dead, Paused }
        BulletState meBulletState;
        private int mdTimeAlive;
        /// <summary>
        /// Gets the time the bullets has spent alive
        /// (measured in frame updates)
        /// </summary>
        public int TimeAlive
        { get { return mdTimeAlive; } }
        private SoundEffect laserSound;

        /**** End of VARIABLES & PROPERTIES region ****/
        #endregion

        #region CONSTANT DECLARATIONS
        /* This region will contain,
         * all constants for this class
         */

        // Bullets Default dimensions
        const int Width = 30;
        const int Height = 90;

        // Bullets Default spawning location
        const int SpawnLocation = -300;

        // Contains bullet's texture file
        const string BulletTexture = "bullet";

        const int ConsMaxTimeAlive = 300;

        public int MaxTimeAlive
        { get { return ConsMaxTimeAlive; } }

        /**** End of CONSTANT DECLARATIONS **/
        #endregion

        #region CONSTRUCTORS
        /* This region will contain,
         * all constructors for this class
         */

        /// <summary>
        /// Default Constructor,
        /// Creates our bullet,
        /// initializing all variables using their:
        /// default values, as 0 or null
        /// </summary>
        public Bullet()
        {
            // Default Values Used
            moSpriteTexture = null;
            moPosition = new Vector2(SpawnLocation);
            moVelocity = new Vector2(0f);
            moBoundaryBox = new Rectangle(SpawnLocation, SpawnLocation, Width, Height);
            moCenterPosition = new Vector2(moBoundaryBox.Center.X, moBoundaryBox.Center.Y);
            moOriginPosition = new Vector2(0f);
            mdAngle = 0f;
            mdSpeed = 0f;
            mdTimeAlive = 0;
            mdDamage = 0;
            mdAlive = false;
            meBulletState = BulletState.Dead;
        }
        /// <summary>
        /// Constructor,
        /// Creates our bullet,
        /// using the passed moPosition,
        /// setting the rest to 0 or null
        /// </summary>
        /// <param name="aoPosition">Will be created at this Vector</param>
        public Bullet(Vector2 aoPosition)
        {
            int adX = (int)aoPosition.X;
            int adY = (int)aoPosition.Y;

            moPosition = aoPosition;
            moBoundaryBox = new Rectangle(adX, adY, Width, Height);
            moCenterPosition = new Vector2(moBoundaryBox.Center.X, moBoundaryBox.Center.Y);

            // Default Values Used
            moSpriteTexture = null;
            moVelocity = new Vector2(0f);
            moOriginPosition = new Vector2(0f);
            mdAngle = 0f;
            mdSpeed = 0f;
            mdTimeAlive = 0;
            mdDamage = 0;
            mdAlive = false;
            meBulletState = BulletState.Dead;
        }
        /// <summary>
        /// Constructor,
        /// Creates our bullet,
        /// using the passed moPosition and speed.
        /// </summary>
        /// <param name="aoPosition">
        /// Will set the bullets moPosition
        /// </param>
        /// <param name="adSpeed">
        /// Will set the bullets speed
        /// </param>
        public Bullet(Vector2 aoPosition, float adSpeed)
        {
            int adX = (int)aoPosition.X;
            int adY = (int)aoPosition.Y;

            moPosition = aoPosition;
            mdSpeed = adSpeed;
            moBoundaryBox = new Rectangle(adX, adY, Width, Height);
            moCenterPosition = new Vector2(moBoundaryBox.Center.X, moBoundaryBox.Center.Y);
            
            // Default Values Used
            moSpriteTexture = null;
            moVelocity = new Vector2(0f);
            moOriginPosition = new Vector2(0f);
            mdAngle = 0f;
            mdTimeAlive = 0;
            mdDamage = 0;
            mdAlive = false;
            meBulletState = BulletState.Dead;
        }
        /// <summary>
        /// Constructor,
        /// Creates our bullet,
        /// using the passed vector moPosition, speed, width and height
        /// </summary>
        /// <param name="aoPosition">
        /// Will set the bullets moPosition
        /// </param>
        /// <param name="adSpeed">
        /// Will set the bullets speed
        /// </param>
        /// <param name="adWidth">
        /// Will set the bullets width
        /// </param>
        /// <param name="adHeight">
        /// Will set the bullets height
        /// </param>
        public Bullet(Vector2 aoPosition, float adSpeed, int adWidth, int adHeight)
        {
            int adX = (int)aoPosition.X;
            int adY = (int)aoPosition.Y;

            moPosition = aoPosition;
            mdSpeed = adSpeed;
            moBoundaryBox = new Rectangle(adX, adY, adWidth, adHeight);
            moCenterPosition = new Vector2(moBoundaryBox.Center.X, moBoundaryBox.Center.Y);

            // Default Values Used
            moSpriteTexture = null;
            moVelocity = new Vector2(0f);
            moOriginPosition = new Vector2(0f);
            mdAngle = 0f;
            mdTimeAlive = 0;
            mdDamage = 0;
            mdAlive = false;
            meBulletState = BulletState.Dead;
        }
        /// <summary>
        /// Constructor,
        /// Creates our bullet,
        /// using the passed rectangle
        /// </summary>
        /// <param name="aoDimensions">
        /// Will set the bullets moPosition, width and height
        /// </param>
        public Bullet(Rectangle aoDimensions)
        {
            moPosition = new Vector2(aoDimensions.X, aoDimensions.Y);
            moBoundaryBox = aoDimensions;
            moCenterPosition = new Vector2(aoDimensions.Center.X, aoDimensions.Center.Y);

            // Default Values Used
            moSpriteTexture = null;
            moVelocity = new Vector2(0f);
            moOriginPosition = new Vector2(0f);
            mdSpeed = 0f;
            mdAngle = 0f;
            mdTimeAlive = 0;
            mdDamage = 0;
            mdAlive = false;
            meBulletState = BulletState.Dead;
        }
        /// <summary>
        /// Constructor,
        /// Creates our bullet,
        /// using the passed rectangle and speed
        /// </summary>
        /// <param name="aoDimensions">
        /// Will set the bullets moPosition, width and height
        /// </param>
        /// <param name="adSpeed">
        /// Will set the bullets speed
        /// </param>
        public Bullet(Rectangle aoDimensions, float adSpeed)
        {
            moPosition = new Vector2(aoDimensions.X, aoDimensions.Y);
            moBoundaryBox = aoDimensions;
            moCenterPosition = new Vector2(aoDimensions.Center.X, aoDimensions.Center.Y);
            mdSpeed = adSpeed;

            // Default Values Used
            moSpriteTexture = null;
            moVelocity = new Vector2(0f);
            moOriginPosition = new Vector2(0f);
            mdAngle = 0f;
            mdTimeAlive = 0;
            mdDamage = 0;
            mdAlive = false;
            meBulletState = BulletState.Dead;
        }
        /// <summary>
        /// Constructor:
        /// Creates our bullet,
        /// using the passed rectangle, speed and damage
        /// </summary>
        /// <param name="aoDimensions">
        /// Passed Rectangle
        /// will set the bullets moPosition, width and height
        /// </param>
        /// <param name="adSpeed">
        /// Passed float
        /// Will set the bullets speed
        /// </param>
        /// <param name="adDamage">
        /// Passed integer
        /// Will set the bullets damage
        /// </param>
        public Bullet(Rectangle aoDimensions, float adSpeed, int adDamage)
        {
            moPosition = new Vector2(aoDimensions.X, aoDimensions.Y);
            moBoundaryBox = aoDimensions;
            moCenterPosition = new Vector2(aoDimensions.Center.X, aoDimensions.Center.Y);
            mdSpeed = adSpeed;
            mdDamage = adDamage;

            // Default Values Used
            moSpriteTexture = null;
            moVelocity = new Vector2(0f);
            moOriginPosition = new Vector2(0f);
            mdAngle = 0f;
            mdTimeAlive = 0;
            mdAlive = false;
            meBulletState = BulletState.Dead;
        }

        /**** End of CONSTRUCTORS region ****/
        #endregion

        #region PUBLIC METHODS
        /* This region will contain,
         * all public methods for this class
         */

        /// <summary>
        /// Will load all external content for the bullet,
        /// using the content manager to handle loading the file and
        /// using the string as the folder path to the texture file
        /// </summary>
        /// <param name="aoAssetsManager">
        /// XNA ContentManager that handles loading external files
        /// </param>
        /// <param name="aoFolderPath">
        /// String that should contain the folder path to the bullet texture
        /// </param>
        public void LoadContent(ContentManager aoAssetsManager, string aoFolderPath)
        {
            // Uses a constant for the string
            moSpriteTexture = aoAssetsManager.Load<Texture2D>(aoFolderPath);
            // Setting the origin of rotation of the texture
            moOriginPosition = new Vector2(moSpriteTexture.Width / 2f, moSpriteTexture.Height / 2f);
            laserSound = aoAssetsManager.Load<SoundEffect>(FilePaths.BulletSound + "Laser_fire");
        }
        /// <summary>
        /// Will handle the Bullets's Update Logic
        /// </summary>
        /// <param name="aoPosition">
        /// Bullets will fire and
        /// keep refreshing to this moPosition
        /// </param>
        public void Update(Vector2 aoPosition)
        {
            AliveTracker();
            switch (meBulletState)
            {
                case BulletState.Alive:
                    mdTimeAlive++;
                    Move();
                    break;
                case BulletState.Dead:
                    FollowPosition(aoPosition);
                    break;
                case BulletState.Paused:
                default:
                    // do nothing
                    break;
            }
            RefreshPositions();
        }
        /// <summary>
        /// Will draw Bullets components
        /// </summary>
        /// <param name="aoSpriteBatch">
        /// Handles Graphical Drawing by
        /// calling its Draw() Overloaded Method
        /// </param>
        public void Draw(SpriteBatch aoSpriteBatch)
        {
            switch (meBulletState)
            {
                case BulletState.Paused:
                case BulletState.Alive:
                    aoSpriteBatch.Draw(moSpriteTexture, moBoundaryBox, null, Color.White, (float)AngleToRadians(mdAngle), moOriginPosition, SpriteEffects.None, 0f);
                    break;

                case BulletState.Dead:
                    // no need to draw because bullet is dead
                    break;

                default:
                    break;
            }
        }
        /// <summary>
        /// Will set bullet states into firing state,
        /// using the passed vector and float,
        /// as its firing moPosition and firing angle, correspondingly
        /// </summary>
        /// <param name="aoFiringPosition">
        /// Passed Vector to be used as start of firing moPosition.
        /// </param>
        /// <param name="adFiringAngle">
        /// Passed Float to be used as the angle,
        /// at which bullet's being fired at.
        /// </param>
        public void Fire(Vector2 aoFiringPosition, float adFiringAngle)
        {
            if (meBulletState != BulletState.Paused)
            {
                moPosition = aoFiringPosition;
                mdAngle = adFiringAngle;
                SetForward();
                meBulletState = BulletState.Alive;
                laserSound.Play();
            }
        }
        /// <summary>
        /// Will set the bullet to its dead state
        /// </summary>
        public void Die()
        {
            if (meBulletState != BulletState.Paused)
            {
                moVelocity = new Vector2(0f);
                meBulletState = BulletState.Dead;
                mdTimeAlive = 0;
            }
        }
        /// <summary>
        /// Will set the bullet to a paused state,
        /// where no update commands are run,
        /// but the bullet is still drawn.
        /// </summary>
        public void Pause()
        {
            if (meBulletState == BulletState.Alive)
            {
                meBulletState = BulletState.Paused;
            }
        }
        /// <summary>
        /// Will set the bullet to the passed integer's.
        /// </summary>
        /// <param name="adX">
        /// Passed integer
        /// Will define it's new X coordinate
        /// </param>
        /// <param name="adY">
        /// Passed integer
        /// Will define it's new Y coordinate
        /// </param>
        public void SetPosition(int adX, int adY)
        {
            moPosition.X = adX;
            moPosition.Y = adY;
            RefreshPositions();
        }
        /// <summary>
        /// Will set the bullet's damage to the passed integer
        /// </summary>
        /// <param name="adDamage"></param>
        public void SetDamage(int adDamage)
        {
            mdDamage = adDamage;
        }

        /**** End of PUBLIC METHODS region ****/
        #endregion

        #region PRIVATE METHODS
        /* This region will contain,
         * all private methods for this class
         */
        
        /// <summary>
        /// Will updates bullets boundary box,
        /// using its moPosition vector
        /// </summary>
        private void RefreshPositions()
        {
            moBoundaryBox.X = (int)moPosition.X;
            moBoundaryBox.Y = (int)moPosition.Y;
        }
        /// <summary>
        /// Will move bullet by,
        /// setting its velocity to forward
        /// adding its moPosition with its velocity
        /// </summary>
        private void Move()
        {
            moPosition += moVelocity;
        }
        /// <summary>
        /// Sets bullet velocity vector to
        /// forward based on its rotation.
        /// </summary>
        private void SetForward()
        {
            moVelocity = new Vector2(0, mdSpeed);
            moVelocity = RotateVector(moVelocity, mdAngle);
        }
        /// <summary>
        /// Will update bullet's moPosition based on 
        /// passed vector.
        /// </summary>
        /// <param name="aoLocationTracked">
        /// Bullets moPosition will remain equal to this vector
        /// </param>
        private void FollowPosition(Vector2 aoLocationTracked)
        {
            moPosition = aoLocationTracked;
        }
        /// <summary>
        /// Will set bullet's alive boolean,
        /// based on this current state.
        /// </summary>
        private void AliveTracker()
        {
            if (meBulletState != BulletState.Dead)
            {
                mdAlive = true;
            }
            else
            {
                mdAlive = false;
            }
        }
        /// <summary>
        /// The vector to rotate will be rotated,
        /// by angle in degrees,
        /// around itself,
        /// by using the following formula:
        /// X =
        /// -(cos(radians) * velocity.X - sin(radians) * velocity.Y)
        /// Y = 
        /// (sin(radians) * velocity.X - cos(radians) * velocity.Y)
        /// </summary>
        /// <param name="aoVectorToRotate">
        /// Vector that will be rotated around itself
        /// </param>
        /// <param name="adAngleInDegrees">
        /// Angle passed as a float in Degrees
        /// </param>
        /// <returns>
        /// The passed Vector rotated around itself,
        /// by the passed angle, in degrees.
        /// </returns>
        private Vector2 RotateVector(Vector2 aoVectorToRotate, float adAngleInDegrees)
        {
            double adAngle = AngleToRadians(adAngleInDegrees);
            double cosTheta = Math.Cos(adAngle);
            double sinTheta = Math.Sin(adAngle);

            return new Vector2(
                (float)
                -(cosTheta * aoVectorToRotate.X - sinTheta * aoVectorToRotate.Y),
                (float)
                (sinTheta * aoVectorToRotate.X - cosTheta * aoVectorToRotate.Y)
                );
        }
        /// <summary>
        /// Will Convert the passed float angle,
        /// from degrees to radians
        /// </summary>
        /// <param name="adInDegrees">
        /// Float passed as a angle in degrees
        /// </param>
        /// <returns>
        /// Returns a double as the angle,
        /// converted from Degrees to Radians
        /// </returns>
        private double AngleToRadians(float adInDegrees)
        {
            return adInDegrees * (Math.PI / 180);
        }

        /**** End of PRIVATE METHODS region ****/
        #endregion

    }
} // End of namespace