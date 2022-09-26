using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiningHall.Entities
{
    public class Table
    {
        public int Number { get; set; }
        public Table(int number)
        {
            Number = number;
        }
        public Order GenerateOrder()
        {
            return new Order(4, this, new DateTimeOffset());
        }
    }
}
