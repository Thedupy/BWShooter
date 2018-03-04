using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShooter
{
    public class HUD
    {
        public static int MaxHUD;
        public int SCORE;
        public Rectangle Cadre;

        public HUD()
        {
            Cadre = new Rectangle(0,0, Main.Width, 50);
            Cadre.Y = MaxHUD = Main.Height - Cadre.Height;
        }


        public void Update(float time)
        {

        }


        public void Draw(SpriteBatch batch)
        {
            batch.Draw(Assets.Pixel, Cadre, Color.Red);

            for (int i = 0; i < Player.MissileCount; i++)
            {
                batch.Draw(Assets.MissileIcon, new Vector2(20 + i * Assets.MissileIcon.Width, 576), Color.White);
            }

            for (int i = 0; i < Player.LifeCount; i++)
            {
                batch.Draw(Assets.LifeIcon, new Vector2(150 + i * Assets.LifeIcon.Width, 576), Color.White);
            }

            batch.DrawString(Assets.Font, SCORE.ToString(), new Vector2(Main.Width - 100, 576), Color.Black);
        }


        public static HUD MyHUD = new HUD();
    }
}
