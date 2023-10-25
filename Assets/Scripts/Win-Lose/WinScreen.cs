using System;
using Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Win_Lose
{
    public class WinScreen : InGameMenu.InGameMenu
    {
        [SerializeField]
        protected TextMeshProUGUI winText;

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public void changeText(string textChange)
        {
            winText.text = textChange;
        }
    }
}