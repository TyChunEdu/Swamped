using Singletons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace InGameMenu
{
    public class InGameMenu : UIMenu
    {
        // Start is called before the first frame update
        public virtual void Start()
        {
            Hide();
        }
        
        public override void Show()
        {
            gameObject.SetActive(true);
            GameTime.Instance.Pause();
        }

        public override void Hide()
        {
            base.Hide();
            GameTime.Instance.Unpause();
        }

        public override void Render() {}

        public void ExitToMenu()
        {
            MainMenu.MainMenu.ReturnToMenu();
        }
        
        public void ExitGame()
        {
            Application.Quit();
        }
        
    }
}
