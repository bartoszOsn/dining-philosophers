using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Dining_philosophers
{
    class Philosopher
    {
        /// <summary>
        /// ogół widelców.
        /// </summary>
        Semaphore[] forks;

        /// <summary>
        /// Stół wspólny dla wszystkich filozofów.
        /// </summary>
        Table table;

        /// <summary>
        /// Semafor reprezentujący zbiór książek.
        /// </summary>
        Semaphore books;

        /// <summary>
        /// Indeks filozofa - używany do wypisywania statusu filozofa ("Eating"/"Thinking") w odpowiedniej kolejności.
        /// </summary>
        int index;

        /// <summary>
        /// Tworzy filozofa.
        /// </summary>
        /// <param name="leftFork">Widelec będący po lewej stronie filozofa.</param>
        /// <param name="rightFork">Widelec będący po prawej stronie filozofa.</param>
        /// <param name="table">Stół wspólny dla wszystkich filozofów.</param>
        /// <param name="books">Semafor reprezentujący zbiór książek.</param>
        /// <param name="seat">Numer siedzienia filozofa.</param>
        /// <param name="index">Indeks filozofa.</param>
        public Philosopher(Semaphore[] forks, Table table, Semaphore books, int index)
        {
            this.forks = forks;
            this.table = table;
            this.books = books;
            this.index = index;
        }

        /// <summary>
        /// Metoda wykonywana w odzielnym wątku.
        /// </summary>
        public void ThreadMethod()
        {
            while(true)
            {
                this.Think();
                this.SitAndEat();
            }
        }

        private void Think()
        {
            SetStatus("Thinking");
            Thread.Sleep(1000);
        }

        private void SitAndEat()
        {
            int seat = table.TakeSeat();
            Semaphore leftFork = forks[seat];
            Semaphore rightFork = forks[(seat + 1) % forks.Length];
            this.Eat(seat, leftFork, rightFork);
            table.FreeSeat(seat);
        }

        private void Eat(int seat, Semaphore leftFork, Semaphore rightFork)
        {
            //TODO: książki
            books.WaitOne();
            leftFork.WaitOne();
            rightFork.WaitOne();
            SetStatus($"Eating [Seat: {seat}]");
            Thread.Sleep(1000);
            rightFork.Release();
            leftFork.Release();
            books.Release();
        }

        /// <summary>
        /// Ustawia status filozofa.
        /// </summary>
        /// <param name="status">"Eating"/"Thinking"</param>
        private void SetStatus(string status)
        {
            Helpers.WriteAt($"Philosopher #{index}:\t{status}".PadRight(Console.WindowWidth - 1, ' '), 0, index);
        }
    }
}
