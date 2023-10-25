using System;
using UnityEngine;
using Singletons;
using System.Collections.Generic;
using System.Linq;
using Items;
using Random = System.Random;

namespace Buffs
{
    [Serializable]
    public class BuffEffects
    {
        public float energyMultiplier = 1f;
        public float thirstMultiplier = 1f;
        public float hungerMultiplier = 1f;
        public float resourceCollectionMultiplier = 1f;
        public float craftingSuccessRateMultiplier = 1f;
        public float foodConsumptionMultiplier = 1f;
        public float craftingConsumptionMultiplier = 1f;
        public float drinkingConsumptionMultiplier = 1f;
        public int numOfMissionsForTheDay = 1;

        public bool isRampaging = false;
        private PseudoDictionary<Robot, float> originalProduction;
        public bool hasRampaged = false;


        public void SetRampaging()
        {
            if (!hasRampaged)
            {
                originalProduction = Player.Instance.robotItemProductionMultiplier;
            }
            var chosenRobots = new List<Robot>(); // Robots that are NOT suppossed to generate any resources
            for (var i = 0; i < 3; i++)
            {
                var availableRobots = Robot.AllRobots.Except(chosenRobots);
                var chosenRobot = Utils.Utils.RandomChoice(availableRobots.ToArray());
                chosenRobots.Add(chosenRobot);
            }

            var mult = new Dictionary<Robot, float>();
            foreach (var robot in chosenRobots)
            {
                mult.Add(robot, 0f);
                // Makes the genereation of the bot zero
            }
            
            Player.Instance.robotItemProductionMultiplier.FromDictionary(mult);
        }

        public void SetNotRampaging()
        {
            Player.Instance.robotItemProductionMultiplier = originalProduction;
        }
        
    }
    
}