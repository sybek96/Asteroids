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
 *  -   Rafael Girao
 */
/* Class Description:
 *  -   Will pertain a pressable rectangle.
 */

namespace Asteroids
{
    class Button
    {
        /**/
        /* 
         * VARIABLES & PROPERTIES
         */
        /**/

        private Texture2D
            moTextureActive,
            moTextureDeactive,
            moTextureHover,
            moTexturePress;
        private Rectangle moButtonRect;
        /// <summary>
        /// Gets the buttons bounding rectangle
        /// </summary>
        public Rectangle Bounds
        { get { return moButtonRect; } }
        /// <summary>
        /// Gets the button's moPosition Vector
        /// </summary>
        public Vector2 Position
        { get { return new Vector2(moButtonRect.Location.X, moButtonRect.Location.Y); } }
        private Vector2 moTextPosition;
        /// <summary>
        /// Gets the button's Text Position Vector
        /// </summary>
        public Vector2 TextPosition
        { get { return moTextPosition; } }
        private int mdCounter;
        private string mdText;
        /// <summary>
        /// Gets the button's text
        /// </summary>
        public string Text
        { get { return mdText; } }
        private SpriteFont moFont;
        /// <summary>
        /// Gets the button's SpriteFont
        /// </summary>
        public SpriteFont Font
        { get { return moFont; } }
        private Color moFontColour;
        /// <summary>
        /// Gets the button's Font colour.
        /// </summary>
        public Color FontColour
        { get { return moFontColour; } }
        public enum ButtonState
        { Active, Hover, Press, Pressed }
        private ButtonState meButtonState;
        /// <summary>
        /// Gets the buttons current state.
        /// </summary>
        public ButtonState State
        { get { return meButtonState; } }
        private SoundEffect clickSound;

        /**/
        /* 
         * CONSTANT DECLARATIONS
         */
        /**/

        // for the draw method to use no filter
        private readonly Color NoFilter = Color.White;
        private readonly Color DefaultColour = Color.White;
        // amount of time we see the button pressed
        private const int WaitTime = 30;
        // Default Dimensions
        private const int ConsWidth = 50;
        private const int ConsHeight = 50;

