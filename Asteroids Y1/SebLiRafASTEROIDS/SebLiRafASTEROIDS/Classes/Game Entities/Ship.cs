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
 *  -   Will represent the players ship
 */

namespace Asteroids
{
    class Ship
    {
        #region VARIABLES & PROPERTIES
        /* This region will contain,
         * all variables and,
         * their respective properties,
         * for this class
         */

        // Ships Bullets
        private Bullet[] moBullets;
        /// <summary>
        /// Gets the ship's bullet.
        /// </summary>
        public Bullet[] Bullets
        { get { return moBullets; } }
        // Ship Sprite texture
        private Texture2D
            moSpriteTexture,
            moShieldTexture;
        private Vector2[] maCenterOfRotation;
        // Ship Source Rectangle Array to be used in animation
        private Rectangle[,] moSourceFrames = new Rectangle[MaxShipTypes, MaxSourceFrames];
        private Rectangle moCurrentFrame;
        private int mdCurrentSourceFrame;
        // Ships Position, Velocity & Acceleration
        private Vector2 moPosition;
        /// <summary>
        /// Gets the ships current moPosition
        /// </summary>
        public Vector2 Position
        {
            get { return moPosition; }
            set { moPosition = value; }
        }
        private Vector2 moVelocity;
        /// <summary>
        /// Gets the ships current velocity
        /// </summary>
        public Vector2 Velocity
        { get { return moVelocity; } }
        private Vector2 moAcceleration;
        private Rectangle moBoundaryBox;
        private Rectangle moHitBox;
        private Rectangle moShieldBox;
        /// <summary>
        /// Gets the bounding box
        /// </summary>
        public Rectangle HitBox
        { get { return moHitBox; } }
        // Ships Statistics
        private int mdHealth;
        private int mdShield;
        private float mdSpeed;
        private float mdHighestSpeed;
        private int mdHoldCapacity;
        private int mdLaserChargeRate;
        private int mdLaserDamage;
        private int[] mdTiers = { 1, 1, 1, 1, 1, 1 };
        /// <summary>
        /// Gets ship's health.
        /// </summary>
        public int MaxHealth
        {
            get { return mdHealth; }
            set { mdHealth = value; }
        }
        /// <summary>
        /// Gets ship's shield.
        /// </summary>
        public int Shield
        {
            get { return mdShield; }
            set { mdShield = value; }
        }
        /// <summary>
        /// Get ship's hold.
        /// </summary>
        public int HoldCapacity
        {
            get { return mdHoldCapacity; }
            set { mdHoldCapacity = value; }
        }
        /// <summary>
        /// Gets ship's speed.
        /// </summary>
        public float Speed
        {
            get { return mdSpeed; }
            set { mdSpeed = value; }
        }
        /// <summary>
        /// Get ship's Laser Charge Rate.
        /// </summary>
        public int LaserCharge
        {
            get { return mdLaserChargeRate; }
            set { mdLaserChargeRate = value; }
        }
        /// <summary>
        /// Get ship's Laser Damage.
        /// </summary>
        public int LaserDamage
        {
            get { return mdLaserDamage; }
            set { mdLaserDamage = value; }
        }
        /// <summary>
        /// Gets the ships array of tiers.
        /// </summary>
        public int[] Tiers
        {
            get { return mdTiers; }
            set { mdTiers = value; }
        }
        private NeutronGem[] moHold;
        /// <summary>
        /// Gets the ship's current hold.
        /// </summary>
        public NeutronGem[] Hold
        {
            get { return moHold; }
            set { moHold = value; }
        }
        private int mdShipType;
        private int mdNiels; // Currency
        /// <summary>
        /// Gets or Sets the current amount of niels in ship.
        /// </summary>
        public int Niels
        {
            get { return mdNiels; }
            set { mdNiels = value; }
        }
        private float mdAngle;
        private int mdHealthCurrent;
        /// <summary>
        /// Gets/Sets the ships current health
        /// </summary>
        public int HealthCurrent
        {
            get { return mdHealthCurrent; }
            set { mdHealthCurrent = value; }
        }
        private int mdShieldCurrent;
        /// <summary>
        /// Gets/Sets the ships current health
        /// </summary>
        public int ShieldCurrent
        {
            get { return mdShieldCurrent; }
            set { mdShieldCurrent = value; }
        }
        private int mdLaserCounter;
        private bool mdLaserReady;
        /// <summary>
        /// Gets if the laser is ready to fire or not.
        /// </summary>
        public bool LaserReady
        { get { return mdLaserReady; } }
        private float mdTurnRate;
        /// <summary>
        /// Gets the ship type integer,
        /// 0 = Small, 1 = Medium, 2 = Large.
        /// </summary>
        public int Type
        { get { return mdShipType; } }
        // Ship States
        private enum ShipStates
        { Alive, Dead, Paused }
        private ShipStates meShipStates, meShipStatesPrevious;
        /// <summary>
        /// Gets the Ships current Width
        /// </summary>
        public int Width
        { get { return ConsWidth; } }
        /// <summary>
        /// Gets the Ships current Height
        /// </summary>
        public int Height
        { get { return ConsHeight; } }

