using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShooter
{
    public class BackgroundManager
    {
        public List<Sprite> Decors;
        float Dtimer;
        public BackgroundManager()
        {
            Decors = new List<Sprite>();
        }

        public void Update(float time)
        {
            foreach (var item in Decors)
            {
                item.Position.X -= 5;
            }

            Decors.RemoveAll(k => k.Position.X + k.Texture.Width < 0);

            Dtimer += time;
            if(Dtimer >= 1500)
            {
                GenerateDecors();
                Dtimer = 0;
            }
        }

        public void GenerateDecors()
        {
            Texture2D kek = Utils.CreateTexture(Main.Rand.Next(40, 100), Main.Rand.Next(100, 200), Color.Gray);
            Vector2 mdr = new Vector2(Main.Width + 100, Main.Rand.Next(0, Main.Height - kek.Height));
            Decors.Add(new Sprite(kek, mdr, false));
            Console.WriteLine(Decors.Count);
        }

        public void Draw(SpriteBatch batch)
        {
            foreach (var item in Decors)
            {
                item.Draw(batch);
            }
        }
    }
}
