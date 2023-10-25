using System;
using System.Collections.Generic;
using System.Linq;

namespace Items
{
    [Serializable]
    public class ItemsWithQuantity : PseudoDictionary<Item, int>
    {
        public int NumberOfItem(Item item)
        {
            int val;
            AsDictionary().TryGetValue(item, out val);
            return val;
        }
    
        public void SetItemQuantity(Item item, int quantity)
        {
            var newDict = AsDictionary();
            newDict[item] = quantity;
            FromDictionary(newDict);
        }
    
        public void AddItem(Item item, int quantity = 1)
        {
            SetItemQuantity(item, NumberOfItem(item) + quantity);
        }

        public void Add(ItemsWithQuantity other)
        {        
            foreach (var (item, quantity) in other.AsDictionary())
            {
                AddItem(item, quantity);
            }
        }
        
        public void Remove(ItemsWithQuantity other)
        {        
            foreach (var (item, quantity) in other.AsDictionary())
            {
                AddItem(item, -quantity);
            }
        }

        public int TotalQuantity()
        {
            return AsDictionary().Values.Sum();
        }

        /// <summary>
        /// Returns a random item in the dictionary, weighted by item quantities.
        /// </summary>
        public Item RandomItem()
        {
            var allItems = new List<Item>();
            foreach (var (item, quantity) in AsDictionary())
                allItems.AddRange(Enumerable.Repeat(item, quantity));
            return Utils.Utils.RandomChoice(allItems.ToArray());
        }

        public static ItemsWithQuantity CreateFromDictionary(Dictionary<Item, int> dict)
        {
            var newDict = new ItemsWithQuantity();
            newDict.FromDictionary(dict);
            return newDict;
        }
    }
}