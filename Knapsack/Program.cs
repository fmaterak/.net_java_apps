using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;



namespace Knapsack
{
    public class Item
    {
        public int Weight { get; set; }
        public int Value { get; set; }

        public Item(int weight, int value)
        {
            Weight = weight;
            Value = value;
        }
    }

    public class Program
    {
        static int Main(string[] args)
        {
            int numItems = 10;
            int capacity = 50;
            long seed = DateTimeOffset.Now.ToUnixTimeSeconds();

            if (!ParseArgs(args, ref numItems, ref capacity, ref seed))
                return 1;

            var items = RandomizeItems(numItems, seed);
            var taken = GetKnapsackContents(capacity, items);

            Console.WriteLine("Ziarno: {0}", seed);
            Console.WriteLine("Pojemność: {0}", capacity);

            var tableWriter = new ResultTableWriter(new ResultTableWriter.LangPL());
            tableWriter.WriteHeader();
            items.ForEach(item => tableWriter.WriteRow(item, taken.Contains(item)));
            tableWriter.WriteFooter();

            return 0;
        }

        static bool ParseArgs(string[] args, ref int numItems, ref int capacity, ref long seed)
        {
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];

                if (arg == "-h" || arg == "--help")
                {
                    Console.WriteLine(
                        "usage: knapsack "+
                        "[-h | --help] "+
                        "[(-n | --num-items) n] "+
                        "[(-c | --capacity) c] "+
                        "[(-s | --seed) s]"
                    );
                    return false;
                }

                int skipChars;

                if (arg.StartsWith("--"))     skipChars = 2;
                else if (arg.StartsWith("-")) skipChars = 1;
                else
                {
                    Console.WriteLine("expected argument name, got '{0}'", arg);
                    return false;
                }

                if (++i >= args.Length)
                {
                    Console.WriteLine("expected value for '{0}'", arg);
                    return false;
                }

                bool isParsed;
                string value = args[i];

                switch (skipChars, arg.Substring(skipChars))
                {
                    case (1, "n"):
                    case (2, "num-items"):
                        isParsed = int.TryParse(value, NumberStyles.None, null, out numItems);
                        break;
                    case (1, "c"):
                    case (2, "capacity"):
                        isParsed = int.TryParse(value, NumberStyles.None, null, out capacity);
                        break;
                    case (1, "s"):
                    case (2, "seed"):
                        isParsed = long.TryParse(value, NumberStyles.None, null, out seed);
                        break;
                    default:
                        Console.WriteLine("unknown argument '{0}'", arg);
                        return false;
                }

                if (!isParsed)
                {
                    Console.WriteLine("expected numeric value for '{0}', got '{1}'", arg, value);
                    return false;
                }
            }

            return true;
        }

        public static List<Item> RandomizeItems(int numItems, long rng_seed)
        {
            var rng = new RandomNumberGenerator(rng_seed);

            return Enumerable.Range(0, numItems)
                .Select(i => new Item(rng.nextInt(1, 29), rng.nextInt(1, 29)))
                .ToList();
        }

        public static List<Item> GetKnapsackContents(int capacity, IEnumerable<Item> items)
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
