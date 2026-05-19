using Raylib_cs;
using System.Numerics;

class Tank
{
    public Vector2 Position;
    public Vector2 Direction;
    public float Speed;
    public Color Color;
    public double LastShotTime;

    public const float Size = 60;

    public Tank(Vector2 position, Vector2 direction, float speed, Color color)
    {
        Position = position;
        Direction = direction;
        Speed = speed;
        Color = color;
        LastShotTime = 0;
    }
}

class Bullet
{
    public Vector2 Position;
    public Vector2 Direction;
    public float Speed;
    public bool Active;

    public const float Radius = 5;
    public const float DefaultSpeed = 105;

    public Bullet()
    {
        Active = false;
    }
}

class Program
{
    static Rectangle GetTankRect(Tank t)
    {
        return new Rectangle(
            t.Position.X - Tank.Size / 2,
            t.Position.Y - Tank.Size / 2,
            Tank.Size,
            Tank.Size
        );
    }

    static void DrawTank(Tank tank)
    {
        Vector2 tankSize = new Vector2(Tank.Size, Tank.Size);
        Vector2 turretSize = new Vector2(20, 20);

        Vector2 topLeft = tank.Position - tankSize / 2;
        Raylib.DrawRectangleV(topLeft, tankSize, tank.Color);

        Vector2 turretPos = tank.Position + tank.Direction * 40;
        Vector2 turretTopLeft = turretPos - turretSize / 2;

        Raylib.DrawRectangleV(turretTopLeft, turretSize, Color.Black);
    }

    static void Main()
    {
        Raylib.InitWindow(1000, 700, "Tanks");
        Raylib.SetTargetFPS(60);

        Tank tank1 = new Tank(new Vector2(200, 350), new Vector2(1, 0), 80, Color.Green);
        Tank tank2 = new Tank(new Vector2(800, 350), new Vector2(-1, 0), 80, Color.Red);

        Bullet bullet1 = new Bullet();
        Bullet bullet2 = new Bullet();

        int score1 = 0;
        int score2 = 0;

        Rectangle wall = new Rectangle(450, 200, 100, 300);

        while (!Raylib.WindowShouldClose())
        {
            float dt = Raylib.GetFrameTime();
            double time = Raylib.GetTime();

            
            Vector2 oldPos1 = tank1.Position;

            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                tank1.Position.Y -= tank1.Speed * dt;
                tank1.Direction = new Vector2(0, -1);
            }
            else if (Raylib.IsKeyDown(KeyboardKey.S))
            {
                tank1.Position.Y += tank1.Speed * dt;
                tank1.Direction = new Vector2(0, 1);
            }
            else if (Raylib.IsKeyDown(KeyboardKey.A))
            {
                tank1.Position.X -= tank1.Speed * dt;
                tank1.Direction = new Vector2(-1, 0);
            }
            else if (Raylib.IsKeyDown(KeyboardKey.D))
            {
                tank1.Position.X += tank1.Speed * dt;
                tank1.Direction = new Vector2(1, 0);
            }

            if (Raylib.CheckCollisionRecs(GetTankRect(tank1), wall))
                tank1.Position = oldPos1;

            
            Vector2 oldPos2 = tank2.Position;

            if (Raylib.IsKeyDown(KeyboardKey.Up))
            {
                tank2.Position.Y -= tank2.Speed * dt;
                tank2.Direction = new Vector2(0, -1);
            }
            else if (Raylib.IsKeyDown(KeyboardKey.Down))
            {
                tank2.Position.Y += tank2.Speed * dt;
                tank2.Direction = new Vector2(0, 1);
            }
            else if (Raylib.IsKeyDown(KeyboardKey.Left))
            {
                tank2.Position.X -= tank2.Speed * dt;
                tank2.Direction = new Vector2(-1, 0);
            }
            else if (Raylib.IsKeyDown(KeyboardKey.Right))
            {
                tank2.Position.X += tank2.Speed * dt;
                tank2.Direction = new Vector2(1, 0);
            }

            if (Raylib.CheckCollisionRecs(GetTankRect(tank2), wall))
                tank2.Position = oldPos2;

            
            if (Raylib.IsKeyPressed(KeyboardKey.Space) && time - tank1.LastShotTime > 1)
            {
                bullet1.Position = tank1.Position;
                bullet1.Direction = tank1.Direction;
                bullet1.Speed = Bullet.DefaultSpeed;
                bullet1.Active = true;
                tank1.LastShotTime = time;
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Enter) && time - tank2.LastShotTime > 1)
            {
                bullet2.Position = tank2.Position;
                bullet2.Direction = tank2.Direction;
                bullet2.Speed = Bullet.DefaultSpeed;
                bullet2.Active = true;
                tank2.LastShotTime = time;
            }

            
            if (bullet1.Active)
                bullet1.Position += bullet1.Direction * bullet1.Speed * dt;

            if (bullet2.Active)
                bullet2.Position += bullet2.Direction * bullet2.Speed * dt;

            
            if (bullet1.Active)
            {
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
                Raylib.DrawCircleV(bullet1.Position, Bullet.Radius, Color.Black);

            if (bullet2.Active)
                Raylib.DrawCircleV(bullet2.Position, Bullet.Radius, Color.Black);

            Raylib.DrawText($"Player 1: {score1}", 20, 20, 20, Color.Green);
            Raylib.DrawText($"Player 2: {score2}", 800, 20, 20, Color.Red);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}