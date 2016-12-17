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
 *  -   Represents the Asteroid in game
 */

namespace Asteroids
{
    class Workshop
    {
        #region VARIABLES & PROPERTIES
        /* This region will contain,
         * all variables and,
         * their respective properties,
         * for this class
         */

        // Background texture
        private Texture2D
            moBackground,
            moShips,
            moShipStatBlock,
            moShipPartSelect,
            moShipStatLine;
        private SpriteFont
            moBtnFont;
        private Rectangle
            moShipStatsRect,
            moShipPartsSelected;
        private Vector2
            moStatLineStart,
            moStatLineEnd;
        private Button
            moBtnUpgrade,
            moBtnBack,
            moBtnUp,
            moBtnDown,
            moBtnSell;
        private int
            mdUpgradeType,
            mdUpgradeTier;
        private bool
            mdBtnPressed;
        private string
            mdStatText;
        private int[] mdUpgradeCost =
            new int[UpgradeTiers] { 700, 800, 1000 },
            mdUpgradeShipCost =
            new int[UpgradeTiers] { 1000, 4000, 10000 };
        private float[, ,] mdUpgrades =
            new float[ShipTypes, UpgradeTypes, UpgradeTiers]
            {
                { // Small Ship
                    { 10, 20, 30 }, // MaxHealth
                    { 2, 3, 4 }, // Shield
                    { 5, 10, 15 }, // HoldCapacity
                    { 0.1f, 0.2f, 0.4f }, // Speed
                    { 100, 90, 80 }, // Laser Charge Time
                    { 1, 2, 3 } // Laser Power
                },
                { // Medium Ship
                    { 25, 50, 70 }, // MaxHealth
                    { 3, 6, 9 }, // Shield
                    { 20, 30, 40}, // HoldCapacity
                    { 0.05f, 0.1f, 0.15f }, // Speed
                    { 85, 65, 50 }, // Laser Charge Time
                    { 4, 8, 12 } // Laser Power
                },
                { // Large Ship
                    { 60, 80, 100 }, // MaxHealth
                    { 7, 10, 12 }, // Shield
                    { 25, 50, 70 }, // HoldCapacity
                    { 0.01f, 0.05f, 0.1f }, // Speed
                    { 60, 30, 15 }, // Laser Charge Time
                    { 5, 10, 15 } // Laser Power
                }
            };
        /* Slc == Selection */
        // MaxHealth Selection
        private int[] mdSlcHealthX = { 130, 144, 140 };
        private int[] mdSlcHealthY = { 165, 130, 210 };
        private int[] mdSlcHealthWidth = { 80, 55, 60 };
        private int[] mdSlcHealthHeight = { 110, 90, 80 };
        private Rectangle[] moSlcHealthRect;
        // Shield Selection
        private int[] mdSlcShieldX = { 95, 150, 235 };
        private int[] mdSlcShieldY = { 197, 90, 215 };
        private int[] mdSlcShieldWidth = { 50, 40, 70 };
        private int[] mdSlcShieldHeight = { 50, 50, 60 };
        private Rectangle[] moSlcShieldRect;
        // HoldCapacity Selection
        private int[] mdSlcHoldX = { 60, 148, 240 };
        private int[] mdSlcHoldY = { 190, 210, 270 };
        private int[] mdSlcHoldWidth = { 35, 50, 65 };
        private int[] mdSlcHoldHeight = { 50, 100, 40 };
        private Rectangle[] moSlcHoldRect;
        // Speed Selection
        private int[] mdSlcSpeedX = { 145, 190, 122 };
        private int[] mdSlcSpeedY = { 290, 340, 310 };
        private int[] SlcSpeedWidth = { 50, 50, 90 };
        private int[] SlcSpeedHeight = { 30, 30, 40 };
        private Rectangle[] moSlcSpeedRect;
        // Laser Charge Time Selection
        private int[] mdSlcLaserChargeX = { 145, 231, 143 };
        private int[] mdSlcLaserChargeY = { 105, 288, 140 };
        private int[] mdSlcLaserChargeWidth = { 50, 50, 50 };
        private int[] mdSlcLaserChargeHeight = { 30, 52, 60 };
        private Rectangle[] moSlcLaserChargeRect;
        // Laser Power Selection
        private int[] mdSlcLaserPowerX = { 65, 150, 145 };
        private int[] mdSlcLaserPowerY = { 80, 40, 50 };
        private int[] mdSlcLaserPowerWidth = { 30, 40, 45 };
        private int[] mdSlcLaserPowerHeight = { 40, 40, 50 };
        private Rectangle[] moSlcLaserPowerRect;
        // Ship Tier Selection
        private readonly Rectangle SlcShipRect = new Rectangle(
            ShipX, ShipY, ShipWidth, ShipHeight
            );

