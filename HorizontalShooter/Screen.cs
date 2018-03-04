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
        public Transition Fondu;
        

        public Screen()
        {
            Batch = new SpriteBatch(Main.Device);
            Fondu = new Transition();
        }

        public virtual void Create()
        {
        }

        public virtual void Update(float time)
        {
            Fondu.Update(time);
        }

        public virtual void Draw()
        {
            Fondu.Draw(Batch);
        }

        public virtual void Dispose()
        {
            Batch.Dispose();
        }
    }

    public class IntroScreen : Screen
    {
        int DualHeight;
        public static Rectangle Duality;


        public IntroScreen()
        {
            Duality = new Rectangle(0, 0, Main.Width, 0);
            DualHeight = 400;
        }


        public override void Update(float time)
        {
            base.Update(time);
            if (Input.KeyPressed(Keys.Enter, true))
            {
                Fondu.Fade(new GameScreen());
            }

            Duality = new Rectangle(0, 0, Main.Width, DualHeight);
            DualHeight = MathHelper.Clamp(DualHeight, 0, Main.Height);

            if (Input.KeyPressed(Keys.Down, false))
                DualHeight += 5;
            else if (Input.KeyPressed(Keys.Up, false))
                DualHeight -= 5;
        }


        public override void Draw()
        {
            Batch.Begin();
            Batch.Draw(Assets.Pixel, Duality, Color.White);
            Batch.Draw(Assets.BGINTRO, Vector2.Zero, Color.White);
            base.Draw();

            Batch.End();
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
            Ennemis = new List<Ennemi>();
            PowerUps = new List<PowerUp>();
            WManager = new WaveManager(ref Ennemis);
            Assets.Intro.Play();
            Assets.Intro.Volume = 0.6f;
        }

        public override void Update(float time)
        {
            base.Update(time);
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

            Ship.Update(time);
            CollisionBulletEnnemis(Ship.Bullets, Ennemis);
            Ennemis.RemoveAll(k => k.Position.X + k.Texture.Width < 0 || k.Ended);
            PowerUps.RemoveAll(k => k.Ended == true || k.Position.X + k.Texture.Width <= 0);
            CollisionPlayerEnnemis();
        }

        public override void Draw()
        {
            Batch.Begin();
            Batch.Draw(Assets.Pixel, Duality, Color.White);
            BG.DrawBack(Batch);

            foreach (var item in PowerUps)
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
            base.Draw();
            Batch.End();
        }

        public static bool IsInRectangle(Sprite sprite)
        {
            return Duality.Contains(sprite.Hitbox);
        }

        public void CollisionPlayerEnnemis()
        {
            foreach (var item in Ennemis)
            {
                if (Ship.Hitbox.Intersects(item.Hitbox) && Ship.Touched == false)
                {
                    Console.WriteLine("lol touche");
                    item.Ended = true;
                    Ship.Touched = true;
                    if (Player.LifeCount == 1)
                        Fondu.Fade(new GOScreen());
                    else
                        Player.LifeCount--;
                }
            }
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
                        ennemis[j].RandomPower();
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
                    kek.Cible.RandomPower();
                    bullet.Remove(kek);
                }
            }
        }
    }

    public class GOScreen : Screen
    {
        public GOScreen()
        {
            Assets.Intro.Stop();
        }


        public override void Update(float time)
        {
            base.Update(time);
            if(Input.KeyPressed(Keys.Space, true))
            {
                Fondu.Fade(new GameScreen());
                Player.LifeCount = 3;
            }
        }


        public override void Draw()
        {
            Batch.Begin();
            Batch.Draw(Assets.Pixel, new Rectangle(0, 0, Main.Width, Main.Height), Color.White);
            Batch.DrawString(Assets.Font, "Score : " + HUD.MyHUD.SCORE.ToString(), new Vector2(Main.Width / 2, Main.Height / 2), Color.Black);
            Batch.DrawString(Assets.Font, "Appuyez sur Barre Espace pour rejouer", new Vector2(Main.Width / 2, Main.Height / 2 + 50), Color.Black);
            base.Draw();
            Batch.End();
        }

        public void ResetAll()
        {

        }

    }
}
