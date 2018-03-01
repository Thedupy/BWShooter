using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace HorizontalShooter
{
    public class Assets
    {
        #region Variable
        public static Texture2D Pixel, Ship;
        public static Effect BlackWhite;

        public static Dictionary<string, SoundEffect> Sounds = new Dictionary<string, SoundEffect>();
        public static List<SoundEffect> MissileSound = new List<SoundEffect>();
        public static List<SoundEffect> MissileHitSound = new List<SoundEffect>();
        #endregion

        #region Methode
        public static void LoadAll()
        {
            Pixel = Utils.CreateTexture(1, 1, Color.White);
            BlackWhite = Main.Content.Load<Effect>("blackwhite");
            Ship = Main.Content.Load<Texture2D>("ship");

            LoadSound();
        }

        private static void LoadSound()
        {
            Sounds.Add("bullet", Main.Content.Load<SoundEffect>("Sound/Bullet"));
            Sounds.Add("missileset", Main.Content.Load<SoundEffect>("Sound/missileset"));
            Sounds.Add("missilelaunch", Main.Content.Load<SoundEffect>("Sound/missilelaunch"));
            Sounds.Add("bullethit", Main.Content.Load<SoundEffect>("Sound/ennemybullethit"));

            //
            MissileSound.Add(Main.Content.Load<SoundEffect>("Sound/missile1"));
            MissileSound.Add(Main.Content.Load<SoundEffect>("Sound/missile2"));
            MissileSound.Add(Main.Content.Load<SoundEffect>("Sound/missile3"));

            //
            MissileHitSound.Add(Main.Content.Load<SoundEffect>("Sound/ennemimissilehit1"));
            MissileHitSound.Add(Main.Content.Load<SoundEffect>("Sound/ennemimissilehit2"));
            MissileHitSound.Add(Main.Content.Load<SoundEffect>("Sound/ennemimissilehit3"));
            MissileHitSound.Add(Main.Content.Load<SoundEffect>("Sound/ennemimissilehit4"));
            MissileHitSound.Add(Main.Content.Load<SoundEffect>("Sound/ennemimissilehit5"));
        }

        public static void PlayRandomSound(List<SoundEffect> list)
        {
            list[Main.Rand.Next(list.Count - 1)].Play();
        }
        #endregion
    }

   
}
