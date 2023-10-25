using System;
using Buffs;
using InGameMenu;
using Singletons;
using UnityEngine;

// Meter class is inspired by the Mana class as shown in this video https://www.youtube.com/watch?v=gHdXkGsqnlw 
// This class ahs been changed as there are no longer default fields. 
// The update function has also been changed from increasing the current amount to decreasing
// the current amount
namespace Meters
{
    [Serializable]
    public class Meter
    {
        [SerializeField]
        public float currentMeterAmount;
        [SerializeField]
        protected float minuteByMinuteDecrease;
        [SerializeField]
        public int maxAmount;
        [SerializeField]
        private bool pauseOvernight;

        /// <summary>
        /// Should be called at the start of the game.
        /// </summary>
        public void SetUp()
        {
            GameTime.Instance.AddRepeatedTimer(1,UpdateAmount,pauseOvernight);
        }

        // Update is called once per minute
        public void UpdateAmount()
        {
            changeMeter(-minuteByMinuteDecrease);
        }

        public void changeMeter(float amount)
        {
            currentMeterAmount += amount;
            if (currentMeterAmount > maxAmount)
                currentMeterAmount = maxAmount;
            else if (currentMeterAmount <= 0f)
            {
                currentMeterAmount = 0f;

                if (loseAtZero())
                {
                    if (this is ThirstMeter)
                    {
                        InGameMenuManager.Instance.LoseGame("You Died of Thirst!");
                    }
                    else
                    {
                        InGameMenuManager.Instance.LoseGame("You Died of Hunger!");
                    }
                }
            }
            
        }

        public float getMeterNormalized()
        {
            return currentMeterAmount / maxAmount;
        }

        private bool loseAtZero()
        {
            return this is ThirstMeter || this is HungerMeter;
        }
    }
}
