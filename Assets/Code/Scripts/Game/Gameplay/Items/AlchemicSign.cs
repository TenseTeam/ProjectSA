namespace ProjectSA.Gameplay.Items
{
    using VUDK.Patterns.Initialization.Interfaces;
    using VUDK.Patterns.Pooling;
    using VUDK.Patterns.Pooling.Interfaces;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.GameConstants;
    using ProjectSA.Gameplay.Items.Data.ScriptableObjects;
    using ProjectSA.Gameplay.InteractSystem.Interactables;

    public class AlchemicSign : GameInteractable, IInit<AlchemicSignIngredientData>, IPooledObject
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
    }
}