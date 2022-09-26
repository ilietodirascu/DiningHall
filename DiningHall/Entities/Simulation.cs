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
        private List<Table> _tables;
        public List<Table> Tables
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
        }
        public void RunSimulation()
        {
            Utility.Client.GetAsync("http://host.docker.internal:60000/StartSimulation");
            Tables.ForEach(x =>
            {
                Utility.Orders.Add(x.GenerateOrder());
            });
            List<Thread> tableThreads = new();
            
            for (int i = 0; i < 3; i++)
            {
                tableThreads.Add(new Thread(() =>
                {
                    while (Utility.Orders.Any())
                    {
                        var orders = new List<Order>(Utility.Orders);
                        var order = Utility.GetOrder();
                        if (order is null) return;
                        Thread.Sleep(Utility.GetRandomNumber(2, 5) * 1000);
                        order.SendOrder();
                        //Task.Delay(Utility.GetRandomNumber(2, 5) * 1000).ContinueWith(_ =>
                        //{
                        //    var order = Utility.GetOrder();
                        //    if(order is null)return;
                        //    order.SendOrder();
                        //});
                    }
                }));
            }
            tableThreads.ForEach(x => x.Start());
        }
    }
}
