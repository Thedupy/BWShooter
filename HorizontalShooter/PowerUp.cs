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
        OnClicked Effect;
        public bool Ended;

        public PowerUp(PowerUpType ptype) : base(Assets.Pixel, new Vector2(Main.Width + 50, Main.Rand.Next(Main.Height)), false)
        {
            switch(ptype)
            {
                case PowerUpType.ShootUp:
                    Texture = Utils.CreateTexture(30, 30, Color.Pink);
                    Effect = () => { Console.WriteLine("Puissance de feu augmenté");};
                    break;
                case PowerUpType.Shower:
                    Texture = Utils.CreateTexture(30, 30, Color.LightBlue);
                    Effect = () => { Console.WriteLine("ON SE CACHE PAS LES CONNARDS");};
                    break;
            }
        }


        public override void Update(float time)
        {
            base.Update(time);
            Velocity.X = -2f;
            if (GameScreen.Ship.Hitbox.Intersects(Hitbox))
            {
                Effect.Invoke();
                Ended = true;
            }
        }

        public delegate void OnClicked();
    }
}
