namespace ProjectSA.Player.Cauldron
{
    using System.Collections.Generic;
    using EventArgs;
    using Managers.GameMachine.Data.Enums;
    using Managers.GameManager;
    using UnityEngine;
    using VUDK.Features.CraftingSystem;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.CraftingSystem.Data.ScriptableObjects;
    using ProjectSA.GameConstants;
    using ProjectSA.Gameplay.CraftingItems.Data.ScriptableObjects;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces.Casts;

    [RequireComponent(typeof(PlayerCauldronInteractable))]
    [RequireComponent(typeof(CauldronGraphicsController))]
    public class PlayerCauldron : CrafterBase, ICastGameManager<PSAGameManager>
    {
        private CauldronGraphicsController _graphicsController;
        private PlayerCauldronInteractable _cauldronInteractable;

        public PSAGameManager GameManager => MainManager.Ins.GameManager as PSAGameManager;
        
        private void Awake()
        {
            TryGetComponent(out _graphicsController);
            TryGetComponent(out _cauldronInteractable);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            EventManager.Ins.AddListener<ElementIngredientData>(PSAEventKeys.OnElementInteracted, OnElementInteracted);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EventManager.Ins.RemoveListener<ElementIngredientData>(PSAEventKeys.OnElementInteracted, OnElementInteracted);
        }

        protected override void OnAddedIngredient(IngredientData ingredient)
        {
            Debug.Log("Adding ingredient: " + ingredient.name);
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnAddedCraftIngredient, ingredient);
            _graphicsController.AddIngredientUI();
        }

        protected override void OnRemovedIngredient(IngredientData ingredient)
        {
            Debug.Log("Removing ingredient: " + ingredient.name);
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnRemovedCraftIngredient, ingredient);
            _graphicsController.RemoveIngredientUI();
        }

        protected override void OnClearIngredients()
        {
            Debug.Log("Clearing ingredients");
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnClearCraftIngredients);
            _graphicsController.ClearIngredientsUI();
        }

        protected override void OnStartCraft()
        {
            if (GameManager.GameMachine.IsState(GameStateKeys.StunState))
            {
                EventManager.Ins.TriggerEvent(PSAEventKeys.OnTriedCraftWhileStunned);
                return;
            }
            
            base.OnStartCraft();
            Debug.Log("Started cauldron craft");
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnCraftStarted);
            _graphicsController.ClearIngredientsUI();
            _graphicsController.ShowCauldronEffect();
            _cauldronInteractable.DisableInteraction();
        }

        protected override void OnCraftCompleted()
        {
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnCraftCompleted);
            _graphicsController.HideCauldronEffect();
            _cauldronInteractable.EnableInteraction(false);
        }

        protected override void OnSuccessCraft(RecipeData craftedRecipe)
        {
            Debug.Log("Crafted recipe: " + craftedRecipe.name);
            CauldronCraftEventArgs args = new CauldronCraftEventArgs(craftedRecipe, new List<IngredientData>(CurrentIngredients));
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnCraftedRecipeSuccess, args);
        }

        protected override void OnFailCraft()
        {
            Debug.Log("Failed craft");
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnCraftedRecipeFail);
        }

        private void OnElementInteracted(ElementIngredientData ingredientData)
        {
            AddIngredient(ingredientData);
        }

#if UNITY_EDITOR
        [Header("Debug")]
        [SerializeField]
        private IngredientData _ingredientToAdd;

        [ContextMenu("Start Craft")]
        private void DebugCraft()
        {
            StartCraft();
        }

        [ContextMenu("Add Ingredient")]
        private void DebugAddIngredient()
        {
            AddIngredient(_ingredientToAdd);
        }
#endif
    }
}