using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Dining_philosophers
{
    class Philosopher
    {
        /// <summary>
        /// Widelec będący po lewej stronie filozofa.
        /// </summary>
        Semaphore leftFork;

        /// <summary>
        /// Widelec będący po prawej stronie filozofa.
        /// </summary>
        Semaphore rightFork;

        /// <summary>
        /// Stół wspólny dla wszystkich filozofów.
        /// </summary>
        Semaphore table;

        /// <summary>
        /// Semafor reprezentujący zbiór książek.
        /// </summary>
        Semaphore books;

        /// <summary>
        /// Numer siedzienia filozofa.
        /// </summary>
        int seat;

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
        public Philosopher(Semaphore leftFork, Semaphore rightFork, Semaphore table, Semaphore books, int seat, int index)
        {
            this.leftFork = leftFork;
            this.rightFork = rightFork;
            this.table = table;
            this.books = books;
            this.seat = seat;
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
                this.Eat();
            }
        }

        private void Think()
        {
            SetStatus("Thinking");
            Thread.Sleep(1000);
        }

        private void Eat()
        {
            //TODO: książki
            table.WaitOne();
            books.WaitOne();
            leftFork.WaitOne();
            rightFork.WaitOne();
            SetStatus("Eating");
            Thread.Sleep(1000);
            rightFork.Release();
            leftFork.Release();
            books.Release();
            table.Release();
        }

        /// <summary>
        /// Ustawia status filozofa.
        /// </summary>
        /// <param name="status">"Eating"/"Thinking"</param>
        private void SetStatus(string status)
        {
            Helpers.WriteAt($"Philosopher #{index}:\t{status}".PadRight(Console.WindowWidth - 1, ' '), 0, seat);
        }
    }
}
