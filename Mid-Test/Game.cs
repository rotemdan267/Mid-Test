using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mid_Test
{
    public static class Game
    {
        private static ConsoleKeyInfo key;

        public static int Points { get; set; } = 0;
        public static int HighScore { get; set; } = 0;
        public static string Name { get; set; }
        public static int NumOfShapes { get; set; }
        public static int ShapeType { get; set; }
        public static int BallLocationI { get; set; }
        public static int BallLocationJ { get; set; }
        public static int ShapePlacingAttempts { get; set; } = 0;
        public static Random Rand { get; set; } = new Random();

        public static void StartGame()
        {
            Console.SetWindowSize(90, 35);
            Console.SetCursorPosition(20, 5);
            Console.WriteLine("Hello new player, what is your name?");
            Console.SetCursorPosition(20, 8);
            Name = Console.ReadLine();
            Console.CursorVisible = false;
            Console.Clear();
            Console.SetCursorPosition(15, 5);
            Console.WriteLine($"Wellcome {Name}!");
            Console.SetCursorPosition(15, 7);
            Console.WriteLine("Instructions:");
            Console.SetCursorPosition(15, 9);
            Console.WriteLine("Use the arrow-keys to move.");
            Console.SetCursorPosition(15, 10);
            Console.WriteLine("Avoid touching the shapes and your own trail. Good Luck!");
            Console.SetCursorPosition(23, 16);
            Console.WriteLine("--Press Enter to begin the game--");
            do
            {
                key = Console.ReadKey(true);
            }
            while (key.Key != ConsoleKey.Enter);

            NumOfShapes = Rand.Next(3, 7);

            NextRound();

        }
        public static void NextRound()
        {
            Board.SetDefaultArraysValues();
            PlaceShapes();
            ShapePlacingAttempts = 0;
            PlaceBall();
            NextTurn();
        }
        public static void NextTurn()
        {
            Console.Clear();
            Board.PrintBoard();
            int i = BallLocationI;
            int j = BallLocationJ;
            key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    if (i == 0)
                    {
                        OutOfBounds();
                    }
                    i--;
                    break;

                case ConsoleKey.DownArrow:
                    if (i == 24)
                    {
                        OutOfBounds();
                    }
                    i++;
                    break;

                case ConsoleKey.LeftArrow:
                    if (j == 0)
                    {
                        OutOfBounds();
                    }
                    j--;
                    break;

                case ConsoleKey.RightArrow:
                    if (j == 79)
                    {
                        OutOfBounds();
                    }
                    j++;
                    break;

                default:
                    Console.Clear();
                    Console.SetCursorPosition(27, 8);
                    Console.WriteLine("Wrong input. Use the arrow-keys to move.");
                    Console.SetCursorPosition(32, 11);
                    Console.WriteLine("--Press Enter to continue--");
                    do
                    {
                        key = Console.ReadKey(true);
                    }
                    while (key.Key != ConsoleKey.Enter);
                    NextTurn();
                    break;
            }

            if (Board.IsClear[i, j])
            {
                Points++;
                Board.BoardColor[BallLocationI, BallLocationJ] = 1;

                Board.Chars[i, j] = '*';
                Board.BoardColor[i, j] = 0;
                Board.IsClear[i, j] = false;

                BallLocationI = i;
                BallLocationJ = j;

                NextTurn();
            }
            else
            {
                NumOfShapes++;
                if (NumOfShapes == 15)
                {
                    GameOver("shapes");
                }
                NextRound();
            }
        }
        public static void GameOver(string reason)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.SetCursorPosition(20, 5);
            if (reason == "no room for shapes")
            {
                Console.WriteLine("The game ended because there isn't room to add shapes");
            }
            if (reason == "ball")
            {
                Console.WriteLine("The game ended because there isn't room to put the ball");
            }
            if (reason == "shapes")
            {
                Console.WriteLine("You made it to the end!");
            }
            Console.SetCursorPosition(20, 7);
            Console.WriteLine($"Congratulations! You scored {Points} points!");
            if (Points > HighScore)
            {
                HighScore = Points;
            }
            Console.SetCursorPosition(20, 9);
            Console.WriteLine($"The current high-score is {HighScore}. Think you can beat that?    y / n");
            do
            {
                key = Console.ReadKey(true);
            }
            while (key.Key != ConsoleKey.Y && key.Key != ConsoleKey.N);
            if (key.Key == ConsoleKey.N)
            {
                Console.Clear();
                Console.SetCursorPosition(32, 11);
                Console.WriteLine("--Press Enter to exit--");
                do
                {
                    key = Console.ReadKey(true);
                }
                while (key.Key != ConsoleKey.Enter);
                Environment.Exit(0);
            }
            if (key.Key == ConsoleKey.Y)
            {
                Points = 0;
                Console.Clear();
                Console.SetCursorPosition(15, 5);
                Console.WriteLine($"Wellcome {Name}!");
                Console.SetCursorPosition(15, 7);
                Console.WriteLine("Instructions:");
                Console.SetCursorPosition(15, 9);
                Console.WriteLine("Use the arrow-keys to move.");
                Console.SetCursorPosition(15, 10);
                Console.WriteLine("Avoid touching the shapes and your own trail. Good Luck!");
                Console.SetCursorPosition(23, 16);
                Console.WriteLine("--Press Enter to begin the game--");
                do
                {
                    key = Console.ReadKey(true);
                }
                while (key.Key != ConsoleKey.Enter);

                NumOfShapes = Rand.Next(3, 7);

                NextRound();
            }
        }
        public static void OutOfBounds()
        {
            Console.Clear();
            Console.SetCursorPosition(32, 8);
            Console.WriteLine("Out of Bounds! -1 Points");
            Points--;
            Console.SetCursorPosition(31, 11);
            Console.WriteLine("--Press Enter to continue--");
            do
            {
                key = Console.ReadKey(true);
            }
            while (key.Key != ConsoleKey.Enter);
            NextTurn();
        }
        private static bool IsThereRoomForBall()
        {
            BallLocationI = Rand.Next(25);
            BallLocationJ = Rand.Next(80);
            return Board.IsClear[BallLocationI, BallLocationJ];
        }
        public static void PlaceBall()
        {
            bool flag = IsThereRoomForBall();
            int count = 1;
            while (!flag)
            {
                flag = IsThereRoomForBall();
                count++;
                if (count == 30)
                {
                    GameOver("ball");
                }
            }

            Board.Chars[BallLocationI, BallLocationJ] = '*';
            Board.BoardColor[BallLocationI, BallLocationJ] = 0;
            Board.IsClear[BallLocationI, BallLocationJ] = false;

        }

        public static void PlaceShapes()
        {
            ShapePlacingAttempts++;
            if (ShapePlacingAttempts == 100)
            {
                GameOver("no room for shapes");
            }
            for (int i = 0; i < NumOfShapes; i++)
            {
                ShapeType = Rand.Next(2, 6);
                switch (ShapeType)
                {
                    case 2:
                        Line line = new Line();
                        line.PlaceShape();
                        break;

                    case 3:
                        Triangle triangle = new Triangle();
                        triangle.PlaceShape();
                        break;

                    case 4:
                        Square square = new Square();
                        square.PlaceShape();
                        break;

                    case 5:
                        Rectangle rectangle = new Rectangle();
                        rectangle.PlaceShape();
                        break;
                }
            }
        }
    }
}