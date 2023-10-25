using System;
using Buffs;
using Items;
using Newtonsoft.Json.Serialization;
using Singletons;
using UnityEngine;

namespace Meters.Components
{
    public class BodyMaintenance : MonoBehaviour
    {
        // Start is called before the first frame update

        [SerializeField] public int increaseOnClick;
        [SerializeField] public int adjustAmount;
        [SerializeField] public Item decreaseInventoryItem;
        [SerializeField] private MeterBar meter;
        [SerializeField] private int currentCompound;
        [SerializeField] private bool isWater;
        public ResourceNotification resourceNotification;

        public void ClickEatDrink()
        {
            float buffAdjustment; 
            if (isWater)
            {
                buffAdjustment = Buff.ActiveEffects().drinkingConsumptionMultiplier;
            }
            else
            {
                buffAdjustment = Buff.ActiveEffects().foodConsumptionMultiplier;
            }

            int adjust = (int) Math.Round (-adjustAmount * buffAdjustment) ;
            
            if (Player.Instance.inventory.IsItemAvailable(decreaseInventoryItem, adjust))
            {
                resourceNotification.PlaySuccess();
                meter.AdjustValue(this.increaseOnClick);
                // This needs to also decrease the number of items that are in the 
                // inventory through eating and drinking
                Player.Instance.inventory.AddItem(decreaseInventoryItem, adjustAmount);
                
                resourceNotification.SetMessage("\n\n" + adjustAmount + " " + decreaseInventoryItem.Name);
            }
            else
            {
                resourceNotification.PlayFailure();
                resourceNotification.SetMessage("No " + decreaseInventoryItem.Name + " in inventory!");
            }
        }

        public void ClickSleep() //adjust this so it compounds if you have not slept
        {
            if (meter.isZero())
            {
                currentCompound = currentCompound + 1;
            }
            else
            {
                currentCompound = 0;
            }
            meter.AdjustValue(increaseOnClick - (currentCompound * 10));
            GameTime.Instance.NextDay();
        }
    }
}
