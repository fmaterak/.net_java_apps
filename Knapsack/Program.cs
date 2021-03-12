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

        public override string ToString()
        {
            return String.Format("Item(W={0}, V={1}, k={2})", Weight, Value, (double) Value/Weight);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var seed = DateTimeOffset.Now.ToUnixTimeSeconds();
            Console.WriteLine("Seed: {0}", seed);

            var items = RandomizeItems(10, seed);
            Console.WriteLine("Items:");
            Console.WriteLine("  " + String.Join("\n  ", items));

            var knapsack = GetKnapsackContents(50, items);
            Console.WriteLine("Knapsack:");
            Console.WriteLine("  " + String.Join("\n  ", knapsack));
        }

        static List<Item> RandomizeItems(int numItems, long? rng_seed = null)
        {
            var rng = new RandomNumberGenerator(rng_seed);

            return Enumerable.Range(0, numItems)
                .Select(i => new Item(rng.nextInt(1, 29), rng.nextInt(1, 29)))
                .ToList();
        }

        static List<Item> GetKnapsackContents(int capacity, IEnumerable<Item> items)
        {
            bool ItemShouldBeAdded(Item item) {
                if (item.Weight <= capacity) {
                    capacity -= item.Weight;
                    return true;
                }
                return false;
            }

            return items
                // Sort by value/weight in reverse order (descending)
                .OrderBy(item => (double) item.Weight / item.Value)
                .Where(ItemShouldBeAdded)
                .ToList();
        }
    }
}
