using System;
using Buffs;

namespace Meters
{
    [Serializable]
    public class StaminaMeter : Meter
    {
        public new void UpdateAmount()
        {
            changeMeter(-minuteByMinuteDecrease * Buff.ActiveEffects().energyMultiplier);
        }
    }
}