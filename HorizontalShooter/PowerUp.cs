using Microsoft.Xna.Framework;
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
        public bool Ended;

        public PowerUp(PowerUpType ptype) : base(Assets.Power, new Vector2(Main.Width + 50, Main.Rand.Next(HUD.MaxHUD - Assets.Power.Height)), false)
        {
            switch(ptype)
            {
                case PowerUpType.ShootUp:
                    //Texture = Utils.CreateTexture(30, 30, Color.Pink);
                    ZeEffect = () => { Player.MissileCount = 3;};
                    break;
                case PowerUpType.Shower:
                    //Texture = Utils.CreateTexture(30, 30, Color.LightBlue);
                    ZeEffect = () =>
                    {
                        foreach (var item in GameScreen.Ennemis)
                        {
                            item.Effect = true;
                        };
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

        public delegate void OnClicked();
    }
}
