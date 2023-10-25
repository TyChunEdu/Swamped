using SaveAndLoad;
using Singletons;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PrologueTutorialScreen : MonoBehaviour
{

    public Button mainMenuButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void GoToTutorial0()
    {
        SceneManager.LoadScene("Tutorial0");
    }

    public void GoToTutorial1()
    {
        SceneManager.LoadScene("Tutorial1");
    }

    public void GoToTutorial2()
    {
        SceneManager.LoadScene("Tutorial2");
    }

    public void GoToTutorial3()
    {
        SceneManager.LoadScene("Tutorial3");
    }

    public void GoToTutorialEnd()
    {
        SceneManager.LoadScene("TutorialEnd");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
