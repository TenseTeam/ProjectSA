namespace ProjectSA.Patterns.Factories
{
    using VUDK.Features.Main.ScriptableKeys;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.Pooling;
    using ProjectSA.Managers;
    using ProjectSA.Gameplay.CraftingItems;
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

        public static AlchemicSignInteractable CreateAlchemicSign(AlchemicSignIngredientData ingredientData)
        {
            ScriptableKey poolKey = ingredientData.IngredientPoolKey;
            AlchemicSignInteractable alchemicSignInteractable = PoolsManager.Pools[poolKey].Get<AlchemicSignInteractable>();
            alchemicSignInteractable.Init(ingredientData);
            return alchemicSignInteractable;
        }
    }
}