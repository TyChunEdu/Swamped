using Singletons;

namespace Meters.Components
{
    public class HungerBar : MeterBar // Override Update from MeterBar to throw an exeption
    {
        private void Start()
        {
            Meter = Player.Instance.HungerMeter;
        }
    }
}