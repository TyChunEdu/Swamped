using Missions.Components;
using UnityEngine;

namespace Singletons
{
    public class LevelPrompts : MonoBehaviour
    {
        public static LevelPrompts Instance { get; private set; }
        public MissionMenu missionMenu;

        private void Awake()
        {
            if (Instance != null && Instance != this) 
                Destroy(this);
            else
            {
                Instance = this;
            }
        }
    }
}