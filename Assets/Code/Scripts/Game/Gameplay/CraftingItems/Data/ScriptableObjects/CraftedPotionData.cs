namespace ProjectSA.Gameplay.CraftingItems.Data.ScriptableObjects
{
    using UnityEngine;
    using VUDK.Features.CraftingSystem.Data.ScriptableObjects;
    using VUDK.Features.More.DialogueSystem.Data;

    [CreateAssetMenu(fileName = "CraftedPotionData", menuName = "Items/Potion", order = 1)]
    public class CraftedPotionData : CraftedItemDataBase
    {
        public string PotionName;
        public Color PotionColor;
        public DSDialogueContainerData DialogueToTrigger;
    }
}