using Singletons;

namespace Meters.Components
{
    public class StaminaBar : MeterBar
    {
        private void Start() // override Update from MeterBar to throw a LoseException when you hit zero
        {
            Meter = Player.Instance.StaminaMeter;
        }
    }
}