using System.Linq;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Crafting.Components
{
    public class DetailsPane : MonoBehaviour
    {
        public TMP_Text recipeName;
        public TMP_Text recipeDescription;
        public TMP_Text failRate;
        public Image recipeImage;
        public TextList ingredientsList;
        public Color missingIngredientColor;
        public string defaultName = "Crafting!";
        public string defaultDescription = "Here you can craft recipes.";

        public void Reset()
        {
            recipeName.text = defaultName;
            recipeDescription.text = defaultDescription;
            recipeImage.gameObject.SetActive(false);
            ingredientsList.gameObject.SetActive(false);
            failRate.text = "";
        }

        public void UpdateDetails(Item item)
        {
            recipeImage.gameObject.SetActive(true);
            ingredientsList.gameObject.SetActive(true);
            recipeName.text = item.Name;
            recipeDescription.text = item.Description;
            recipeImage.sprite = item.Image;
            if (item.failRate > 0)
            {
                failRate.text = item.failRate + "% Chance Crafting Fails";
            }
            else
            {
                failRate.text = "";
            }

            var ingredientNames = item.GetRecipe()
                .Select(entry =>
                {
                    var inventoryItem = entry.Key;
                    var quantity = entry.Value;
                    var quantityInInventory = Singletons.Player.Instance.inventory.NumberOfItem(inventoryItem);
                    var ingredientString = $"{quantity}x {inventoryItem.Name} (have {quantityInInventory})";
                    return quantityInInventory < quantity 
                        ? new TextList.TextData(ingredientString, missingIngredientColor) 
                        : new TextList.TextData(ingredientString);
                }).ToList();
            ingredientsList.RenderList(ingredientNames);
        }
    }
}
