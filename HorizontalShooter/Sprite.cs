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
    public class Sprite
    {
        public Color TrueColor
        {
            get {
                if (GameScreen.IsInRectangle(this))
                    return Color.Black;
                else
                    return Color.White;
            }
        }
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Velocity;
        public Rectangle Black;
        public float DualityValue;
        public bool Effect;
        public bool Ended;
        public bool Visible;


        public Rectangle Hitbox
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); }
        }

        public Vector2 Origin
        {
            get { return new Vector2(Position.X + (Texture.Width / 2), Position.Y + (Texture.Height / 2)); }
        }

        public Sprite(Texture2D texture, Vector2 position, bool effect)
        {
            Texture = texture;
            Position = position;
            Black = new Rectangle(0, 0, Texture.Width, 0);
            Effect = effect;
        }

        public virtual void Update(float time)
        {
            Position += Velocity;
            DualityValue = (GameScreen.Duality.Height - Position.Y) / Texture.Height;

        }

        public virtual void Draw(SpriteBatch batch)
        {
            if (Effect)
            {
                Assets.BlackWhite.Parameters["param1"].SetValue(DualityValue);
                Assets.BlackWhite.CurrentTechnique.Passes[0].Apply();
                batch.Draw(Texture, Position, Color.White);
            }
            else
                batch.Draw(Texture, Position, Color.White);
        }

    }
}