        private Keys[] meKeybinds;
        /// <summary>
        /// 
        /// </summary>
        public Keys[] Keybinds
        {
            get { return meKeybinds; }
            set { meKeybinds = value; }
        }

        private SoundEffect moEngineSound;
        private SoundEffectInstance moEngine;
        private SoundEffect moThrustersSound;
        private SoundEffectInstance moThrusters;

        /**** End of VARIABLES & PROPERTIES region ****/
        #endregion

        #region CONSTANT DECLARATIONS
        /* This region will contain,
         * all constants for this class
         */

        // Default Values
        private const int ConsWidth = 50;
        private const int ConsHeight = 60;
        private const int SpawnLocation = 200;
        private const float SlowDown = 0.9f;
        private const float SlowestSpeed = 0.05f;
        private const float FastestSpeedMult = 20f;
        private const float ReverseSpeedMult = 0.2f;
        private const int MaxBullets = 100;
        // Bullets values
        private const int ConsBulletWidth = 20;
        private const int ConsBulletHeight = 40;
        private const float ConsBulletSpeed = 4f;
        private const int ConsBulletDamage = 1;
        // Maximum Ship Types
        private const int MaxShipTypes = 3;
        // Ship Type No.
        private const int LargeShipType = 2;
        private const int MediumShipType = 1;
        private const int SmallShipType = 0;
        // Maximum Animation Frames
        private const int MaxSourceFrames = 4;
        private const int MaxLargeFrames = MaxSourceFrames;
        private const int MaxMediumFrames = MaxSourceFrames;
        private const int MaxSmallFrames = MaxSourceFrames;
        // Animation Frames Dimensions
        private const int LargeFrameWidth = 447;
        private const int LargeFrameHeight = 480;
        private const int MediumFrameWidth = 385;
        private const int MediumFrameHeight = 448;
        private const int SmallFrameWidth = 375;
        private const int SmallFrameHeight = 452;

        private const string ShipTextureFileName = "spaceship-spritesheet";
        private const string ShipShieldTextureFileName = "shield_state_active";
        private const string ShipLaserTextureFileName = "ShipLaser";

        // Keybind constants
        private const int MoveUpKey = 0;
        private const int MoveRightKey = 1;
        private const int MoveDownKey = 2;
        private const int MoveLeftKey = 3;
        private const int FireKey = 4;
        private const int BrakeKey = 5;

        private const int ShieldOffSet = 10;

        // For use in the draw method so no filter is applied
        private readonly Color NoFilter = Color.White;
        // For use in when setting a vector to zero
        private readonly Vector2 Zero = Vector2.Zero;

        /**** End of CONSTANT DECLARATIONS **/
        #endregion

        #region CONSTRUCTORS
        /* This region will contain,
         * all constructors for this class
         */
        
