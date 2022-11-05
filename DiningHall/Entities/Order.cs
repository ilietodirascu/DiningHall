using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiningHall.Entities
{
    public class Order
    {
        public static int IdNum { get; set; }
        private int _id;
        public int Id { get { return _id; } set { IdNum++; _id = value; } }
        public int[] Items { get; set; }
        public float Priority { get; set; }
        public int MaxWait { get; set; }
        public Table Table { get; set; }
        public DateTimeOffset TimeOfCreation { get; set; }

        public Order( Table table, DateTimeOffset timeOfCreation)
        {
            Id = IdNum;
            Items = GenerateItems();
            MaxWait = GetMaxWait(Items);
            Table = table;
            TimeOfCreation = timeOfCreation;
        }

        private int[] GenerateItems()
        {
            var items = new int[Utility.GetRandomNumber(1, 11)];
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = Utility.GetRandomNumber(1, 14);
            }
            var itemPrio = new List<int>();
            items.ToList().ForEach(x => itemPrio.Add(Utility.Menu.First(y => y.Id == x).Complexity));
            Priority = (float)(5.0 / itemPrio.Average());
            return items;
        }
        private static int GetMaxWait(int[] items)
        {
            int maxWait = 0;
            for (int i = 0; i < items.Length; i++)
            {
                var prepTime = Utility.Menu.First(x => x.Id == items[i]).PreparationTime;
                maxWait = prepTime > maxWait ? prepTime : maxWait;
            }
            return (int)Math.Ceiling(maxWait * 1.3);
        }
    }
}
