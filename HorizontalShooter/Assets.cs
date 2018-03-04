using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Media;

//prout

namespace HorizontalShooter
{
    public class Assets
    {
        #region Variable
        public static Texture2D Pixel, Ship, Bullet, EnnemiPath, EnnemiSin, EnnemiNormal, Missile, Power, Shield;
        public static Texture2D MissileIcon, LifeIcon;
        public static Effect BlackWhite;

        public static Song Intro, Music;

        public static SpriteFont Font;

        public static Dictionary<string, SoundEffect> Sounds = new Dictionary<string, SoundEffect>();
        public static List<SoundEffect> MissileSound = new List<SoundEffect>();
        public static List<SoundEffect> MissileHitSound = new List<SoundEffect>();
        #endregion

        #region Methode
        public static void LoadAll()
        {
            Pixel = Utils.CreateTexture(1, 1, Color.White);
            BlackWhite = Main.Content.Load<Effect>("blackwhite");
            Ship = Main.Content.Load<Texture2D>("Sprite/ship");
            EnnemiPath = Main.Content.Load<Texture2D>("Sprite/ennemiPath");
            EnnemiSin = Main.Content.Load<Texture2D>("Sprite/ennemiSin");
            EnnemiNormal = Main.Content.Load<Texture2D>("Sprite/ennemiNormal");
            Missile = Main.Content.Load<Texture2D>("Sprite/missile");
            Bullet = Main.Content.Load<Texture2D>("Sprite/bullet");
            Power = Main.Content.Load<Texture2D>("Sprite/powerup");

            Music = Main.Content.Load<Song>("Music/music");
            Intro = Main.Content.Load<Song>("Music/intro");


            //HUD
            LifeIcon = Main.Content.Load<Texture2D>("Sprite/icon_life");
            MissileIcon = Main.Content.Load<Texture2D>("Sprite/icon_missile");
            Font = Main.Content.Load<SpriteFont>("font");

            LoadSound();
        }

        private static void LoadSound()
        {
            Sounds.Add("bullet", Main.Content.Load<SoundEffect>("Sound/Bullet"));
            Sounds.Add("missileset", Main.Content.Load<SoundEffect>("Sound/missileset"));
            Sounds.Add("missilelaunch", Main.Content.Load<SoundEffect>("Sound/missilelaunch"));
            Sounds.Add("bullethit", Main.Content.Load<SoundEffect>("Sound/ennemybullethit"));
            Sounds.Add("powerup", Main.Content.Load<SoundEffect>("Sound/powerup"));

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
            //ROBINSON
            list[Main.Rand.Next(list.Count - 1)].Play();
        }
        #endregion
    }

   
}
