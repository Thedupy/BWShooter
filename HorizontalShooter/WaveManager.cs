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
        float Speed;

        float WaveTimer;

        public WaveManager(ref List<Ennemi> buffer)
        {
            Buffer = buffer;
            Sin = 0;
        }


        public void Update(float time)
        {
            WaveTimer += time;
            if(WaveTimer >= 5000)
            {
                var lel = Enum.GetValues(typeof(EnnemiType));
                EnnemiType randomBar = (EnnemiType)lel.GetValue(Main.Rand.Next(lel.Length));
                NormalWave(5, randomBar, Main.Rand.Next(50, HUD.MaxHUD - 50));
                WaveTimer = 0;
            }


            if (Run)
            {
                Timer += time;
                if (Timer >= 1000)
                {
                    Console.WriteLine(Color);
                    switch (Etype)
                    {
                        case EnnemiType.Normal:
                            Buffer.Add(new NormalEnnemi(new Vector2(Main.Width + 50, PosY), Color));
                            break;
                        case EnnemiType.Path:
                            Buffer.Add(new PathEnnemi(new Vector2(Main.Width + 50, PosY), Color, Path, Speed));
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
            {
                Path = CreateRandomPath();
                Speed = Main.Rand.Next(1000, 1500);
            }
            else if (Etype == EnnemiType.Sine)
            {
                int bufferY = 0;
                if (MathHelper.Distance(PosY, 0) > MathHelper.Distance(PosY, HUD.MaxHUD))
                {
                    bufferY = (int)MathHelper.Distance(PosY, HUD.MaxHUD);
                }
                else
                    bufferY = (int)MathHelper.Distance(PosY, 0);

                Sin = Main.Rand.Next(10, bufferY);
            }
            //Color = RandomColor();
            Color = Color.Black;
            Start();
        }

        public Vector2[] CreateRandomPath()
        {
            Vector2[] BufferPath = new Vector2[4];
            for (int i = 1; i < 5; i++)
            {
                BufferPath[i-1] = new Vector2(Main.Width - (i*220), Main.Rand.Next(HUD.MaxHUD - 50));
            }
            return BufferPath;
        }

        public Color RandomColor()
        {
            Color[] BW = new Color[2] { Color.White, Color.Black };
            return BW[Main.Rand.Next(2)%2];
        }
    }
}
