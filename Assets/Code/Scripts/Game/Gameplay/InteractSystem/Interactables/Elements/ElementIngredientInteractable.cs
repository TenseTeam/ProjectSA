namespace ProjectSA.Gameplay.CraftingItems.Elements
{
    using VUDK.Patterns.Pooling;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Patterns.Pooling.Interfaces;
    using VUDK.Patterns.Initialization.Interfaces;
    using ProjectSA.GameConstants;
    using ProjectSA.Gameplay.InteractSystem.Interactables.Base;
    using ProjectSA.Gameplay.CraftingItems.Data.ScriptableObjects;

    public class ElementIngredientInteractable : GameInteractable, IInit<ElementIngredientData>, IPooledObject
    {
        public Pool RelatedPool { get; private set; }
        public ElementIngredientData IngredientData { get; private set; }

        public void Init(ElementIngredientData arg)
        {
            IngredientData = arg;
            ((ElementInteractableGraphicsController)GraphicsController).SetElementIngredientData(IngredientData);
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
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnElementInteracted, IngredientData);
        }

        public override void SecondaryInteract()
        {
            base.SecondaryInteract();
            
            if (IngredientData.UsableElementPoolKey) // If the element is usable is pickable
                EventManager.Ins.TriggerEvent(PSAEventKeys.OnElementSecondaryInteracted, IngredientData);
        }
    }
}