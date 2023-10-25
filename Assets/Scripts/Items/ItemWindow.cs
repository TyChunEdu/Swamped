using TMPro;
using UnityEngine;

public class ItemWindow : MonoBehaviour
{
    public TextMeshProUGUI itemText;
    public RectTransform itemBox;
    
    // Start is called before the first frame update
    void Start()
    {
        HideMessage();
    }

    private void Update()
    {
        var mousePos = Input.mousePosition;
        itemBox.transform.position = new Vector2(mousePos.x + itemBox.sizeDelta.x * 0.5f, mousePos.y);
    }

    public void DisplayMessage(string text)
    {
        var mousePos = Input.mousePosition;
        itemText.text = text;
        itemBox.sizeDelta = new Vector2(itemText.preferredWidth > 200 ? 200 : itemText.preferredWidth,
            itemText.preferredHeight);
        
        itemBox.gameObject.SetActive(true);
        itemBox.transform.position = new Vector2(mousePos.x + itemBox.sizeDelta.x * 0.5f, mousePos.y);
    }

    public void HideMessage()
    {
        itemText.text = default;
        itemBox.gameObject.SetActive(false);
    }
}
