﻿using System;
using System.Threading.Tasks.Dataflow;

namespace ConsoleTetris;

class Programm
{
    public static void Main(String[] args)
    {
        Game game = new Game();
        J i = new J();
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
            0, 5, 3, 4, 5, 2, 6, 4, 3, 2,
            0, 5, 3, 4, 5, 2, 6, 4, 3, 2,
            0, 5, 3, 4, 5, 2, 6, 4, 3, 2,
            0, 5, 3, 4, 5, 2, 6, 4, 3, 2,
            0, 5, 3, 4, 5, 2, 6, 4, 3, 2,
        };
        
        
        game.BotPlay();
        //game.Play();
    }
}