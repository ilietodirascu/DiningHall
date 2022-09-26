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
        public static void SendOrder(this Order order)
        {
            Utility.Client.PostAsJsonAsync("http://host.docker.internal:60500/LogInfo", new { Message = $"Dining Hall to Kitchen Table:{order.Table.Number} sent order of {Utility.GetItems(order)}" });
            Utility.Client.PostAsJsonAsync("http://host.docker.internal:60000/AddOrder", order);
        }
    }
}
