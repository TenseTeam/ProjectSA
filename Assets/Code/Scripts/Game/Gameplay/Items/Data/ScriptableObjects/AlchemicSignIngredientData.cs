namespace ProjectSA.Gameplay.Items.Data.ScriptableObjects
{
    using UnityEngine;
    using VUDK.Features.CraftingSystem.Data.ScriptableObjects;
    using VUDK.Features.Main.ScriptableKeys;

    [CreateAssetMenu(fileName = "AlchemicSignIngredientData", menuName = "Items/AlchemicSign", order = 1)]
    public class AlchemicSignIngredientData : IngredientData
    {
        public Texture2D SignDecalTexture;
        [Min(0f)]
        public float InkCost;
        [Min(0f)]
        public float BloodCost;
        public ScriptableKey IngredientPoolKey;
    }
}