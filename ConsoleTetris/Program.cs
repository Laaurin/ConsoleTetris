using System;
using System.Threading.Tasks.Dataflow;

namespace ConsoleTetris
{
   class Programm
   {
       public static void Main(String[] args)
       {
           Game game = new Game();
           var i = new I();
           i.X = 0;
           i.Y = 0;
   
           // game._grid = new[]
           // {
           //     0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
           //     0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
           //     0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
           //     0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
           //     0, 5, 0, 0, 0, 5, 0, 0, 0, 0,
           //     5, 0, 5, 5, 5, 5, 5, 5, 5, 5,
           //     0, 5, 0, 0, 0, 0, 5, 0, 0, 0,
           //     0, 4, 0, 0, 0, 0, 0, 0, 0, 0,
           //     0, 4, 0, 0, 0, 0, 0, 0, 0, 0,
           //     0, 4, 0, 0, 0, 0, 0, 5, 0, 0,
           //     0, 5, 5, 0, 5, 5, 5, 0, 3, 3,
           //     4, 0, 0, 5, 0, 0, 0, 0, 0, 0,
           //     0, 4, 0, 0, 0, 0, 0, 0, 0, 0,
           //     0, 4, 0, 0, 0, 0, 0, 0, 0, 0,
           //     0, 4, 0, 0, 0, 0, 0, 0, 0, 0,
           //     0, 4, 0, 4, 0, 0, 6, 4, 0, 2,
           //     0, 0, 3, 4, 5, 0, 6, 4, 3, 2,
           //     4, 0, 3, 4, 5, 0, 6, 4, 3, 2,
           //     0, 5, 3, 4, 5, 2, 6, 4, 3, 2,
           //     0, 5, 3, 4, 5, 2, 6, 4, 3, 2,
           // };
   
           game.BotPlay();
           
           i.X = game.BotMove(i);
           while (game.Fall(i, game._grid))
           {
               
           }
           game.PrintGrid(game._grid);
           Console.ReadKey();
           
           
           //game.Play();
       }
   } 
}

