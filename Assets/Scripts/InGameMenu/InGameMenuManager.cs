using System;
using Singletons;
using UnityEngine;
using Win_Lose;

namespace InGameMenu
{
    public class InGameMenuManager : MonoBehaviour
    {
        public static InGameMenuManager Instance { get; private set; }
        
        public InGameMenu menu;
        public LoseScreen loseScreen;
        public WinScreen winScreen;

        public bool gameLost;
        public bool gameWon;

        [SerializeField] protected AudioClip chillAudio;
        
        [SerializeField] protected AudioClip winAudio;
        
        [SerializeField] protected AudioClip loseAudio;

        [SerializeField] protected AudioSource audioSource;

        private void Awake()
        {
            Instance = this;
            audioSource.loop = true;
            audioSource.clip = chillAudio;
            audioSource.Play();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                menu.ToggleVisibility();
        }

        public void LoseGame(string reason)
        {
            gameLost = true;
            audioSource.clip = loseAudio;
            audioSource.Play();
            loseScreen.changeText(reason + Environment.NewLine + "Try Again!");
            loseScreen.Show();
            GameTime.Instance.Pause();
        }        
        
        public void WinGame(string reason)
        {
            gameWon = true;
            audioSource.clip = winAudio;
            audioSource.Play();
            winScreen.changeText(reason + Environment.NewLine + "Happy Travels!");
            winScreen.Show();
            GameTime.Instance.Pause();
        }

        public bool IsGameOver()
        {
            return gameLost || gameWon;
        }
    }
}