        /**/
        /* 
         * CONSTRUCTORS
         */
        /**/

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Button()
        {
            moButtonRect = new Rectangle(
                (int)Vector2.Zero.X, (int)Vector2.Zero.Y, ConsWidth, ConsHeight
                );
            mdCounter = 0;
            meButtonState = ButtonState.Active;
        }
        /// <summary>
        /// Constructor:
        /// Will create a button of default dimensions
        /// at the passed integers X coordinate and Y coordinate.
        /// </summary>
        /// <param name="ldX">
        /// Passed integer that will set the button's X coordinate.
        /// </param>
        /// <param name="ldY">
        /// Passed integer that will set the button's Y coordinate.
        /// </param>
        public Button(int ldX, int ldY)
        {
            moButtonRect = new Rectangle(
                ldX, ldY, ConsWidth, ConsHeight
                );
            mdCounter = 0;
            meButtonState = ButtonState.Active;
        }
        /// <summary>
        /// Constructor:
        /// Will create a button at the origin (0,0),
        /// with the passed integers as its dimensions.
        /// </summary>
        /// <param name="ldWidth">
        /// Passed integer that will set the button's width.
        /// </param>
        /// <param name="ldHeight">
        /// Passed integer that will set the button's height.
        /// </param>
        /// <summary>
        /// Constructor:
        /// Will create a button taking the passed integer's
        /// as it's x/y coordinate, width and height of the button.
        /// </summary>
        /// <param name="ldX">
        /// Passed integer that will set the button's X coordinate.
        /// </param>
        /// <param name="ldY">
        /// Passed integer that will set the button's Y coordinate.
        /// </param>
        /// <param name="ldWidth">
        /// Passed integer that will set the button's width.
        /// </param>
        /// <param name="ldHeight">
        /// Passed integer that will set the button's height.
        /// </param>
        public Button(int ldX, int ldY, int ldWidth, int ldHeight)
        {
            moButtonRect = new Rectangle(
                ldX, ldY, ldWidth, ldHeight
                );
            mdCounter = 0;
            meButtonState = ButtonState.Active;
        }
        /// <summary>
        /// Constructor:
        /// Will create a button of default dimensions
        /// at the passed Vector2's coordinates.
        /// </summary>
        /// <param name="loPosition">
        /// Passed Vector2 that will set the button's top-left coordinates.
        /// </param>
        public Button(Vector2 loPosition)
        {
            moButtonRect = new Rectangle(
                (int)loPosition.X, (int)loPosition.Y, ConsWidth, ConsHeight
                );
            mdCounter = 0;
            meButtonState = ButtonState.Active;
        }
        /// <summary>
        /// Constructor:
        /// Will create a button taking the passed Vector2's
        /// as it's x/y coordinate and the other vector as its width/height.
        /// </summary>
        /// <param name="loPosition">
        /// Passed Vector2 that will set the button's top-left coordinates.
        /// </param>
        /// <param name="loDimensions">
        /// Passed Vector2 that will set the button's width and height.
        /// </param>
        public Button(Vector2 loPosition, Vector2 loDimensions)
        {
            moButtonRect = new Rectangle(
                (int)loPosition.X, (int)loPosition.Y, (int)loDimensions.X, (int)loDimensions.Y
                );
            mdCounter = 0;
            meButtonState = ButtonState.Active;
        }
        /// <summary>
        /// Constructor:
        /// Will create a button taking the passed Vector2
        /// as it's x/y cooridnate,
        /// the SpriteFont will be used to
        /// </summary>
        /// <param name="loPosition">
        /// Passed Vector2 that will set the button's top-left coordinates.
        /// </param>
        /// <param name="loSpriteFont">
        /// Passed SpriteFont that will set what font will the button be drawn in.
        /// </param>
        /// <param name="loText">
        /// Passed String that will what text the button will have.
        /// </param>
        public Button(Vector2 loPosition, SpriteFont loSpriteFont, string loText)
        {
            Vector2 loDimensions = loSpriteFont.MeasureString(loText);
            moButtonRect = new Rectangle(
                (int)loPosition.X, (int)loPosition.Y, (int)loDimensions.X, (int)loDimensions.Y
                );
            moTextPosition = loPosition;
            mdText = loText;
            moFont = loSpriteFont;
            moFontColour = DefaultColour;
            mdCounter = 0;
            meButtonState = ButtonState.Active;
        }
        /// <summary>
        /// Constructor:
        /// Will create a button taking,
        /// the passed Vector2 as it's x/y coordinate,
        /// using the SpriteFont will be used to define the dimensions of the button.
        /// </summary>
        /// <param name="loPosition">
        /// Passed Vector2 that will set the button's top-left coordinates.
        /// </param>
        /// <param name="loSpriteFont">
        /// Passed SpriteFont that will set what font will the button be drawn in.
        /// </param>
        /// <param name="loFontColour">
        /// Passed Color that will set the Colour that the text will be written in.
        /// </param>
        /// <param name="loText">
        /// Passed String that will what text the button will have.
        /// </param>
        public Button(Vector2 loPosition, SpriteFont loSpriteFont, Color loFontColour, string loText)
        {
            Vector2 loDimensions = loSpriteFont.MeasureString(loText);
            moButtonRect = new Rectangle(
                (int)loPosition.X, (int)loPosition.Y, (int)loDimensions.X, (int)loDimensions.Y
                );
            moTextPosition = loPosition;
            mdText = loText;
            moFont = loSpriteFont;
            moFontColour = loFontColour;
            mdCounter = 0;
            meButtonState = ButtonState.Active;
        }
        /// <summary>
        /// Constructor:
        /// Will create a button taking,
        /// the passed Vector2 as it's x/y coordinate,
        /// using the SpriteFont to define the dimensions of the button.
        /// </summary>
        /// <param name="loPosition">
        /// Passed Vector2 that will set the button's top-left coordinates.
        /// </param>
        /// <param name="loSpriteFont">
        /// Passed SpriteFont that will set what font will the button be drawn in.
        /// </param>
        /// <param name="loFontColour">
        /// Passed Color that will set the Colour that the text will be written in.
        /// </param>
        /// <param name="loText">
        /// Passed String that will what text the button will have.
        /// </param>
        /// <param name="loTextOffset">
        /// Passed Vector2 that will offset the text and increase the button's dimensions in all directions.
        /// </param>
        public Button(Vector2 loPosition, SpriteFont loSpriteFont, Color loFontColour, string loText, Vector2 loTextOffset)
        {
            Vector2 loDimensions = loSpriteFont.MeasureString(loText);
            moButtonRect = new Rectangle(
                (int)loPosition.X, (int)loPosition.Y, (int)(loDimensions.X + (loTextOffset.X * 2)), (int)(loDimensions.Y + (loTextOffset.Y * 2))
                );
            moTextPosition = new Vector2(loPosition.X + loTextOffset.X, loPosition.Y + loTextOffset.Y);
            mdText = loText;
            moFont = loSpriteFont;
            moFontColour = loFontColour;
            mdCounter = 0;
            meButtonState = ButtonState.Active;
        }
        /// <summary>
        /// Constructor:
        /// Will create a button taking,
        /// the passed Vector2 as it's x/y coordinate,
        /// a passed Vector2 as it's width/height as the vector's components,
        /// with the passed string as the text inside the button,
        /// with its passed font and colour.
        /// </summary>
        /// <param name="loPosition">
        /// Passed Vector2 that will set the button's top-left coordinates.
        /// </param>
        /// <param name="loDimensions">
        /// Passed Vector2 that will set the button's width and height.
        /// </param>
        /// <param name="loSpriteFont">
        /// Passed SpriteFont that will set what font will the button be drawn in.
        /// </param>
        /// <param name="loFontColour">
        /// Passed Color that will set the Colour that the text will be written in.
        /// </param>
        /// <param name="loText">
        /// Passed String that will what text the button will have.
        /// </param>
        public Button(Vector2 loPosition, Vector2 loDimensions, SpriteFont loSpriteFont, Color loFontColour, string loText)
        {
            moButtonRect = new Rectangle(
                (int)loPosition.X, (int)loPosition.Y, (int)(loDimensions.X), (int)(loDimensions.Y)
                );
            Vector2 loTextDimensions = loSpriteFont.MeasureString(loText);
            moTextPosition = new Vector2(
                (moButtonRect.Center.X - (loTextDimensions.X / 2f)),
                (moButtonRect.Center.Y - (loTextDimensions.Y / 2f))
                );
            mdText = loText;
            moFont = loSpriteFont;
            moFontColour = loFontColour;
            mdCounter = 0;
            meButtonState = ButtonState.Active;
        }

