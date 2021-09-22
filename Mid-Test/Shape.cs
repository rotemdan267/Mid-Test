using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mid_Test
{
    public abstract class Shape
    {
        public int Size { get; set; }
        public int Color { get; set; }
        public int LocationI { get; set; }
        public int LocationJ { get; set; }
        public char TheChar { get; set; }
        public Random Rand { get; set; } = new Random();

        public abstract void PlaceShape();
        protected abstract bool IsThereRoomForShape();
    }
    public class Line : Shape
    {
        public Line()
        {
            TheChar = '=';
        }

        protected override bool IsThereRoomForShape()
        {
            Size = Rand.Next(2, 11);
            LocationI = Rand.Next(25);
            LocationJ = Rand.Next(81 - Size); // מונע אפשרות לגלישה מחוץ לגבולות המערך

            bool flag = true;
            for (int j = LocationJ; j < LocationJ + Size; j++)
            {
                if (!Board.IsClear[LocationI, j])
                {
                    flag = false;
                }
            }
            return flag;
        }

        public override void PlaceShape()
        {
            bool flag = IsThereRoomForShape();
            int count = 0;
            while (!flag)
            {
                flag = IsThereRoomForShape();
                count++;
                if (count == 100)
                { // אם הקומפיילר לא הצליח 100 פעמים למצוא מקום לצורה
                  // מוחקים את הצורות הקיימות ומנסים מחדש

                    Board.SetDefaultArraysValues();
                    Game.PlaceShapes();
                }
            }

            Color = Rand.Next(2, 7);
            for (int j = LocationJ; j < LocationJ + Size; j++)
            {
                Board.Chars[LocationI, j] = TheChar;
                Board.BoardColor[LocationI, j] = Color;
                Board.IsClear[LocationI, j] = false;
            }
        }
    }
    public class Square : Shape
    {
        public Square()
        {
            TheChar = 'O';
        }

        protected override bool IsThereRoomForShape()
        {
            Size = Rand.Next(3, 11);
            LocationI = Rand.Next(26 - Size);
            LocationJ = Rand.Next(81 - Size);

            bool flag = true;
            for (int i = LocationI; i < LocationI + Size; i++)
            {
                for (int j = LocationJ; j < LocationJ + Size; j++)
                {
                    if (!Board.IsClear[i, j])
                    {
                        flag = false;
                    }
                }
            }
            return flag;
        }

        public override void PlaceShape()
        {
            bool flag = IsThereRoomForShape();
            int count = 0;
            while (!flag)
            {
                flag = IsThereRoomForShape();
                count++;
                if (count == 100)
                { // אם הקומפיילר לא הצליח 100 פעמים למצוא מקום לצורה
                  // מוחקים את הצורות הקיימות ומנסים מחדש
                    Board.SetDefaultArraysValues();
                    Game.PlaceShapes();
                }
            }

            Color = Rand.Next(2, 7);
            for (int i = LocationI; i < LocationI + Size; i++)
            {
                for (int j = LocationJ; j < LocationJ + Size; j++)
                {
                    Board.Chars[i, j] = TheChar;
                    Board.BoardColor[i, j] = Color;
                    Board.IsClear[i, j] = false;
                }
            }

        }
    }
    public class Rectangle : Shape
    {
        public int SizeJ { get; set; }
        public Rectangle()
        {
            TheChar = 'O';
        }

        protected override bool IsThereRoomForShape()
        {
            Size = Rand.Next(2, 11);
            SizeJ = Rand.Next(3, 11);
            LocationI = Rand.Next(26 - Size);
            LocationJ = Rand.Next(81 - SizeJ);

            bool flag = true;
            for (int i = LocationI; i < LocationI + Size; i++)
            {
                for (int j = LocationJ; j < LocationJ + SizeJ; j++)
                {
                    if (!Board.IsClear[i, j])
                    {
                        flag = false;
                    }
                }
            }
            return flag;
        }

        public override void PlaceShape()
        {
            bool flag = IsThereRoomForShape();
            int count = 0;
            while (!flag)
            {
                flag = IsThereRoomForShape();
                count++;
                if (count == 100)
                { // אם הקומפיילר לא הצליח 100 פעמים למצוא מקום לצורה
                  // מוחקים את הצורות הקיימות ומנסים מחדש
                    Board.SetDefaultArraysValues();
                    Game.PlaceShapes();
                }
            }

            Color = Rand.Next(2, 7);
            for (int i = LocationI; i < LocationI + Size; i++)
            {
                for (int j = LocationJ; j < LocationJ + SizeJ; j++)
                {
                    Board.Chars[i, j] = TheChar;
                    Board.BoardColor[i, j] = Color;
                    Board.IsClear[i, j] = false;
                }
            }
        }
    }
    public class Triangle : Shape
    {
        public Triangle()
        {
            TheChar = '#';
        }

        protected override bool IsThereRoomForShape()
        {
            Size = Rand.Next(2, 10);
            LocationI = Rand.Next(26 - Size);
            LocationJ = Rand.Next(81 - Size);
            int temp = LocationJ;
            int count = 1;

            bool flag = true;
            for (int i = LocationI; i < LocationI + Size; i++)
            {
                for (int j = Size - count; j < Size; j++)
                {
                    if (!Board.IsClear[i, LocationJ])
                    {
                        flag = false;
                    }
                    LocationJ++;
                }
                count++;
                LocationJ = temp;
            }
            return flag;
        }

        public override void PlaceShape()
        {
            bool flag = IsThereRoomForShape();
            int count = 0;
            while (!flag)
            {
                flag = IsThereRoomForShape();
                count++;
                if (count == 100)
                { // אם הקומפיילר לא הצליח 100 פעמים למצוא מקום לצורה
                  // מוחקים את הצורות הקיימות ומנסים מחדש
                    Board.SetDefaultArraysValues();
                    Game.PlaceShapes();
                }
            }
            count = 1;
            int temp = LocationJ;
            Color = Rand.Next(2, 7);
            for (int i = LocationI; i < LocationI + Size; i++)
            {
                for (int j = Size - count; j < Size; j++)
                {
                    Board.Chars[i, LocationJ] = TheChar;
                    Board.BoardColor[i, LocationJ] = Color;
                    Board.IsClear[i, LocationJ] = false;
                    LocationJ++;
                }
                LocationJ = temp;
                count++;
            }

        }
    }
}
