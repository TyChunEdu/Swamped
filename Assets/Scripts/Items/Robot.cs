using System;
using Buffs;
using DailyUpdates.Components;
using Singletons;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Items
{
    [CreateAssetMenu(menuName = "Robot")]
    public class Robot : Item
    {
        public const float ChanceOfBreaking = 0.08f;
        public const int MinimumRobotsRequiredForBreaking = 4;

        public static Robot[] AllRobots;
        
        public ItemsWithQuantity dailyResourceGeneration;
        
        protected void CollectItem(Item item, int quantity)
        {
            var finalQuantity = quantity;
            finalQuantity *= (int) Math.Round (Buff.ActiveEffects().resourceCollectionMultiplier);

            foreach (var (checkBot, adjustment) in Player.Instance.robotItemProductionMultiplier.AsDictionary())
            {
                if (checkBot == this)
                {
                    finalQuantity = (int) Math.Round (finalQuantity * adjustment);
                }
            }
            
            Player.Instance.inventory.AddItem(item, finalQuantity);
            DailyItemGeneration.Instance.Record(this, item, finalQuantity);
        }

        public virtual void AddDailyResourcesToInventory()
        {
            foreach (var (item, quantity) in dailyResourceGeneration.AsDictionary())
                CollectItem(item, quantity);
        }

        public static RobotsWithQuantity BreakRobots()
        {
            var brokenRobots = new RobotsWithQuantity();
            var robotsInInventory = Player.Instance.inventory.GetRobotsInInventory();
            if (robotsInInventory.TotalQuantity() >= MinimumRobotsRequiredForBreaking)
            {
                foreach (var (robot, quantity) in robotsInInventory.AsDictionary())
                    for (var i = 0; i < quantity; i++)
                        if (Random.Range(0f, 1f) <= ChanceOfBreaking)
                            brokenRobots.AddRobot(robot);
                Player.Instance.inventory.Remove(brokenRobots);
            }
            return brokenRobots;
        }

        public static string GetBrokenRobotsSummary()
        {
            var brokenRobots = BreakRobots();

            if (brokenRobots.TotalQuantity() == 0)
                return "";
            
            string summary = "\n\nOh no!: Some of your robots broke overnight.\nYou lost ";

            foreach (var (robot, quantity) in brokenRobots.AsDictionary())
            {
                summary += $"{quantity} {robot.Name}, ";
            }
            summary = summary.Remove(summary.Length - 2);
            
            return summary;
        }
    }
}