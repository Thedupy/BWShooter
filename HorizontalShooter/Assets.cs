using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HorizontalShooter
{
    public class Assets
    {
        #region Variable
        public static Texture2D Pixel, Ship;
        public static Effect BlackWhite;
        #endregion

        #region Methode
        public static void LoadAll()
        {
            Pixel = Utils.CreateTexture(1, 1, Color.White);
            BlackWhite = Main.Content.Load<Effect>("blackwhite");
            Ship = Main.Content.Load<Texture2D>("ship");
        }
        #endregion
    }
}
