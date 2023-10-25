using System;
using System.Collections.Generic;
using Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using Button = UnityEngine.UI.Button;

public class ButtonList : MonoBehaviour
{

    public GameObject scrollArea;
    public GameObject buttonPrefab;
    public GameObject disabledButtonPrefab;
    
    private readonly UIListManager _listManager = new();

    public class ButtonData
    {
        public string ButtonText { get; }
        public Action ClickCallback { get; }
        public Action MouseEnterCallback { get; }
        public Action MouseExitCallback { get; }
        public bool Enabled { get; }

        public ButtonData(
            string buttonText, 
            Action clickCallback = null, 
            Action mouseEnterCallback = null, 
            Action mouseExitCallback = null,
            bool enabled = true)
        {
            ButtonText = buttonText;
            ClickCallback = clickCallback ?? (() => {});
            MouseEnterCallback = mouseEnterCallback ?? (() => {});
            MouseExitCallback = mouseExitCallback ?? (() => {});
            Enabled = enabled;
        }
    }

    public void RenderButtons(List<ButtonData> buttons)
    {
        _listManager.DestroyListObjects();
        
        // Add buttons
        foreach (var buttonData in buttons)
        {
            var newButton = Instantiate(buttonData.Enabled ? buttonPrefab : disabledButtonPrefab, scrollArea.transform);
            
            // Set button text
            newButton.GetTextComponent().text = buttonData.ButtonText;

            // Add click listener
            newButton.GetComponent<Button>().onClick.AddListener(() => buttonData.ClickCallback());
            
            var eventTrigger = newButton.AddComponent<EventTrigger>();
            
            // Add mouse enter listener
            eventTrigger.AddTrigger(EventTriggerType.PointerEnter, buttonData.MouseEnterCallback);
            
            // Add mouse exit
            eventTrigger.AddTrigger(EventTriggerType.PointerExit, buttonData.MouseExitCallback);
            
            _listManager.AddListObject(newButton);
        }
    }
}
