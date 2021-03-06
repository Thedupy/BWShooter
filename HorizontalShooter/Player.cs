﻿using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Microsoft.Xna.Framework.Input; using System.Collections.Generic;  namespace HorizontalShooter {     public class Player : Sprite     {         public List<Bullet> Bullets;         //TIR         float CDShoot = 500, TimerShoot;         //MISSILE         public static int MissileCount;         public float MissileTimer;         internal static int LifeCount;          //INVINCIBILITE         float InvincTimer;         public bool Touched;           public Player(Vector2 position) : base(Assets.Ship, position, true)         {             Bullets = new List<Bullet>();             MissileCount = 3;             LifeCount = 3;         }          public override void Update(float time)         {             base.Update(time);             if(MissileCount < 3)
            {
                MissileTimer += time;
                if(MissileTimer >= 5000)
                {
                    MissileCount++;
                    MissileTimer = 0;
                }
            }             if (Touched)
            {
                InvincTimer += time;
                Visible = (InvincTimer % 200 == 0);
                if (InvincTimer >= 2000)
                {
                    Touched = false;
                    InvincTimer = 0;
                }
            }             else
                Visible = true;             //MOUVEMENT             if (Input.KeyPressed(Keys.Z, false))             {                 Velocity.Y = -5;             }             else if (Input.KeyPressed(Keys.S, false))             {                 Velocity.Y = 5;             }             else                 Velocity.Y = 0;              //TIR BALLE             TimerShoot += time;              if (Input.KeyPressed(Keys.Space, false))             {                 if (TimerShoot >= CDShoot)                 {                     Bullets.Add(new GunShoot(new Vector2(Position.X + Texture.Width + Velocity.X + 5, Position.Y + (Texture.Height / 2))));                     //ROBINSON                                       Assets.Sounds["bullet"].Play(0.5f,0f,0f);                      TimerShoot = 0;                                      }             }              //TIR MISSILE             if (Input.KeyPressed(Keys.E, true))             {                 if (MissileCount > 0)                 {                     //ROBINSON                     Assets.PlayRandomSound(Assets.MissileSound);                     Bullets.Add(new Missile(new Vector2(Position.X, Position.Y + Velocity.Y)));                     MissileCount--;                 }             }              //CLAMP POSITION             Position = Vector2.Clamp(Position, Vector2.Zero, new Vector2(Main.Width, HUD.MaxHUD - Texture.Height));                          //UPDATE BALL             for (int i = 0; i < Bullets.Count; i++)             {                 Bullets[i].Update(time);             }             Bullets.RemoveAll(k => k.Position.X > Main.Width);         }          public override void Draw(SpriteBatch batch)         {             if (Visible)
            {
                Assets.BlackWhite.Parameters["param1"].SetValue(DualityValue);
                Assets.BlackWhite.CurrentTechnique.Passes[0].Apply();
                base.Draw(batch);
            }             foreach (var item in Bullets)             {                 item.Draw(batch);             }         }     } } 