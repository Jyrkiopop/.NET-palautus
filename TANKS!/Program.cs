using Raylib_cs;
using System.Numerics;

class Program
{
    struct Tank
    {
        public Vector2 Position;
        public Vector2 Direction;
        public float Speed;
        public Color Color;
        public double LastShotTime;
    }

    struct Bullet
    {
        public Vector2 Position;
        public Vector2 Direction;
        public float Speed;
        public bool Active;
    }

    static Rectangle GetTankRect(Tank t)
    {
        return new Rectangle(t.Position.X - 30, t.Position.Y - 30, 60, 60);
    }

    static void DrawTank(Tank tank)
    {
        Vector2 tankSize = new Vector2(60, 60);
        Vector2 turretSize = new Vector2(20, 20);

        Vector2 topLeft = tank.Position - tankSize / 2f;
        Raylib.DrawRectangleV(topLeft, tankSize, tank.Color);

        Vector2 turretPos = tank.Position + tank.Direction * 40;
        Vector2 turretTopLeft = turretPos - turretSize / 2f;

        Raylib.DrawRectangleV(turretTopLeft, turretSize, Color.Black);
    }

    static void Main()
    {
        Raylib.InitWindow(1000, 700, "Tanks");
        Raylib.SetTargetFPS(60);

        Tank tank1 = new Tank
        {
            Position = new Vector2(200, 350),
            Direction = new Vector2(1, 0),
            Speed = 3,
            Color = Color.Green,
            LastShotTime = 0
        };

        Tank tank2 = new Tank
        {
            Position = new Vector2(800, 350),
            Direction = new Vector2(-1, 0),
            Speed = 3,
            Color = Color.Red,
            LastShotTime = 0
        };

        Bullet bullet1 = new Bullet();
        Bullet bullet2 = new Bullet();

        int score1 = 0;
        int score2 = 0;

        Rectangle wall = new Rectangle(450, 200, 100, 300);

        while (!Raylib.WindowShouldClose())
        {
            double time = Raylib.GetTime();

            Vector2 oldPos1 = tank1.Position;

            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                tank1.Position.Y -= tank1.Speed;
                tank1.Direction = new Vector2(0, -1);
            }
            else if (Raylib.IsKeyDown(KeyboardKey.S))
            {
                tank1.Position.Y += tank1.Speed;
                tank1.Direction = new Vector2(0, 1);
            }
            else if (Raylib.IsKeyDown(KeyboardKey.A))
            {
                tank1.Position.X -= tank1.Speed;
                tank1.Direction = new Vector2(-1, 0);
            }
            else if (Raylib.IsKeyDown(KeyboardKey.D))
            {
                tank1.Position.X += tank1.Speed;
                tank1.Direction = new Vector2(1, 0);
            }

            if (Raylib.CheckCollisionRecs(GetTankRect(tank1), wall))
                tank1.Position = oldPos1;

            Vector2 oldPos2 = tank2.Position;

            if (Raylib.IsKeyDown(KeyboardKey.Up))
            {
                tank2.Position.Y -= tank2.Speed;
                tank2.Direction = new Vector2(0, -1);
            }
            else if (Raylib.IsKeyDown(KeyboardKey.Down))
            {
                tank2.Position.Y += tank2.Speed;
                tank2.Direction = new Vector2(0, 1);
            }
            else if (Raylib.IsKeyDown(KeyboardKey.Left))
            {
                tank2.Position.X -= tank2.Speed;
                tank2.Direction = new Vector2(-1, 0);
            }
            else if (Raylib.IsKeyDown(KeyboardKey.Right))
            {
                tank2.Position.X += tank2.Speed;
                tank2.Direction = new Vector2(1, 0);
            }

            if (Raylib.CheckCollisionRecs(GetTankRect(tank2), wall))
                tank2.Position = oldPos2;

           
            if (Raylib.IsKeyPressed(KeyboardKey.Space) && time - tank1.LastShotTime > 1)
            {
                bullet1.Position = tank1.Position;
                bullet1.Direction = tank1.Direction;
                bullet1.Speed = 6;
                bullet1.Active = true;
                tank1.LastShotTime = time;
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Enter) && time - tank2.LastShotTime > 1)
            {
                bullet2.Position = tank2.Position;
                bullet2.Direction = tank2.Direction;
                bullet2.Speed = 6;
                bullet2.Active = true;
                tank2.LastShotTime = time;
            }

            if (bullet1.Active)
            {
                bullet1.Position += bullet1.Direction * bullet1.Speed;

                if (Raylib.CheckCollisionPointRec(bullet1.Position, wall))
                    bullet1.Active = false;

                if (Raylib.CheckCollisionPointRec(bullet1.Position, GetTankRect(tank2)))
                {
                    score1++;
                    bullet1.Active = false;

                    tank1.Position = new Vector2(200, 350);
                    tank2.Position = new Vector2(800, 350);
                }
            }

            if (bullet2.Active)
            {
                bullet2.Position += bullet2.Direction * bullet2.Speed;

                if (Raylib.CheckCollisionPointRec(bullet2.Position, wall))
                    bullet2.Active = false;

                if (Raylib.CheckCollisionPointRec(bullet2.Position, GetTankRect(tank1)))
                {
                    score2++;
                    bullet2.Active = false;

                    tank1.Position = new Vector2(200, 350);
                    tank2.Position = new Vector2(800, 350);
                }
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.DarkGreen);

            Raylib.DrawRectangleRec(wall, Color.Gray);

            DrawTank(tank1);
            DrawTank(tank2);

            if (bullet1.Active)
                Raylib.DrawCircleV(bullet1.Position, 5, Color.Black);

            if (bullet2.Active)
                Raylib.DrawCircleV(bullet2.Position, 5, Color.Black);

            Raylib.DrawText($"Player 1: {score1}", 20, 20, 20, Color.Green);
            Raylib.DrawText($"Player 2: {score2}", 800, 20, 20, Color.Red);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}