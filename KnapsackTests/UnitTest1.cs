using NUnit.Framework;
using Knapsack;
using System.Collections.Generic;
using System.Linq;
using System;

namespace KnapsackTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestOneElement()
        {
            Item test_item = new(49, 1);
            var test_items = new Item[] { test_item };
            Assert.IsNotEmpty(Program.GetKnapsackContents(50, test_items));
        }
        [Test]
        public void TestIncorrectElement()
        {
            Item test_item = new(51, 1);
            var test_items = new Item[] { test_item };
            Assert.IsEmpty(Program.GetKnapsackContents(50, test_items));
        }
        [Test]
        public void TestFixedSeed()
        {
            long fixed_seed = 1615992303;
            var test_items = Program.RandomizeItems(10, fixed_seed);
            var test_content = Program.GetKnapsackContents(50, test_items);
            var expected_content = new List<Item>();
            expected_content.Add(new(9, 15));
            expected_content.Add(new(11, 15));
            expected_content.Add(new(23, 28));
            Assert.AreEqual(test_content, expected_content);
        }
        [Test]
        public void TestOrder()
        {
            long seed = DateTimeOffset.Now.ToUnixTimeSeconds();
            var test_items = Program.RandomizeItems(10, seed);
            test_items.OrderBy(item => (double)item.Weight / item.Value);
            var content_ascending = Program.GetKnapsackContents(50, test_items);
            test_items.OrderBy(item => (double)item.Value / item.Weight);
            var content_descending = Program.GetKnapsackContents(50, test_items);
            Assert.AreEqual(content_ascending, content_descending);
        }
    }
}
