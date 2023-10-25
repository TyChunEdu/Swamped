using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Extensions
{
    public static class GameObjectExtensions
    {
        public static TMP_Text GetTextComponent(this GameObject obj)
        {
            return obj.transform.GetChild(0).GetComponent<TMP_Text>();
        }
    }

    public static class EventTriggerExtensions
    {
        public static void AddTrigger(this EventTrigger trigger, EventTriggerType eventType, Action callback)
        {
            var eventTriggerEntry = new EventTrigger.Entry();
            eventTriggerEntry.callback.AddListener(_ => callback());
            eventTriggerEntry.eventID = eventType;
            trigger.triggers.Add(eventTriggerEntry);
        }
    }
}