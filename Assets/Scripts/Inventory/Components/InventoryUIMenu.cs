

/*
 * A very scuffed Inventory UI Script put together solely for the purpose of opening and closing
 * Could (and probably should) be enhanced to allow for interaction when the deliverables require it
 * The reason there's a lot of code commented out is because I figured it could serve as a decent template
 * for when the inventory ui menu code needs to expand.
 */

using System.Linq;
using Items;
using Singletons;
using UnityEngine;

namespace Inventory.Components
{
    public class InventoryUIMenu : UIMenu
    {
        public ItemWindow itemWindow;
        public InventorySlot inventorySlot;
        public GameObject slots;
    
        private readonly UIListManager _listManager = new();
        
        void Start()
        {
            Player.Instance.inventory.AddChangeListener(this);
            Hide();
        }

        public override void Show()
        {
            Render();
            gameObject.SetActive(true);
        }

        public override void Render()
        {
            _listManager.DestroyListObjects();
            var ownedItems = Item.AllItems.ToList().FindAll(item => item.OwnedByPlayer());
            var notOwnedItems = Item.AllItems.ToList().FindAll(item => !item.OwnedByPlayer());
            var sortedItems = Enumerable.Concat(ownedItems, notOwnedItems); // owned items should be displayed first in inventory
            foreach (var item in sortedItems)
            {
                var slotGameObject = Instantiate(inventorySlot.gameObject, slots.transform);
                var slot = slotGameObject.GetComponent<InventorySlot>();
                slot.RenderSlot(item, itemWindow);
                _listManager.AddListObject(slotGameObject);
            }
        }
    }
}