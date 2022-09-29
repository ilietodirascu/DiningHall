using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiningHall.Entities
{
    public class Table
    {
        public int Number { get; set; }
        private Order _order { get; set; }
        public Table(int number)
        {
            Number = number;
        }
        public Order GenerateOrder()
        {
            _order = _order ??= new Order(Utility.GetRandomNumber(1,5), this, new DateTimeOffset(DateTime.Now));
            return _order;
        }
        public Order GetOrder()
        {
            return _order;
        }
        public void ResetOrder()
        {
            _order = null;
        }
    }
}
