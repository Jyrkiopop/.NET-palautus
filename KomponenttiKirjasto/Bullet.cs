using Raylib_cs;
using System.Numerics;

namespace KomponenttiKirjasto

{
    public class Bullet
    {
        public Vector2 Position;
        public Vector2 Direction;
        public float Speed;

        public Bullet(Vector2 position, Vector2 direction, float speed)
        {
            Position = position;
            Direction = direction;
            Speed = speed;
        }

        public void Move(float deltaTime)
        {
            Position += Direction * Speed * deltaTime;
        }

        public bool IsInsideScreen()
        {
            return Position.X >= 0 &&
                   Position.X <= Raylib.GetScreenWidth() &&
                   Position.Y >= 0 &&
                   Position.Y <= Raylib.GetScreenHeight();
        }
    }
}