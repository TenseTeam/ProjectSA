namespace ProjectSA.Gameplay.MatchRequestSystem.Potions
{
    using VUDK.Patterns.Initialization.Interfaces;
    using VUDK.Patterns.Pooling;
    using ProjectSA.Gameplay.CraftingItems.Data.ScriptableObjects;

    public class Potion : PooledObjectBase, IInit<CraftedPotionData>
    {
        public CraftedPotionData PotionData { get; private set; }
        
        public void Init(CraftedPotionData arg)
        {
            PotionData = arg;
        }
        
        public bool Check()
        {
            return PotionData != null;
        }
    }
}