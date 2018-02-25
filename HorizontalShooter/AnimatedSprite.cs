//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HorizontalShooter
//{
//    public class AnimatedSprite : Sprite
//    {
//        public int FrameWidth, FrameHeight, Frame, FrameNumber;
//        public float AnimationTimer, Delay;
//        public bool Loop, InAnim, BackTo;

//        public Rectangle FrameBox
//        {
//            get { return new Rectangle(Frame * FrameWidth, 0, FrameWidth, FrameHeight); }
//        }

//        public new Rectangle Hitbox
//        {
//            get { return new Rectangle((int)Position.X, (int)Position.Y, FrameWidth, FrameHeight); }
//        }

//        public AnimatedSprite(Texture2D _texture, Vector2 _position, int frameWidth, int frameHeight, int frameNumber)
//            : base(_texture, _position)
//        {
//            Frame = 0;
//            FrameWidth = frameWidth;
//            FrameHeight = frameHeight;
//            FrameNumber = frameNumber;
//        }

//        public override void Update(float time)
//        {
//            if (InAnim)
//            {
//                AnimationTimer += time;
//                if (AnimationTimer >= Delay)
//                {
//                    Anim();
//                }
//            }
//        }

//        private void Anim()
//        {
//            if (Frame == FrameNumber - 1)
//            {
//                if (Loop)
//                    Frame = 0;
//                else
//                {
//                    if (BackTo)
//                        Frame = 0;
//                    InAnim = false;
//                }
//            }
//            else
//                Frame++;

//            AnimationTimer = 0;
//        }

//        public void Animate(bool loop, float delay, bool backto)
//        {
//            Loop = loop;
//            Delay = delay;
//            BackTo = backto;
//            Anim();
//            InAnim = true;
//        }

//        public override void Draw(SpriteBatch batch)
//        {
//            batch.Draw(Texture, Position, FrameBox, Color.White);
//        }
//    }
//}