        /**** End of VARIABLES & PROPERTIES region ****/
        #endregion

        #region CONSTANT DECLARATIONS
        /* This region will contain,
         * all constants for this class
         */

        // A constant no filter colour
        private readonly Color NoFilter = Color.White;
        /* File Paths */
        private const string BackgroundTextureFile = "Workshop Background";
        private const string SpaceshipTextureFile = "Spaceships";
        private const string ButtonFontFile = "WorkshopBtnFont";
        private const string ShipStatBlock = "ShipStatsBlock";
        private const string ShipPartSelected = "ShipSelected";
        private const string ShipStatLine = "StatLine";
        // Upgrade constants
        private const int ShipTypes = 3;
        private const int UpgradeTypes = 6;
        private const int UpgradeTiers = 3;
        private const int GemRedPrice = 20;
        private const int GemYellowPrice = 50;
        private const int GemLightBluePrice = 70;
        private const int GemBluePrice = 80;
        private const int GemGreenPrice = 140;
        private const int GemPurplePrice = 200;
        private const int GemBlackPrice = 500;
        // Ship Frame Constants
        private const int LargeFrameWidth = 447;
        private const int LargeFrameHeight = 480;
        private const int MediumFrameWidth = 385;
        private const int MediumFrameHeight = 448;
        private const int SmallFrameWidth = 375;
        private const int SmallFrameHeight = 452;
        private readonly Rectangle[] moSourceRectangle =
        {
            new Rectangle(
                0,LargeFrameHeight+MediumFrameHeight,   // Position
                SmallFrameWidth, SmallFrameHeight),     // Dimensions
            new Rectangle(
                0,LargeFrameHeight,                     // Position
                MediumFrameWidth, MediumFrameHeight),   // Dimensions
            new Rectangle(
                0,0,                                    // Position
                LargeFrameWidth, LargeFrameWidth)       // Dimensions
        };
        // Ship Drawing Constants
        private const int ShipX = 20;
        private const int ShipY = 50;
        private const int ShipWidth = 300;
        private const int ShipHeight = 400;
        private readonly Rectangle ShipRectangle =
            new Rectangle(ShipX, ShipY, ShipWidth, ShipHeight);
        /* Ship Stat Block Constants */
        // Stat Block constant
        private const int StatBlockX = 550;
        private const int StatBlockY = 0;
        private const int StatBlockWidth = 250;
        private const int StatBlockHeight = 600;
        // Stat Line
        private const int StatLineThickness = 5;
        /* Stats Constants */
        // Stat Constants
        private const int StatX = StatBlockX + 20;
        // MaxHealth Stat Constants
        private const string StatHealthText = "Health:";
        private const int StatHealthY = StatBlockY + 70;
        private readonly Vector2 StatHealthPosition = new Vector2(StatX, StatHealthY);
        // Shield Stat Constants
        private const string StatShieldText = "Shield:";
        // HoldCapacity Stat Constants
        private const string StatHoldText = "Hold:";
        // Speed Stat Constants
        private const string StatSpeedText = "Speed:";
        // Laser Charge Rate Stat Constants
        private const string StatLaserChargeText = "Laser Charge Rate:";
        // Laser Power Stat Constants
        private const string StatLaserPowerText = "Laser Power:";
        // Currency Stat constant
        private const string StatCurrencyText = "Niels:";
        /* Button Constants */
        // All Buttons
        private const int BtnWidth = 200;
        private const int BtnHeight = 100;
        private const int BtnX = 350;
        private readonly Vector2 BtnDimensions = new Vector2(BtnWidth, BtnHeight);
        private readonly Color BtnTextColour = new Color(255, 255, 255, 255);
        // Back Button
        private const int BtnBackX = 0;
        private const int BtnBackY = 500;
        private readonly Vector2 BtnBackPosition = new Vector2(BtnBackX, BtnBackY);
        private const string BtnBackText = "Back";
        // Select Button
        private const int BtnSelectY = 250;
        private readonly Vector2 BtnSelectPosition = new Vector2(BtnX, BtnSelectY);
        private const string BtnSelectText = "Select";
        // Up Button
        private const int BtnUpY = 150;
        private readonly Vector2 BtnUpPosition = new Vector2(BtnX, BtnUpY);
        private const string BtnUpText = "Up";
        // Down Button
        private const int BtnDownY = 350;
        private readonly Vector2 BtnDownPosition = new Vector2(BtnX, BtnDownY);
        private const string BtnDownText = "Down";
        // Sell Button
        private const int BtnSellX = 200;
        private const int BtnSellY = 500;
        private readonly Vector2 BtnSellPosition = new Vector2(BtnSellX, BtnSellY);
        private const string BtnSellText = "Sell Gems";
        /* Selection Block Constants */
        private const string SlcText = "\nSelected:\n";
        // Selection Type Constants
        private const int HealthType = 0;
        private const int ShieldType = HealthType + 1;
        private const int HoldType = ShieldType + 1;
        private const int SpeedType = HoldType + 1;
        private const int LaserChargeType = SpeedType + 1;
        private const int LaserPowerType = LaserChargeType + 1;
        private const int ShipType = LaserPowerType + 1;
        // Selection Tier Constants
        private const int TierTwo = 1;
        private const int TierThree = TierTwo + 1;
        
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
        public Workshop()
        {
            mdUpgradeType = 0;
            mdUpgradeTier = 1;
            mdBtnPressed = false;
            moShipStatsRect = new Rectangle(
                StatBlockX,
                StatBlockY,
                StatBlockWidth,
                StatBlockHeight
                );
            moStatLineStart = new Vector2(0f);
            moStatLineEnd = new Vector2(0f);
            mdStatText = "";
            /* Selection Rectangles initialization */
            // MaxHealth Selection Rectangle
            moSlcHealthRect = new Rectangle[]
            {
                new Rectangle(
                    mdSlcHealthX[0], mdSlcHealthY[0], mdSlcHealthWidth[0], mdSlcHealthHeight[0]
                    ),
                new Rectangle(
                    mdSlcHealthX[1], mdSlcHealthY[1], mdSlcHealthWidth[1], mdSlcHealthHeight[1]
                    ),
                new Rectangle(
                    mdSlcHealthX[2], mdSlcHealthY[2], mdSlcHealthWidth[2], mdSlcHealthHeight[2]
                    )
            };
            // Shield Selection Rectangle
            moSlcShieldRect = new Rectangle[]
            {
                new Rectangle(
                    mdSlcShieldX[0], mdSlcShieldY[0], mdSlcShieldWidth[0], mdSlcShieldHeight[0]
                    ),
                new Rectangle(
                    mdSlcShieldX[1], mdSlcShieldY[1], mdSlcShieldWidth[1], mdSlcShieldHeight[1]
                    ),
                new Rectangle(
                    mdSlcShieldX[2], mdSlcShieldY[2], mdSlcShieldWidth[2], mdSlcShieldHeight[2]
                    )
            };
            // Hold Selection Rectangle
            moSlcHoldRect = new Rectangle[]
            {
                new Rectangle(
                mdSlcHoldX[0], mdSlcHoldY[0], mdSlcHoldWidth[0], mdSlcHoldHeight[0]
                ),
                new Rectangle(
                mdSlcHoldX[1], mdSlcHoldY[1], mdSlcHoldWidth[1], mdSlcHoldHeight[1]
                ),
                new Rectangle(
                mdSlcHoldX[2], mdSlcHoldY[2], mdSlcHoldWidth[2], mdSlcHoldHeight[2]
                )
            };
            // Speed Selection Rectangle
            moSlcSpeedRect = new Rectangle[]
            {
                new Rectangle(
                mdSlcSpeedX[0], mdSlcSpeedY[0], SlcSpeedWidth[0], SlcSpeedHeight[0]
                ),
                new Rectangle(
                mdSlcSpeedX[1], mdSlcSpeedY[1], SlcSpeedWidth[1], SlcSpeedHeight[1]
                ),
                new Rectangle(
                mdSlcSpeedX[2], mdSlcSpeedY[2], SlcSpeedWidth[2], SlcSpeedHeight[2]
                )
            };
            // Laser Charge Selection Rectangle
            moSlcLaserChargeRect = new Rectangle[]
            {
                new Rectangle(
                    mdSlcLaserChargeX[0], mdSlcLaserChargeY[0], mdSlcLaserChargeWidth[0], mdSlcLaserChargeHeight[0]
                    ),
                new Rectangle(
                    mdSlcLaserChargeX[1], mdSlcLaserChargeY[1], mdSlcLaserChargeWidth[1], mdSlcLaserChargeHeight[1]
                    ),
                new Rectangle(
                    mdSlcLaserChargeX[2], mdSlcLaserChargeY[2], mdSlcLaserChargeWidth[2], mdSlcLaserChargeHeight[2]
                    )
            };
            // Laser Power Selection Rectangle
            moSlcLaserPowerRect = new Rectangle[]
            {
                new Rectangle(
                    mdSlcLaserPowerX[0], mdSlcLaserPowerY[0], mdSlcLaserPowerWidth[0], mdSlcLaserPowerHeight[0]
                    ),
                new Rectangle(
                    mdSlcLaserPowerX[1], mdSlcLaserPowerY[1], mdSlcLaserPowerWidth[1], mdSlcLaserPowerHeight[1]
                    ),
                new Rectangle(
                    mdSlcLaserPowerX[2], mdSlcLaserPowerY[2], mdSlcLaserPowerWidth[2], mdSlcLaserPowerHeight[2]
                    )
            };
            moShipPartsSelected = moSlcHealthRect[0];
        }

