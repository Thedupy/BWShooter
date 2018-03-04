using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace HorizontalShooter
{
    public class Bullet : Sprite
    {

        public Bullet(Texture2D texture, Vector2 position, bool effect) : base(texture, position, true)
        {

        }


        public override void Update(float time)
        {
            base.Update(time);
        }


        public override void Draw(SpriteBatch batch)
        {

            base.Draw(batch);
        }
    }

    public class GunShoot : Bullet
    {
        public GunShoot(Vector2 position) : base(Assets.Bullet, position, true)
        {

        }


        public override void Update(float time)
        {
            base.Update(time);
            Velocity.X = 5;
        }


        public override void Draw(SpriteBatch batch)
        {
            Assets.BlackWhite.Parameters["param1"].SetValue(DualityValue);
            Assets.BlackWhite.CurrentTechnique.Passes[0].Apply();
            batch.Draw(Texture, Position, Color.White);
        }

    }

    public class Missile : Bullet
    {
        public bool Fire;
        float EffectTimer;
        public Ennemi Cible;
        private float Rotation;
        TweenPosition TPosition;

        public SoundEffectInstance Launch = Assets.Sounds["missilelaunch"].CreateInstance();

        public Missile(Vector2 position) : base(Assets.Missile, position, false)
        {
            TPosition = new TweenPosition(this);
            TPosition.Move(new Vector2(Position.X, Position.Y - 100), 1000, EaseFunction.EaseOutQuad);
        }


        public override void Update(float time)
        {
            DualityValue = (GameScreen.Duality.Height - Position.Y) / Texture.Height;

            TPosition.Update(time, ref Position);
            EffectTimer += time;
            if(EffectTimer >= 1000)
            {
                Fire = true;
                EffectTimer = 0;
            }

            if(Fire)
            {
                base.Update(time);
                if (Cible == null)
                {
                    List<Ennemi> Prout = GameScreen.Ennemis.FindAll(k => k.Targeted == false && k.Touched  == false && k.Position.X < Main.Width);
                    if (Prout.Count > 0)
                    {
                        //ROBINSON
                        Assets.Sounds["missileset"].Play();
                        Launch.Play();
                        Cible = Prout[Main.Rand.Next(Prout.Count)];
                        Cible.Targeted = true;
                    }
                    else
                        Velocity.X = 5;
                }
                else
                {
                    Vector2 direction = Cible.Origin - Position;
                    Rotation = (float)(Math.Atan2(direction.Y, direction.X));
                    Velocity.X = (float)Math.Cos(Rotation) * 5;
                    Velocity.Y = (float)Math.Sin(Rotation) * 5;
                }
            }
        }


        public override void Draw(SpriteBatch batch)
        {
            Assets.BlackWhite.Parameters["param1"].SetValue(DualityValue);
            Assets.BlackWhite.CurrentTechnique.Passes[0].Apply();
            batch.Draw(Texture, Position, null, TrueColor, Rotation, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }
    }
}
