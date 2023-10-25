using System;
using Buffs;
using Singletons;
using UnityEngine;

namespace Meters
{
    [Serializable]
    public class ThirstMeter : Meter
    {
        public new void UpdateAmount()
        {
            changeMeter(-minuteByMinuteDecrease * Buff.ActiveEffects().thirstMultiplier);
            
        }
        
    }
}