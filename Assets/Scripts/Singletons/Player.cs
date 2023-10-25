using Buffs;
using Items;
using Meters;
using Missions;
using SaveAndLoad;
using UnityEngine;
using UnityEngine.SceneManagement;
using Win_Lose;

namespace Singletons
{
    /// <summary>
    /// Singleton pattern inspired by https://gamedevbeginner.com/singletons-in-unity-the-right-way/
    /// </summary>
    public class Player : MonoBehaviour
    {
        public static Player Instance { get; private set; }
        public Inventory.Inventory inventory;
        public Mission currentMission;
        public StaminaMeter StaminaMeter;
        public HungerMeter HungerMeter;
        public ThirstMeter ThirstMeter;
        public Buff activeBuff;
        public PseudoDictionary<Robot, float> robotItemProductionMultiplier;
        public int numOfMissionsCompletedToday = 0;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            robotItemProductionMultiplier = new();
            
            StaminaMeter.SetUp();
            HungerMeter.SetUp();
            ThirstMeter.SetUp();
            
            Mission.AllMissions ??= Utils.Utils.GetAllScriptableObjects<Mission>("Missions");
            Buff.AllBuffs ??= Utils.Utils.GetAllScriptableObjects<Buff>("Buffs");
            Robot.AllRobots ??= Utils.Utils.GetAllScriptableObjects<Robot>("Items/Craftables/Robots");
            Item.AllItems ??= Utils.Utils.GetAllScriptableObjects<Item>("Items");
        }

        public void OnValidate()
        {
            // This ensures any scripts listening to inventory changes
            // are updated when the player's inventory is edited from the inspector
            if (Instance != null)
            {
                inventory.CallChangeListeners();
            }
        }
    }
}
