using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShooter
{
    class Input
    {
        private static KeyboardState oldK, currentK;
        private static MouseState oldM, currentM;

        public static Rectangle MouseBox;
        public static Vector2 MousePos;

        public static void Update()
        {
            oldK = currentK;
            oldM = currentM;

            currentK = Keyboard.GetState();
            currentM = Mouse.GetState();

            MouseBox = new Rectangle(currentM.X, currentM.Y, 1, 1);
            MousePos = new Vector2(currentM.X, currentM.Y);
        }

        public static bool KeyPressed(Keys k, bool u)
        {
            return u ? (oldK[k] == KeyState.Up && currentK[k] == KeyState.Down) : (currentK[k] == KeyState.Down);
        }

        public static bool Left(bool u)
        {
            return u ? (oldM.LeftButton == ButtonState.Released && currentM.LeftButton == ButtonState.Pressed) : (currentM.LeftButton == ButtonState.Pressed);
        }

        public static bool Right(bool u)
        {
            return u ? (oldM.RightButton == ButtonState.Released && currentM.RightButton == ButtonState.Pressed) : (currentM.RightButton == ButtonState.Pressed);
        }

        public static bool Middle(bool u)
        {
            return u ? (oldM.MiddleButton == ButtonState.Released && currentM.MiddleButton == ButtonState.Pressed) : (currentM.MiddleButton == ButtonState.Pressed);
        }
    }
}
