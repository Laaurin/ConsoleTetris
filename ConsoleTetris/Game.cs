using System.Threading.Channels;

namespace ConsoleTetris;

public class Game
{
    public int[] _grid = new int[200];

    
    public void Play()
    {
        var playing = true;
        char key;

        while (playing)
        {
            Shape s = Spawn();
            Console.SetCursorPosition(25, 0);
            Console.Write(s.X);
            while (Fall(s, _grid))
            {
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true).KeyChar;

                    switch (key)
                    {
                        case 'd':
                            if (!SideFit(s, s.X+1, s.Y, _grid)) break;
                            s.X++;
                            break;
                        case 'a':
                            if (!SideFit(s, s.X-1, s.Y, _grid)) break;
                            s.X--;
                            break;
                        case 'w':
                            s.RotateLeft();
                            break;
                        case 's':
                            continue;
                        
                            
                    }
                    Console.SetCursorPosition(25, 0);
                    Console.Write(s.X);
                }
                Tetris();
                PrintGrid(_grid);
                PrintFalling(s);
                Thread.Sleep(700);
            }

            Thread.Sleep(400);
        }
    }

    public void BotPlay()
    {
        var score = 0; //killed lines
        var playing = true;
        while (playing)
        {
            Shape s = Spawn();
            s.X = BotMove(s);
            Console.SetCursorPosition(25, 0);
            Console.Write(s.X);
            
            while (Fall(s, _grid))
            { 
                PrintGrid(_grid);
                PrintFalling(s);
                Thread.Sleep(10);
            }
            score+=Tetris();

            if (s.Y == 0)
            {
                Console.SetCursorPosition(0, 21);
                Console.WriteLine($"score: {score}");
                playing = false;
            }
            Thread.Sleep(200);
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
    
    public bool Fall(Shape s, int[] _grid)
    {
        if (!Fits(s, s.X, s.Y + 1, _grid))
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
    private bool Fits(Shape s, int newX, int newY, int[] _grid)
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
    private bool SideFit(Shape s, int newX, int newY, int[] _grid)
    {
        for (var i = 0; i < s.Len*s.Len; i++)
        {
            if (s[i] != s.Value) continue;
            
            if (newX + i % s.Len < 0 || newX + i % s.Len > 9) return false;
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
    public void PrintGrid(int[] _grid)
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
    public int Tetris()
    {
        int counter = 0;
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
            }

            if (tetris)
            {
                for (int j = 0; j < 10; j++)
                {
                    _grid[i * 10 + j] = 0;
                }
                DropAll(i-1);
                i = 20;
                counter++;
            }

            if (empty) return counter;


        }

        return counter;
    }
    private void DropAll(int row)
    {
        int count = 1;
        for (int i = row; i > -1; i--)
        {
            for (int j = 0; j < 10; j++)
            {
                if (_grid[i * 10 + j] == 0) continue;
                _grid[i * 10 + j + 10] = _grid[i * 10 + j];
                _grid[i * 10 + j] = 0;
            }
        }
    }

    public int EvaluateBoard(int []copyGrid)
    {
        int score = 0;
        bool tetris;
        bool empty;
        for (int i = 19; i > -1; i--)
        {
            tetris = true;
            empty = true;
            for (int j = 0; j < 10; j++)
            {
                
                if (copyGrid[i * 10 + j] == 0)
                {
                    tetris = false;
                    if (i > 0 && copyGrid[(i - 1) * 10 + j] != 0) score -= 10;
                }
                else
                {
                    empty = false;
                    if (i < 19 && copyGrid[(i + 1) * 10 + j] == 0)
                    {
                        score -= 10;
                    }
                    if (i < 19 && copyGrid[(i + 1) * 10 + j] != 0)
                    {
                        score += 1;
                    }
                }
            }

            if (tetris) score += 100;
            if (empty)
            {
                return score;
            }
        }

        return score;
    }

    public int BotMove(Shape shape)
    {
        int dropPos = 0, score = 0, maxScore = 0, rotateAmount = 3, toRotate = 0;
        int[] copyGrid;
        
        rotateAmount = shape switch
        {
            O => 0,
            _ => rotateAmount
        };
        for (int j = 0; j <= rotateAmount; j++)
        {
            for (int i = -3; i < 10; i++)
            {
                copyGrid = _grid.Clone() as int[];
                if (!SideFit(shape, i, 0, copyGrid)) continue;
                shape.X = i;
                while (Fall(shape, copyGrid))
                {
                
                }

                score = shape.Y * 5;
                score += EvaluateBoard(copyGrid);
                
                if (score > maxScore)
                {
                    maxScore = score;
                    dropPos = i;
                    toRotate = j;
                }
                shape.Y = 0;
            }
            shape.RotateLeft();
        }
        shape.X = dropPos;

        for (int i = 0; i < toRotate; i++)
        {
            shape.RotateLeft();
        }
        return dropPos;

        Console.WriteLine($"dropPos: {dropPos}, score: {maxScore}");
    }

}