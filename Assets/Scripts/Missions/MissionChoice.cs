using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Missions
{
    [Serializable]
    public class MissionChoice
    {
        [FormerlySerializedAs("choiceDescription")] [TextArea]
        public string description;
        public MissionOutcome greatOutcome;
        public MissionOutcome neutralOutcome;
        public MissionOutcome badOutcome;

        [Header("Outcome chance overrides")]
        [SerializeField]
        private bool overrideOutcomeChances;
        [SerializeField]
        private int greatChanceOverride;
        [SerializeField]
        private int neutralChanceOverride;
        [SerializeField]
        private int badChanceOverride;

        private static int _greatOutcomeChance = 25;
        private static int _neutralOutcomeChance = 50;
        private static int _badOutcomeChance = 25;

        public int GreatOutcomeChance => overrideOutcomeChances ? greatChanceOverride : _greatOutcomeChance;
        public int NeutralOutcomeChance => overrideOutcomeChances ? neutralChanceOverride : _neutralOutcomeChance;
        public int BadOutcomeChance => overrideOutcomeChances ? badChanceOverride : _badOutcomeChance;

        public MissionOutcome GetOutcome()
        {
            var allChoices = Enumerable.Repeat(greatOutcome, GreatOutcomeChance)
                .Concat(Enumerable.Repeat(neutralOutcome, NeutralOutcomeChance))
                .Concat(Enumerable.Repeat(badOutcome, BadOutcomeChance))
                .ToArray();
            return Utils.Utils.RandomChoice(allChoices);
        }
    }
}