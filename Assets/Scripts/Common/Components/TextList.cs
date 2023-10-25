using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TextList : MonoBehaviour
{
    public GameObject scrollArea;
    public GameObject itemPrefab;
    
    private readonly UIListManager _listManager = new();
    
    public class TextData
    {
        public string Text { get; }
        public Color Color { get; }
        public bool IsColored { get; }

        public TextData(string text, Color color)
        {
            Text = text;
            Color = color;
            IsColored = true;
        }

        public TextData(string text)
        {
            Text = text;
            IsColored = false;
        }
    }
    
    public void RenderList(List<string> strings)
    {
        var itemList = strings.Select(str => new TextData(str)).ToList();
        RenderList(itemList);
    }
    
    public void RenderList(List<TextData> items)
    {
        _listManager.DestroyListObjects();
        
        // Add items
        foreach (var textData in items)
        {
            var newItem = Instantiate(itemPrefab, scrollArea.transform);
            var textComponent = newItem.GetComponent<TMP_Text>();
            textComponent.text = textData.Text;
            if (textData.IsColored)
                textComponent.color = textData.Color;
            _listManager.AddListObject(newItem);
        }
    }
}
