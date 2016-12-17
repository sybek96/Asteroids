using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

/* Group Members:
 *  -   Sebastian Kruzel
 *      C00206244
 *  -   Liam Hickey
 *      C00204864
 *  -   Rafael Girao
 *      C00203250
 *          
 * ASTEROIDS:
 *  - Make a Asteroids game.
 */
/* Prefix System:
 *  In this game we will be using a prefix system,
 *  for all our variables and classes.
 *  Details:
 *      -   m*  : is the starting letter for all,
 *      class instance variables.
 *      -   *o* : this letter indicates that,
 *      the relating variable is of type Object
 *      -   a*  : this starting letter is for all,
 *      local variables (their lifetime is limited to,
 *      a method or loop)
 *      -   *d* : this letter indicates that,
 *      the relating variable is in fact a data type variable,
 *      for example (integers, floats, bools, doubles, strings)
 *      -   *e* : this letter is used only to indicate a enum
 */
/* Work Distribution:
 */
/*  LIAM
 *      License Screen (S)
 *	    Pirate (S)
 *  	Options (R)
 *  	Asteroid (S)
 *  	NeutronGem (R)
 */
/*  RAF
 *  	Ship (L)
 *  	Pause (S)
 *      Main Menu (L)
 *  	Game Over (S)
 *  	Bullets (L)
 *  	Workshop (L)
 *  	Outer Map (S)
 *  	Planet (S)
 *  	Button
 *  	Input
 */
/*  SEB
 *  	Splash Screen (L)
 *  	Help Menu (R)
 *  	Contracts Menu (L)
 *  	Planet
 *  	Boss (L)
 */
/* Total time spent:
 *  - Rafael:
 *      on 05th/03
 *          from:   12:00
 *          to:     20:00 (8 hours)
 *      on 02nd/04
 *          from:   12:00
 *          to:     20:00 (8 hours)
 *      on 26th/03
 *          from:   12:00
 *          to:     20:00 (8 hours)
 *      on 27th/03
 *          from:   12:00
 *          to:     20:00 (8 hours)
 *      on 2nd/04
 *          from:   10:00
 *          to:     20:00 (10 hours)
 *      on 3rd/04
 *          from:   10:00
 *          to:     20:00 (10 hours)
 *      on 16th/04
 *          from:   08:00
 *          to:     22:00 (14 hours)
 *      on 17th/04
 *          from:   08:00
 *          to:     22:00 (14 hours)
 *      on 18th/04
 *          from:   13:00
 *          to:     23:00 (10 hours)
 *      on 19th/04
 *          from:   11:00
 *          to:     13:00 (2 hours)
 *      - 92h:00min
 *  - Liam
 *      - 32h:00min
 *  - Sebastian:
 *      on 30th/03
 *          from:   14:00
 *          to:     15:00 (1 hour)
 *      on 05th/04
 *          from:   13:45
 *          to:     15:00 (1 hour 15 min)
 *      on 06th/04
 *          from:   09:00
 *          to:     10:00 (1 hour)
 *      on 07th/04
 *          from:   13:00
 *          to:     15:00 (2 hours)
 *      on 12th/04
 *          from:   13:00
 *          to:     15:00 (2 hours)
 *      on 13th/04
 *          from:   09:00
 *          to:     11:00 (2 hours)
 *      on 16th/04
 *          from:   14:00
 *          to:     19:00 (5 hours)
 *      on 18th/04
 *          from:   12:00
 *          to:     18:00 (6 hours)
 *      on 19th/04
 *          from:   11:00
 *          to:     13:00 (2 hours)
 *      - 22h:00min
 */
/* Percentage Work Done:
 *  -   Rafael Girao
 *       >  40%
 *  -   Liam Hickey:
 *       >  35%
 *  -   Sebastian Kruzel:
 *       >  25%
 */
/* Known bugs:
 *  Asteroid sometimes collide when they arent suppose to.
 */

