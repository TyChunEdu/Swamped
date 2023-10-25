using Singletons;

namespace Meters.Components
{
    public class ThirstBar : MeterBar
    {
        private void Start()
        {
            Meter = Player.Instance.ThirstMeter;
        }
    }
}