        /**** End of CONSTRUCTORS region ****/
        #endregion

        #region PUBLIC METHODS
        /* This region will contain,
         * all public methods for this class
         */

        /// <summary>
        /// Will load all external content.
        /// </summary>
        /// <param name="aoAssetsManager"></param>
        public void LoadContent(ContentManager aoAssetsManager)
        {
            moBackground = aoAssetsManager.Load<Texture2D>(FilePaths.WorkshopPath + BackgroundTextureFile);
            moShips = aoAssetsManager.Load<Texture2D>(FilePaths.WorkshopPath + SpaceshipTextureFile);
            moShipStatBlock = aoAssetsManager.Load<Texture2D>(FilePaths.WorkshopPath + ShipStatBlock);
            moShipStatLine = aoAssetsManager.Load<Texture2D>(FilePaths.WorkshopPath + ShipStatLine);
            moShipPartSelect = aoAssetsManager.Load<Texture2D>(FilePaths.WorkshopPath + ShipPartSelected);
            moBtnFont = aoAssetsManager.Load<SpriteFont>(FilePaths.WorkshopPath + ButtonFontFile);

            /* Initializing all externally dependant classes */
            moBtnBack = new Button(
                BtnBackPosition,
                BtnDimensions,
                moBtnFont,
                BtnTextColour,
                BtnBackText
                );
            moBtnUpgrade = new Button(
                BtnSelectPosition,
                BtnDimensions,
                moBtnFont,
                BtnTextColour,
                BtnSelectText
                );
            moBtnUp = new Button(
                BtnUpPosition,
                BtnDimensions,
                moBtnFont,
                BtnTextColour,
                BtnUpText
                );
            moBtnDown = new Button(
                BtnDownPosition,
                BtnDimensions,
                moBtnFont,
                BtnTextColour,
                BtnDownText
                );
            moBtnSell = new Button(
                BtnSellPosition,
                BtnDimensions,
                moBtnFont,
                BtnTextColour,
                BtnSellText
                );
            moStatLineStart = new Vector2(
                moBtnUpgrade.Bounds.Left,
                moBtnUpgrade.Bounds.Center.Y
                );

            moBtnBack.LoadContent(aoAssetsManager);
            moBtnUpgrade.LoadContent(aoAssetsManager);
            moBtnUp.LoadContent(aoAssetsManager);
            moBtnDown.LoadContent(aoAssetsManager);
            moBtnSell.LoadContent(aoAssetsManager);
        }
        /// <summary>
        /// Will handle the Screen's Update Logic
        /// </summary>
        public void Update(ref Ship aoPlayerShip, ref Game.GameState aeGameState)
        {
            moBtnBack.Update();
            moBtnUpgrade.Update();
            moBtnUp.Update();
            moBtnDown.Update();
            moBtnSell.Update();
            UpdateStats(ref aoPlayerShip);

            if (mdBtnPressed == false)
            {
                RefreshMouse();
                ButtonCollisions(moBtnUpgrade);
                ButtonCollisions(moBtnUp);
                ButtonCollisions(moBtnDown);
                ButtonCollisions(moBtnBack);
                ButtonCollisions(moBtnSell);
            }
            else
            {
                if (moBtnUpgrade.IsPressed() == true)
                {
                    if (mdUpgradeType == ShipType)
                    {
                        if (aoPlayerShip.Type < 2)
                        {
                            if (aoPlayerShip.Niels - mdUpgradeShipCost[aoPlayerShip.Type + 1] >= 0)
                            {
                                PurchaseUpgrade(ref aoPlayerShip);
                            }
                        }
                    }
                    else
                    {
                        if (aoPlayerShip.Tiers[mdUpgradeType] < 3)
                        {
                            if (aoPlayerShip.Niels - mdUpgradeCost[aoPlayerShip.Tiers[mdUpgradeType]] >= 0)
                            {
                                PurchaseUpgrade(ref aoPlayerShip);
                            }
                        }
                    }
                    ButtonPressed(moBtnUpgrade);
                }
                else if (moBtnUp.IsPressed() == true)
                {
                    if (mdUpgradeType < UpgradeTypes)
                    {
                        mdUpgradeType++;
                    }
                    else
                    {
                        mdUpgradeType = 0;
                    }
                    ButtonPressed(moBtnUp);
                }
                else if (moBtnDown.IsPressed() == true)
                {
                    if (mdUpgradeType - 1 >= 0)
                    {
                        mdUpgradeType--;
                    }
                    else
                    {
                        mdUpgradeType = UpgradeTypes;
                    }
                    ButtonPressed(moBtnDown);
                }
                else if (moBtnSell.IsPressed() == true)
                {
                    SoldGems(ref aoPlayerShip);
                    ButtonPressed(moBtnSell);
                }
                else if (moBtnBack.IsPressed() == true)
                {
                    aeGameState = Game.GameState.MainMenu;
                    ButtonPressed(moBtnBack);
                }
            }
            switch (mdUpgradeType)
            {
                case ShipType:
                    moShipPartsSelected = SlcShipRect;
                    break;
                case LaserPowerType:
                    moShipPartsSelected = moSlcLaserPowerRect[aoPlayerShip.Type];
                    break;
                case LaserChargeType:
                    moShipPartsSelected = moSlcLaserChargeRect[aoPlayerShip.Type];
                    break;
                case SpeedType:
                    moShipPartsSelected = moSlcSpeedRect[aoPlayerShip.Type];
                    break;
                case HoldType:
                    moShipPartsSelected = moSlcHoldRect[aoPlayerShip.Type];
                    break;
                case ShieldType:
                    moShipPartsSelected = moSlcShieldRect[aoPlayerShip.Type];
                    break;
                case HealthType:
                    moShipPartsSelected = moSlcHealthRect[aoPlayerShip.Type];
                    break;
                default:
                    break;
            }
            moStatLineEnd.X = moShipPartsSelected.Right;
            moStatLineEnd.Y = moShipPartsSelected.Center.Y;
        }
        /// <summary>
        /// Will draw Screen components
        /// </summary>
        /// <param name="aoSpriteBatch"></param>
        public void Draw(SpriteBatch aoSpriteBatch, Rectangle aoMainFrame, ref Ship aoPlayerShip)
        {
            aoSpriteBatch.Draw(moBackground, aoMainFrame, NoFilter);
            aoSpriteBatch.Draw(moShips, ShipRectangle, moSourceRectangle[aoPlayerShip.Type], NoFilter);
            moBtnBack.Draw(aoSpriteBatch);
            moBtnUpgrade.Draw(aoSpriteBatch);
            moBtnUp.Draw(aoSpriteBatch);
            moBtnDown.Draw(aoSpriteBatch);
            moBtnSell.Draw(aoSpriteBatch);
            DrawStatBlock(aoSpriteBatch);
            aoSpriteBatch.Draw(moShipPartSelect, moShipPartsSelected, NoFilter);
            DrawLine(aoSpriteBatch, moStatLineStart, moStatLineEnd);
        }

