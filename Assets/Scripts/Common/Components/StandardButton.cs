using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Components
{
    [RequireComponent(typeof(EventTrigger))]
    [RequireComponent(typeof(Button))]
    public class StandardButton : MonoBehaviour
    {
        public TransformUtility text;
        public float textMoveAmount = 6f;

        private Button _button;
        
        private void Start()
        {
            _button = GetComponent<Button>();
            
            EventTrigger trigger = GetComponent<EventTrigger>();
            
            EventTrigger.Entry pointerDown = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerDown
            };
            pointerDown.callback.AddListener(data => OnPointerDown((PointerEventData)data));
            trigger.triggers.Add(pointerDown);
            
            EventTrigger.Entry pointerUp = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerUp
            };
            pointerUp.callback.AddListener(data => OnPointerUp((PointerEventData)data));
            trigger.triggers.Add(pointerUp);
        }

        private void OnPointerDown(PointerEventData data)
        {
            if (_button.interactable && data.button == PointerEventData.InputButton.Left)
                text.TranslateY(-textMoveAmount);
        }

        private void OnPointerUp(PointerEventData data)
        {
            if (_button.interactable && data.button == PointerEventData.InputButton.Left)
                text.TranslateY(textMoveAmount);
        }
    }
}