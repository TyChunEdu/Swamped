// using System.Collections.Generic;
// using Items;

using System;
using System.Collections;
using System.Collections.Generic;
using Buffs;
using Items;
using Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Missions.Components
{
    public class MissionMenu : MonoBehaviour
    {
        public ButtonList missionOptions;
        public TMP_Text missionText;
        public TextList missionRewards;
        public TMP_Text difficultyText;
        public TMP_Text pRewards;
        public TMP_Text pConsequences;
        private int numOfMissionsForTheDay;
        public ResourceNotification resourceNotification;
        private string resourcesGained = "";
        
        [SerializeField] private AudioClip audioClipStartMission;
        [SerializeField] private AudioClip audioClipEndMission;
        [SerializeField] private AudioSource audioSource;
        
        void Start()
        {
            Hide();
        }
        
        
        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        

        public bool isMissionAvailable()
        {
            int remainingMissions = Buff.ActiveEffects().numOfMissionsForTheDay -
                                    Player.Instance.numOfMissionsCompletedToday;
            return remainingMissions > 0;
        }
        
        public void StartMission(Mission mission)
        {
            Player.Instance.numOfMissionsCompletedToday += 1;

            audioSource.PlayOneShot(audioClipStartMission);

            SceneManager.LoadScene(mission.name);
            SceneManager.LoadScene("Scenes/Level UI Layer", LoadSceneMode.Additive);
        }
        
        public void EndMission()
        {
            audioSource.PlayOneShot(audioClipEndMission);

            Hide();
            GameTime.Instance.Unpause();
        }

        public void StartRandomMission()
        {
            if (isMissionAvailable())
            {
                RenderMission(Player.Instance.activeBuff.mission);
                Show();
            }
            else
            {
                resourceNotification.SetMessage("You're out of missions for today!");
                resourceNotification.Show();
            }
            
        }
        
        
        
        public void RenderMission(Mission mission)
        {
            GameTime.Instance.Pause();
            missionRewards.gameObject.SetActive(false);
            Player.Instance.currentMission = mission;
            
            // Render mission description
            missionText.text = mission.description;
            
            // Render mission details
            // Difficulty
            switch (mission.difficulty)
            {
                case "Easy":
                    difficultyText.text = "<color=#c7c6c1>Difficulty:</color> <color=green>Easy</color>";
                    break;
                case "Medium":
                    difficultyText.text = "<color=#c7c6c1>Difficulty:</color> <color=yellow>Medium</color>";
                    break;
                case "Hard":
                    difficultyText.text = "<color=#c7c6c1>Difficulty:</color> <color=orange>Hard</color>";
                    break;
                case "Extreme":
                    difficultyText.text = "<color=#c7c6c1>Difficulty:</color> <color=red>Extreme</color>";
                    break;
            }

            // Possible Rewards
            IList<string> possibleItems = new List<string>();

            IDictionary<Item, int> dict1 = mission.greatOutcome.reward.AsDictionary();
            foreach (KeyValuePair<Item, int> kvp in dict1)
            {
                if (!possibleItems.Contains(kvp.Key.Name))
                {
                    possibleItems.Add(kvp.Key.Name);
                }
            }
            
            IDictionary<Item, int> dict2 = mission.neutralOutcome.reward.AsDictionary();
            foreach (KeyValuePair<Item, int> kvp in dict2)
            {
                if (!possibleItems.Contains(kvp.Key.Name))
                {
                    possibleItems.Add(kvp.Key.Name);
                }
            }
            
            IDictionary<Item, int> dict3 = mission.badOutcome.reward.AsDictionary();
            foreach (KeyValuePair<Item, int> kvp in dict3)
            {
                if (!possibleItems.Contains(kvp.Key.Name))
                {
                    possibleItems.Add(kvp.Key.Name);
                }
            }
            
            string joinedPItems = string.Join(", ", possibleItems);
            if (joinedPItems.Replace(" ", "").Equals(""))
            {
                pRewards.text = "<color=#c7c6c1>Possible Rewards:</color> ???";
            }
            else
            {
                pRewards.text = "<color=#c7c6c1>Possible Rewards:</color> " + joinedPItems;
            }
            
            // Possible stamina cost
            int max = -20;

            int min = -20;

            max = Math.Max(mission.greatOutcome.energyPercentChange, max);
            min = Math.Min(mission.greatOutcome.energyPercentChange, min);
            max = Math.Max(mission.neutralOutcome.energyPercentChange, max);
            min = Math.Min(mission.neutralOutcome.energyPercentChange, min);
            max = Math.Max(mission.badOutcome.energyPercentChange, max);
            min = Math.Min(mission.badOutcome.energyPercentChange, min);

            if (min == max)
            {
                pConsequences.text = "<color=#c7c6c1>Stamina:</color> " + min;
            }
            else if (max > 0)
            {
                pConsequences.text = "<color=#c7c6c1>Stamina:</color> Between " + min + " and +" + max;
            }
            else
            {
                pConsequences.text = "<color=#c7c6c1>Stamina:</color> Between " + min + " and " + max;
            }

            // Render mission choices
            var continueButton = new ButtonList.ButtonData(
                mission.startMissionPrompt, 
                () => StartMission(mission)
            );
            var exitButton = new ButtonList.ButtonData("Ignore it", EndMission);
            var buttonList = new List<ButtonList.ButtonData> { exitButton, continueButton };
            missionOptions.RenderButtons(buttonList);
        }

        public void RenderMissionOutcome(MissionOutcome outcome)
        {
            Time.timeScale = 0f;
            Show();
            audioSource.Stop();
            var exitButton = new ButtonList.ButtonData("Head back home", () =>
            {
                audioSource.PlayOneShot(audioClipEndMission);
                Time.timeScale = 1f;
                GameTime.Instance.SkipHours(Player.Instance.currentMission.lengthInHours);
                SceneManager.LoadScene("TestScene");
                GameTime.Instance.Unpause();
            });
            
            // Render outcome description
            missionText.text = outcome.description;
            
            // Render outcome options
            var buttonList = new List<ButtonList.ButtonData> { exitButton };
            missionOptions.RenderButtons(buttonList);

            // Render outcome rewards
            var rewards = outcome.PerformOutcome();
            if (rewards.TotalQuantity() == 0)
            {
                missionRewards.gameObject.SetActive(false);
                resourceNotification.PlayFailure();
            } 
            else 
            {
                missionRewards.gameObject.SetActive(true);
                resourceNotification.PlaySuccess();
                var rewardStrings = new List<string>();
                foreach (var (item, quantity) in rewards.AsDictionary())
                    rewardStrings.Add($"{quantity}x {item.Name}");
                
                // Notification
                resourcesGained = "";
                foreach (var (i_, q_) in rewards.AsDictionary())
                {
                    resourcesGained += "\n\n" + "+" + q_ + " " + i_.Name;
                }
                
                resourceNotification.SetMessage(resourcesGained);

                missionRewards.RenderList(rewardStrings);
            }
        }
    }
}