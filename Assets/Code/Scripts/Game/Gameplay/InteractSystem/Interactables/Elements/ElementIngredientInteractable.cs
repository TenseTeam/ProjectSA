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

        protected override void Awake()
        {
            base.Awake();
            // In Awake, not in OnEnable, because you can spawn an element while is crafting
            EventManager.Ins.AddListener(PSAEventKeys.OnCraftStarted, OnCraftStarted);
            EventManager.Ins.AddListener(PSAEventKeys.OnCraftCompleted, OnCraftCompleted);
        }

        private void OnDestroy()
        {
            EventManager.Ins.RemoveListener(PSAEventKeys.OnCraftStarted, OnCraftStarted);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnCraftCompleted, OnCraftCompleted);
        }

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

        protected override void OnInteract()
        {
            base.OnInteract();
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnElementInteracted, IngredientData);
        }

        public override void SecondaryInteract()
        {
            base.SecondaryInteract();
            
            if (IngredientData.UsableElementPoolKey) // If the element is usable is pickable
                EventManager.Ins.TriggerEvent(PSAEventKeys.OnElementSecondaryInteracted, IngredientData);
        }
        
        private void OnCraftCompleted()
        {
            EnableInteraction(false);
        }
        
        private void OnCraftStarted()
        {
            DisableInteraction(true);
        }
    }
}