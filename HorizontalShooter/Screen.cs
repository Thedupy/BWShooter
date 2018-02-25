using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShooter
{
    public class Screen
    {
        public SpriteBatch Batch;

        public Screen()
        {
            Batch = new SpriteBatch(Main.Device);
        }

        public virtual void Create()
        {

        }

        public virtual void Update(float time)
        {

        }

        public virtual void Draw()
        {

        }

        public virtual void Dispose()
        {
            Batch.Dispose();
        }
    }

    public class GameScreen : Screen
    {
        BackgroundManager BG;
        int DualHeight;
        public static Rectangle Duality;
        public WaveManager WManager;
        Player Ship;
        public static List<Ennemi> Ennemis;
        public GameScreen()
            : base()
        {
        }

        public override void Create()
        {
            BG = new BackgroundManager();
            Duality = new Rectangle(0, 0, Main.Width, 0);
            DualHeight = Main.Height/2;
            Ship = new Player(new Vector2(50, 500));
            Ennemis = new List<Ennemi>()
            {
                new Ennemi(new Vector2(750, 150), Color.White),
                new Ennemi(new Vector2(750, 450), Color.Black),
                new Ennemi(new Vector2(750, 500), Color.Black),
                new Ennemi(new Vector2(750, 320), Color.White),
                new Ennemi(new Vector2(750, 560), Color.Black)
            };
            WManager = new WaveManager(ref Ennemis);
        }

        public override void Update(float time)
        {
            BG.Update(time);
            WManager.Update(time);
            Duality = new Rectangle(0, 0, Main.Width, DualHeight);
            DualHeight = MathHelper.Clamp(DualHeight, 0, Main.Height);
            foreach (var item in Ennemis)
            {
                item.Update(time);
            }
            if (Input.KeyPressed(Keys.Down, false))
                DualHeight += 5;
            else if (Input.KeyPressed(Keys.Up, false))
                DualHeight -= 5;

            if (Input.KeyPressed(Keys.NumPad5, true))
            {
                WManager.NormalWave(5, EnnemiType.Sine, Main.Rand.Next(50,550));
            }
            Ship.Update(time);
            CollisionBulletEnnemis(Ship.Bullets, Ennemis);
            Ennemis.RemoveAll(k => k.Position.X + k.Texture.Width < 0);
        }

        public override void Draw()
        {
            Batch.Begin();
            Batch.Draw(Assets.Pixel, Duality, Color.White);
            BG.Draw(Batch);
            foreach (var item in Ennemis)
            {
                item.Draw(Batch);
            }
            Batch.End();
            Batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            Ship.Draw(Batch);
            Batch.End();
        }

        public static bool IsInRectangle(Sprite sprite)
        {
            return Duality.Contains(sprite.Hitbox);
        }

        public void CollisionBulletEnnemis(List<Bullet> bullet, List<Ennemi> ennemis)
        {
            List<Bullet> Shoot = bullet.FindAll(k => k is GunShoot);
            List<Bullet> Missile = bullet.FindAll(k => k is Missile);

            for (int i = 0; i < Shoot.Count; i++)
            {
                for (int j = 0; j < ennemis.Count; j++)
                {

                    if (Shoot[i].Hitbox.Intersects(ennemis[j].Hitbox))
                    {
                        bullet.Remove(bullet[i]);
                        ennemis.Remove(ennemis[j]);
                    }
                }
            }

            for (int i = 0; i < Missile.Count; i++)
            {
                Missile kek = Missile[i] as Missile;
                if (kek.Cible != null && kek.Hitbox.Intersects(kek.Cible.Hitbox))
                {
                    bullet.Remove(kek);
                    ennemis.Remove(kek.Cible);
                }
            }
        }
    }
}
