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
 *  -   Everyone
 */
/* Class Description:
 *  -   This is the FilePaths Static class,
 *  it will hold all the paths and directory to all assets for our game.
 */

namespace Asteroids
{
    class FilePaths
    {
        //
        /*
         * CONSTANT DECLARATION
         */
        //

        /* String File / Paths Declarations */

        // Content Folders Declarations
        private const string AssetsFolder =
            "Assets/";
        private const string ArtFolder =
            AssetsFolder + "Art/";
        /* BUTTONS FOLDER */
        private const string ButtonFolder =
            ArtFolder + "Button/";
        private const string ButtonActiveTextureFile =
            ButtonFolder + "button";
        private const string ButtonHoverTextureFile =
            ButtonFolder + "button_hover";
        private const string ButtonPressedTextureFile =
            ButtonFolder + "button_pressed";
        /* ASTEROIDS FOLDER */
        private const string AsteroidsFolder =
            ArtFolder + "Asteroids/";
        private const string AsteroidsBrownTextureFile =
            AsteroidsFolder + "asteroid_brown";
        private const string AsteroidsGrey1TextureFile =
            AsteroidsFolder + "asteroid_grey";
        private const string AsteroidsGrey2TextureFile =
            AsteroidsFolder + "asteroid_grey_2";
        /* ENEMIES FOLDER */
        private const string EnemiesFolder =
            ArtFolder + "Enemies/";
        /* BOSS FOLDER */
        private const string FinalBossFolder =
            EnemiesFolder + "FinalBoss/";
        /* PIRATE SHIP FOLDER */
        private const string PirateShipFolder =
            EnemiesFolder + "PirateShip/";
        /* GEMS FOLDER */
        private const string NeutronGemsFolder =
            ArtFolder + "Neutron Gems/";
        /* PLAYER FOLDER */
        private const string PlayerFolder =
            ArtFolder + "Player/";
        /* SCREENS FOLDER */
        private const string ScreensFolder =
            ArtFolder + "Screens/";
        /* CONTRACTS FOLDER */
        private const string ContractsFolder =
            ScreensFolder + "Contracts/";
        private const string ContractsFontFile =
            ContractsFolder + "ContractsFont";
        /* PAUSE FOLDER */
        private const string PauseFolder =
            ScreensFolder + "Pause/";
        private const string PauseButtonFontFile =
            PauseFolder + "PauseBtnFont";
        /* LICENSE SCREEN FOLDER */
        private const string LicenseScreenFolder =
            ScreensFolder + "License/";
        /* SPLASH SCREEN FOLDER */
        private const string SplashScreenFolder =
            ScreensFolder + "Splash/";
        /* HELP SCREEN FOLDER */
        private const string HelpScreenFolder =
            ScreensFolder + "Help/";
        /* MAIN MENU FOLDER */
        private const string MainMenuFolder =
            ScreensFolder + "MainMenu/";
        /* OPTIONS FOLDER */
        private const string OptionsMenuFolder =
            ScreensFolder + "Options/";
        // Level
        private const string LevelFolder =
            ScreensFolder + "Level/";
        /* OUTER MAP FOLDER */
        private const string OuterMapFolder =
            ScreensFolder + "OuterMap/";
        /* WORKSHOP FOLDER */
        private const string WorkshopFolder =
            ScreensFolder + "Workshop/";

        // Sound Folders
        private const string SoundsFolder =
            AssetsFolder + "Sounds/";
        private const string AsteroidsSoundFolder =
            SoundsFolder + "Asteroids/";
        private const string EnemiesSoundFolder =
            SoundsFolder + "Enemies/";
        private const string FinalBossSoundFolder =
            EnemiesSoundFolder + "FinalBoss/";
        private const string PirateShipSoundFolder =
            EnemiesSoundFolder + "PirateShip/";
        private const string NeutronGemsSoundFolder =
            SoundsFolder + "Neutron Gems/";
        private const string PlayerSoundFolder =
            SoundsFolder + "Player/";
        private const string ScreensSoundFolder =
            SoundsFolder + "Screens/";
        private const string BulletSoundFolder =
            SoundsFolder + "Bullet/";
        private const string ButtonSoundFolder =
            SoundsFolder + "Button/";
        private const string BackgroundSongFolder =
            SoundsFolder + "Songs/";


        //
        /*
         * PUBLIC METHODS
         */
        //

