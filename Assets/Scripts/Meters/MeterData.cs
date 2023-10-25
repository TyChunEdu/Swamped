using UnityEngine;

namespace Meters
{
    [CreateAssetMenu(menuName = "Meter Data")]
    public class MeterData : ScriptableObject
    {
        public float defaultFill;
        public int meterMax;
        public float minuteByMinuteDecrease;
        public bool pauseOvernight;
    }
}