using System;
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
            List<Item> items = new List<Item>();
            RandomNumberGenerator rng = new RandomNumberGenerator(rng_seed);

            for (int i = 0; i < numItems; i++)
            {
                items.Add(new Item(rng.nextInt(1, 29), rng.nextInt(1, 29)));
            }

            return items;
        }
    }
}
