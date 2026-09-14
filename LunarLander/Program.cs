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

        int screenMiddleX = Raylib.GetScreenWidth() / 2;

        Vector2 shipPosition = new Vector2(screenMiddleX, 100);
        Vector2 shipVelocity = Vector2.Zero;
        Vector2 engineForce = new Vector2(0, -1500);
        Vector2 gravityForce = new Vector2(0, 750);

        float landingWidth = 220;
        float landingHeight = 20;
        float landing_top = Raylib.GetScreenHeight() - landingHeight;

        float crash_limit = 200;
        bool shipLanded = false;
        bool shipExploded = false;
            
        float fuel = 100f;
        float fuelConsumption = 50f;

        Raylib.InitAudioDevice();

        Sound win = Raylib.LoadSound("Win effect.mp3");
        Sound lose = Raylib.LoadSound("Lose.mp3");

        while (Raylib.WindowShouldClose() == false && Raylib.IsAudioDeviceReady())
        {
            float delta = Raylib.GetFrameTime();

            if (!shipLanded && !shipExploded)
            {
                Vector2 acceleration = gravityForce;

                bool isThrusting = Raylib.IsKeyDown(KeyboardKey.Up) && fuel > 0;

                if (isThrusting)
                {
                    acceleration += engineForce;     
                    fuel -= fuelConsumption * delta;  

                    if (fuel < 0)
                        fuel = 0;                     
                }

                if (Raylib.IsKeyDown(KeyboardKey.Up))
                {
                    acceleration += engineForce;
                }

                shipVelocity += acceleration * delta;



                float maxvelocity = 1250;
                if (shipVelocity.Length() > maxvelocity)
                {
                    shipVelocity = Vector2.Normalize(shipVelocity) * maxvelocity;
                }

                shipPosition += shipVelocity * delta;

                float ship_bottom = shipPosition.Y + texture.Height / 2f;

                if (ship_bottom > landing_top)
                {
                    shipPosition.Y = landing_top - texture.Height / 2f;

                    if (shipVelocity.Length() > crash_limit)
                    {
                        shipExploded = true;
                    }
                    else
                    {
                        shipLanded = true;
                    }

                    shipVelocity = Vector2.Zero;
                }
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.DarkPurple);

            Raylib.DrawRectangle(
                (int)(screenMiddleX - landingWidth / 2),
                (int)landing_top,
                (int)landingWidth,
                (int)landingHeight,
                Color.White);

            Raylib.DrawText($"Fuel: {(int)fuel}", 10, 10, 20, Color.White);

            if (shipExploded)
            {
                Raylib.DrawText("CRASHED!", screenMiddleX - 60, 250, 30, Color.Red);
                if(!Raylib.IsSoundPlaying(lose))
                { 
                    Raylib.PlaySound(lose);
                }

            }
            else if (shipLanded)
            {
                Raylib.DrawText("LANDED!", screenMiddleX - 60, 250, 30, Color.Green);
                if (!Raylib.IsSoundPlaying(win))
                {
                    Raylib.PlaySound(win);
                }
                Raylib.DrawTextureV(texture, shipPosition - new Vector2(texture.Width / 2f, texture.Height / 2f), Color.White);
            }
            else
            {
                Raylib.DrawTextureV(texture, shipPosition - new Vector2(texture.Width / 2f, texture.Height / 2f), Color.White);
            }

            Raylib.EndDrawing();

        }
        Raylib.UnloadSound(win);
        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }

    static void Main(string[] args)
    {
        Program LunarGame = new Program();
        LunarGame.run();
    }
}