namespace ProjectSA.Patterns.Factories
{
    using Gameplay.UsableItems.Base;
    using Player;
    using VUDK.Features.Main.ScriptableKeys;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.Pooling;
    using ProjectSA.Managers;
    using ProjectSA.Gameplay.CraftingItems.Elements;
    using ProjectSA.Gameplay.CraftingItems.Data.ScriptableObjects;
    using ProjectSA.Gameplay.MatchRequestSystem.Potions;

    /// <summary>
    /// Responsible for creating game objects.
    /// </summary>
    public static class GameFactory
    {
        private static PoolsManager PoolsManager => MainManager.Ins.PoolsManager;
        private static PSAGamePoolsKeys GamePoolsKeys => MainManager.Ins.GamePoolsKeys as PSAGamePoolsKeys;

        public static Potion CreatePotionComboItem(CraftedPotionData potionData)
        {
            Potion potion = PoolsManager.Pools[GamePoolsKeys.PotionPoolKey].Get<Potion>();
            potion.Init(potionData);
            return potion;
        }
        
        public static UsableItemBase CreateUsableItem(ElementIngredientData elementData, PlayerHand playerHand)
        {
            ScriptableKey poolKey = elementData.UsableElementPoolKey;
            UsableItemBase usableItem = PoolsManager.Pools[poolKey].Get<UsableItemBase>();
            usableItem.Init(playerHand, elementData);
            return usableItem;
        }

        public static ElementIngredientInteractable CreateElementIngredient(ElementIngredientData ingredientData)
        {
            ScriptableKey poolKey = ingredientData.IngredientPoolKey;
            ElementIngredientInteractable elementIngredientInteractable = PoolsManager.Pools[poolKey].Get<ElementIngredientInteractable>();
            elementIngredientInteractable.Init(ingredientData);
            return elementIngredientInteractable;
        }
    }
}