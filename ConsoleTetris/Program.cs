using System;
using System.Threading.Tasks.Dataflow;

namespace ConsoleTetris;

class Programm
{
    public static void Main(String[] args)
    {
        Game game = new Game();
        L i = new L();
        i.X = 0;
        i.Y = 0;

        game._grid = new[]
        {
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 4, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 4, 0, 0, 0, 0, 0, 0,
            0, 0, 3, 4, 0, 0, 0, 0, 0, 0,
            0, 5, 3, 4, 5, 2, 6, 4, 3, 2,
        };
        game.BotPlay();
        //game.Play();
        
        i.X = game.BotMove(i);
        Console.WriteLine(i.X);
        Console.ReadKey();
        while (game.Fall(i, game._grid))
        {
            game.PrintFalling(i);
            Thread.Sleep(150);
        }
        game.PrintGrid(game._grid);
        //game.Play();
        // game.PrintGrid();
        // game.DropAll(19);
        // game.PrintGrid();
    }
}