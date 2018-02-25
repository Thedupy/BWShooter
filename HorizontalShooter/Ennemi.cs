using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorizontalShooter
{
    public class Ennemi : Sprite
    {
        public bool Targeted;
        public Color Color;
        public float StartPositionY;
        public TweenPosition TPosition;
        
        

        //EFFECT
        bool Effect;


        public Ennemi(Vector2 position, Color color) : base(Utils.CreateTexture(40,40, Color.White), position, false)
        {
            Color = color;
            StartPositionY = position.Y;
        }

        public override void Update(float time)
        {
            base.Update(time);
               
        }

        new public void Draw(SpriteBatch batch)
        {
            //base.Draw(batch);
            batch.Draw(Texture, Position, Color);
        }

    }

    public class PathEnnemi : Ennemi
    {
        //PATH
        float PathTimer;
        int PathIndex;
        Vector2[] Path;

        public PathEnnemi(Vector2 position, Color color, Vector2[] path) : base(position, color)
        {
            Color = color;
            StartPositionY = position.Y;
            Path = path;
            TPosition = new TweenPosition(this);
        }

        public override void Update(float time)
        {
            TPosition.Update(time, ref Position);
            PathTimer += time;
            if (PathTimer > 1500)
            {
                TPosition.Move(Path[PathIndex], 1000, EaseFunction.EaseInOutQuad);
                if (PathIndex == Path.Length - 1)
                    PathIndex = 0;
                else
                    PathIndex++;

                PathTimer = 0;
            }
        }

    }

    public class NormalEnnemi : Ennemi
    {
        public NormalEnnemi(Vector2 position, Color color) : base(position, color)
        {

        }

        public override void Update(float time)
        {
            Velocity.X = -2;
        }

    }

    public class SineEnnemi : Ennemi
    {
        //SIN
        int Sin;

        public SineEnnemi(Vector2 position, Color color, int sin) : base(position, color)
        {
            Color = color;
            StartPositionY = position.Y;
            Sin = sin;
        }

        public override void Update(float time)
        {
            Position.Y = StartPositionY + (-(float)Math.Cos(Position.X / 100) * Sin);
            Velocity.X = -2;
        }

    }
}