        /// <summary>
        /// Gets the Contract Menu's File Directory
        /// </summary>
        public static string ContractsPath
        { get { return ContractsFolder; } }
        /// <summary>
        /// Gets the Contract Menu's Font File Directory
        /// </summary>
        public static string ContractsFont
        { get { return ContractsFontFile; } }
        /// <summary>
        /// Gets the Pause Menu's Font File Path and Name.
        /// </summary>
        public static string PauseButtonFont
        { get { return PauseButtonFontFile; } }
        /// <summary>
        /// Gets the Button's IsActive Texture File Path and Name.
        /// </summary>
        public static string ButtonActiveTexture
        { get { return ButtonActiveTextureFile; } }
        /// <summary>
        /// Gets the Button's Hovered Texture File Path and Name.
        /// </summary>
        public static string ButtonHoverTexture
        { get { return ButtonHoverTextureFile; ; } }
        /// <summary>
        /// Gets the Button's Pressed Texture File Path and Name.
        /// </summary>
        public static string ButtonPressedTexture
        { get { return ButtonPressedTextureFile; } }
        /// <summary>
        /// Read only property of the boss art folder
        /// </summary>
        public static string BossArtPath
        { get { return FinalBossFolder; } }
        /// <summary>
        /// Property of the splash art folder
        /// </summary>
        public static string SplashArtPath
        { get { return SplashScreenFolder; } }
        /// <summary>
        /// Gets the license screen art folder
        /// </summary>
        public static string LicenseFolderPath
        { get { return LicenseScreenFolder; } }
        /// <summary>
        /// Property of the help menu folder
        /// </summary>
        public static string HelpScreenPath
        { get { return HelpScreenFolder; } }
        /// <summary>
        /// Gets the MainMenu Folder path directory
        /// </summary>
        public static string MainMenuPath
        { get { return MainMenuFolder; } }
        /// <summary>
        /// Gets the Level's Folder path directory
        /// </summary>
        public static string LevelPath
        { get { return LevelFolder; } }
        /// <summary>
        /// Gets the Options Menu Folder path directory
        /// </summary>
        public static string OptionMenuPath
        { get { return OptionsMenuFolder; } }
        /// <summary>
        /// Gets the Outer Map folder path directory
        /// </summary>
        public static string OuterMapPath
        { get { return OuterMapFolder; } }
        /// <summary>
        /// Gets the Neutron Gems Folder Path directory
        /// </summary>
        public static string NeutronGemsPath
        { get { return NeutronGemsFolder; } }
        /// <summary>
        /// Gets the Directory leading to the Art Workshop Folder.
        /// </summary>
        public static string WorkshopPath
        { get { return WorkshopFolder; } }
        /// <summary>
        /// Property of the button folder
        /// </summary>
        public static string ButtonPath
        { get { return ButtonFolder; } }
        /// <summary>
        /// Gets te Player's ship folder path
        /// </summary>
        public static string PlayerShipPath
        { get { return PlayerFolder; } }
        /// <summary>
        /// Property of the boss folder
        /// </summary>
        public static string FinalBossPath
        { get { return FinalBossFolder; } }
        /// <summary>
        /// Gets the PirateShip's Folder Path
        /// </summary>
        public static string PirateShipPath
        { get { return PirateShipFolder; } }
        /// <summary>
        /// Gets the Asteroid Brown Texture File Path and Name
        /// </summary>
        public static string AsteroidBrownTexture
        { get { return AsteroidsBrownTextureFile; } }
        /// <summary>
        /// Gets the Asteroid 1st Gray Texture File Path and Name
        /// </summary>
        public static string Asteroid1stGrayTextureFile
        { get { return AsteroidsGrey1TextureFile; } }
        /// <summary>
        /// Gets the Asteroid 2nd Grey Texture File Path and Name
        /// </summary>
        public static string Asteroid2ndGreyTextureFile
        { get { return AsteroidsGrey2TextureFile; } }
        /// <summary>
        /// Gets the screens sound File Path and Name
        /// </summary>
        public static string ScreensSound
        { get { return ScreensSoundFolder; } }
        /// <summary>
        /// Gets the asteroid sound File Path and Name
        /// </summary>
        public static string AsteroidSound
        { get { return AsteroidsSoundFolder; } }
        /// <summary>
        /// Gets the bullet sound File Path and Name
        /// </summary>
        public static string BulletSound
        { get { return BulletSoundFolder; } }
        /// <summary>
        /// Gets the bullet sound File Path and Name
        /// </summary>
        public static string BackgroundSong
        { get { return BackgroundSongFolder; } }
        /// <summary>
        /// Gets the player sound File Path and Name
        /// </summary>
        public static string PlayerSound
        { get { return PlayerSoundFolder; } }
        /// <summary>
        /// Gets the button sound file path and name
        /// </summary>
        public static string ButtonSound
        { get { return ButtonSoundFolder; } }

    }
}
