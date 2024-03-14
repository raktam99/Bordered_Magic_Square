using System;
using Bordered_Magic_Square.Helper;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Bordered_Magic_Square
{
    internal class Program
    {
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        private static Square baseSquare;
        private static Square borderSquare;
        

        static void Main(string[] args)
        {
            int choice;
            int order;
            ConsoleKeyInfo cki;
            
            ShowWindow(Process.GetCurrentProcess().MainWindowHandle, 3);

            do
            {
                Console.CursorVisible = true;
                Console.Clear();
                Console.Write("Would you like an even(0) or an odd(1) square: ");
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    choice = -1;
                }

                while (choice != 0 && choice != 1)
                {
                    Console.Clear();
                    Console.Write("Try again! Would you like an even(0) or an odd(1) square: ", choice == 0 ? "even" : "odd");
                    try
                    {
                        choice = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        choice = -1;
                    }
                }

                Console.CursorVisible = false;

                Console.Clear();

                if (choice == 0)
                {
                    baseSquare = GenerateSquare.Even();
                }
                else
                {
                    baseSquare = GenerateSquare.Odd();
                }

                Console.WriteLine("Base magic square:\n\n" + baseSquare.ToString() + "\nPress any key to continue...");

                Console.ReadKey();

                Console.CursorVisible = true;

                Console.Clear();

                Console.Write("Type in the order of the bordered magic square you'd like: ");
                try
                {
                    order = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    order = -1;
                }

                while (order % 2 != choice || order < 3)
                {
                    Console.Clear();
                    Console.Write("Try again! Please type in an {0} number, bigger than {1}: "
                        , choice == 0 ? "even" : "odd", choice == 0 ? "2" : "1");
                    try
                    {
                        order = int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        order= -1;
                    }
                }

                Console.CursorVisible = false;

                Console.Clear();

                borderSquare = AlterSquare.AddBorders(baseSquare, order);

                if (borderSquare.ToString().Split('\n')[0].Length > Console.WindowWidth)
                {
                    Console.WriteLine("Too wide to show on console:)");
                    Console.ReadKey();
                    goto tooBig;
                }

                Console.WriteLine(borderSquare.ToString());

                Console.WriteLine("Press any key to remove outer layers one by one!\n\n");

                Console.ReadKey();

                while (borderSquare.Order > 4)
                {
                    borderSquare = AlterSquare.RemoveLayer(borderSquare);

                    Console.WriteLine(borderSquare.ToString());

                    Console.ReadKey();
                }

                tooBig:
                Console.Clear();
                Console.Write("Press esc to exit, any other key to continue...");
                cki = Console.ReadKey();
            } while (cki.Key != ConsoleKey.Escape);
        }
    }
}
