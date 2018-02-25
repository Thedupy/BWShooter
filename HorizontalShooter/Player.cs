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
    class Player : Sprite
    {
        public List<Bullet> Bullets;
        public Player(Vector2 position) : base(Assets.Ship, position, true)
        {
            Bullets = new List<Bullet>();
        }

        public override void Update(float time)
        {
            base.Update(time);
            if (Input.KeyPressed(Keys.Z, false))
            {
                Velocity.Y = -5;
            }
            else if (Input.KeyPressed(Keys.S, false))
            {
                Velocity.Y = 5;
            }
            else
                Velocity.Y = 0;

            if(Input.KeyPressed(Keys.Space, true))
            {
                Bullets.Add(new GunShoot(new Vector2(Position.X + Texture.Width + Velocity.X + 5, Position.Y + (Texture.Height / 2))));
            }
            if (Input.KeyPressed(Keys.E, true))
            {
                Bullets.Add(new Missile(new Vector2(Position.X, Position.Y + Velocity.Y)));
            }

            Position = Vector2.Clamp(Position, Vector2.Zero, new Vector2(Main.Width, Main.Height));
            
            for (int i = 0; i < Bullets.Count; i++)
            {
                Bullets[i].Update(time);
            }
            Bullets.RemoveAll(k => k.Position.X > Main.Width);
        }

        public override void Draw(SpriteBatch batch)
        {
            Assets.BlackWhite.Parameters["param1"].SetValue(pouet);
            Assets.BlackWhite.CurrentTechnique.Passes[0].Apply();
            base.Draw(batch);

            foreach (var item in Bullets)
            {
                item.Draw(batch);
            }
        }
    }
}
