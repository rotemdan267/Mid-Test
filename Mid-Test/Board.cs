using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mid_Test
{
    public static class Board
    {
        public static char[,] Chars { get; set; } = new char[25, 80];

        public static int[,] BoardColor { get; set; } = new int[25, 80];

        public static bool[,] IsClear { get; set; } = new bool[25, 80];

        public static void SetDefaultArraysValues()
        {
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 80; j++)
                {
                    Chars[i, j] = ' ';
                }
            }

            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 80; j++)
                {
                    IsClear[i, j] = true;
                }
            }
        }

        public static void PrintBoard()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(1, 1);
            Console.Write(Game.Name);
            if (Game.HighScore > 0)
            {
                Console.SetCursorPosition(36, 1);
                Console.Write($"Score to beat: {Game.HighScore}");
            }
            Console.SetCursorPosition(78, 1);
            Console.Write($"Points: {Game.Points}");
            Console.SetCursorPosition(5, 4);
            Console.Write("/");
            for (int i = 0; i < 80; i++)
            {
                Console.Write("-");

            }
            Console.Write(@"\");
            Console.SetCursorPosition(0, 5);
            for (int i = 0; i < 25; i++)
            {
                Console.Write("     |");
                for (int j = 0; j < 80; j++)
                {
                    switch (BoardColor[i, j])
                    {
                        case 0:
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case 1:
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            break;

                        case 2:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;

                        case 3:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;

                        case 4:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;

                        case 5:
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            break;

                        case 6:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                    }

                    Console.Write(Chars[i, j]);

                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("|");
            }
            Console.Write(@"     \");
            for (int i = 0; i < 80; i++)
            {
                Console.Write("-");
            }
            Console.Write("/");
        }
    }
}
