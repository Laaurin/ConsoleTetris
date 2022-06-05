namespace ConsoleTetris;
public class Game
{
    public int[] _grid = new int[200];

    private Shape _current, _next, _hold;
    public Game()
    {
        Console.CursorVisible = false;
        _current = Spawn();
        _hold = Spawn();
        _next = Spawn();
    }
    public void Play()
    {
        var playing = true;
        char key;

        while (playing)
        {
            _current = _next;
            _next = Spawn();
            Console.SetCursorPosition(25, 0);
            Console.Write(_current.X);
            while (Fall(_current, _grid))
            {
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true).KeyChar;

                    switch (key)
                    {
                        case 'd':
                            if (!SideFit(_current, _current.X + 1, _current.Y)) break;
                            _current.X++;
                            break;
                        case 'a':
                            if (!SideFit(_current, _current.X - 1, _current.Y)) break;
                            _current.X--;
                            break;
                        case 'w':
                            _current.RotateLeft();
                            break;
                        case 's':
                            continue;

                    }

                    Console.SetCursorPosition(25, 0);
                    Console.Write(_current.X);
                }

                Tetris();
                PrintGrid();
                PrintGrid(_grid);
                PrintFalling(_current);
                Thread.Sleep(700);
            }

            Thread.Sleep(400);
        }
    }

    public void BotPlay()
    {
        var score = 0; //killed lines
        var playing = true;
        int dropPos, scoreMove;
        int dropPosHold, scoreMoveHold;
        PrintGrid();
        while (playing)
        {
            _current = _next;
            _next = Spawn();

            (dropPos, scoreMove) = BottMove(_current);
            (dropPosHold, scoreMoveHold) = BottMove(_hold);
            if (scoreMove < scoreMoveHold)
            {
                (_current, _hold) = (_hold, _current);
                _current.X = dropPosHold;
            }
            else _current.X = dropPos;

            PrintNext();
            PrintHold();
            while (Fall(_current, _grid))
            {
                PrintGrid(_grid);
                Console.WriteLine($"killed lines: {score}   ");
                PrintFalling(_current);
                Thread.Sleep(10);
            }

            score += Tetris();

            // if (_current.Y == 0)
            // {
            //     playing = false;
            // }

            Thread.Sleep(20);
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
    public int Tetris()
    {
        int counter = 0;
        bool tetris, empty;

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

                DropAll(i - 1);
                i = 20;
                counter++;
            }
        }

        return counter;
    }
    public void DropAll(int row)
    {
        int counter = 1;
        for (var i = row; i > -1; i--)
        {
            
            
            for (int j = 0; j < 10; j++)
            {
                if (_grid[i * 10 + j] == 0) continue;
                _grid[(i + 1) * 10 + j] = _grid[i * 10 + j];
                _grid[i * 10 + j] = 0;
            }
        }
    }
    public bool Fall(Shape s, int[] _grid)
    {
        if (!Fits(s, s.X, s.Y + 1, _grid))
        {
            for (var i = 0; i < s.Len * s.Len; i++)
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
        for (int i = 0; i < s.Len * s.Len; i++)
        {
            if (s[i] == s.Value)
            {
                if ((newY + i / s.Len) * 10 + newX + i % s.Len > 199 ||
                    _grid[(newY + i / s.Len) * 10 + newX + i % s.Len] != 0)
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
        for (var i = 0; i < s.Len * s.Len; i++)
        {
            if (s[i] != s.Value) continue;
            if (newX + i % s.Len < 0 || newX + i % s.Len > 9) return false;
        }

        return true;
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
    
    private bool SideFit(Shape s, int newX, int newY, int[] _grid)
    {
        for (var i = 0; i < s.Len * s.Len; i++)
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
                Console.SetCursorPosition(cursorX * 2, cursorY);
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
            if (_grid[i] == 0) Console.Write("  ");
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
            if (_grid[i] == 0) Console.Write("  ");
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
            if (i % 10 == 0 && i != 0)
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

    public void PrintNext()
    {
        Console.SetCursorPosition(25, 0);
        Console.WriteLine("next Shape:");
        int cursorX = 20, cursorY = 1;
        Console.ForegroundColor = GetColor(_next.Value);
        for (int i = 0; i < _next.Len * _next.Len; i++)
        {
            if (i % _next.Len == 0 && i != 0)
            {
                cursorY++;
                cursorX = 20;
            }

            Console.SetCursorPosition(cursorX * 2, cursorY);
            if (_next[i] != 0) Console.Write("██");
            else Console.WriteLine("  ");
            cursorX++;
        }

        if (_next.Len == 3)
        {
            Console.SetCursorPosition(40, cursorY + 1);
            Console.WriteLine("            ");
        }

        Console.ForegroundColor = ConsoleColor.Black;
    }

    public void PrintHold()
    {
        Console.SetCursorPosition(25, 10);
        Console.WriteLine("holding Shape:");
        int cursorX = 20, cursorY = 11;
        Console.ForegroundColor = GetColor(_hold.Value);
        for (int i = 0; i < _hold.Len * _hold.Len; i++)
        {
            if (i % _hold.Len == 0 && i != 0)
            {
                cursorY++;
                cursorX = 20;
            }

            Console.SetCursorPosition(cursorX * 2, cursorY);
            if (_hold[i] != 0) Console.Write("██");
            else Console.WriteLine("  ");
            cursorX++;
        }

        if (_hold.Len == 3)
        {
            Console.SetCursorPosition(40, cursorY + 1);
            Console.WriteLine("            ");
            cursorY = 11;
        }

        Console.ForegroundColor = ConsoleColor.Black;
    }

    public int EvaluateBoard(int[] copyGrid)
    {
        int unebenheit = 0, level = 0;
        int splits = 0;
        int score = 0;
        int holes = 0;
        bool split;
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
                    split = true;
                    tetris = false;
                    if (i > 0 && copyGrid[(i - 1) * 10 + j] != 0) //ein Block ist über einem freien Feld
                    {
                        holes++;
                    }

                    if(i > 2)
                    for (int k = 0; k < 3; k++)
                    {
                        if (!((j == 0 || copyGrid[(i - k) * 10 + j - 1] != 0) &&
                              (j == 9 || copyGrid[(i - k) * 10 + j + 1] != 0))) split = false;
                    }

                    if (split) splits++;
                }
                else
                {
                    empty = false;
                }
            }

            if (tetris) score += 200;
            if (empty)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int k = i; k < 19; k++)
                    {
                        if (copyGrid[k * 10 + j] == 0) continue;
                        if (j == 0) level = k;
                        else
                        {
                            unebenheit += Math.Abs(level - k);
                            level = k;
                        }

                        break;
                    }
                }

                break;
            }

        }

        score -= unebenheit;
        score -= 21 * holes;
        score -= 5 * splits;
        return score;
    }

    public int BotMove(Shape shape)
    {
        int dropPos = 0, score, maxScore = 0, rotateAmount = 3, toRotate = 0;
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

                score = shape.Y * 10;
                score += EvaluateBoard(copyGrid);

                if (score > maxScore)
                {
                    maxScore = score;
                    dropPos = i;
                    toRotate = j;

                }

                PrintGrid(copyGrid);
                Console.WriteLine(score);
                Console.ReadKey();
                shape.Y = 0;
            }

            shape.RotateLeft();
        }
        //shape.X = dropPos;

        for (int i = 0; i < toRotate; i++)
        {
            shape.RotateLeft();
        }

        return dropPos;

        Console.WriteLine($"dropPos: {dropPos}, score: {maxScore}");
    }

    public (int ,int) BottMove(Shape shape)
    {
        int dropPos = 0, score, maxScore = Int32.MinValue, rotateAmount = 3, toRotate = 0;
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

                score = shape.Y * 10;
                score += EvaluateBoard(copyGrid);

                if (score > maxScore)
                {
                    maxScore = score;
                    dropPos = i;
                    toRotate = j;

                }

                // PrintGrid(copyGrid);
                // Console.WriteLine($"{score}   ");
                // Console.ReadKey();
                shape.Y = 0;
            }

            shape.RotateLeft();
        }
        //shape.X = dropPos;

        for (int i = 0; i < toRotate; i++)
        {
            shape.RotateLeft();
        }

        return (dropPos, maxScore);
    }
}




