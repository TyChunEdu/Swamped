using System;
using Buffs;
using Singletons;
using UnityEngine;

namespace Meters
{
    [Serializable]
    public class HungerMeter : Meter
    {
        public new void UpdateAmount()
        {
            changeMeter(-minuteByMinuteDecrease * Buff.ActiveEffects().hungerMultiplier);
            
        }
    }
}