        /**** End of PUBLIC METHODS region ****/
        #endregion

        #region PRIVATE METHODS
        /* This region will contain,
         * all private methods for this class
         */

        /// <summary>
        /// will update the current mouse state
        /// </summary>
        private void RefreshMouse()
        {
            Input.UpdateMouse();
        }
        /// <summary>
        /// Will update the stats based on curret ship's stats.
        /// </summary>
        /// <param name="aoPirateShip"></param>
        private void UpdateStats(ref Ship aoPlayerShip)
        {
            mdStatText =
                StatHealthText + "\n" + aoPlayerShip.MaxHealth.ToString("n0") + "\n" +
                StatShieldText + "\n" + aoPlayerShip.Shield.ToString("n0") + "\n" +
                StatHoldText + "\n" + aoPlayerShip.HoldCapacity.ToString("n0") + "\n" +
                StatSpeedText + "\n" + (aoPlayerShip.Speed * 100).ToString("n0") + "\n" +
                StatLaserChargeText + "\n" + aoPlayerShip.LaserCharge.ToString("n0") + "\n" +
                StatLaserPowerText + "\n" + aoPlayerShip.LaserDamage.ToString("n0") + "\n" +
                StatCurrencyText + "\n" + aoPlayerShip.Niels.ToString("n0") +
                SlcText;
            switch (mdUpgradeType)
            {
                case ShipType:
                    mdStatText += " Ship Upgrade to";
                    mdUpgradeTier = aoPlayerShip.Type + 1;
                    break;
                case LaserPowerType:
                    mdStatText += " Laser Power\n Upgrade";
                    mdUpgradeTier = aoPlayerShip.Tiers[mdUpgradeType];
                    break;
                case LaserChargeType:
                    mdStatText += " Laser Charge\n Upgrade";
                    mdUpgradeTier = aoPlayerShip.Tiers[mdUpgradeType];
                    break;
                case SpeedType:
                    mdStatText += " Speed Upgrade";
                    mdUpgradeTier = aoPlayerShip.Tiers[mdUpgradeType];
                    break;
                case HoldType:
                    mdStatText += " Hold Upgrade";
                    mdUpgradeTier = aoPlayerShip.Tiers[mdUpgradeType];
                    break;
                case ShieldType:
                    mdStatText += " Shield Upgrade";
                    mdUpgradeTier = aoPlayerShip.Tiers[mdUpgradeType];
                    break;
                case HealthType:
                    mdStatText += " Health Upgrade";
                    mdUpgradeTier = aoPlayerShip.Tiers[mdUpgradeType];
                    break;
                default:
                    break;
            }
            switch (mdUpgradeTier)
            {
                case TierThree:
                    mdStatText += "\n   Tier 3\n";
                    if (mdUpgradeType == ShipType)
                    {
                        mdStatText += "    " + mdUpgradeShipCost[aoPlayerShip.Type + 1].ToString("n0") + " Niels";
                    }
                    else
                    {
                        mdStatText += "    " + mdUpgradeCost[mdUpgradeTier].ToString("n0") + " Niels";
                    }
                    break;
                case TierTwo:
                    mdStatText += "\n   Tier 2\n";
                    if (mdUpgradeType == ShipType)
                    {
                        mdStatText += "    " + mdUpgradeShipCost[aoPlayerShip.Type + 1].ToString("n0") + " Niels";
                    }
                    else
                    {
                        mdStatText += "    " + mdUpgradeCost[mdUpgradeTier].ToString("n0") + " Niels";
                    }
                    break;
                default:
                    mdStatText += "\n   No Upgrade\n    Available";
                    break;
            }
        }
        /// <summary>
        /// Will upgrade the corresponding component of the ship.
        /// </summary>
        /// <param name="aoPirateShip">
        /// Passed Ship
        /// referenced player ship that will be upgraded.
        /// </param>
        private void PurchaseUpgrade(ref Ship aoPlayerShip)
        {
            switch (mdUpgradeType)
            {
                case ShipType:
                    aoPlayerShip.UpgradedShip(aoPlayerShip.Type + 1, mdUpgrades);
                    aoPlayerShip.Niels -= mdUpgradeShipCost[aoPlayerShip.Type];
                    break;
                case LaserPowerType:
                    aoPlayerShip.UpgradedLaserPower((int)mdUpgrades[aoPlayerShip.Type, mdUpgradeType, aoPlayerShip.Tiers[mdUpgradeType]]);
                    aoPlayerShip.Niels -= mdUpgradeCost[aoPlayerShip.Tiers[mdUpgradeType]];
                    aoPlayerShip.Tiers[mdUpgradeType]++;
                    break;
                case LaserChargeType:
                    aoPlayerShip.UpgradedLaserCharge((int)mdUpgrades[aoPlayerShip.Type, mdUpgradeType, aoPlayerShip.Tiers[mdUpgradeType]]);
                    aoPlayerShip.Niels -= mdUpgradeCost[aoPlayerShip.Tiers[mdUpgradeType]];
                    aoPlayerShip.Tiers[mdUpgradeType]++;
                    break;
                case SpeedType:
                    aoPlayerShip.UpgradedSpeed(mdUpgrades[aoPlayerShip.Type, mdUpgradeType, aoPlayerShip.Tiers[mdUpgradeType]]);
                    aoPlayerShip.Niels -= mdUpgradeCost[aoPlayerShip.Tiers[mdUpgradeType]];
                    aoPlayerShip.Tiers[mdUpgradeType]++;
                    break;
                case HoldType:
                    aoPlayerShip.UpgradedHold((int)mdUpgrades[aoPlayerShip.Type, mdUpgradeType, aoPlayerShip.Tiers[mdUpgradeType]]);
                    aoPlayerShip.Niels -= mdUpgradeCost[aoPlayerShip.Tiers[mdUpgradeType]];
                    aoPlayerShip.Tiers[mdUpgradeType]++;
                    break;
                case ShieldType:
                    aoPlayerShip.UpgradedShield((int)mdUpgrades[aoPlayerShip.Type, mdUpgradeType, aoPlayerShip.Tiers[mdUpgradeType]]);
                    aoPlayerShip.Niels -= mdUpgradeCost[aoPlayerShip.Tiers[mdUpgradeType]];
                    aoPlayerShip.Tiers[mdUpgradeType]++;
                    break;
                case HealthType:
                    aoPlayerShip.UpgradedHealth((int)mdUpgrades[aoPlayerShip.Type, mdUpgradeType, aoPlayerShip.Tiers[mdUpgradeType]]);
                    aoPlayerShip.Niels -= mdUpgradeCost[aoPlayerShip.Tiers[mdUpgradeType]];
                    aoPlayerShip.Tiers[mdUpgradeType]++;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Will sell all the gems in the players ship.
        /// </summary>
        /// <param name="aoPirateShip"></param>
        private void SoldGems(ref Ship aoPlayerShip)
        {
            for (int i = 0; i < aoPlayerShip.Hold.Length; i++)
            {
                if (aoPlayerShip.Hold[i] != null)
                {
                    aoPlayerShip.Niels += SellGem(ref aoPlayerShip.Hold[i]);
                }
            }
        }
        /// <summary>
        /// Will sell the passed gem,
        /// and return its price
        /// </summary>
        /// <param name="aoGem"></param>
        /// <returns></returns>
        private int SellGem(ref NeutronGem aoGem)
        {
            int adMoney = 0;
            switch (aoGem.Colour)
            {
                case NeutronGem.GemColor.Black:
                    adMoney += GemBlackPrice;
                    break;
                case NeutronGem.GemColor.Blue:
                    adMoney += GemBluePrice;
                    break;
                case NeutronGem.GemColor.Green:
                    adMoney += GemGreenPrice;
                    break;
                case NeutronGem.GemColor.LightBlue:
                    adMoney += GemLightBluePrice;
                    break;
                case NeutronGem.GemColor.Purple:
                    adMoney += GemPurplePrice;
                    break;
                case NeutronGem.GemColor.Red:
                    adMoney += GemRedPrice;
                    break;
                case NeutronGem.GemColor.Yellow:
                    adMoney += GemYellowPrice;
                    break;
                default:
                    break;
            }
            aoGem = null;
            return adMoney;
        }
        /// <summary>
        /// Event for the button actually being hovered over
        /// </summary>
        /// <param name="aoBtn"></param>
        private void ButtonCollisions(Button aoBtn)
        {
            if (RectangleCollision(Input.MouseState.X, Input.MouseState.Y, aoBtn.Bounds) == true)
            {
                if (Input.MouseState.LeftButton == ButtonState.Pressed &&
                    Input.MouseOldState.LeftButton == ButtonState.Released)
                {
                    aoBtn.Pressed();
                    mdBtnPressed = true;
                }
                else
                {
                    aoBtn.Hovered();
                }
            }
            else
            {
                aoBtn.SetActive();
            }
        }
        /// <summary>
        /// Rectangular collision checking if mouse inside a rectangle
        /// </summary>
        /// <param name="aoXCoord"></param>
        /// <param name="aoYCoord"></param>
        /// <param name="aoRectangle"></param>
        /// <returns></returns>
        private bool RectangleCollision(int aoXCoord, int aoYCoord, Rectangle aoRectangle)
        {
            bool adCollided = false;
            if (aoXCoord > aoRectangle.X &&
                aoXCoord < aoRectangle.X + aoRectangle.Width &&
                aoYCoord > aoRectangle.Y &&
                aoYCoord < aoRectangle.Y + aoRectangle.Height)
            {
                adCollided = true;
            }
            return adCollided;
        }
        /// <summary>
        /// Will Draw a line between the two Passed Vector2's
        /// </summary>
        /// <param name="aoSpriteBatch">
        /// Passed SpriteBatch
        /// required for all screen drawing.
        /// </param>
        /// <param name="aoStart">
        /// Passed Vector2
        /// will define the starting point of the line.
        /// </param>
        /// <param name="aoEnd">
        /// Passed Vector2
        /// will define the end point of the line.
        /// </param>
        private void DrawLine(SpriteBatch aoSpriteBatch, Vector2 aoStart, Vector2 aoEnd)
        {
            float adLineOffset = StatLineThickness / 2f;
            aoStart.Y -= adLineOffset;
            aoEnd.Y -= adLineOffset;
            Vector2 aoEdge = aoEnd - aoStart;
            float aoAngle =
                (float)Math.Atan2(aoEdge.Y, aoEdge.X);
            aoStart.Y += adLineOffset;
            aoEnd.Y += adLineOffset;
            Rectangle aoLine =
                new Rectangle(
                    (int)aoStart.X,
                    (int)(aoStart.Y),
                    (int)aoEdge.Length(),
                    StatLineThickness
                    );
            aoSpriteBatch.Draw(
                moShipStatLine,
                aoLine,
                null,
                NoFilter,
                aoAngle,
                Vector2.Zero,
                SpriteEffects.None,
                0);
        }
        /// <summary>
        /// Will draw all stat block elements using the Passed SpriteBatch
        /// </summary>
        /// <param name="aoSpriteBatch">
        /// Passed SpriteBatch
        /// will be used to draw stat blocks elements.
        /// </param>
        private void DrawStatBlock(SpriteBatch aoSpriteBatch)
        {
            aoSpriteBatch.Draw(moShipStatBlock, moShipStatsRect, NoFilter);
            aoSpriteBatch.DrawString(moBtnFont, mdStatText, StatHealthPosition, NoFilter);
        }
        /// <summary>
        /// Will run the computations,
        /// for the passed button.
        /// </summary>
        /// <param name="aoButton"></param>
        private void ButtonPressed(Button aoButton)
        {
            mdBtnPressed = false;
            aoButton.SetActive();
        }

        /**** End of PRIVATE METHODS region ****/
        #endregion

    }
} // End of namespace