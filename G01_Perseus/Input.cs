using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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

        public static bool IsKeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        public static bool IsKeyClicked(Keys key)
        {
            return IsKeyPressed(key) && !previousKeyboardState.IsKeyDown(key);
        }

        public static Keys Up { get => up; private set => up = value; }
        public static Keys Left { get => left; private set => left = value; }
        public static Keys Down { get => down; private set => down = value; }
        public static Keys Right { get => right; private set => right = value; }
        public static Vector2 MouseWorldPosition => new Vector2(EntityManager.Player.Position.X + 20, EntityManager.Player.Position.Y + 20) + Input.mouseState.Position.ToVector2() - new Vector2(Game1.camera.Viewport.Width / 2, Game1.camera.Viewport.Height / 2);

        public static Vector2 MouseScreenPosition => mouseState.Position.ToVector2();
        public static bool IsLeftMouseButtonPressed { get => mouseState.LeftButton == ButtonState.Pressed ? true : false; } // No need for it to be an if statement, simply return the value
        public static bool isRightMouseButtonPressed { get => mouseState.RightButton == ButtonState.Pressed ? true : false; }// No need for it to be an if statement, simply return the value
        public static bool IsLeftMouseButtonClicked { get => mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released ? true : false; } // No need for it to be an if statement, simply return the value
        public static bool IsRightMouseButtonClicked { get => mouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released ? true : false; } // No need for it to be an if statement, simply return the value
    }
}
