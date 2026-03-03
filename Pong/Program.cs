using Raylib_cs;
using System.Numerics;

class Pong
{
    static void Main()
    {
        int ruutuLeveys = 800;
        int ruutuKorkeus = 600;

        Raylib.InitWindow(ruutuLeveys, ruutuKorkeus, "Pong");
        Raylib.SetTargetFPS(60);

        Vector2 pelaaja1 = new Vector2(30, 250);
        Vector2 pelaaja2 = new Vector2(750, 250);

        int pisteet1 = 0;
        int pisteet2 = 0;

        Vector2 pisteet1Paikka = new Vector2(200, 20);
        Vector2 pisteet2Paikka = new Vector2(600, 20);

        int mailanLeveys = 20;
        int mailanKorkeus = 100;
        int mailanNopeus = 5;

        Vector2 pallo = new Vector2(ruutuLeveys / 2, ruutuKorkeus / 2);

        Vector2 pallonSuunta = new Vector2(1, 1);
        int pallonNopeus = 5;

        while (!Raylib.WindowShouldClose())
        {

            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                pelaaja1.Y -= mailanNopeus;
            }

            if (Raylib.IsKeyDown(KeyboardKey.S))
            {
                pelaaja1.Y += mailanNopeus;
            }

            if (Raylib.IsKeyDown(KeyboardKey.Up))
            {
                pelaaja2.Y -= mailanNopeus;
            }

            if (Raylib.IsKeyDown(KeyboardKey.Down))
            {
                pelaaja2.Y += mailanNopeus;
            }


            if (pelaaja1.Y < 0)
                pelaaja1.Y = 0;

            if (pelaaja1.Y > ruutuKorkeus - mailanKorkeus)
                pelaaja1.Y = ruutuKorkeus - mailanKorkeus;

            if (pelaaja2.Y < 0)
                pelaaja2.Y = 0;

            if (pelaaja2.Y > ruutuKorkeus - mailanKorkeus)
                pelaaja2.Y = ruutuKorkeus - mailanKorkeus;


            pallo += pallonSuunta * pallonNopeus;


            if (pallo.Y <= 0)
                pallonSuunta.Y = -pallonSuunta.Y;

            if (pallo.Y >= ruutuKorkeus)
                pallonSuunta.Y = -pallonSuunta.Y;


            if (pallo.X >= pelaaja1.X && pallo.X <= pelaaja1.X + mailanLeveys &&
                pallo.Y >= pelaaja1.Y && pallo.Y <= pelaaja1.Y + mailanKorkeus)
            {
                pallonSuunta.X = -pallonSuunta.X;
            }

            if (pallo.X >= pelaaja2.X && pallo.X <= pelaaja2.X + mailanLeveys &&
                pallo.Y >= pelaaja2.Y && pallo.Y <= pelaaja2.Y + mailanKorkeus)
            {
                pallonSuunta.X = -pallonSuunta.X;
            }


            if (pallo.X < 0)
            {
                pisteet2++;
                pallo = new Vector2(ruutuLeveys / 2, ruutuKorkeus / 2);
            }

            if (pallo.X > ruutuLeveys)
            {
                pisteet1++;
                pallo = new Vector2(ruutuLeveys / 2, ruutuKorkeus / 2);
            }


            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.Black);

            Raylib.DrawRectangle((int)pelaaja1.X, (int)pelaaja1.Y, mailanLeveys, mailanKorkeus, Color.White);
            Raylib.DrawRectangle((int)pelaaja2.X, (int)pelaaja2.Y, mailanLeveys, mailanKorkeus, Color.White);

            Raylib.DrawCircle((int)pallo.X, (int)pallo.Y, 5, Color.White);

            Raylib.DrawText(pisteet1.ToString(), (int)pisteet1Paikka.X, (int)pisteet1Paikka.Y, 40, Color.White);
            Raylib.DrawText(pisteet2.ToString(), (int)pisteet2Paikka.X, (int)pisteet2Paikka.Y, 40, Color.White);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}