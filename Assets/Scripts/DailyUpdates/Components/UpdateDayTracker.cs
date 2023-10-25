using Singletons;
using UnityEngine;

namespace DailyUpdates.Components
{
    public class UpdateDayTracker : MonoBehaviour
    {
        public static UpdateDayTracker Instance { get; private set; }
        
        [SerializeField] private DailyUpdates du;
        
        private void Awake()
        {
            Instance = this;
        }

        public void DisplayDailyUpdates()
        {
            du.Display(GameTime.Instance.daysElapsed);
        }
    }
}
