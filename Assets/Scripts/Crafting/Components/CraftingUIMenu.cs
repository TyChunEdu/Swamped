using System.Collections.Generic;
using Items;
using TMPro;

namespace Crafting.Components
{
    public class CraftingUIMenu : UIMenu
    {
        public ButtonList recipeList;
        public DetailsPane detailsPane;
        public ResourceNotification resourceNotification;

        // Start is called before the first frame update
        void Start()
        {
            Hide();
            Singletons.Player.Instance.inventory.AddChangeListener(this);
        }

        public override void Show()
        {
            Render();
            detailsPane.Reset();
            gameObject.SetActive(true);
        }

        public override void Render()
        {
            var buttonData = new List<ButtonList.ButtonData>();
            foreach (var item in Item.GetCraftableItems())
            {
                var buttonEnabled = Singletons.Player.Instance.inventory.CanMake(item);
                buttonData.Add(
                    new(
                        item.Name, 
                        () => Singletons.Player.Instance.inventory.MakeItem(item, resourceNotification),
                        () => detailsPane.UpdateDetails(item),
                        () => detailsPane.Reset(),
                        buttonEnabled
                    )
                );            
            }

            recipeList.RenderButtons(buttonData);
        }

    }
}
