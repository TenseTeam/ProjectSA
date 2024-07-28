namespace ProjectSA.Gameplay.CraftingItems.Data.ScriptableObjects
{
    using UnityEngine;
    using VUDK.Features.CraftingSystem.Data.ScriptableObjects;

    [CreateAssetMenu(fileName = "CraftedPotionData", menuName = "Items/Potion", order = 1)]
    public class CraftedPotionData : CraftedItemDataBase
    {
        public string PotionName;
        public Color PotionColor;
    }
}