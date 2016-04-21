using System;

namespace Test.CheckCities
{
    internal static class Progress
    {
        public static int Curr;
        public static int TotalProgress;
        private static readonly object _thisLock = new object();

        public static void DrawTextProgressBar(string msg)
        {
            lock (_thisLock)
            {
                Console.Clear();

                //draw empty progress bar
                Console.CursorLeft = 0;
                Console.Write("["); //start
                Console.CursorLeft = 32;
                Console.Write("]"); //end
                Console.CursorLeft = 1;
                var onechunk = 30.0f/TotalProgress;

                //draw filled part
                var position = 1;
                for (var i = 0; i < onechunk*Curr; i++)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.CursorLeft = position++;
                    Console.Write(" ");
                }

                //draw unfilled part
                for (var i = position; i <= 31; i++)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.CursorLeft = position++;
                    Console.Write(" ");
                }

                //draw totals
                Console.CursorLeft = 35;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(Curr + " of " + TotalProgress + "\n"); //blanks at the end remove any excess

                Console.WriteLine(msg);
            }
        }
    }
}