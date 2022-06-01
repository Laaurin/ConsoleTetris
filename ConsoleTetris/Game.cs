namespace ConsoleTetris;

public class Game
{
    public int[] _grid = new int[200];

    
    public void Play()
    {
        
        for (int i = 0; i < 200; i++)
        {
            _grid[i] = 0;
        }
        var playing = true;
        char key;

        while (playing)
        {
            Shape s = Spawn();
            Console.SetCursorPosition(25, 0);
            Console.Write(s.X);
            while (Fall(s))
            {
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true).KeyChar;

                    switch (key)
                    {
                        case 'd':
                            if (!SideFit(s, s.X+1, s.Y)) break;
                            s.X++;
                            break;
                        case 'a':
                            if (!SideFit(s, s.X-1, s.Y)) break;
                            s.X--;
                            break;
                        case 'w':
                            s.RotateLeft();
                            break;
                            
                    }
                    Console.SetCursorPosition(25, 0);
                    Console.Write(s.X);
                }
                Tetris();
                PrintGrid();
                PrintFalling(s);
                Thread.Sleep(700);
            }

            Thread.Sleep(400);
        }
    }

    private Shape Spawn()
    {
        
        var rnd = new Random();

        return rnd.Next(0, 7) switch
        {
            0 => new I(),
            1 => new J(),
            2 => new L(),
            3 => new O(),
            4 => new S(),
            5 => new T(),
            6 => new Z(),
            _ => null!
        };
    }
    
    private bool Fall(Shape s)
    {
        if (!Fits(s, s.X, s.Y + 1))
        {
            for (var i = 0; i < s.Len*s.Len; i++)
            {
                if (s[i] == s.Value)
                {
                    _grid[(s.Y + i / s.Len) * 10 + s.X + i % s.Len] = s.Value;
                }
            }

            return false;
        }

        s.Y++;
        return true;
    }
    
    private bool Fits(Shape s, int newX, int newY)
    {
        for (int i = 0; i < s.Len*s.Len; i++)
        { 
            if (s[i] == s.Value)
            {
                if ((newY+i/s.Len) * 10 + newX + i % s.Len > 199 || _grid[(newY+i/s.Len) * 10 + newX + i % s.Len] != 0)
                {
                    return false;
                }
                // grid[(newY+i/4) * 10 + newX + i % 4] = '#';
            }
        }
        
        return true;
    }

    private bool SideFit(Shape s, int newX, int newY)
    {
        for (var i = 0; i < s.Len*s.Len; i++)
        {
            if (s[i] != s.Value) continue;
            if (newX % s.Len < 0 || newX + i % s.Len > 9) return false;
        }
        return true;
    }

    public void PrintFalling(Shape s)
    {
        int cursorX = s.X, cursorY = s.Y;
        Console.ForegroundColor = GetColor(s.Value);
        for (int i = 0; i < s.Len * s.Len; i++)
        {
            if (i % s.Len == 0 && i != 0)
            {
                cursorY++;
                cursorX = s.X;
            }
            if (s[i] != 0)
            {
                Console.SetCursorPosition(cursorX *2, cursorY);
                Console.Write("██");
            }
            cursorX++;
        }

        Console.ForegroundColor = ConsoleColor.Black;
    }
    public void PrintGrid()
    {
        int cursorX = 0, cursorY = 0;
        Console.SetCursorPosition(0, 0);
        
        for (int i = 0; i < 200; i++)
        {
            if (i % 10 == 0 && i != 0)
            {
                cursorY++;
                cursorX = 0;
            }
            Console.SetCursorPosition(cursorX + i % 10, cursorY);
            if(_grid[i] == 0) Console.Write("  ");
            else
            {
                Console.ForegroundColor = GetColor(_grid[i]);
                Console.Write("██");
            }

            cursorX++;
        }

        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine();
        Console.WriteLine("####################");
    }

    public void PrintConsole()
    {
        Console.ForegroundColor = ConsoleColor.Black;
        int cursorX = 0, cursorY = 0;
        Console.SetCursorPosition(0, 0);
        
        for (int i = 0; i < 200; i++)
        {
            if (i % 10 == 0 && i!= 0)
            {
                cursorY++;
                cursorX = 0;
            }
            Console.SetCursorPosition(cursorX + i % 10, cursorY);
            Console.WriteLine(_grid[i]);

            cursorX++;
        }

        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("####################");
    }

    public void PrintFallingConsole(Shape s)
    {
        
        int cursorX = s.X * 2, cursorY = s.Y;
        Console.ForegroundColor = GetColor(s.Value);
        for (int i = 0; i < s.Len * s.Len; i++)
        {
            cursorX++;
            if (i % s.Len == 0)
            {
                cursorY++;
                cursorX = s.X;
            }
            if (s[i] != 0)
            {
                Console.SetCursorPosition(cursorX * 2, cursorY);
                Console.Write('#');
            }
            
        }

        //Console.ForegroundColor = ConsoleColor.Black;
    }

    private ConsoleColor GetColor(int value)
    {
        return value switch
        {
            1 => ConsoleColor.Yellow,
            2 => ConsoleColor.DarkYellow,
            3 => ConsoleColor.DarkBlue,
            4 => ConsoleColor.Cyan,
            5 => ConsoleColor.DarkMagenta,
            6 => ConsoleColor.Green,
            7 => ConsoleColor.Red,
            _ => ConsoleColor.Black
        };
    }

    public void Tetris()
    {
        bool tetris;
        bool empty;
        for (int i = 19; i > -1; i--)
        {
            tetris = true;
            empty = true;
            for (int j = 0; j < 10; j++)
            {
                if (_grid[i * 10 + j] != 0)
                {
                    empty = false;
                    continue;
                }
                tetris = false;
                break;
            }

            if (tetris)
            {
                for (int j = 0; j < 10; j++)
                {
                    _grid[i * 10 + j] = 0;
                }
                DropAll(i-1);
                i = 20;
            }

                
        }


    }

    public void DropAll(int row)
    {
        int count = 1;
        for (int i = row; i > -1; i--)
        {
            for (int j = 0; j < 10; j++)
            {
                if (_grid[i * 10 + j] != 0)
                {
                    count = 1;
                    for (int k = 0; k < 18 - i; k++)
                    {
                        if (_grid[i * 10 + j + 10 * count] != 0) break;
                        count++;
                        
                    }
                    _grid[i * 10 + j + 10 * count] = _grid[i * 10 + j];
                    _grid[i * 10 + j] = 0;
                }
            }
        }
    }
}