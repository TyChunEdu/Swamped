using System;
using System.Collections.Generic;
using Buffs;
using Singletons;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Item")]
    public class Item : ScriptableObject
    {
        public static Item[] AllItems;
        
        public string Name;
        public string Description;
        public Sprite Image;
        [SerializeField]
        private ItemsWithQuantity IngredientsList;
        public string meterName;
        public int decrease;
        [Tooltip("Integer out of 100")]
        public int failRate; // Out of 100: the rate of which crafting this item can fail
        // For example, failRate = 100 would mean this item always fails to craft
        // Additionally, failRate = 0 would mean this item never fails to craft

        public Dictionary<Item, int> GetRecipe()
        {
            var dict = IngredientsList.AsDictionary();
            Dictionary<Item, int> dictWithBuffConsidered = new Dictionary<Item, int>();
            foreach (var (item, quantity) in dict)
            {
                int trueQuantity = (int)Math.Round(quantity * Buff.ActiveEffects().craftingConsumptionMultiplier);
                trueQuantity = Math.Max(1, trueQuantity); // Required ingredient amount should not be below 1
                dictWithBuffConsidered.Add(item, trueQuantity);
            }
            
            return dictWithBuffConsidered.Count == 0 ? null : dictWithBuffConsidered;
        }
        
        public ItemsWithQuantity GetIngredients()
        {
            return ItemsWithQuantity.CreateFromDictionary(GetRecipe());
        }

        public static Item[] GetCraftableItems()
        {
            return new List<Item>(AllItems)
                .FindAll(i => i.GetRecipe() != null)
                .ToArray();
        }

        public static Item GetByName(string itemName)
        {
            return Array.Find(AllItems, item => item.Name == itemName);
        }

        public bool OwnedByPlayer()
        {
            return Player.Instance.inventory.IsItemAvailable(this, 1);
        }
    }
}