        /// <summary>
        /// Default Constructor,
        /// Creates our ship,
        /// initializing all variables
        /// </summary>
        public Ship()
        {
            moPosition = new Vector2(400f, 300f);
            moVelocity = new Vector2(0f);
            moAcceleration = new Vector2(0f);
            moBoundaryBox = new Rectangle((int)moPosition.X, (int)moPosition.Y, ConsWidth, ConsHeight);
            moHitBox = moBoundaryBox;
            moHitBox.X -= moBoundaryBox.Width / 2;
            moHitBox.Y -= moBoundaryBox.Height / 2;
            moShieldBox = moHitBox;
            moShieldBox.X -= ShieldOffSet;
            moShieldBox.Y -= ShieldOffSet;
            moShieldBox.Width += ShieldOffSet * 2;
            moShieldBox.Height += ShieldOffSet * 2;
            moBullets = new Bullet[MaxBullets];
            for (int i = 0; i < moBullets.Length; i++)
            {
                moBullets[i] = new Bullet(
                    new Rectangle(
                        moBoundaryBox.X, moBoundaryBox.Y, ConsBulletWidth, ConsBulletHeight
                    ),
                    ConsBulletSpeed,
                    ConsBulletDamage
                );
            }
            meKeybinds = new Keys[]
            {
                Keys.Up,
                Keys.Right,
                Keys.Down,
                Keys.Left,
                Keys.Space,
                Keys.LeftControl
            };
            maCenterOfRotation = new Vector2[MaxShipTypes];
            maCenterOfRotation[LargeShipType] = new Vector2(LargeFrameWidth / 2f, LargeFrameHeight / 2f);
            maCenterOfRotation[MediumShipType] = new Vector2(MediumFrameWidth / 2f, MediumFrameHeight / 2f);
            maCenterOfRotation[SmallShipType] = new Vector2(SmallFrameWidth / 2f, SmallFrameHeight / 2f);
            moSourceFrames = new Rectangle[MaxShipTypes, MaxSourceFrames];
            mdCurrentSourceFrame = 0;
            CreateSourceFrames();
            // Setting the initial ship type
            mdShipType = SmallShipType;
            // setting its initial frame
            moCurrentFrame = moSourceFrames[SmallShipType, mdCurrentSourceFrame];
            // ship statistics
            mdHealth = 10;
            mdShield = 2;
            mdHoldCapacity = 5;
            moHold = new NeutronGem[mdHoldCapacity];
            mdSpeed = 0.1f;
            mdLaserChargeRate = 100;
            mdLaserDamage = 1;
            mdTurnRate = 2f;
            mdNiels = 100000;
            meShipStates = ShipStates.Alive;
        }

        /**** End of CONSTRUCTORS region ****/
        #endregion

        #region PUBLIC METHODS
        /* This region will contain,
         * all public methods for this class
         */