        /**/
        /* 
         * PUBLIC METHODS
         */
        /**/

        /// <summary>
        /// Will load all external content for this button.
        /// </summary>
        /// <param name="loAssetsManager">
        /// Passed ContentManager that controls the assets from our content pipeline.
        /// </param>
        public void LoadContent(ContentManager loAssetsManager)
        {
            moTextureActive = loAssetsManager.Load<Texture2D>(FilePaths.ButtonActiveTexture);
            moTextureHover = loAssetsManager.Load<Texture2D>(FilePaths.ButtonHoverTexture);
            moTexturePress = loAssetsManager.Load<Texture2D>(FilePaths.ButtonPressedTexture);
            clickSound = loAssetsManager.Load<SoundEffect>(FilePaths.ButtonSound + "Button_Click");
        }
        /// <summary>
        /// Will handle all button update logic
        /// </summary>
        public void Update()
        {
            switch (meButtonState)
            {
                case ButtonState.Pressed:
                    break;
                case ButtonState.Press:
                    if (mdCounter > WaitTime)
                    {
                        meButtonState = ButtonState.Pressed;
                    }
                    else
                    {
                        mdCounter++;
                    }
                    break;
                case ButtonState.Hover:
                    break;
                case ButtonState.Active:
                default:
                    break;
            }
        }
        /// <summary>
        /// Has the button Draw itself using the passed SpriteBatch
        /// </summary>
        /// <param name="aoSpriteBatch"></param>
        public void Draw(SpriteBatch aoSpriteBatch)
        {
            switch (meButtonState)
            {
                case ButtonState.Pressed:
                case ButtonState.Press:
                    aoSpriteBatch.Draw(moTexturePress, moButtonRect, NoFilter);
                    goto default;
                case ButtonState.Hover:
                    aoSpriteBatch.Draw(moTextureHover, moButtonRect, NoFilter);
                    goto default;
                case ButtonState.Active:
                    aoSpriteBatch.Draw(moTextureActive, moButtonRect, NoFilter);
                    goto default;
                default:
                    if (mdText != null || mdText != "")
                    {
                        aoSpriteBatch.DrawString(moFont, mdText, moTextPosition, moFontColour);
                    }
                    break;
            }
        }
        /// <summary>
        /// Will set the button to its hover state.
        /// </summary>
        public void Hovered()
        {
            meButtonState = ButtonState.Hover;
        }
        /// <summary>
        /// Will check if button is in its hovered state.
        /// </summary>
        /// <returns>
        /// Returns true if the button is in its Hovered State.
        /// </returns>
        public bool IsHovered()
        {
            bool ldHovered = false;
            if (meButtonState == ButtonState.Hover)
            {
                ldHovered = true;
            }
            return ldHovered;
        }
        /// <summary>
        /// Will set the button to its press state.
        /// </summary>
        public void Pressed()
        {
            clickSound.Play();
            meButtonState = ButtonState.Press;
            mdCounter = 0;
        }
        /// <summary>
        /// Will check if button is in its pressed state.
        /// </summary>
        /// <returns>
        /// Returns true if the button is in its Pressed State.
        /// </returns>
        public bool IsPressed()
        {
            bool ldPressed = false;
            if (meButtonState == ButtonState.Pressed)
            {
                ldPressed = true;
            }
            return ldPressed;
        }
        /// <summary>
        /// Will check if button is in its being pressed state.
        /// </summary>
        /// <returns>
        /// Returns true if the button is in its being pressed state.
        /// </returns>
        public bool IsBeingPressed()
        {
            bool ldBeingPressed = false;
            if (meButtonState == ButtonState.Press)
            {
                ldBeingPressed = true;
            }
            return ldBeingPressed;
        }
        /// <summary>
        /// Will set the button to its mdActive state.
        /// </summary>
        public void SetActive()
        {
            meButtonState = ButtonState.Active;
        }
        /// <summary>
        /// Will set the buttons text to the passed text
        /// </summary>
        /// <param name="adText"></param>
        public void SetText(string adText)
        {
            if (mdText != adText)
	        {
                mdText = adText;
                Vector2 loTextDimensions = moFont.MeasureString(adText);
                moTextPosition.X = moButtonRect.Center.X - (loTextDimensions.X / 2f);
                moTextPosition.Y = moButtonRect.Center.Y - (loTextDimensions.Y / 2f);
            }
        }
        /// <summary>
        /// Will check if button is mdActive
        /// </summary>
        /// <returns>
        /// Returns true if the button is mdActive,
        /// else returns false.
        /// </returns>
        public bool IsActive()
        {
            bool ldActive = false;
            if (meButtonState == ButtonState.Active)
            {
                ldActive = true;
            }
            return ldActive;
        }

    }
}
