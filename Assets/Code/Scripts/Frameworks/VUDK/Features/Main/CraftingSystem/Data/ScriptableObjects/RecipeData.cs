namespace VUDK.Features.CraftingSystem.Data.ScriptableObjects
{
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "CraftedItemData", menuName = "VUDK/Crafting/Recipe", order = 1)]
    public class RecipeData : ScriptableObject
    {
        public IngredientData[] Ingredients;
        public CraftedItemDataBase Result;
    }
}