        /// <summary>
        /// Will load external content
        /// </summary>
        /// <param name="aoAssetsManager">
        /// Passed ContentManager,
        /// will load up our assets from our content pipeline.
        /// </param>
        /// <param name="adFolderPath">
        /// Passed string,
        /// will contain the folder path to the assets of our Ship class.
        /// </param>
        public void LoadContent(ContentManager aoAssetsManager)
        {
            // Loading Textures
            moSpriteTexture = aoAssetsManager.Load<Texture2D>(FilePaths.PlayerShipPath + ShipTextureFileName);
            moShieldTexture = aoAssetsManager.Load<Texture2D>(FilePaths.PlayerShipPath + ShipShieldTextureFileName);
            moEngineSound = aoAssetsManager.Load<SoundEffect>(FilePaths.PlayerSound + "Engine");
            moEngine = moEngineSound.CreateInstance();
            moEngine.IsLooped = true;
            moThrustersSound = aoAssetsManager.Load<SoundEffect>(FilePaths.PlayerSound + "Thrusters");
            moThrusters = moThrustersSound.CreateInstance();
            moThrusters.IsLooped = true;
            for (int i = 0; i < moBullets.Length; i++)
            {
                moBullets[i].LoadContent(aoAssetsManager, FilePaths.PlayerShipPath + ShipLaserTextureFileName);
            }
        }
        /// <summary>
        /// Will handle the Screen's Update Logic
        /// </summary>
        public void Update()
        {
            switch (meShipStates)
            {
                case ShipStates.Alive:
                    PlayerLogic();
                    RefreshSourceFrame();
                    RefreshPosition();
                    break;
                case ShipStates.Paused:
                    break;
                case ShipStates.Dead:
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Will draw Screen components
        /// </summary>
        /// <param name="aoSpriteBatch"></param>
        public void Draw(SpriteBatch aoSpriteBatch)
        {
            switch (meShipStates)
            {
                case ShipStates.Paused:
                case ShipStates.Alive:
                    switch (mdShipType)
                    {
                        case LargeShipType:
                            aoSpriteBatch.Draw(moSpriteTexture, moBoundaryBox, moCurrentFrame, NoFilter, ConvertToRadians(mdAngle),
                                maCenterOfRotation[LargeShipType], SpriteEffects.None, 0f);
                            break;
                        case MediumShipType:
                            aoSpriteBatch.Draw(moSpriteTexture, moBoundaryBox, moCurrentFrame, NoFilter, ConvertToRadians(mdAngle),
                                maCenterOfRotation[MediumShipType], SpriteEffects.None, 0f);
                            break;
                        case SmallShipType:
                        default:
                            aoSpriteBatch.Draw(moSpriteTexture, moBoundaryBox, moCurrentFrame, NoFilter, ConvertToRadians(mdAngle),
                                maCenterOfRotation[SmallShipType], SpriteEffects.None, 0f);
                            break;
                    }
                    if (mdShield > 0)
                    {
                        aoSpriteBatch.Draw(moShieldTexture, moShieldBox, NoFilter);
                    }
                    for (int i = 0; i < moBullets.Length; i++)
                    {
                        moBullets[i].Draw(aoSpriteBatch);
                    }
                    break;
                case ShipStates.Dead:
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Set's the ship's moPosition to the passed float's.
        /// </summary>
        /// <param name="adX">
        /// Passed float
        /// Will define the x coordinate
        /// </param>
        /// <param name="adY">
        /// Passed float
        /// Will define the y coordinate
        /// </param>
        public void SetPosition(float adX, float adY)
        {
            moPosition.X = adX;
            moPosition.Y = adY;
        }
        /// <summary>
        /// Set's the ship's moPosition to the passed float's
        /// and makes the ship alive.
        /// </summary>
        /// <param name="adX">
        /// Passed float
        /// Will define the x coordinate
        /// </param>
        /// <param name="adY">
        /// Passed float
        /// Will define the y coordinate
        /// </param>
        public void Spawn(float adX, float adY)
        {
            SetPosition(adX, adY);
            moVelocity = Zero;
            moAcceleration = Zero;
            mdHealthCurrent = mdHealth;
            mdShieldCurrent = mdShield;
            meShipStates = ShipStates.Alive;
        }
        /// <summary>
        /// Will flip the ship to it's paused state or back
        /// </summary>
        public void Pause()
        {
            if (meShipStates != ShipStates.Paused)
            {
                meShipStatesPrevious = meShipStates;
                meShipStates = ShipStates.Paused;
            }
            else
            {
                meShipStates = meShipStatesPrevious;
            }
        }
        /// <summary>
        /// Will check if the player is dead,
        /// and the return the result.
        /// </summary>
        /// <returns>
        /// Returns true if player is not dead,
        /// else false.
        /// </returns>
        public bool IsAlive()
        {
            if (meShipStates != ShipStates.Dead)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Will cause the Player's shields to take a hit,
        /// and if their shield is down,
        /// their health takes damage.
        /// </summary>
        /// <param name="adDamage"></param>
        public void Hit(int adDamage)
        {
            mdShieldCurrent--;
            if (mdShieldCurrent < 0)
            {
                mdShieldCurrent = 0;
                mdHealthCurrent -= adDamage;
            }
            if (mdHealthCurrent <= 0)
            {
                meShipStates = ShipStates.Dead;
            }
        }
        /// <summary>
        /// Will cause the player to take a direct hit,
        /// ignoring his shield,
        /// and hitting health points directly
        /// </summary>
        /// <param name="adDamage"></param>
        public void DirectHit(int adDamage)
        {
            mdHealthCurrent -= adDamage;
            if (mdHealthCurrent <= 0)
            {
                meShipStates = ShipStates.Dead;
            }
        }
        /// <summary>
        /// Will upgrade health
        /// </summary>
        /// <param name="adNewHealth"></param>
        public void UpgradedHealth(int adNewHealth)
        {
            mdHealth = adNewHealth;
            mdHealthCurrent = mdHealth;
        }
        /// <summary>
        /// Will upgrade shield
        /// </summary>
        /// <param name="adNewShield"></param>
        public void UpgradedShield(int adNewShield)
        {
            mdShield = adNewShield;
            mdShieldCurrent = mdShield;
        }
        /// <summary>
        /// Will upgrade Speed
        /// </summary>
        /// <param name="adNewSpeed"></param>
        public void UpgradedSpeed(float adNewSpeed)
        {
            mdSpeed = adNewSpeed;
        }
        /// <summary>
        /// Will transfer the old hold contents to the newly bought hold
        /// </summary>
        /// <param name="adNewHold"></param>
        public void UpgradedHold(int adNewHold)
        {
            mdHoldCapacity = adNewHold;
            NeutronGem[] aoOldHold = moHold;
            moHold = new NeutronGem[adNewHold];
            for (int i = 0; i < aoOldHold.Length; i++)
            {
                moHold[i] = aoOldHold[i];
            }
        }
        /// <summary>
        /// Will upgrade laser power
        /// </summary>
        /// <param name="adNewLaserDamage"></param>
        public void UpgradedLaserPower(int adNewLaserDamage)
        {
            mdLaserDamage = adNewLaserDamage;
            for (int i = 0; i < moBullets.Length; i++)
            {
                moBullets[i].SetDamage(adNewLaserDamage);
            }
        }
        /// <summary>
        /// Will upgrade laser charge
        /// </summary>
        /// <param name="adNewLaserCharge"></param>
        public void UpgradedLaserCharge(int adNewLaserCharge)
        {
            mdLaserChargeRate = adNewLaserCharge;
        }
        /// <summary>
        /// Will upgrade the entire ship to its new size,
        /// reseting all ship stats to their initial values for the new ship type
        /// </summary>
        /// <param name="adNewTier"></param>
        public void UpgradedShip(int adNewTier, float[,,] adNewStats)
        {
            const int ResetTier = 0;
            mdShipType = adNewTier;
            int adUpgradeType = 0;
            UpgradedHealth((int)adNewStats[mdShipType, adUpgradeType, ResetTier]);
            adUpgradeType++;
            UpgradedShield((int)adNewStats[mdShipType, adUpgradeType, ResetTier]);
            adUpgradeType++;
            UpgradedHold((int)adNewStats[mdShipType, adUpgradeType, ResetTier]);
            adUpgradeType++;
            UpgradedSpeed(adNewStats[mdShipType, adUpgradeType, ResetTier]);
            adUpgradeType++;
            UpgradedLaserCharge((int)adNewStats[mdShipType, adUpgradeType, ResetTier]);
            adUpgradeType++;
            UpgradedLaserPower((int)adNewStats[mdShipType, adUpgradeType, ResetTier]);
            for (int i = 0; i < mdTiers.Length; i++)
            {
                mdTiers[i] = ResetTier + 1;
            }
        }


        /**** End of PUBLIC METHODS region ****/
        #endregion

        #region PRIVATE METHODS
        /* This region will contain,
         * all private methods for this class
         */

        /// <summary>
        /// Will iterate through our source frames,
        /// and create the appropriate source frame,
        /// for our spaceship spritesheet
        /// </summary>
        private void CreateSourceFrames()
        {
            for (int ldShipType = 0; ldShipType < MaxShipTypes; ldShipType += 1)
            {
                int ldY, ldX = 0;
                switch (ldShipType)
                {
                    case LargeShipType: // Large Ship Type
                        ldY = 0;
                        for (int ldFrameIndex = 0; ldFrameIndex < MaxLargeFrames; ldFrameIndex += 1)
                        {
                            moSourceFrames[ldShipType, ldFrameIndex] = new Rectangle(ldX, ldY, LargeFrameWidth, LargeFrameHeight);
                            ldX += LargeFrameWidth;
                        }
                        break;
                    case MediumShipType: // Medium Ship Type
                        ldY = LargeFrameHeight;
                        for (int ldFrameIndex = 0; ldFrameIndex < MaxMediumFrames; ldFrameIndex++)
                        {
                            moSourceFrames[ldShipType, ldFrameIndex] = new Rectangle(ldX, ldY, MediumFrameWidth, MediumFrameHeight);
                            ldX += MediumFrameWidth;
                        }
                        break;
                    case SmallShipType: // Small Ship Type
                        ldY = LargeFrameHeight + MediumFrameHeight;
                        for (int ldFrameIndex = 0; ldFrameIndex < MaxSmallFrames; ldFrameIndex++)
                        {
                            moSourceFrames[ldShipType, ldFrameIndex] = new Rectangle(ldX, ldY, SmallFrameWidth, SmallFrameHeight);
                            ldX += SmallFrameWidth;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        /// <summary>
        /// Will add our acceleration to our velocity vector,
        /// add our velocity to our moPosition vector,
        /// update our boundary box's moPosition,
        /// according to the moPosition vector.
        /// </summary>
        private void RefreshPosition()
        {
            if (mdLaserCounter > mdLaserChargeRate)
            {
                mdLaserCounter = mdLaserChargeRate;
            }
            else if (mdLaserCounter == mdLaserChargeRate)
            {
                mdLaserReady = true;
            }
            else
            {
                mdLaserCounter++;
            }
            mdHighestSpeed = mdSpeed * FastestSpeedMult;
            moVelocity += moAcceleration;
            if (moVelocity.X > 0)
            {
                if (moVelocity.X + moAcceleration.X > mdHighestSpeed)
                {
                    moVelocity.X -= moAcceleration.X;
                }
            }
            else if (moVelocity.X < 0)
            {
                if (moVelocity.X + moAcceleration.X < -mdHighestSpeed)
                {
                    moVelocity.X -= moAcceleration.X;
                }
            }
            if (moVelocity.Y > 0)
            {
                if (moVelocity.Y + moAcceleration.Y > mdHighestSpeed)
                {
                    moVelocity.Y -= moAcceleration.Y;
                }
            }
            else if (moVelocity.Y < 0)
            {
                if (moVelocity.Y + moAcceleration.Y < -mdHighestSpeed)
                {
                    moVelocity.Y -= moAcceleration.Y;
                }
            }

            moPosition += moVelocity;
            moBoundaryBox.X = (int)moPosition.X;
            moBoundaryBox.Y = (int)moPosition.Y;
            moHitBox.X = moBoundaryBox.X - (moBoundaryBox.Width / 2);
            moHitBox.Y = moBoundaryBox.Y - (moBoundaryBox.Height / 2);
            moShieldBox = moHitBox;
            moShieldBox.X -= ShieldOffSet;
            moShieldBox.Y -= ShieldOffSet;
            moShieldBox.Width += ShieldOffSet * 2;
            moShieldBox.Height += ShieldOffSet * 2;
        }
        /// <summary>
        /// Will assign to the current frame,
        /// the appropriate source frame, according to its
        /// ship type and current source frame.
        /// </summary>
        private void RefreshSourceFrame()
        {
            moCurrentFrame = moSourceFrames[mdShipType, mdCurrentSourceFrame];
        }
        /// <summary>
        /// Will handle all player input
        /// </summary>
        private void PlayerLogic()
        {
            Input.UpdateKeyboard();
            PlayerInput(Input.KeyboardState);
            for (int i = 0; i < moBullets.Length; i++)
            {
                moBullets[i].Update(moPosition);
            }
        }
        /// <summary>
        /// Will move the ship,
        /// based on the passed keyboard states.
        /// </summary>
        /// <param name="aoKeyboard">
        /// Passed KeyboardState, will containt the keys just pressed.
        /// </param>
        /// <param name="aoOldKeyboard">
        /// Passed KeyboardState, will contain the keys pressed 1 tick ago.
        /// </param>
        private void PlayerInput(KeyboardState aoKeyboard)
        {
            if (aoKeyboard.IsKeyDown(meKeybinds[MoveLeftKey]))
            {
                mdAngle -= mdTurnRate;
                if (moThrusters.State != SoundState.Playing)
                {
                    moThrusters.Play();
                }
            }
            else if (aoKeyboard.IsKeyDown(meKeybinds[MoveRightKey]))
            {
                mdAngle += mdTurnRate;
                if (moThrusters.State != SoundState.Playing)
                {
                    moThrusters.Play();
                }
            }
            if (aoKeyboard.IsKeyDown(meKeybinds[MoveUpKey]))
            {
                SetForward(mdSpeed);
            }
            else if (aoKeyboard.IsKeyDown(meKeybinds[MoveDownKey]))
            {
                SetForward(-mdSpeed);
            }
            else if (aoKeyboard.IsKeyDown(meKeybinds[BrakeKey]))
            {
                moAcceleration = Zero;
                moVelocity *= SlowDown;
                if (moVelocity.X > 0)
                {
                    if (moVelocity.X < SlowestSpeed)
                    {
                        moVelocity.X = Zero.X;
                    }
                }
                else
                {
                    if (moVelocity.X > -SlowestSpeed)
                    {
                        moVelocity.X = Zero.X;
                    }
                }
                if (moVelocity.Y > 0)
                {
                    if (moVelocity.Y < SlowestSpeed)
                    {
                        moVelocity.Y = Zero.Y;
                    }
                }
                else
                {
                    if (moVelocity.Y > -SlowestSpeed)
                    {
                        moVelocity.Y = Zero.Y;
                    }
                }
            }
            else
            {
                moAcceleration = Zero;
                mdCurrentSourceFrame = 0;
                moThrusters.Stop();
                moEngine.Stop();
            }
            if (aoKeyboard.IsKeyDown(meKeybinds[FireKey]) == true &&
                mdLaserReady == true)
            {
                for (int i = 0; i < moBullets.Length; i++)
                {
                    if (moBullets[i].Alive == false)
                    {
                        moBullets[i].Fire(moPosition, mdAngle);
                        mdLaserReady = false;
                        mdLaserCounter = 0;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// Sets bullet velocity vector to
        /// forward based on its rotation.
        /// </summary>
        /// <param name="adSpeed">
        /// Passed float will set the speed at which the velocity will be set at
        /// </param>
        private void SetForward(float adSpeed)
        {
            if (adSpeed > 0)
            {
                switch (mdShipType)
                {
                    case LargeShipType:
                        if (mdCurrentSourceFrame + 1 < MaxLargeFrames)
                        {
                            mdCurrentSourceFrame++;
                        }
                        else
                        {
                            mdCurrentSourceFrame = 1;
                        }
                        break;
                    case MediumShipType:
                        if (mdCurrentSourceFrame + 1 < MaxMediumFrames)
                        {
                            mdCurrentSourceFrame++;
                        }
                        else
                        {
                            mdCurrentSourceFrame = 1;
                        }
                        break;
                    case SmallShipType:
                        if (mdCurrentSourceFrame + 1 < MaxSmallFrames)
                        {
                            mdCurrentSourceFrame++;
                        }
                        else
                        {
                            mdCurrentSourceFrame = 1;
                        }
                        break;
                    default:
                        break;
                }

                moAcceleration.X = 0;
                moAcceleration.Y = adSpeed;
                if (moEngine.State != SoundState.Playing)
                {
                    moEngine.Play();
                }
            }
            else
            {
                mdCurrentSourceFrame = 0;
                moAcceleration.X = 0;
                moAcceleration.Y = adSpeed * ReverseSpeedMult;
            }
            moAcceleration = RotateVector(moAcceleration, mdAngle);
            
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
            double ldAngle = ConvertToRadians(adAngleInDegrees);
            double cosTheta = Math.Cos(ldAngle);
            double sinTheta = Math.Sin(ldAngle);

            return new Vector2(
                (float)
                -(cosTheta * aoVectorToRotate.X - sinTheta * aoVectorToRotate.Y),
                (float)
                (sinTheta * aoVectorToRotate.X - cosTheta * aoVectorToRotate.Y)
                );
        }
        /// <summary>
        /// Will convert the Angle from degrees to radians
        /// </summary>
        /// <param name="adAngleInDegrees">
        /// Passed Angle in degrees to be converted.
        /// </param>
        /// <returns>
        /// Returns the angle in radians.
        /// </returns>
        private float ConvertToRadians(float adAngleInDegrees)
        {
            return (float)(adAngleInDegrees * (Math.PI / 180));
        }

        /**** End of PRIVATE METHODS region ****/
        #endregion

    }
} // End of namespace