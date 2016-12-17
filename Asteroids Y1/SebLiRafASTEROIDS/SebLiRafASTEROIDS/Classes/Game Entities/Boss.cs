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
 * This is the boss class it deals with the movement/firing pattern and everything else to do with the final game boss
 * 
 * Worked on: [05/04/16     13:45 - 15:00]
 * 
 */

namespace Asteroids
{
    class Boss
    {
        #region VARIABLES & PROPERTIES
        /* This region will contain,
         * all variables and,
         * their respective properties,
         * for this class
         */

        // Background texture
        enum direction { Left, Right };
        enum bossState {Phase1 , Phase2, Phase3, Paused };
        Texture2D image;
        Texture2D bossPhase1Image;
        Texture2D bossPhase2Image;
        Texture2D bossPhase3Image;
        Rectangle hitbox;

        public Rectangle BoundaryBox
        { get { return hitbox; } }
        Vector2 position;
        Vector2 cannonPosition;
        /// <summary>
        /// property to get the cannon position
        /// </summary>
        public Vector2 CannonPosition
        { get { return (position + CannonOffset); } }
        int speed = 1;
        int bulletSpeed = 3;
        int mineSpeed = 2;
        int health = 500;       //HEALTH CHANGE IF TOO HIGH
        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        direction directionToMove;
        bossState currentBossState;
        Bullet[] mines;
        Bullet[] bullets;
        /// <summary>
        /// Gets the bosses bullets
        /// </summary>
        public Bullet[] Bullets
        { get { return bullets; } }
        double counter = 0;
        int noOfBulletFired = 0;
        int noOfBulletToFire = 1;
        float bulletFireAngle = 0f;
        bool active = false;
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        bool alive = true;
        public bool Alive
        {
            get { return alive; }
        }
        int adAngleChange = 10;  //used for the change in shooting angle
        /**** End of VARIABLES & PROPERTIES region ****/
        #endregion

        #region CONSTANT DECLARATIONS
        /* This region will contain,
         * all constants for this class
         */

        const int BulletWidth = 10;
        const int BulletHeight = 30;
        const int BulletDamage = 10;
        const int MineWidth = 30;
        const int MineHeight = 30;
        const double FireRate = 0.5;
        readonly Vector2 CannonOffset = new Vector2(175f, 20f);
        const string BossText = "final_boss";

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
        public Boss()
        {
            alive = true;
            active = false;
            currentBossState = bossState.Phase1;
            cannonPosition.X = position.X + 150;
            cannonPosition.Y = position.Y + 20;
            position = new Vector2(300, 350);
            directionToMove = direction.Left;
            bullets = new Bullet[100];
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = new Bullet(new Rectangle((int)cannonPosition.X, (int)cannonPosition.Y, BulletWidth, BulletHeight), bulletSpeed, BulletDamage);
            }
            mines = new Bullet[20];
            for (int i = 0; i < mines.Length; i++)
            {
                mines[i] = new Bullet(cannonPosition, mineSpeed, MineWidth, MineHeight);
            }
        }

        /**** End of CONSTRUCTORS region ****/
        #endregion

        #region PUBLIC METHODS
        /* This region will contain,
         * all public methods for this class
         */

