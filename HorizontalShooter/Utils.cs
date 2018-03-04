using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShooter
{
    class Utils
    {
        //DESSIN DE HITBOX
        public static Texture2D CreateTexture(int w, int h, Color col)
        {
            Texture2D texture = new Texture2D(Main.Device, w, h);
            Color[] cols = new Color[w * h];
            for (int i = 0; i < cols.Length; i++)
            {
                cols[i] = col;
            }
            texture.SetData(cols);
            return texture;
        }

        public static Texture2D Slice(Rectangle region, Texture2D sheet)
        {
            Color[] rawdata = new Color[region.Width * region.Height];
            sheet.GetData(0, region, rawdata, 0, rawdata.Length);
            Texture2D BufferTexture = new Texture2D(Main.Device, region.Width, region.Height);
            BufferTexture.SetData(rawdata);
            return BufferTexture;
        }
    }
}
