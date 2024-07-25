namespace ProjectSA.Player.Cauldron
{
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.GameConstants;
    using VUDK.Features.CraftingSystem;
    using VUDK.Features.CraftingSystem.Data.ScriptableObjects;

    [RequireComponent(typeof(CauldronGraphicsController))]
    public class PlayerCauldron : CrafterBase
    {
        private CauldronGraphicsController _graphicsController;

        private void Awake()
        {
            TryGetComponent(out _graphicsController);
        }

        protected override void OnAddedIngredient(IngredientData ingredient)
        {
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnAddedCraftIngredient, ingredient);
        }

        protected override void OnRemovedIngredient(IngredientData ingredient)
        {
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnRemovedCraftIngredient, ingredient);
        }

        protected override void OnClearIngredients()
        {
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnClearCraftIngredients);
        }

        protected override void OnStartCraft()
        {
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnStartCraft);
            _graphicsController.ChangeColor(Color.blue);
        }

        protected override void OnSuccessCraft(RecipeData craftedRecipe)
        {
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnCraftedRecipe, craftedRecipe);
            _graphicsController.ChangeColor(Color.green);
        }

        protected override void OnFailCraft()
        {
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnFailedCraft);
            _graphicsController.ChangeColor(Color.red);
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