        public void LoadContent(ContentManager aoAssetsManager)
        {
            bossPhase1Image = aoAssetsManager.Load<Texture2D>(FilePaths.BossArtPath + BossText + "_phase1");
            bossPhase2Image = aoAssetsManager.Load<Texture2D>(FilePaths.BossArtPath + BossText + "_phase2");
            bossPhase3Image = aoAssetsManager.Load<Texture2D>(FilePaths.BossArtPath + BossText + "_phase3");
            image = bossPhase1Image;
            hitbox = new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height);
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i].LoadContent(aoAssetsManager,FilePaths.FinalBossPath + "boss_laser");
            }
        }
        /// <summary>
        /// Will handle the Screen's Update Logic
        /// currently the firing patterns determined by the health of the boss
        /// there are 3 states each progressively harder
        /// </summary>
        public void Update(Rectangle aoMainFrame, GameTime time)
        {
            if (alive && active)
            {
                counter += time.ElapsedGameTime.TotalSeconds;
                switch (currentBossState)
                {
                    case bossState.Phase1:
                        Phase1Update(aoMainFrame);
                        break;
                    case bossState.Phase2:
                        Phase2Update(aoMainFrame);
                        break;
                    case bossState.Phase3:
                        Phase3Update(aoMainFrame);
                        break;
                    case bossState.Paused:
                        break;
                    default:
                        break;
                }
                CheckBossHealth();
                hitbox = new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height);
            }
        }
        /// <summary>
        /// Will draw Screen components
        /// </summary>
        /// <param name="aoSpriteBatch"></param>
        public void Draw(SpriteBatch aoSpriteBatch)
        {
            switch (currentBossState)   //change image depending on state of the boss
            {
                case bossState.Phase1:
                    image = bossPhase1Image;
                    break;
                case bossState.Phase2:
                    image = bossPhase2Image;
                    break;
                case bossState.Phase3:
                    image = bossPhase3Image;
                    break;
                case bossState.Paused:
                    break;
                default:
                    break;
            }
            if (alive && active)
            {
                aoSpriteBatch.Draw(image, hitbox, Color.White);
                for (int i = 0; i < bullets.Length; i++)
                {
                    if (bullets[i].Alive)
                    {
                        bullets[i].Draw(aoSpriteBatch);
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
        /// Move the sprite left till it is at the left side of the screen
        /// then it switches direction
        /// </summary>
        private void MoveLeft()
        {
            if (position.X > 0)
            {
                position.X -= speed;
            }
            else
            {
                directionToMove = direction.Right;
            }
        }
        /// <summary>
        /// Will move the sprite right untill it reaches the right edge of the screen
        /// then it switches direction
        /// </summary>
        /// <param name="aoMainFrame"></param>
        private void MoveRight(Rectangle aoMainFrame)
        {
            if (position.X + image.Width < aoMainFrame.Width)
            {
                position.X += speed;
            }
            else
            {
                directionToMove = direction.Left;
            }
        }
        /// <summary>
        /// phase 1 of the boss, fires a single bullet straight up.
        /// </summary>
        private void Phase1Update(Rectangle aoMainFrame)
        {
            switch (directionToMove)    //Boss movement for first phase
            {
                case direction.Left:
                    MoveLeft();
                    break;
                case direction.Right:
                    MoveRight(aoMainFrame);
                    break;
                default:
                    break;
            }
            for (int i = 0; i < bullets.Length; i++)    //firing pattern phase 1
            {
                bullets[i].Update(this.CannonPosition);     //update position to canon
                if (counter > FireRate) //while counter is greater than fire rate, if a bullet in the array is dead make it alive and shoot
                {
                    if (bullets[i].Alive == false && noOfBulletFired < noOfBulletToFire)
                    {
                        bullets[i].Fire(this.CannonPosition, bulletFireAngle);
                        noOfBulletFired++;      //add bullets fired
                        bulletFireAngle += adAngleChange;
                    }
                    else if (noOfBulletFired >= noOfBulletToFire)   //reset counter and bullets
                    {
                        noOfBulletFired = 0;
                        counter = 0;
                    }
                    if (bulletFireAngle >= 60f) //if angle is at 45 reset angle
                    {
                        adAngleChange = -10;
                    }
                    if (bulletFireAngle <= -60f)
                    {
                        adAngleChange = 10;
                    }
                }
            }
        }
        /// <summary>
        /// phase 2 of the boss, fires 3 bullets with a 45 degree offset
        /// </summary>
        private void Phase2Update(Rectangle aoMainFrame)
        {
            noOfBulletToFire = 3;
            speed = 2;
            bulletFireAngle = -45f;
            switch (directionToMove)    //Boss movement for first phase
            {
                case direction.Left:
                    MoveLeft();
                    break;
                case direction.Right:
                    MoveRight(aoMainFrame);
                    break;
                default:
                    break;
            }
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i].Update(this.CannonPosition);
                if (counter > FireRate)
                {
                    if (bullets[i].Alive == false && noOfBulletFired < noOfBulletToFire)
                    {
                        bullets[i].Fire(this.CannonPosition, bulletFireAngle);
                        noOfBulletFired++;
                        bulletFireAngle += 45f;
                    }
                    else if (noOfBulletFired >= noOfBulletToFire)
                    {
                        noOfBulletFired = 0;
                        counter = 0;
                        bulletFireAngle = -45f;
                    }
                }
            }
        }
        /// <summary>
        /// phase 3 of the boss, fires 4 bullets with a 30 degree offset and a quicker fire rate
        /// </summary>
        private void Phase3Update(Rectangle aoMainFrame)
        {
            noOfBulletToFire = 5;
            speed = 3;
            bulletFireAngle = -60;
            switch (directionToMove)    //Boss movement for first phase
            {
                case direction.Left:
                    MoveLeft();
                    break;
                case direction.Right:
                    MoveRight(aoMainFrame);
                    break;
                default:
                    break;
            }

            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i].Update(this.CannonPosition);
                if (counter > FireRate)
                {
                    if (bullets[i].Alive == false && noOfBulletFired < noOfBulletToFire)
                    {
                        bullets[i].Fire(this.CannonPosition, bulletFireAngle);
                        noOfBulletFired++;
                        bulletFireAngle += 30f;
                    }
                    else if (noOfBulletFired >= noOfBulletToFire)
                    {
                        noOfBulletFired = 0;
                        counter = 0;
                        bulletFireAngle = -60f;
                    }
                }
            }
        }
        /// <summary>
        /// This will check the amount of health that the boss currently has,
        /// set the state depending on the current health.
        /// </summary>
        private void CheckBossHealth()
        {
            if (health <= 300)
            {
                currentBossState = bossState.Phase2;
            }
            if (health <= 100)
            {
                currentBossState = bossState.Phase3;
            }
            if (health <= 0)
            {
                Death();
            }
        }
        /// <summary>
        /// This is the method for killing the boss will set the alive variable to false
        /// </summary>
        private void Death()
        {
            alive = false;
        }

        /**** End of PRIVATE METHODS region ****/
        #endregion

    }
} // End of namespace