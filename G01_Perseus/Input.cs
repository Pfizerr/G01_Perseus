using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G01_Perseus
{
    public class Input
    {
        public static KeyboardState keyboardState, previousKeyboardState;
        public static MouseState mouseState, previousMouseState;
        private static Keys up, left, down, right;

        public static void Init()
        {
            up = Keys.W;
            left = Keys.A;
            down = Keys.S;
            right = Keys.D;
        }

        public static void Update()
        {
            previousKeyboardState = keyboardState;
            previousMouseState = mouseState;
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        public static Keys Up { get => up; private set => up = value; }
        public static Keys Left { get => left; private set => left = value; }
        public static Keys Down { get => down; private set => down = value; }
        public static Keys Right { get => right; private set => right = value; }
        public static bool IsLeftMouseButtonPressed { get => mouseState.LeftButton == ButtonState.Pressed ? true : false; }
        public static bool isRightMouseButtonPressed { get => mouseState.RightButton == ButtonState.Pressed ? true : false; }
        public static bool IsLeftMouseButtonClicked { get => mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released ? true : false; }
        public static bool IsRightMouseButtonClicked { get => mouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released ? true : false; }
    }
}
