using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShooter//LMOOOOL
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
        public static Player Ship;
        public static List<Ennemi> Ennemis;
        public static List<PowerUp> PowerUps;

        //Rectangle Fade;
        //Color FadeColor;

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
                new NormalEnnemi(new Vector2(750, 150), Color.White),
                new NormalEnnemi(new Vector2(750, 450), Color.Black),
                new NormalEnnemi(new Vector2(750, 500), Color.Black),
                new NormalEnnemi(new Vector2(750, 320), Color.White),
                new NormalEnnemi(new Vector2(750, 210), Color.Black)
            };
            PowerUps = new List<PowerUp>()
            {
                new PowerUp(PowerUpType.ShootUp),
                new PowerUp(PowerUpType.Shower)
            };
            WManager = new WaveManager(ref Ennemis);


            //Fade = new Rectangle(0, 0, Main.Width, Main.Height);
            //FadeColor = new Color(Color.Red, 0f);
        }

        public override void Update(float time)
        {
            Console.WriteLine(MediaPlayer.PlayPosition);
            if(Input.KeyPressed(Keys.NumPad0, true))
            {
                MediaPlayer.PlayPosition.Add(TimeSpan.FromMilliseconds(5000));
            }
            //if (Input.Left(true))
            //{
            //    if (FadeColor.A + 5 <= 255)
            //        FadeColor.A += 5;
            //    else
            //        FadeColor.A = 255;
            //    Console.WriteLine(FadeColor.A);
            //}
            //if (Input.Right(true) && FadeColor.A - 5 >= 0)
            //{
            //    if (FadeColor.A - 5 >= 0)
            //        FadeColor.A -= 5;
            //    else
            //        FadeColor.A = 0;
            //    Console.WriteLine(FadeColor.A);
            //}
            BG.Update(time);
            WManager.Update(time);
            Duality = new Rectangle(0, 0, Main.Width, DualHeight);
            DualHeight = MathHelper.Clamp(DualHeight, 0, HUD.MaxHUD);
            foreach (var item in Ennemis)
            {
                item.Update(time);
            }
            foreach (var item in PowerUps)
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
            if (Input.KeyPressed(Keys.NumPad7, true))
            {
                WManager.NormalWave(5, EnnemiType.Path, Main.Rand.Next(50, 550));
            }
            if (Input.KeyPressed(Keys.NumPad6, true))
            {
                PowerUps.Add(new PowerUp(PowerUpType.ShootUp));
            }
            if (Input.KeyPressed(Keys.NumPad6, true))
            {
                PowerUps.Add(new PowerUp(PowerUpType.Shower));
            }

            Ship.Update(time);
            CollisionBulletEnnemis(Ship.Bullets, Ennemis);
            Ennemis.RemoveAll(k => k.Position.X + k.Texture.Width < 0 || k.Ended);
            PowerUps.RemoveAll(k => k.Ended == true || k.Position.X + k.Texture.Width <= 0);
        }

        public override void Draw()
        {
            Batch.Begin();
            Batch.Draw(Assets.Pixel, Duality, Color.White);
            BG.DrawBack(Batch);

            foreach (var item in PowerUps)//LOL
            {
                item.Draw(Batch);
            }
            //Batch.Draw(Assets.Pixel, Fade, FadeColor);
            Batch.End();
            Batch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            foreach (var item in Ennemis)
            {
                item.Draw(Batch);
            }
            Ship.Draw(Batch);
            Batch.End();
            Batch.Begin();
            BG.Draw(Batch);
            HUD.MyHUD.Draw(Batch);
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
                        //ROBINSON
                        Assets.Sounds["bullethit"].Play();
                        HUD.MyHUD.SCORE += ennemis[j].Value;
                        bullet.Remove(bullet[i]);
                        ennemis[j].Touched = true;
                    }
                }
            }

            for (int i = 0; i < Missile.Count; i++)
            {
                Missile kek = Missile[i] as Missile;
                if (kek.Cible != null && kek.Hitbox.Intersects(kek.Cible.Hitbox))
                {
                    kek.Launch.Stop();
                    HUD.MyHUD.SCORE += kek.Cible.Value;
                    Assets.PlayRandomSound(Assets.MissileHitSound);
                    kek.Cible.Touched = true;
                    bullet.Remove(kek);
                }
            }
        }
    }
}
