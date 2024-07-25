namespace ProjectSA.Patterns.Factories
{
    using VUDK.Features.Main.ScriptableKeys;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.Pooling;
    using ProjectSA.Gameplay.Items;
    using ProjectSA.Managers;
    using ProjectSA.Gameplay.Items.Data.ScriptableObjects;

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

        public static AlchemicSign CreateAlchemicSign(AlchemicSignIngredientData ingredientData)
        {
            ScriptableKey poolKey = ingredientData.IngredientPoolKey;
            AlchemicSign alchemicSign = PoolsManager.Pools[poolKey].Get<AlchemicSign>();
            alchemicSign.Init(ingredientData);
            return alchemicSign;
        }
    }
}