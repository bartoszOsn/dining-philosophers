using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Dining_philosophers
{
    class Table
    {
        /// <summary>
        /// Array of seats. If value is false, then seat if taken.
        /// </summary>
        private bool[] seats;

        /// <summary>
        /// Random object.
        /// </summary>
        private Random random = new Random();
        public Semaphore Semaphore { get; }

        public Table(int seatCount)
        {
            seats = new bool[seatCount];
            for(int i = 0; i < seatCount; i++)
            {
                seats[i] = true;
            }
            Semaphore = new Semaphore(seatCount - 1, seatCount - 1);
        }

        /// <summary>
        /// Zajmuje jedno wolne, losowe miejsce przy stole.
        /// </summary>
        /// <returns>indeks miejsca.</returns>
        public int TakeSeat()
        {
            Semaphore.WaitOne();
            var freeSeatIndexes = seats.Select((t, i) => i).Where(i => seats[i]).ToArray();
            int index = freeSeatIndexes[random.Next(0, freeSeatIndexes.Length)];
            seats[index] = false;
            return index;
        }

        /// <summary>
        /// zwalnia miejsce
        /// </summary>
        /// <param name="seat">indeks miejsca, które ma zostać zwolnione</param>
        public void FreeSeat(int seat)
        {
            seats[seat] = true;
            Semaphore.Release();
        }
    }
}
