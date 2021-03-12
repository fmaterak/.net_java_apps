using System;



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
    }
}
