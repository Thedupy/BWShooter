using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShooter
{
    public class Ennemi : Sprite
    {
        public bool Targeted;
        public Color Color;
        public float StartPositionY;
        public TweenPosition TPosition;
        public int Value;
        public bool Touched;
        public Sprite[] DeathAnimation;
        public bool CreateDeath = false;
        public float TimerEnded;

        public new Rectangle Hitbox
        {
            get { if (!Touched) return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); else return new Rectangle(-40, -40, 0, 0); }
        }


        public Ennemi(Texture2D texture, Vector2 position, Color color) : base(texture, position, true)
        {
            Color = color;
            StartPositionY = position.Y;
            Effect = false;
            DeathAnimation = new Sprite[4];
        }

        public override void Update(float time)
        {
            base.Update(time);
            if(Touched)
            {
                UpdateDeath(time);
            }
               
        }

        private void UpdateDeath(float time)
        {
            if(!CreateDeath)
            {
                DeathAnimation[0] = new Sprite(Utils.Slice(new Rectangle(0, 0, Texture.Width / 2, Texture.Height / 2), Texture), new Vector2(Position.X, Position.Y), true);
                DeathAnimation[1] = new Sprite(Utils.Slice(new Rectangle(Texture.Width / 2, 0, Texture.Width / 2, Texture.Height / 2), Texture), new Vector2(Position.X + Texture.Width / 2, Position.Y), true);
                DeathAnimation[2] = new Sprite(Utils.Slice(new Rectangle(0, Texture.Height / 2, Texture.Width / 2, Texture.Height / 2), Texture), new Vector2(Position.X, Position.Y + Texture.Height / 2), true);
                DeathAnimation[3] = new Sprite(Utils.Slice(new Rectangle(Texture.Width / 2, Texture.Height / 2, Texture.Width / 2, Texture.Height / 2), Texture), new Vector2(Position.X + Texture.Width / 2, Position.Y + Texture.Height / 2), true);
                for (int i = 0; i < DeathAnimation.Length; i++)
                {
                    DeathAnimation[i].Velocity = new Vector2((float)(Main.Rand.NextDouble() * 2.0 - 1.0));
                }
                CreateDeath = true;
            }
            else
            {
                TimerEnded += time;
                if (TimerEnded >= 1000)
                {
                    Ended = true;
                }
                    
                foreach (var item in DeathAnimation)
                {
                    item.Update(time);
                }
            }

        }

        new public void Draw(SpriteBatch batch)
        {
            
            if (Touched && DeathAnimation[0] != null)
            {
                foreach (var item in DeathAnimation)
                {
                    item.Draw(batch);
                }
            }
            else if (!Touched && Effect)
            {
                {
                    Assets.BlackWhite.Parameters["param1"].SetValue(DualityValue);
                    Assets.BlackWhite.CurrentTechnique.Passes[0].Apply();
                    base.Draw(batch);
                }
            }
            else
            {
                if(Color == Color.Black)
                {
                    Assets.BlackWhite.Parameters["param1"].SetValue(1f);
                    Assets.BlackWhite.CurrentTechnique.Passes[0].Apply();
                    base.Draw(batch);
                }
                else if(Color == Color.White)
                {
                    Assets.BlackWhite.Parameters["param1"].SetValue(0f);
                    Assets.BlackWhite.CurrentTechnique.Passes[0].Apply();
                    base.Draw(batch);
                }
            }
        }

        public void RandomPower()
        {
            int Bufferbool = Main.Rand.Next(5);
            if (Bufferbool == 1)
            {
                PowerUpType[] BW = new PowerUpType[2] { PowerUpType.ShootUp, PowerUpType.Shower };
                GameScreen.PowerUps.Add(new PowerUp(BW[Main.Rand.Next(2)], Position));
            }
        }

    }

    public class PathEnnemi : Ennemi
    {
        //PATH
        float PathTimer;
        int PathIndex;
        Vector2[] Path;
        float Speed;

        public PathEnnemi(Vector2 position, Color color, Vector2[] path, float speed) : base(Assets.EnnemiPath, position, color)
        {
            Color = color;
            StartPositionY = position.Y;
            Path = path;
            TPosition = new TweenPosition(this);
            Speed = speed;
            Value = 50;
        }

        public override void Update(float time)
        {
            base.Update(time);
            TPosition.Update(time, ref Position);
            PathTimer += time;
            if (PathTimer > Speed && PathIndex <= Path.Length - 1)
            {
                TPosition.Move(Path[PathIndex], Speed);
                PathIndex++;

                PathTimer = 0;
            }
        }

    }

    public class NormalEnnemi : Ennemi
    {
        public NormalEnnemi(Vector2 position, Color color) : base(Assets.EnnemiNormal, position, color)
        {
            Value = 10;
        }

        public override void Update(float time)
        {
            base.Update(time);
            Velocity.X = -2;
        }

    }

    public class SineEnnemi : Ennemi
    {
        //SIN
        int Sin;

        public SineEnnemi(Vector2 position, Color color, int sin) : base(Assets.EnnemiSin, position, color)
        {
            Color = color;
            StartPositionY = position.Y;
            Sin = sin;

            Value = 30;
        }

        public override void Update(float time)
        {

            base.Update(time);
            Position.Y = StartPositionY + (-(float)Math.Cos(Position.X / 100) * Sin);
            Velocity.X = -2;
        }

    }
}
