using System.Numerics;
using Raylib_cs;

internal class Program
{
    Texture2D shipTexture;
    Vector2 shipPosition;
    Vector2 shipVelocity;
    Vector2 engineForce;
    Vector2 gravityForce;
    void run()
    {
        Raylib.InitWindow(600, 600, "Lunar");
        Texture2D texture = Raylib.LoadTexture("Player.png");
        shipPosition = Raylib.GetScreenCenter();
        shipVelocity = Vector2.Zero;
        engineForce = new Vector2(0, -10);
        gravityForce= new Vector2(0, 3);
        while (Raylib.WindowShouldClose()== false) 
        {
            Vector2 acceleration = gravityForce;
            float delta = Raylib.GetFrameTime();
            shipVelocity += acceleration * delta;
            shipPosition += shipVelocity * delta;

            if (Raylib.IsKeyDown(KeyboardKey.Up))
            { 
                acceleration += engineForce;
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Gray);
            Raylib.DrawTextureV(texture, shipPosition, Color.White);



  
            Raylib.EndDrawing();

        }
        Raylib.CloseWindow();
    }
    static void Main(string[] args)
    {
        Program LunarGame = new Program();
        LunarGame.run();
    }
}