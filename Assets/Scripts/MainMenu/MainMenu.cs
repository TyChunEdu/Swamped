using SaveAndLoad;
using Singletons;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        public Button loadGameButton;
    
        // Start is called before the first frame update
        void Start()
        {
            GameTime.Instance.Pause();
            loadGameButton.interactable = Save.SaveFileExists();
        }

        public void NewGame()
        {
            SceneManager.LoadScene("TestScene");
            GameTime.Instance.Unpause();
        }

        public void LoadGame()
        {
            Save.LoadGame();
            SceneManager.LoadScene("TestScene");
            GameTime.Instance.Unpause();
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void LoadTutorial0()
        {
            SceneManager.LoadScene("Tutorial0");

        }

        public static void ReturnToMenu()
        {
            Destroy(Player.Instance.gameObject);
            SceneManager.LoadScene("Main Menu");
        }
    }
}
