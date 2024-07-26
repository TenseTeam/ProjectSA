namespace ProjectSA.Gameplay.CraftingItems
{
    using VUDK.Patterns.Initialization.Interfaces;
    using VUDK.Patterns.Pooling;
    using VUDK.Patterns.Pooling.Interfaces;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.GameConstants;
    using ProjectSA.Gameplay.CraftingItems.Data.ScriptableObjects;
    using ProjectSA.Gameplay.InteractSystem.Interactables.Base;

    public class AlchemicSignInteractable : GameInteractable, IInit<AlchemicSignIngredientData>, IPooledObject
    {
        public Pool RelatedPool { get; private set; }
        public AlchemicSignIngredientData IngredientData { get; private set; }

        public void Init(AlchemicSignIngredientData arg)
        {
            IngredientData = arg;
        }

        public bool Check()
        {
            return IngredientData != null;
        }

        public void AssociatePool(Pool associatedPool)
        {
            RelatedPool = associatedPool;
        }

        public void Dispose()
        {
            RelatedPool.Dispose(gameObject);
        }

        public void Clear()
        {
            IngredientData = null;
        }

        public override void Interact()
        {
            base.Interact();
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnAlchemicSignInteracted, this);
        }

        public override void SecondaryInteract()
        {
            base.SecondaryInteract();
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnAlchemicSignSecondaryInteracted, this);
        }
    }
}