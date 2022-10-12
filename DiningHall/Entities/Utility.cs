using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiningHall.Entities
{
    public static class Utility
    {
        public static Random Random { get; set; } = new();
        public static List<double> Ratings = new();
        public static List<Order> Orders { get; set; } = new();
        public static ConcurrentQueue<Order> FinishedOrders { get; set; } = new();
        public static HttpClient Client { get; set; } = new();
        public static Object Lock = new object();
        private static Object _randomLock = new object();
        public static Food[] Menu { get; set; }
        static Utility()
        {
            using StreamReader u = new(@"../menu.json");
            string foods = u.ReadToEnd();
            Menu = JsonConvert.DeserializeObject<Food[]>(foods);
        }
        public static int GetRandomNumber(int a,int b)
        {
            lock (_randomLock)
            {
                return Random.Next(a, b);
            }
        }
        public static Order GetOrder()
        {
            lock(Lock)
            {
                var order = Orders.FirstOrDefault();
                Orders.Remove(order);
                return order;
            }
        }
        public static string GetItems(Order order)
        {
            var result = new List<string>();
            order.Items.ToList().ForEach(x => result.Add(Menu.Where(y => y.Id == x).Select(z => z.Name).First().ToString()));
            return String.Join(",", result);
        }
        public static double GetRating(int cookTime, int maxWait)
        {
            if (maxWait >= cookTime)
            {
                return 5;
            }
            if (maxWait * 1.1 >= cookTime)
            {
                return 4;
            }
            if (maxWait * 1.2 >= cookTime)
            {
                return 3;
            }
            if (maxWait * 1.3 >= cookTime)
            {
                return 2;
            }
            if (maxWait * 1.4 >= cookTime)
            {
                return 1;
            }
            else return 0;
        }
    }
}
