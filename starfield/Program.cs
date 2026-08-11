using Raylib_cs;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
namespace starfield

{
    internal class Program
    {
        static void Main(string[] args)
        {
            Raylib.InitWindow(800, 600, "starfield");
            Star[] stars = new Star[400];
            Raylib.SetTargetFPS(60);
            Random rng = new Random();
            Color[] palette = new Color[]
            {
                Color.Red,
                Color.Purple,
                Color.Yellow
            };
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i] = new Star();
                stars[i].position.X = rng.Next(-5,Raylib.GetScreenWidth());
                stars[i].position.Y = rng.Next(-5, Raylib.GetScreenWidth());
            }
            {
                while (!Raylib.WindowShouldClose())
                {

                    Raylib.BeginDrawing();
                    Raylib.ClearBackground(Color.Black);
                    for (int i = 0; i < stars.Length; i++)
                    {
                        stars[i] += 200 * Raylib.GetFrameTime();

                        if (stars[i] > Raylib.GetScreenWidth())
                        {
                            stars[i] = rng.Next(-5, Raylib.GetScreenWidth());
                        }
                        Raylib.DrawRectangle((int)stars[i], i*10, 10, 10, Color.Yellow);
                    }
                    Raylib.EndDrawing();
                }
            }
        }
    }
}