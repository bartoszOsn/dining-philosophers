using System;
using System.Collections.Generic;
using System.Text;

namespace Dining_philosophers
{
    static class Helpers
    {
        /// <summary>
        /// Wypisuje na ekranie tekst w podanym miejscu. Jest bezpieczny wątkowo.
        /// </summary>
        /// <param name="text">Tekst który ma zostać wypisany.</param>
        /// <param name="x">pozycja startowa tekstu poziomo.</param>
        /// <param name="y">pozycja startowa tekstu pionowo.</param>
        public static void WriteAt(string text, int x, int y)
        {
            lock(Console.Out)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(text);
            }
        }
    }
}
