namespace VUDK.Features.CraftingSystem.Data.ScriptableObjects
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "IngredientData", menuName = "VUDK/Crafting/Ingredient", order = 1)]
    public class IngredientData : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
    }
}