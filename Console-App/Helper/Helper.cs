using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_App.Helper
{
    public static class Helper
    {
        public static void PrintConsole(ConsoleColor color, string message)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
        }

        public static void PrintCentered(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;

            int width = Console.WindowWidth;
            int left = (width - text.Length) / 2;

            if (left < 0) left = 0;

            Console.SetCursorPosition(left, Console.CursorTop);
            Console.WriteLine(text);

            Console.ResetColor();
        }
    } 
}
