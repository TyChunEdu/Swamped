using System;
using Items;
using Singletons;
using UnityEngine;

namespace Missions
{
    [Serializable]
    public class MissionOutcome
    {
        [TextArea]
        public string description;
        public ItemsWithQuantity reward;
        public int energyPercentChange;
        [Header("Random Rewards")]
        [Min(0)]
        public int randomRewardQuantity;
        public ItemsWithQuantity randomReward;
    
        /// <summary>
        /// Applies the outcome effects (e.g. rewards, energy change) to the player
        /// </summary>
        /// <returns>An <see cref="ItemsWithQuantity"/> representing the rewards.</returns>
        public ItemsWithQuantity PerformOutcome()
        {
            // Calculate rewards and add to inventory
            var allRewards = new ItemsWithQuantity();
            if (reward != null)
                allRewards.Add(reward);
            for (var i = 0; i < randomRewardQuantity; i++)
                allRewards.AddItem(randomReward.RandomItem());
            Player.Instance.inventory.Add(allRewards);
            
            // Reduce stamina
            var staminaChange = Player.Instance.StaminaMeter.maxAmount * energyPercentChange * .01f;
            Player.Instance.StaminaMeter.changeMeter(staminaChange);
            
            return allRewards;
        }
    }
}