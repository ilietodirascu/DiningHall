using DiningHall.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DiningHall.Extensions
{
    public static class ExtensionMethods
    {
        public static void SendOrder(this Order order,Waiter waiter)
        {
            Utility.Client.PostAsJsonAsync("http://host.docker.internal:60500/LogInfo", new { Message = $"Hello, Table Nr:{order.Table.Number}. My name is {waiter.Name}. Let me send " +
                $"your order of {Utility.GetItems(order)} Priority:{order.Priority}" });
            Utility.Client.PostAsJsonAsync("http://host.docker.internal:60000/AddOrder", order);
        }
        public static void ReturnOrder(this Order order, Waiter waiter)
        {
            var cookTime = (int)Math.Ceiling(DateTimeOffset.Now.Subtract(order.TimeOfCreation).TotalSeconds) * 250;
            Utility.Ratings.Add(Utility.GetRating(cookTime, order.MaxWait * 250));
            Utility.Client.PostAsJsonAsync("http://host.docker.internal:60500/LogInfo", new
            {
                Message = $"{waiter.Name}: Sorry fo the wait. Order for Table Nr:{order.Table.Number} of {Utility.GetItems(order)} is finished. Max Wait: {order.MaxWait * 250} " +
                $"It took {cookTime}\nRestaurant rating:{Utility.Ratings.Average()}"
            });
            Utility.FinishedOrders.TryDequeue(out _);
        }
    }
}
