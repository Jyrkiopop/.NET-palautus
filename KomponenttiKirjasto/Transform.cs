using Raylib_cs;
using System.Numerics;

namespace KomponenttiKirjasto

{
    public class Transform
    {
        public Vector2 position;
        public Vector2 direction;
        public float speed;

        public Transform(Vector2 position, Vector2 direction, float speed)
        {
            this.position = position;
            this.direction = direction;
            this.speed = speed;
        }

        public Transform(Vector2 position) : this(position, Vector2.UnitX, 0.0f)
        { }

        public Transform() : this(Raylib.GetScreenCenter())
        { }
        public void Move(float deltaTime)
        {
            position += direction * speed * deltaTime;
        }

        public void Turn(float degrees)
        {
            Matrix3x2 rotation = Matrix3x2.CreateRotation(Raylib.DEG2RAD * degrees);
            direction = Vector2.Transform(direction, rotation);
        }
        public bool IsInsideScreen()
        {
            return (position.X >= 0 && position.X < Raylib.GetScreenWidth()
              && position.Y >= 0 && position.Y < Raylib.GetScreenHeight());
        }
        public void MoveWithKeyboard(KeyboardKey upKey, KeyboardKey downKey, KeyboardKey leftKey, KeyboardKey rightKey, float deltaTime)
        {
            if (Raylib.IsKeyDown(upKey))
            {
                direction = new Vector2(0, -1);
            }
            else if (Raylib.IsKeyDown(downKey))
            {
                direction = new Vector2(0, 1);
            }
            if (Raylib.IsKeyDown(leftKey))
            {
                direction = new Vector2(-1, 0);
            }
            else if (Raylib.IsKeyDown(rightKey))
            {
                direction = new Vector2(1, 0);
            }
            Move(deltaTime);
        }
    }
}