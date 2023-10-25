using Buffs;
using Items;
using Singletons;
using TMPro;
using UnityEngine;

namespace DailyUpdates.Components
{ 
    public class DailyUpdates : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;

        [SerializeField] private TMP_Text details;
        
        private StoryDialogue _sd;

        [SerializeField] private new AudioClip audio;

        [SerializeField] private AudioSource audioSource;

        // Start is called before the first frame update
        private void Start()
        {
            gameObject.SetActive(false);
        }

        // To be called for when the user clicks the continue button
        public void Continue()
        {
            gameObject.SetActive(false);
            GameTime.Instance.Unpause();
        }

        // To be displayed when a day is over, or when the player wakes up to a new day
        public void Display(int day)
        {
            gameObject.SetActive(true);
            audioSource.PlayOneShot(audio);
            GameTime.Instance.Pause();
            title.text = "Day " + day + " Complete!";
            details.text = StoryDialogue.GetMsg(day)
                           + "\n\n"
                           + DailyItemGeneration.Instance.ItemGenerationDaySummary()
                           + Robot.GetBrokenRobotsSummary()
                           + "\n\n"
                           + Buff.GetActiveBuffsToString();
        }
    }
}