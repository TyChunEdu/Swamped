using Extensions;
using Items;
using Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.Components
{
    public class InventorySlot : MonoBehaviour
    {
        public TMP_Text quantityText;
        public GameObject quantityDisplay;
        public Image image;

        public void RenderSlot(Item item, ItemWindow itemWindow)
        {
            if (item.OwnedByPlayer())
            {
                quantityDisplay.SetActive(true);
                
                var amount = Player.Instance.inventory.NumberOfItem(item);
                quantityText.text = amount.ToString();
                
                image.sprite = item.Image;
                image.enabled = true;
                    
                var eventTrigger = gameObject.AddComponent<EventTrigger>();
                eventTrigger.AddTrigger(EventTriggerType.PointerEnter, () =>
                {
                    itemWindow.DisplayMessage(item.Name + "\n" + item.Description);
                });
                eventTrigger.AddTrigger(EventTriggerType.PointerExit, () =>
                {
                    itemWindow.HideMessage();
                });
            }
        }
    }
}