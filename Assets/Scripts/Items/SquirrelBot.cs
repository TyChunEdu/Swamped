using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Squirrel Bot")]
    public class SquirrelBot : Robot
    {
        public ItemsWithQuantity itemPercentages;
        
        public override void AddDailyResourcesToInventory()
        {
            CollectItem(itemPercentages.RandomItem(), 1);
        }
    }
}
