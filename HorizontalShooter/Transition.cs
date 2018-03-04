using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShooter
{
    public class Transition
    {
        public Texture2D Texture;
        public Color Color;
        public bool Action;
        public bool Change;
        public Screen ScreenToChange;

        int AlphaValue = 1;
        int FadeIncrement = 3;
        double FadeDelay = 0.35;
        int CptFade = 0;

        public Transition()
        {
            Texture = Assets.PixelB;
            Color = Color.Transparent;
            Action = false;
        }

        public void Update(float time)
        {
            if (Action) 
            {
                FadeDelay -= time;

                if (FadeDelay <= 0)
                {
                    FadeDelay = 3;
                    AlphaValue += FadeIncrement;

                    if (AlphaValue >= 255 || AlphaValue <= 0)
                    {
                        if (AlphaValue >= 255)
                            Change = true;
                        FadeIncrement *= -1;
                        CptFade++;
                    }
                }

                if(Change)
                {
                    Change = false;
                    Main.SetScreen(ScreenToChange);
                }

                if (CptFade == 2)
                {
                    Action = false;
                    CptFade = 0;
                }
            }
        }

        public void Fade(Screen newScreen)
        {
            ScreenToChange = newScreen;
            Action = true;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(Texture, new Rectangle(0,0, Main.Width, Main.Height), new Color(0,0,0, MathHelper.Clamp(AlphaValue, 0, 255)));
        }
    }
}
