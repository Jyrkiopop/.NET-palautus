using System;
using System.Numerics;
using Raylib_cs;
using Color = Raylib_cs.Color;

class Program
{
    static void Main()
    {
        Raylib.InitWindow(800, 800, "");
        Raylib.SetTargetFPS(60);

        Vector2 pointA = new Vector2(Raylib.GetScreenWidth() / 2f, 0f);
        Vector2 pointB = new Vector2(0f, Raylib.GetScreenHeight() / 2f);
        Vector2 pointC = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight() / 0.75f);

        Vector2 velocityA = new Vector2(1, 1);
        Vector2 velocityB = new Vector2(1, -1);
        Vector2 velocityC = new Vector2(-1, 1);

        float moveSpeed = 400f;

        while (!Raylib.WindowShouldClose())
        {
            float dt = Raylib.GetFrameTime();
            int screenWidth = Raylib.GetScreenWidth();
            int screenHeight = Raylib.GetScreenHeight();

            pointA += velocityA * moveSpeed * dt;
            pointB += velocityB * moveSpeed * dt;
            pointC += velocityC * moveSpeed * dt;

            if (pointA.X < 0 || pointA.X > screenWidth) velocityA.X *= -1;
            if (pointA.Y < 0 || pointA.Y > screenHeight) velocityA.Y *= -1;

            if (pointB.X < 0 || pointB.X > screenWidth) velocityB.X *= -1;
            if (pointB.Y < 0 || pointB.Y > screenHeight) velocityB.Y *= -1;

            if (pointC.X < 0 || pointC.X > screenWidth) velocityC.X *= -1;
            if (pointC.Y < 0 || pointC.Y > screenHeight) velocityC.Y *= -1;

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.DrawLineV(pointA, pointB, Color.Green);
            Raylib.DrawLineV(pointB, pointC, Color.Yellow);
            Raylib.DrawLineV(pointC, pointA, Color.Blue);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}