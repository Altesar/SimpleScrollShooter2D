using System;
using OpenTK;

namespace ScrollShooter2D.util
{
    public enum BoxCorner
    {
        TopLeft = 0,
        BotRight
    }
    
    public class ColliderBox
    {
        public Vector2 Size { get; set; }
        public Vector2 Position { get; set; }
        public bool Enabled { get; set; }

        public Vector2[] Corners => new[]
            { new Vector2(Position.X, Position.Y), new Vector2(Position.X + Size.X, Position.Y + Size.Y) };

        public ColliderBox(Vector2 size, Vector2 position, bool enabled)
        {
            Size = size;
            Position = position;
            Enabled = enabled;
        }

        //TODO: Method using line intersection
        
        public static bool CollidingAABB(ColliderBox boxA, ColliderBox boxB)
        {
            return !(boxA.Corners[(int)BoxCorner.BotRight].X < boxB.Corners[(int)BoxCorner.TopLeft].X ||
                     boxB.Corners[(int)BoxCorner.BotRight].X < boxA.Corners[(int)BoxCorner.TopLeft].X ||
                     boxA.Corners[(int)BoxCorner.BotRight].Y < boxB.Corners[(int)BoxCorner.TopLeft].Y ||
                     boxB.Corners[(int)BoxCorner.BotRight].Y < boxA.Corners[(int)BoxCorner.TopLeft].Y );
        }
    }
}