using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
 *  -   I am making this class so that,
 *  we don't have a public static KeyboardState and MouseState,
 *  in the game class as this way its more logical.
 */

namespace Asteroids
{
    class Input
    {
        #region VARIABLES & PROPERTIES

        // Keyboard State Containers
        private static KeyboardState moKeyboardState;
        /// <summary>
        /// Gets the current Keyboard State.
        /// </summary>
        public static KeyboardState KeyboardState
        { get { return moKeyboardState; } }
        private static KeyboardState moKeyboardOldState;
        /// <summary>
        /// Gets the previous Keyboard State
        /// </summary>
        public static KeyboardState KeyboardOldState
        { get { return moKeyboardOldState; } }

        // Mouse State Containers
        private static MouseState moMouseState;
        /// <summary>
        /// Gets the current Mouse State
        /// </summary>
        public static MouseState MouseState
        { get { return moMouseState; } }
        private static MouseState moMouseOldState;
        /// <summary>
        /// Gets the previous Mouse State
        /// </summary>
        public static MouseState MouseOldState
        { get { return moMouseOldState; } }

        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Will update the Inputs properties,
        /// namely KeyboardState and KeyboardOldState.
        /// </summary>
        public static void UpdateKeyboard()
        {
            moKeyboardOldState = moKeyboardState;
            moKeyboardState = Keyboard.GetState();
        }
        /// <summary>
        /// Will update the Inputs properties,
        /// namely MouseState and MouseOldState
        /// </summary>
        public static void UpdateMouse()
        {
            moMouseOldState = moMouseState;
            moMouseState = Mouse.GetState();
        }

        #endregion

    }
} // End of namespace