namespace ConsoleTetris;

public abstract class Shape
    {
        protected int _x = 5, _y, _value, _len = 3;
        public int X
        {
            get => _x;
            set => _x = value;
        }

        public int Y
        {
            get => _y;
            set => _y = value;
        }

        public int Value => _value;
        public int Len => _len;

        protected int[] shape;

        public int this[int i] => shape[i];

        public virtual void RotateLeft()
        {
            int[] rotated = new int[shape.Length];
            for (int i = 0; i < shape.Length; i++)
            {
                rotated[i] = shape[6 - 3 * (i % 3) + i / 3];
            }

            shape = rotated;
        }
    }
    public class I : Shape
    {
        public I()
        {
            _len = 4;
            _value = 1;
            shape = new[]
            {
                0, 1, 0, 0,
                0, 1, 0, 0,
                0, 1, 0, 0,
                0, 1, 0, 0,
            };
        }

        public override void RotateLeft()
        {
            int[] rotated = new int[16];
            for (int i = 0; i < 16; i++)
            {
                rotated[i] = shape[12 - 4 * i % 4 + i / 4];
            }

            shape = rotated;
        }
    }
    
    public class J : Shape
    {
        public J()
        {
            _value = 2;
            shape = new[]
            {
                0, 2, 0,
                0, 2, 0,
                2, 2, 0,
            };
        }
    
    }
    
    public class L : Shape
    {
        public L()
        {
            _value = 3;
            shape = new[]
            {
                3, 0, 0,
                3, 0, 0,
                3, 3, 0,
            };
        }
    
    }
    
    public class O : Shape
    {
        public O()
        {
            _value = 4;
            shape = new[]
            {
                0, 4, 4,
                0, 4, 4,
                0, 0, 0
            };
        }
    
        public override void RotateLeft()
        {
        }
    }
    
    public class S : Shape
    {
        public S()
        {
            _value = 5;
            shape = new[]
            {
                0, 5, 5,
                5, 5, 0,
                0, 0, 0
            };
        }
    }
    
    public class T : Shape
    {
        public T()
        {
            _value = 6;
            shape = new[]
            {
                6, 6, 6,
                0, 6, 0,
                0, 0, 0
            };
        }
    
    }
    
    public class Z : Shape
    {
        public Z()
        {
            _value = 7;
            shape = new[]
            {
                7, 7, 0,
                0, 7, 7,
                0, 0, 0
            };
        }
    }