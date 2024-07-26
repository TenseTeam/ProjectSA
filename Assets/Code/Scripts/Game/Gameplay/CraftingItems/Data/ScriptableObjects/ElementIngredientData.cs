namespace ProjectSA.Gameplay.CraftingItems.Data.ScriptableObjects
{
    using UnityEngine;
    using VUDK.Features.CraftingSystem.Data.ScriptableObjects;
    using VUDK.Features.Main.ScriptableKeys;

    [CreateAssetMenu(fileName = "ElementIngredientData", menuName = "Items/ElementIngredient", order = 1)]
    public class ElementIngredientData : IngredientData
    {
        public Texture2D SignDecalTexture;
        [Min(0f)]
        public float InkCost;
        [Min(0f)]
        public float BloodCost;
        public ScriptableKey IngredientPoolKey;
        public ScriptableKey UsableElementPoolKey;
    }
}