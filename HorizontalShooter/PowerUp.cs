using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShooter
{
    public enum PowerUpType { Shower, ShootUp }
    public class PowerUp : Sprite
    {
        OnClicked ZeEffect;
        Color Color;

        public PowerUp(PowerUpType ptype, Vector2 position) : base(Assets.Power, position, false)
        {
            switch(ptype)
            {
                case PowerUpType.ShootUp:
                    Color = Color.Red;
                    ZeEffect = () => { Player.MissileCount = 3;
                        //ROBINSON
                        Assets.Sounds["powerup"].Play();
                    };
                    break;
                case PowerUpType.Shower:
                    Color = Color.Yellow;
                    ZeEffect = () =>
                    {
                        foreach (var item in GameScreen.Ennemis)
                        {
                            item.Effect = true;
                        };
                        //ROBINSON
                        Assets.Sounds["powerup"].Play();
                    };
                    break;
            }
        }


        public override void Update(float time)
        {
            base.Update(time);
            Velocity.X = -2f;
            if (GameScreen.Ship.Hitbox.Intersects(Hitbox))
            {
                ZeEffect.Invoke();
                Ended = true;
            }
        }


        public new void Draw(SpriteBatch batch)
        {
            batch.Draw(Texture, Position, Color);
        }


        public delegate void OnClicked();
    }
}
