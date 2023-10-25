using System;
using Singletons;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Win_Lose
{
    public class LoseScreen : InGameMenu.InGameMenu
    {
        [SerializeField]
        protected TextMeshProUGUI loseText;

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public void changeText(string textChange)
        {
            loseText.text = textChange;
        }
    }
}