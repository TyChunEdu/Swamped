using UnityEngine;

public abstract class UIMenu : MonoBehaviour
{
    public abstract void Render();

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
    
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public void ToggleVisibility()  
    {
        if (gameObject.activeInHierarchy)
            Hide();
        else
            Show();
    }
}