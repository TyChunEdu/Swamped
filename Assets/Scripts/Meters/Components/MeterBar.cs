using Singletons;
using UnityEngine;
using UnityEngine.UI;

// Meter Bar class is inspired by the ManaBar class in  https://www.youtube.com/watch?v=gHdXkGsqnlw 
// The meter bar has been changed from horizontal to vertical
// The names of this class have been changed to be more relevant
// The visuals of the bar have also been changed, with the bar being vertical and text added to clarify what the bar is
// for.

namespace Meters.Components
{
    public abstract class MeterBar : MonoBehaviour
    {
        private Image _meterImage;
        public Meter Meter;
        
        private void Awake()
        {
            _meterImage = transform.Find("Bar").GetComponent<Image>();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            _meterImage.fillAmount = Meter.getMeterNormalized();
        }

        public void AdjustValue(int value)
        {
            Meter.changeMeter(value);
        }

        public bool isZero()
        {
            return Meter.getMeterNormalized() <= 0f;
        }
        
    }
}
