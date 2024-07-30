namespace VUDK.Features.CraftingSystem.Data.ScriptableObjects
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "CookbookData", menuName = "VUDK/Crafting/Cookbook", order = 1)]
    public class CookbookData : ScriptableObject
    {
        public RecipeData[] Recipes;
    }
}