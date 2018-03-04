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

            batch.DrawString(Assets.Font, "MISSILES : ", new Vector2(10, 576), Color.Black);

            for (int i = 0; i < Player.MissileCount; i++)
            {
                batch.Draw(Assets.MissileIcon, new Vector2(100 + i * Assets.MissileIcon.Width, 576), Color.White);
            }

            batch.DrawString(Assets.Font, "VIES : ", new Vector2(250, 576), Color.Black);

            for (int i = 0; i < Player.LifeCount; i++)
            {
                batch.Draw(Assets.LifeIcon, new Vector2(320 + i * Assets.LifeIcon.Width, 576), Color.White);
            }

            batch.DrawString(Assets.Font, "SCORE : ", new Vector2(Main.Width - 200, 576), Color.Black);

            batch.DrawString(Assets.Font, SCORE.ToString(), new Vector2(Main.Width - 100, 576), Color.Black);
        }


        public static HUD MyHUD = new HUD();
    }
}
