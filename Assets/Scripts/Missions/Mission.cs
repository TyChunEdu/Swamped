using System;
using UnityEngine;

namespace Missions
{
    [CreateAssetMenu(menuName = "Mission")]
    public class Mission : ScriptableObject
    {
        public static Mission[] AllMissions;
        
        [TextArea]
        public string description;
        [Min(0f)]
        public float lengthInHours;
        [Min(1)]
        public int minimumDay = 1;
        [Min(1)] public int maximumDay = 1;
        public string startMissionPrompt;
        public string difficulty;
        public MissionOutcome greatOutcome;
        public MissionOutcome neutralOutcome;
        public MissionOutcome badOutcome;
        public float timeLimit;
    }
}