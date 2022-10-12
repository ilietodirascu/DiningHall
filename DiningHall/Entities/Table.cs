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
        public bool IsEmpty { get; set; } = true;
        public Table(int number)
        {
            Number = number;
            WaitToFill();
        }
        public Order GenerateOrder()
        {
            _order = _order ??= new Order(this, new DateTimeOffset(DateTime.Now));
            return _order;
        }
        public Order GetOrder()
        {
            return _order;
        }
        public void ResetOrder()
        {
            _order = null;
            IsEmpty = true;
            WaitToFill();
        }
        public void WaitToFill()
        {
            Task.Delay(Utility.GetRandomNumber(5, 10) * 250).ContinueWith(_ =>
            {
                IsEmpty = false;
            });
        }
    }
}
