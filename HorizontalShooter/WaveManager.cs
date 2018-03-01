using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShooter
{
    public enum EnnemiType { Sine, Path, Normal }
    public class WaveManager
    {
        List<Ennemi> Buffer;
        public bool Run;
        //PARAM
        int cpt;
        int NbrEnnemi;
        float Timer;
        EnnemiType Etype;
        int PosY;
        Vector2[] Path;
        int Sin;
        Color Color;

        public WaveManager(ref List<Ennemi> buffer)
        {
            Buffer = buffer;
            Sin = 0;
        }


        public void Update(float time)
        {
            if (Run)
            {
                Timer += time;
                if (Timer >= 1000)
                {
                    switch (Etype)
                    {
                        case EnnemiType.Normal:
                            Buffer.Add(new NormalEnnemi(new Vector2(Main.Width + 50, PosY), Color));
                            break;
                        case EnnemiType.Path:
                            Buffer.Add(new PathEnnemi(new Vector2(Main.Width + 50, PosY), Color, Path));
                            break;
                        case EnnemiType.Sine:
                            Buffer.Add(new SineEnnemi(new Vector2(Main.Width + 50, PosY), Color, Sin));
                            break;
                    }
                    cpt++;
                    Timer = 0;
                }
            }

            if (cpt == NbrEnnemi)
                Run = false;
        }

        public void Start()
        {
            Timer = 0;
            cpt = 0;
            Run = true;
        }

        public void NormalWave(int nbr, EnnemiType type, int posY)
        {
            NbrEnnemi = nbr;
            PosY = posY;
            Etype = type;
            if (Etype == EnnemiType.Path)
                Path = CreateRandomPath(Main.Rand.Next(3,6));
            else if(Etype == EnnemiType.Sine)
            {
                int bufferY = 0;
                if (MathHelper.Distance(PosY, 0) > MathHelper.Distance(PosY, Main.Height))
                {
                    bufferY = (int)MathHelper.Distance(PosY, Main.Height);
                }
                else
                    bufferY = (int)MathHelper.Distance(PosY, 0);

                Sin = Main.Rand.Next(10, bufferY);
            }
            Color = RandomColor();
            Start();
        }

        public Vector2[] CreateRandomPath(int cpt)
        {
            Vector2[] BufferPath = new Vector2[cpt + 1];
            for (int i = 0; i < cpt - 1; i++)
            {
                BufferPath[i] = new Vector2(Main.Rand.Next(800), Main.Rand.Next(600));
            }
            BufferPath[cpt-1] = new Vector2(-100, Main.Rand.Next(600));
            return BufferPath;
        }

        public Color RandomColor()
        {
            Color[] BW = new Color[2] { Color.Black, Color.White };
            return BW[Main.Rand.Next(2)];
        }
    }
}
