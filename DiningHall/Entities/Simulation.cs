using DiningHall.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DiningHall.Entities
{
    public class Simulation
    {
        private static List<Table> _tables;
        private List<Waiter> _waiters;
        public List<Waiter> Waiters
        {
            get { return _waiters; }
            set
            {
                _waiters = value;
                for (int i = 0; i < _tables.Count / 2; i++)
                {
                    _waiters.Add(new Waiter());
                }
            }
        }
        public static List<Table> Tables
        {
            get { return _tables; }
            set
            {
                _tables = value;
                for (int i = 0; i < _tables.Count; i++)
                {
                    _tables[i] = new Table(i + 1);
                }
            }
        }
        public Simulation(int nrOfTables)
        {
            Tables = new Table[nrOfTables].ToList();
            Waiters = new();
        }
        public void RunSimulation()
        {
            Utility.Client.GetAsync("http://localhost:60000/StartSimulation");
            List<Thread> waiterThreads = new();
            foreach (var waiter in Waiters)
            {
                waiterThreads.Add(new Thread(() =>
                {
                    waiter.WaiterJob();
                }));
            }
            waiterThreads.ForEach(x => x.Start());
        }

    }
}
