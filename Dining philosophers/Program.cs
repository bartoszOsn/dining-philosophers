using System;
using System.Linq;
using System.Threading;

namespace Dining_philosophers
{
    class Program
    {
        static void Main(string[] args)
        {
            //liczba filozofów
            const int n = 5;

            //liczba książek
            const int k = 3;
            
            //tworzy widelce, stół oraz samych filozofów
            Semaphore[] forks = CreateForks(n);
            Table table = new Table(n);
            Semaphore books = new Semaphore(k, k);
            Philosopher[] philosophers = CreatePhilosophers(n, forks, table, books);

            //tworzy wątków oraz je rozpoczyna
            Thread[] threads = philosophers.Select(t => new Thread(t.ThreadMethod)).ToArray();
            foreach(Thread t in threads)
            {
                t.Start();
            }
        }

        /// <summary>
        /// Tworzy widelce - jako semafory.
        /// </summary>
        /// <param name="n">liczba widelców/semaforów</param>
        static Semaphore[] CreateForks(int n)
        {
            Semaphore[] forks = new Semaphore[n];
            for(int i = 0; i < n; i++)
            {
                forks[i] = new Semaphore(1, 1);
            }
            return forks;
        }

        /// <summary>
        /// Tworzy filozofów
        /// </summary>
        /// <param name="n">liczba filozofów</param>
        /// <param name="forks">tablica widelców. jej wielkość musi wynosić tyle samo co <c>n</c></param>
        /// <param name="table">semafor reprezentujący stół</param>
        private static Philosopher[] CreatePhilosophers(int n, Semaphore[] forks, Table table, Semaphore books)
        {
            Philosopher[] philosophers = new Philosopher[n];
            for(int i = 0; i < n; i++)
            {
                //TODO: losowe miejsca
                philosophers[i] = new Philosopher(forks, table, books, i);
            }
            return philosophers;
        }
    }
}
