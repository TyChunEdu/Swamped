using System;
using Buffs;
using InGameMenu;
using Items;
using Meters.Components;
using Singletons;
using TMPro;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public class Inventory : ItemsWithQuantity
    {
        // Checks if the player has [x] amount of a given item available
        public bool IsItemAvailable(Item item, int minimumQuantity)
        {
            return NumberOfItem(item) >= minimumQuantity;
        }
        
        public bool CanMake(Item item, int itemQuantity = 1)
        {
            var recipe = item.GetRecipe();
            if (recipe == null) return false;
            foreach (var (requiredItem, requiredQuantity) in recipe)
            {
                var totalRequiredQuantity = requiredQuantity * itemQuantity;
                if (!IsItemAvailable(requiredItem, totalRequiredQuantity))
                    return false;
            }
            return true;
        }
    
        // Checks if the Player can make a given item
        // Has two versions because we may want to give the player the option to either
        // make one, or an amount they choose of an item.
        // Returns whether they successfully crafted the item.
        public bool MakeItem(Item item, int quantity, ResourceNotification resourceNotification)
        {
            // Check if the player has enough energy
            MeterBar meter = GameObject.Find(item.meterName).GetComponent<MeterBar>();
            if (meter.Meter.currentMeterAmount + item.decrease < 0)
            {
                resourceNotification.SetMessage("Not enough stamina!");
                return false;
            }
            
            bool crafted = true;
            if (!CanMake(item, quantity))
                return false;
            
            for (int i = 0; i < quantity; i++)
            {
                Remove(item.GetIngredients());
                float f = UnityEngine.Random.Range(0, 100);
                f++;
                if (f > (item.failRate * Buff.ActiveEffects().craftingSuccessRateMultiplier))
                {
                    AddItem(item);
                    if (item.name == "Boat" || item.name == "Rocket")
                    {
                        string baseString = "Congratulations!" + System.Environment.NewLine +
                                            "You Escaped Using The";
                        if (item.name == "Boat")
                        {
                            baseString = baseString + " Boat!";
                            
                        }
                        else
                        {
                            baseString = baseString + " Rocket!";
                        }
                        InGameMenuManager.Instance.WinGame(baseString);
                    }
                }
                else
                {
                    crafted = false;
                }
            } 
            
            // reduce the energy for the given meter
            meter.AdjustValue(item.decrease);

            if (crafted)
            {
                if (item.name != "Boat" && item.name != "Rocket")
                {
                    resourceNotification.PlaySuccess();
                    string resourceGained = "\n\n" + "+1 " + item.Name;
                    string resourcesLost = "";
                    foreach (var (i_, q_) in item.GetRecipe())
                    {
                        resourcesLost += "\n\n" + "-" + q_ + " " + i_.Name;
                    }
                    resourceNotification.SetMessage(resourceGained + resourcesLost);
                }
            }
            else
            {
                resourceNotification.PlayFailure();
                resourceNotification.SetMessage("You accidentally broke the " 
                                                + item.Name + " while trying to craft it!"
                                                + "\n\nItems used to craft the " + item.Name + " were lost...");
            }

            return crafted;
        }

        // The second version of MakeItem that only makes one item.
        public bool MakeItem(Item item, ResourceNotification resourceNotification)
        {
            return MakeItem(item, 1, resourceNotification);
        }

        public RobotsWithQuantity GetRobotsInInventory()
        {
            var allRobots = Robot.AllRobots;
            RobotsWithQuantity robotsInInventory = new();
            foreach (var robot in allRobots)
            {
                var numberInInventory = NumberOfItem(robot);
                robotsInInventory.AddRobot(robot, numberInInventory);
            }
            return robotsInInventory;
        }
    }
}
