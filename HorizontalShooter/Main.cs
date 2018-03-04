using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShooter
{
    class Main
    {
        public static GraphicsDeviceManager Graphics;
        public static GraphicsDevice Device;
        public static Game Instance;
        public static ContentManager Content;
        public static Random Rand;
        public static Screen CurrentScreen;

        public static int Width = 800, Height = 600;

        public Main(GraphicsDeviceManager graphics, Game game)
        {
            Graphics = graphics;
            Device = graphics.GraphicsDevice;
            Instance = game;
            Content = game.Content;
            Rand = new Random();
        }

        public void Initialize()
        {
            Assets.LoadAll();

            Graphics.PreferredBackBufferWidth = Width;
            Graphics.PreferredBackBufferHeight = Height;
            Graphics.SynchronizeWithVerticalRetrace = false;
            Graphics.ApplyChanges();

            SetScreen(new IntroScreen());

        }

        public void Update(float time)
        {
            Input.Update();

            if (CurrentScreen != null) CurrentScreen.Update(time);
        }

        public void Draw()
        {
            Device.Clear(Color.Black);

            if (CurrentScreen != null) CurrentScreen.Draw();

        }

        public static void SetScreen(Screen screen)
        {
            CurrentScreen = screen;
            CurrentScreen.Create();
        }
    }
}
