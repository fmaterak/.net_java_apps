using System;
using System.Linq;
using System.Collections.Generic;



namespace Knapsack
{
    class Item
    {
        public int Weight { get; set; }
        public int Value { get; set; }

        public Item(int weight, int value)
        {
            Weight = weight;
            Value = value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Item i = new Item(20, 30);
            Console.WriteLine("Hello World!");
        }

        static List<Item> RandomizeItems(int numItems, long? rng_seed = null)
        {
            RandomNumberGenerator rng = new RandomNumberGenerator(rng_seed);

            return Enumerable.Range(0, numItems)
                .Select(i => new Item(rng.nextInt(1, 29), rng.nextInt(1, 29)))
                .ToList();
        }

        static List<Item> GetKnapsackContents(long capacity, IEnumerable<Item> items)
        {
            return items
                .OrderBy(item => item.Value / item.Weight)
                .TakeWhile(item => (capacity -= item.Weight) >= 0)
                .ToList();
        }
    }
}