namespace Asteroids
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        #region VARIABLES

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Color moBackgroundColour;
        
        // GAMES STATES
        public enum GameState
        {
            SplashScreen,
            LicenseScreen,
            MainMenu,
            OptionsMenu,
            HelpMenu,
            OuterMap,
            PauseScreen,
            GameOver,
            ContractsMenu,
            Level,
            Workshop,
            Exit
        }
        GameState meGameState;

        // Game State Class Declarations
        SplashScreen moSplashScreen;
        LicenseScreen moLicenseScreen;
        MainMenu moMainMenu;
        HelpMenu moHelpMenu;
        OuterMap moOuterMap;
        ContractsMenu moContractsMenu;
        Workshop moWorkshop;
        PauseScreen moPauseScreen;
        OptionsMenu moOptionsMenu;

        // GamePlay Class Declarations
        Random moRandNumGen;
        Ship moPlayerShip;
        Pirate moEnemyShip;
        Boss moBoss;
        Asteroid[] moAsteroids;
        NeutronGem[] moNeutronGems;
        Planet[] moPlanets;
        private string[] mdPltNames = new string[] {
                "Kapono","Hal","Areios","Padma","Parvaiz","Kyriakos","Eloy","Null Zone"
            };
        private int mdNoOfGems = 0;

        private Texture2D
            laserCheckTexture,
            laserChargedTexture,
            laserUnreadyTexture,
            barSquareTexture;
        private Rectangle
            healthBarRect,
            shieldBarRect;
        // Sound stuff
        SoundEffect breakSound;
        Song mainSong;
        Song bossSong;

        #endregion

        #region CONSTANTS

        // Game Window Dimensions
        private const int WindowWidth = 800;
        private const int WindowHeight = 600;
        // (0,0) Vector on the screen
        private readonly Vector2 Origin = new Vector2(0f, 0f);
        // (0,0,800,600) Rectangle depicting the screen
        private readonly Rectangle GameWindow = new Rectangle(0, 0, WindowWidth, WindowHeight);
        // Colour for game background
        readonly Color BackgroundColour = Color.White;
        readonly Color NoFilter = Color.White;
        // Tractor beam radius
        private const float ConsTractorBeamRadius = 100f;
        // In Game UI
        private const int HealthBarX = 10;
        private const int HealthBarY = 10;
        private const int ShieldBarX = 10;
        private const int ShieldBarY = 35;
        private const int BarHeight = 20;
        private const int MaxBarWidth = 250;
        // maximum no. of planets
        const int MaxPlanets = 8;
        // Damage Constants
        private const int PirateHitPlayerDamage = 30;
        private const int AsteroidHitPlayerDamage = 5;
        /* PLANET CONSTANTS */ // Plt = Planet
        // Planet Constants
        private const int PltWidth = 120;
        private const int PltHeight = 60;
        // 1st Planet Kapono
        private const int Plt1X = 60;
        private const int Plt1Y = 445;
        private const float Plt1GemPercent = 0.3f;
        private const float Plt1PiratePercent = 0f;
        private const int Plt1AsteroidNo = 1;
        // 2nd Planet Hal
        private const int Plt2X = 90;
        private const int Plt2Y = 320;
        private const float Plt2GemPercent = 0.5f;
        private const float Plt2PiratePercent = 0.1f;
        private const int Plt2AsteroidNo = 2;
        // 3rd
        private const int Plt3X = 100;
        private const int Plt3Y = 80;
        private const float Plt3GemPercent = 0.4f;
        private const float Plt3PiratePercent = 0.3f;
        private const int Plt3AsteroidNo = 3;
        // 4th
        private const int Plt4X = 350;
        private const int Plt4Y = 30;
        private const float Plt4GemPercent = 0.7f;
        private const float Plt4PiratePercent = 0.5f;
        private const int Plt4AsteroidNo = 4;
        // 5th
        private const int Plt5X = 610;
        private const int Plt5Y = 80;
        private const float Plt5GemPercent = 0.5f;
        private const float Plt5PiratePercent = 0.7f;
        private const int Plt5AsteroidNo = 5;
        // 6th
        private const int Plt6X = 600;
        private const int Plt6Y = 300;
        private const float Plt6GemPercent = 0.8f;
        private const float Plt6PiratePercent = 0.8f;
        private const int Plt6AsteroidNo = 6;
        // 7th
        private const int Plt7X = 400;
        private const int Plt7Y = 300;
        private const float Plt7GemPercent = 0.9f;
        private const float Plt7PiratePercent = 0.9f;
        private const int Plt7AsteroidNo = 7;
        // 8th
        private const int Plt8X = 400;
        private const int Plt8Y = 150;
        private const float Plt8GemPercent = 1f;
        private const float Plt8PiratePercent = 1f;
        private const int Plt8AsteroidNo = 0;
        // Gem Dimensions
        private const int GemSpawnX = -200;
        private const int GemSpawnY = -200;
        private const int GemSpawnSize = 100;
        private readonly Rectangle GemRect = new Rectangle(
            GemSpawnX, GemSpawnY, GemSpawnSize, GemSpawnSize
            );
        private readonly Vector2 laserReadyPosition = new Vector2(3, 540);
        // Starting Game State (used for debuging specific game states)
        private const GameState StartingGameState = GameState.LicenseScreen;

        #endregion

        #region CONSTRUCTOR

        /// <summary>
        /// Will construct our Game class
        /// </summary>
        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            graphics.ApplyChanges();
            this.IsMouseVisible = true;
        }

        #endregion

        #region XNA OVERRIDE METHODS

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            /***** INITIALIZE ALL VARAIBLES HERE *****/

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Initializing our PlayerInput class Keyboard and Mouse States
            // These will get the keyboard's & mouse's current state,
            // and pass it to their corresponding Property
            Input.UpdateKeyboard();
            Input.UpdateMouse();
            
            // Random Number Generator Initialization
            moRandNumGen = new Random();
            // GameState Class Initializations
            moLicenseScreen = new LicenseScreen();
            moSplashScreen = new SplashScreen();
            moHelpMenu = new HelpMenu();
            moMainMenu = new MainMenu();
            moPauseScreen = new PauseScreen();
            moContractsMenu = new ContractsMenu();
            moOuterMap = new OuterMap();
            moWorkshop = new Workshop();
            moOptionsMenu = new OptionsMenu();
            // UI initialization
            healthBarRect = new Rectangle(HealthBarX, HealthBarY, 0, BarHeight);
            shieldBarRect = new Rectangle(ShieldBarX, ShieldBarY, 0, BarHeight);

            // GamePlay Class Initializations
            moPlayerShip = new Ship();
            moEnemyShip = new Pirate(GameWindow.Width, GameWindow.Height);
            moBoss = new Boss();
            moPlanets = new Planet[]
            {
                new Planet(
                    new Rectangle(Plt1X, Plt1Y, PltWidth, PltHeight),
                    Planet.PlanetState.Active,
                    Plt1GemPercent,
                    Plt1PiratePercent,
                    Plt1AsteroidNo,
                    mdPltNames[0],
                    NeutronGem.GemColor.Red
                    ),
                new Planet(
                    new Rectangle(Plt2X, Plt2Y, PltWidth, PltHeight),
                    Planet.PlanetState.Active,
                    Plt2GemPercent,
                    Plt2PiratePercent,
                    Plt2AsteroidNo,
                    mdPltNames[1],
                    NeutronGem.GemColor.Yellow
                    ),
                new Planet(
                    new Rectangle(Plt3X, Plt3Y, PltWidth, PltHeight),
                    Planet.PlanetState.Active,
                    Plt3GemPercent,
                    Plt3PiratePercent,
                    Plt3AsteroidNo,
                    mdPltNames[2],
                    NeutronGem.GemColor.LightBlue
                    ),
                new Planet(
                    new Rectangle(Plt4X, Plt4Y, PltWidth, PltHeight),
                    Planet.PlanetState.Active,
                    Plt4GemPercent,
                    Plt4PiratePercent,
                    Plt4AsteroidNo,
                    mdPltNames[3],
                    NeutronGem.GemColor.Blue
                    ),
                new Planet(
                    new Rectangle(Plt5X, Plt5Y, PltWidth, PltHeight),
                    Planet.PlanetState.Active,
                    Plt5GemPercent,
                    Plt5PiratePercent,
                    Plt5AsteroidNo,
                    mdPltNames[4],
                    NeutronGem.GemColor.Green
                    ),
                new Planet(
                    new Rectangle(Plt6X, Plt6Y, PltWidth, PltHeight),
                    Planet.PlanetState.Active,
                    Plt6GemPercent,
                    Plt6PiratePercent,
                    Plt6AsteroidNo,
                    mdPltNames[5],
                    NeutronGem.GemColor.Purple
                    ),
                new Planet(
                    new Rectangle(Plt7X, Plt7Y, PltWidth, PltHeight),
                    Planet.PlanetState.Active,
                    Plt7GemPercent,
                    Plt7PiratePercent,
                    Plt7AsteroidNo,
                    mdPltNames[6],
                    NeutronGem.GemColor.Black
                    ),
                new Planet(
                    new Rectangle(Plt8X, Plt8Y, PltWidth, PltHeight),
                    Planet.PlanetState.Active,
                    Plt8GemPercent,
                    Plt8PiratePercent,
                    Plt8AsteroidNo,
                    mdPltNames[7],
                    NeutronGem.GemColor.Yellow
                    )
            };
            // Asteroids is only initialized in the outermap when a map is choosen.

            // Setting starting game state
            meGameState = StartingGameState;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            /***** LOAD ALL EXTERNAL CONTENT HERE *****/
            // All Game States
            moLicenseScreen.LoadContent(this.Content);
            moSplashScreen.LoadContent(this.Content);
            moHelpMenu.LoadContent(this.Content);
            moMainMenu.LoadContent(this.Content);
            moContractsMenu.LoadContent(this.Content);
            moPauseScreen.LoadContent(this.Content);
            moOuterMap.LoadContent(this.Content);
            moOptionsMenu.LoadContent(this.Content);
            moWorkshop.LoadContent(this.Content);
            
            // Game Objects
            for (int i = 0; i < moPlanets.Length; i++)
            {
                moPlanets[i].LoadContent(this.Content, FilePaths.OuterMapPath + mdPltNames[i]);
            }
            moPlayerShip.LoadContent(this.Content);
            moEnemyShip.LoadContent(this.Content);
            moBoss.LoadContent(this.Content);

            laserChargedTexture = this.Content.Load<Texture2D>(FilePaths.LevelPath + "laser_ready");
            laserUnreadyTexture = this.Content.Load<Texture2D>(FilePaths.LevelPath + "laser_not_ready");
            laserCheckTexture = laserChargedTexture;
            barSquareTexture = this.Content.Load<Texture2D>(FilePaths.LevelPath + "the_base_square");
            // Sound
            breakSound = this.Content.Load<SoundEffect>(FilePaths.AsteroidSound + "Asteroid_Breaking");
            mainSong = this.Content.Load<Song>(FilePaths.BackgroundSong + "Music_Background");
            bossSong = this.Content.Load<Song>(FilePaths.BackgroundSong + "Music_FinalBoss");
        }

        #region Unload Content is not used in this Game
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        #endregion

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            /***** UPDATE ALL GAME LOGIC HERE *****/
            Input.UpdateKeyboard();

            switch (meGameState)
            {
                case GameState.LicenseScreen:
                    moBackgroundColour = Color.White;
                    moLicenseScreen.Update(gameTime, ref meGameState);
                    break;
                case GameState.SplashScreen:
                    MediaPlayer.Play(mainSong);
                    moSplashScreen.Update(gameTime, ref meGameState);
                    break;
                case GameState.MainMenu:
                    moBackgroundColour = Color.Black;
                    moMainMenu.Update(ref meGameState);
                    break;
                case GameState.OptionsMenu:
                    moOptionsMenu.Update(moPlayerShip.Keybinds, ref meGameState);
                    break;
                case GameState.HelpMenu:
                    moHelpMenu.Update(ref meGameState);
                    break;
                case GameState.OuterMap:
                    moOuterMap.Update(moPlanets, ref meGameState, this.Content);
                    break;
                case GameState.GameOver:
                    break;
                case GameState.ContractsMenu:
                    moContractsMenu.Update(moOuterMap.LevelSelected, ref meGameState);
                    if (meGameState == GameState.Level)
                    {
                        LevelSelected(moOuterMap.LevelSelected);
                    }
                    break;
                case GameState.PauseScreen:
                    moPauseScreen.Update(ref meGameState);
                    if (meGameState != GameState.PauseScreen)
                    {
                        UnPause();
                    }
                    break;
                case GameState.Level:
                    if (Input.KeyboardState.IsKeyDown(Keys.Escape))
                    {
                        Pause();
                    }
                    moPlayerShip.Update();
                    moEnemyShip.Update(moPlayerShip.Position);
                    moBoss.Update(GameWindow, gameTime);
                    if (moBoss.Active == false)
                    {
                        for (int i = 0; i < moAsteroids.Length; i++)
                        {
                            if (moAsteroids[i] != null)
                            {
                                moAsteroids[i].Update();
                            }
                            if (moNeutronGems[i] != null)
                            {
                                moNeutronGems[i].Update(moPlayerShip.Position, ConsTractorBeamRadius);
                            }
                        }
                    }
                    ComputeCollisions();
                    UpdateUI();
                    break;
                case GameState.Workshop:
                    moWorkshop.Update(ref moPlayerShip, ref meGameState);
                    break;
                case GameState.Exit:
                    this.Exit();
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            /**** DRAW ALL SCREEN OBJECTS HERE *****/
            GraphicsDevice.Clear(moBackgroundColour);

            spriteBatch.Begin();
            switch (meGameState)
            {
                case GameState.SplashScreen:
                    moSplashScreen.Draw(spriteBatch, GameWindow);
                    break;
                case GameState.LicenseScreen:
                    moLicenseScreen.Draw(spriteBatch, GameWindow);
                    break;
                case GameState.MainMenu:
                    moMainMenu.Draw(spriteBatch, GameWindow);
                    break;
                case GameState.OptionsMenu:
                    moOptionsMenu.Draw(spriteBatch, GameWindow);
                    break;
                case GameState.HelpMenu:
                    moHelpMenu.Draw(spriteBatch, GameWindow);
                    break;
                case GameState.OuterMap:
                    moOuterMap.Draw(spriteBatch, moPlanets, GameWindow);
                    break;
                case GameState.GameOver:
                    break;
                case GameState.ContractsMenu:
                    moContractsMenu.Draw(spriteBatch, GameWindow);
                    break;
                case GameState.PauseScreen:
                case GameState.Level:
                    Draw(spriteBatch, moPlanets[moOuterMap.LevelSelected]);
                    moEnemyShip.Draw(spriteBatch);
                    moBoss.Draw(spriteBatch);
                    if (moBoss.Active == false)
                    {
                        for (int i = 0; i < moAsteroids.Length; i++)
                        {
                            if (moAsteroids[i] != null)
                            {
                                moAsteroids[i].Draw(spriteBatch);
                            }
                            if (moNeutronGems[i] != null)
                            {
                                moNeutronGems[i].Draw(spriteBatch);
                            }
                        }
                    }
                    moPlayerShip.Draw(spriteBatch);
                    if (meGameState == GameState.PauseScreen)
                    {
                        moPauseScreen.Draw(spriteBatch);
                    }
                    DrawUI();
                    break;
                case GameState.Workshop:
                    moWorkshop.Draw(spriteBatch, GameWindow, ref moPlayerShip);
                    break;
                case GameState.Exit:
                    break;
                default:
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Will Pause all in game elements,
        /// and set the game state to pause.
        /// </summary>
        private void Pause()
        {
            if (moPlayerShip.IsAlive() == true)
            {
                moPlayerShip.Pause();
            }
            meGameState = GameState.PauseScreen;
        }
        /// <summary>
        /// Will UnPause all in game elements.
        /// </summary>
        private void UnPause()
        {
            if (moPlayerShip.IsAlive() == true)
            {
                moPlayerShip.Pause();
            }
        }
        /// <summary>
        /// Will handle the collisions of all objects in game
        /// </summary>
        private void ComputeCollisions()
        {
            ComputeCollisions(moPlayerShip);
            ComputeCollisions(moEnemyShip);
            ComputeCollisions(moBoss);
            ComputeCollisions(moPlayerShip, moAsteroids);
            ComputeCollisions(moPlayerShip, moBoss);
            if (moAsteroids != null)
            {
                for (int i = 0; i < moAsteroids.Length; i++)
                {
                    if (moAsteroids[i] != null)
                    {
                        if (moAsteroids[i].Active == true)
                        {
                            ComputeCollisions(moEnemyShip, moAsteroids[i]);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Overloaded Method:
        /// Will compute collisions between the ship and the in game world.
        /// </summary>
        /// <param name="aoShip">
        /// Passed Ship
        /// Will have its collisions computed
        /// </param>
        private void ComputeCollisions(Ship aoShip)
        {
            if (aoShip.Position.X < Origin.X - aoShip.Width)
            {
                aoShip.SetPosition(WindowWidth + aoShip.Width, aoShip.Position.Y);
            }
            else if (aoShip.Position.X > Origin.X + WindowWidth + aoShip.Width)
            {
                aoShip.SetPosition(-aoShip.Width, aoShip.Position.Y);
            }
            if (aoShip.Position.Y < Origin.Y - aoShip.Height)
            {
                aoShip.SetPosition(aoShip.Position.X, WindowHeight + aoShip.Height);
            }
            else if (aoShip.Position.Y > Origin.Y + WindowHeight + aoShip.Height)
            {
                aoShip.SetPosition(aoShip.Position.X, -aoShip.Height);
            }
            for (int i = 0; i < aoShip.Bullets.Length; i++)
            {
                ComputeCollisions(aoShip.Bullets[i]);
            }
            for (int i = 0; i < moNeutronGems.Length; i++)
            {
                ComputeCollisions(aoShip, moNeutronGems[i]);
            }
        }
        /// <summary>
        /// Overloaded Method:
        /// Will compute collisions between the ship and the neutron gem.
        /// </summary>
        /// <param name="aoShip"></param>
        /// <param name="aoNeutronGem"></param>
        private void ComputeCollisions(Ship aoShip, NeutronGem aoNeutronGem)
        {
            if (aoShip.IsAlive() == true && aoNeutronGem.Active == true)
            {
                const int CollisionOffset = 30;
                if (aoShip.HitBox.Center.X - CollisionOffset <= aoNeutronGem.Position.X &&
                    aoShip.HitBox.Center.X + CollisionOffset >= aoNeutronGem.Position.X &&
                    aoShip.HitBox.Center.Y - CollisionOffset <= aoNeutronGem.Position.Y &&
                    aoShip.HitBox.Center.Y + CollisionOffset >= aoNeutronGem.Position.Y)
                {
                    for (int j = 0; j < aoShip.Hold.Length; j++)
                    {
                        if (aoShip.Hold[j] == null)
                        {
                            aoShip.Hold[j] = aoNeutronGem.Collect();
                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Overloaded Method:
        /// Will compute collisions between the Pirate and the in game world.
        /// </summary>
        /// <param name="aoPirate">
        /// Passed Pirate
        /// Will have its collisions computed
        /// </param>
        private void ComputeCollisions(Pirate aoPirate)
        {
            ComputeCollisions(aoPirate.Bullet);
            if (aoPirate.Active == true)
            {
                ComputeCollisions(moPlayerShip, aoPirate);
            }
        }
        /// <summary>
        /// Overloaded Method:
        /// Will compute collisions between the Boss and the in game world.
        /// </summary>
        /// <param name="aoBoss"></param>
        private void ComputeCollisions(Boss aoBoss)
        {
            if (aoBoss.Active == true && aoBoss.Alive == true)
            {
                for (int i = 0; i < aoBoss.Bullets.Length; i++)
                {
                    ComputeCollisions(aoBoss.Bullets[i]);
                }
            }
        }
        /// <summary>
        /// Overloaded Method:
        /// Will compute collisions between the Ship and the Boss
        /// </summary>
        /// <param name="aoShip"></param>
        /// <param name="aoBoss"></param>
        private void ComputeCollisions(Ship aoShip, Boss aoBoss)
        {
            if (aoBoss.Active == true && aoBoss.Alive == true)
            {
                for (int i = 0; i < aoShip.Bullets.Length; i++)
                {
                    if (aoShip.Bullets[i].Alive == true)
                    {
                        ComputeCollisions(aoBoss, aoShip.Bullets[i]);
                    }
                }
                for (int i = 0; i < aoBoss.Bullets.Length; i++)
                {
                    if (aoBoss.Bullets[i].Alive == true)
                    {
                        ComputeCollisions(aoShip, aoBoss.Bullets[i]);
                    }
                }
            }
        }
        /// <summary>
        /// Overloaded Method:
        /// Will compute collisions between the Boss and the Bullet
        /// </summary>
        /// <param name="aoBoss"></param>
        /// <param name="aoBullet"></param>
        private void ComputeCollisions(Boss aoBoss, Bullet aoBullet)
        {
            if (CheckRectangleCollision(aoBoss.BoundaryBox, aoBullet.BoundaryBox) == true)
            {
                aoBoss.Health -= aoBullet.Damage;
                aoBullet.Die();
            }
        }
        /// <summary>
        /// Overloaded Method:
        /// Will compute collisions between the bullet and the in game world.
        /// </summary>
        /// <param name="aoBullet">
        /// Passed Bullets
        /// Will have its collisions computed
        /// </param>
        private void ComputeCollisions(Bullet aoBullet)
        {
            if (moBoss.Active == false)
            {
                if (aoBullet.BoundaryBox.X < Origin.X - aoBullet.BoundaryBox.Width)
                {
                    aoBullet.SetPosition(WindowWidth + aoBullet.BoundaryBox.Width, aoBullet.BoundaryBox.Y);
                }
                else if (aoBullet.BoundaryBox.X > Origin.X + WindowWidth + aoBullet.BoundaryBox.Width)
                {
                    aoBullet.SetPosition(-aoBullet.BoundaryBox.Width, aoBullet.BoundaryBox.Y);
                }
                if (aoBullet.BoundaryBox.Y < Origin.Y - aoBullet.BoundaryBox.Height)
                {
                    aoBullet.SetPosition(aoBullet.BoundaryBox.X, WindowHeight + aoBullet.BoundaryBox.Height);
                }
                else if (aoBullet.BoundaryBox.Y > Origin.Y + WindowHeight + aoBullet.BoundaryBox.Height)
                {
                    aoBullet.SetPosition(aoBullet.BoundaryBox.X, -aoBullet.BoundaryBox.Height);
                }
                if (aoBullet.TimeAlive > aoBullet.MaxTimeAlive)
                {
                    aoBullet.Die();
                }
            }
            else
            {
                if (aoBullet.Alive == true)
                {
                    if (aoBullet.BoundaryBox.X < Origin.X - aoBullet.BoundaryBox.Width ||
                    aoBullet.BoundaryBox.X > Origin.X + WindowWidth + aoBullet.BoundaryBox.Width ||
                    aoBullet.BoundaryBox.Y < Origin.Y - aoBullet.BoundaryBox.Height ||
                    aoBullet.BoundaryBox.Y > Origin.Y + WindowHeight + aoBullet.BoundaryBox.Height)
                    {
                        aoBullet.Die();
                    }
                }
            }
        }
        /// <summary>
        /// Overloaded Method:
        /// Will compute collisions between the Ship and the Pirate
        /// </summary>
        /// <param name="aoPirateShip">
        /// Passed Ship
        /// Will have its collisions computed
        /// </param>
        /// <param name="aoPirateShip">
        /// Passed Pirate
        /// Will have its collisions computed
        /// </param>
        private void ComputeCollisions(Ship aoPlayerShip, Pirate aoPirateShip)
        {
            if (aoPlayerShip.IsAlive() == true && aoPirateShip.Active == true)
            {
                if (CheckRectangleCollision(aoPlayerShip.HitBox, aoPirateShip.HitBox) == true)
                {
                    aoPlayerShip.DirectHit(PirateHitPlayerDamage);
                    aoPirateShip.Die();
                }
            }
            ComputeCollisions(aoPlayerShip, aoPirateShip.Bullet);
            for (int i = 0; i < aoPlayerShip.Bullets.Length; i++)
            {
                if (aoPlayerShip.Bullets[i].Alive)
                {
                    ComputeCollisions(aoPirateShip, aoPlayerShip.Bullets[i]);
                }
            }
        }
        /// <summary>
        /// Overloaded Method:
        /// Will compute collisions between the Pirate and the Bullets
        /// </summary>
        /// <param name="aoPirateShip">
        /// Passed Pirate
        /// Will have its collisions computed
        /// </param>
        /// <param name="aoBullet">
        /// Passed Bullets
        /// Will have its collisions computed
        /// </param>
        private void ComputeCollisions(Pirate aoPirateShip, Bullet aoBullet)
        {
            if (aoPirateShip.Active == true)
            {
                if (CheckRectangleCollision(aoPirateShip.HitBox, aoBullet.BoundaryBox) == true)
                {
                    aoPirateShip.Die();
                    aoBullet.Die();
                }
            }
        }
        /// <summary>
        /// Overloaded Method:
        /// Will compute collisions between the Pirate and the Asteroid
        /// </summary>
        /// <param name="aoPirateShip">
        /// Passed Pirate
        /// Will have its collisions computed
        /// </param>
        /// <param name="aoAsteroids">
        /// Passed Asteroid
        /// Will have its collisions computed
        /// </param>
        private void ComputeCollisions(Pirate aoPirateShip, Asteroid aoAsteroids)
        {
            if (aoPirateShip.Active == true)
            {
                if (CheckRectangleCollision(aoPirateShip.HitBox, aoAsteroids.HitBox) == true)
                {
                    int adMinimumDist = (int)((aoPirateShip.HitBox.Width * 0.5f) + (aoAsteroids.HitBox.Width * 0.5f));
                    if (CheckCircularCollision(aoPirateShip.HitBox.Center, aoAsteroids.HitBox.Center, adMinimumDist) == true)
                    {
                        aoPirateShip.Die();
                    }
                }
            }
        }
        /// <summary>
        /// Overloaded Method:
        /// Will compute collisions between the Ship and the Bullets
        /// </summary>
        /// <param name="aoPirateShip">
        /// Passed Ship
        /// Will have its collisions computed
        /// </param>
        /// <param name="aoBullet">
        /// Passed Bullets
        /// Will have its collisions computed
        /// </param>
        private void ComputeCollisions(Ship aoPlayerShip, Bullet aoBullet)
        {
            if (aoPlayerShip.IsAlive() == true && aoBullet.Alive == true)
            {
                if (CheckRectangleCollision(aoPlayerShip.HitBox, aoBullet.BoundaryBox) == true)
                {
                    aoPlayerShip.Hit(aoBullet.Damage);
                    aoBullet.Die();
                }
            }
        }
        /// <summary>
        /// Overloaded Method:
        /// Will compute collisions between the Ship and the Asteroid
        /// </summary>
        /// <param name="aoPirateShip">
        /// Passed Ship
        /// Will have its collisions computed
        /// </param>
        /// <param name="aoAsteroids">
        /// Passed Asteroid
        /// Will have its collisions computed
        /// </param>
        private void ComputeCollisions(Ship aoPlayerShip, Asteroid[] aoAsteroids)
        {
            if (aoPlayerShip.IsAlive() == true)
            {
                if (aoAsteroids != null)
                {
                    for (int i = 0; i < aoAsteroids.Length; i++)
                    {
                        if (aoAsteroids[i] != null)
                        {
                            if (aoAsteroids[i].Active == true)
                            {
                                if (CheckRectangleCollision(aoPlayerShip.HitBox, aoAsteroids[i].HitBox) == true)
                                {
                                    int adMinimumDist = (int)((aoPlayerShip.HitBox.Width * 0.5f) + (aoAsteroids[i].HitBox.Width * 0.5f));
                                    if (CheckCircularCollision(aoPlayerShip.HitBox.Center, aoAsteroids[i].HitBox.Center, adMinimumDist) == true)
                                    {
                                        if (aoAsteroids[i].AsteroidSize == Asteroid.Size.Small)
                                        {
                                            for (int j = 0; j < moNeutronGems.Length; j++)
                                            {
                                                if (moNeutronGems[j].Active == false)
                                                {
                                                    moNeutronGems[j].Activate(aoAsteroids[i].Position);
                                                    break;
                                                }
                                            }
                                        }
                                        Asteroid aoNewAsteroid = aoAsteroids[i].Destroyed(aoPlayerShip.Velocity, 0, this.Content);
                                        switch (aoAsteroids[i].AsteroidSize)
                                        {
                                            case Asteroid.Size.Large:
                                                aoPlayerShip.DirectHit(AsteroidHitPlayerDamage * 3);
                                                break;
                                            case Asteroid.Size.Medium:
                                                aoPlayerShip.DirectHit(AsteroidHitPlayerDamage * 2);
                                                break;
                                            case Asteroid.Size.Small:
                                                aoPlayerShip.DirectHit(AsteroidHitPlayerDamage);
                                                break;
                                            default:
                                                break;
                                        }
                                        if (aoNewAsteroid != null)
                                        {
                                            for (int j = 0; j < aoAsteroids.Length; j++)
                                            {
                                                if (aoAsteroids[j] == null)
                                                {
                                                    aoAsteroids[j] = aoNewAsteroid;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                for (int j = 0; j < aoPlayerShip.Bullets.Length; j++)
                                {
                                    if (aoPlayerShip.Bullets[j].Alive == true)
                                    {
                                        ComputeCollisions(aoAsteroids, aoPlayerShip.Bullets[j]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aoAsteroids"></param>
        /// <param name="aoBullet"></param>
        private void ComputeCollisions(Asteroid[] aoAsteroids, Bullet aoBullet)
        {
            if (aoBullet.Alive == true)
            {
                if (aoAsteroids != null)
                {
                    for (int i = 0; i < aoAsteroids.Length; i++)
                    {
                        if (aoAsteroids[i] != null)
                        {
                            if (aoAsteroids[i].Active == true)
                            {
                                if (CheckRectangleCollision(aoBullet.BoundaryBox, aoAsteroids[i].HitBox) == true)
                                {
                                    aoBullet.Die();
                                    if (aoAsteroids[i].AsteroidSize == Asteroid.Size.Small)
                                    {
                                        Asteroid aoNewAsteroid = aoAsteroids[i].Destroyed(aoBullet.Velocity, 0, this.Content);
                                        for (int j = 0; j < moNeutronGems.Length; j++)
                                        {
                                            if (moNeutronGems[j].Active == false)
                                            {
                                                moNeutronGems[j].Activate(aoAsteroids[i].Position);
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Asteroid aoNewAsteroid = aoAsteroids[i].Destroyed(aoBullet.Velocity, 0, this.Content);
                                        if (aoNewAsteroid != null)
                                        {
                                            for (int j = 0; j < aoAsteroids.Length; j++)
                                            {
                                                if (aoAsteroids[j] == null)
                                                {
                                                    aoAsteroids[j] = aoNewAsteroid;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aoPirateShip"></param>
        /// <param name="aoAsteroids"></param>
        private void ComputeCollisions(Pirate aoPirateShip, Asteroid[] aoAsteroid)
        {
            if (aoPirateShip.Active == true)
            {
                if (aoAsteroid != null)
                {
                    for (int i = 0; i < aoAsteroid.Length; i++)
                    {
                        if (aoAsteroid[i].Active == true)
                        {
                            if (CheckRectangleCollision(aoPirateShip.HitBox, aoAsteroid[i].HitBox) == true)
                            {
                                int adMinimumDist = (int)((aoPirateShip.HitBox.Width * 0.5f) + (aoAsteroid[i].HitBox.Width * 0.5f));
                                if (CheckCircularCollision(aoPirateShip.HitBox.Center, aoAsteroid[i].HitBox.Center, adMinimumDist) == true)
                                {
                                    Asteroid aoNewAsteroid = aoAsteroid[i].Destroyed(aoPirateShip.Velocity, 0, this.Content);
                                    aoPirateShip.Die();
                                    if (aoNewAsteroid != null)
                                    {
                                        for (int j = 0; j < aoAsteroid.Length; j++)
                                        {
                                            if (aoAsteroid[j].Active == false)
                                            {
                                                aoAsteroid[j] = aoNewAsteroid;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        moNeutronGems[mdNoOfGems].Activate(aoAsteroid[i].Position);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Checks if the rectangles are intersecting,
        /// than returns the result.
        /// </summary>
        /// <param name="ad1stRect">
        /// Passed Rectangle
        /// will determine the first rectangle for collision checking
        /// </param>
        /// <param name="ad2ndRect">
        /// Passed Rectangle
        /// will determine the second rectangle for collisions checking
        /// </param>
        /// <returns>
        /// Returns true if the two rectangles have intersected,
        /// else false.
        /// </returns>
        private bool CheckRectangleCollision(Rectangle ad1stRect, Rectangle ad2ndRect)
        {
            bool mdCollided = false;
            if (ad1stRect.X >= ad2ndRect.X - ad1stRect.Width &&
                ad1stRect.X <= ad2ndRect.X + ad2ndRect.Width &&
                ad1stRect.Y >= ad2ndRect.Y - ad1stRect.Height &&
                ad1stRect.Y <= ad2ndRect.Y + ad2ndRect.Height)
            {
                mdCollided = true;
            }
            return mdCollided;
        }
        /// <summary>
        /// Checks for collision between 2 vectors based as parameters,
        /// by getting the distance between the first and second Vector2's x,y components,
        /// squaring the minimum distance between the two vectors
        /// comparing using the following algorithm
        /// (distanceOfX * distanceOfX) + (distanceOfY * distanceOfY) greater than (minDistBetween * minDistBetween)
        /// </summary>
        /// <param name="ad1stPoint">
        /// Passed Vector2
        /// Determines the first center of circular collision
        /// </param>
        /// <param name="ad2ndPoint">
        /// Passed Vector2
        /// Determines the second center of circular collision
        /// </param>
        /// <param name="adMinDistBetween">
        /// Passed float
        /// Determines the minimum distance between the two circles to get collision (add both circle's radius)
        /// </param>
        /// <returns>
        /// Returns true if the distance squared between the two vectors is less than the minimum distance between.
        /// </returns>
        private bool CheckCircularCollision(Point ad1stPoint, Point ad2ndPoint, float adMinDistBetween)
        {
            bool mdCollided = false;
            Vector2 adDistance;
            adDistance.X = ad2ndPoint.X - ad1stPoint.X;
            adDistance.Y = ad2ndPoint.Y - ad1stPoint.Y;

            if ((adDistance.LengthSquared()) < (adMinDistBetween * adMinDistBetween))
            {
                mdCollided = true;
            }
            return mdCollided;
        }
        /// <summary>
        /// Will draw the passed planets background
        /// </summary>
        /// <param name="aoSpriteBatch"></param>
        /// <param name="aoPlanet"></param>
        private void Draw(SpriteBatch aoSpriteBatch, Planet aoPlanet)
        {
            aoSpriteBatch.Draw(aoPlanet.Background, GameWindow, NoFilter);
        }
        /// <summary>
        /// Will load the next level preparing the asteroid array for it.
        /// </summary>
        /// <param name="adLevel"></param>
        private void LevelSelected(int adLevel)
        {
            const int MaxVelocity = 100;
            int
                adPosX = 0, adPosY = 0;
            float
                adVelX = 0, adVelY = 0;
            Vector2
                aoPosition = new Vector2(0f),
                aoVelocity = new Vector2(0f);
            moPlayerShip.Spawn(GameWindow.Width / 2f, GameWindow.Height / 2f);
            switch (adLevel)
            {
                case 7:
                    moBoss.Active = true;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(bossSong);
                    break;
                case 6:
                case 5:
                case 4:
                case 3:
                case 2:
                case 1:
                    if (moRandNumGen.NextDouble() < moPlanets[adLevel].PiratePercentage)
                    {
                        moEnemyShip.Spawn();
                    }
                    goto case 0;
                case 0:
                    moBoss.Active = false;
                    moNeutronGems = new NeutronGem[moPlanets[adLevel].AsteroidNo * 6];
                    for (int adIndex = 0; adIndex < moNeutronGems.Length; adIndex++)
                    {
                        moNeutronGems[adIndex] = new NeutronGem(moPlanets[adLevel].GemColour, GemRect);
                        moNeutronGems[adIndex].LoadContent(this.Content);
                    }
                    moAsteroids = new Asteroid[moPlanets[adLevel].AsteroidNo * 6];
                    for (int adIndex = 0; adIndex < moPlanets[adLevel].AsteroidNo; adIndex++)
                    {
                        adPosX = RandomiseNo(adPosX, GameWindow.Width);
                        adPosY = RandomiseNo(adPosY, GameWindow.Height);
                        adVelX = RandomiseNo(0, MaxVelocity) / 100f;
                        adVelY = RandomiseNo(0, MaxVelocity) / 100f;
                        aoPosition.X = adPosX;
                        aoPosition.Y = adPosY;
                        aoVelocity.X = adVelX;
                        aoVelocity.Y = adVelY;
                        moAsteroids[adIndex] = new Asteroid(
                            aoPosition,
                            aoVelocity,
                            GameWindow.Width,
                            GameWindow.Height,
                            Asteroid.Size.Large
                            );
                        moAsteroids[adIndex].LoadContent(this.Content);
                    }
                    moPlayerShip.Spawn(GameWindow.Width / 2f, GameWindow.Height / 2f);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Will randomise the passed number,
        /// between either (0 to 100) or (limit-100 to limit)
        /// </summary>
        /// <param name="adNumber"></param>
        /// <returns></returns>
        private int RandomiseNo(int adNumber, int adLimit)
        {
            const int EdgeSpawn = 100;
            double adChance = 0;
            adChance = moRandNumGen.NextDouble();
            if (adChance > 0.5)
            {
                adNumber = moRandNumGen.Next(adLimit - EdgeSpawn, adLimit);
            }
            else // adChance <= 0.5
            {
                adNumber = moRandNumGen.Next(0, EdgeSpawn);
            }
            return adNumber;
        }
        /// <summary>
        /// Will update in game UI
        /// </summary>
        private void UpdateUI()
        {
            healthBarRect.Width = (int)((moPlayerShip.HealthCurrent / (float)moPlayerShip.MaxHealth) * MaxBarWidth);
            shieldBarRect.Width = (int)((moPlayerShip.ShieldCurrent / (float)moPlayerShip.Shield) * MaxBarWidth);
            if (moPlayerShip.LaserReady == true)
            {
                laserCheckTexture = laserChargedTexture;
            }
            else
            {
                laserCheckTexture = laserUnreadyTexture;
            }
        }
        /// <summary>
        /// Will draw in game UI
        /// </summary>
        private void DrawUI()
        {
            spriteBatch.Draw(laserCheckTexture, laserReadyPosition, NoFilter);
            spriteBatch.Draw(barSquareTexture, healthBarRect, Color.Green);
            spriteBatch.Draw(barSquareTexture, shieldBarRect, Color.Blue);
        }

        #endregion

    } // End of class

} // End of namespace