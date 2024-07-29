namespace VUDK.Features.CraftingSystem
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Generic.Serializable;
    using VUDK.Features.CraftingSystem.Data.ScriptableObjects;

    public abstract class CrafterBase : MonoBehaviour
    {
        [Header("Crafter Settings")]
        [SerializeField]
        private CookbookData _cookbook;
        [SerializeField]
        private bool _useExactIngredients;
        [SerializeField]
        private DelayTask _craftTask;

        protected List<IngredientData> CurrentIngredients = new List<IngredientData>();

        public bool IsCrafting => _craftTask.IsRunning;
        
        protected virtual void OnEnable()
        {
            _craftTask.OnTaskCompleted += CheckCraft;
        }

        protected virtual void OnDisable()
        {
            _craftTask.OnTaskCompleted -= CheckCraft;
        }

        protected virtual void Update()
        {
            _craftTask.Process();
        }

        public void AddIngredient(IngredientData ingredient)
        {
            if (IsCrafting) return;
            
            CurrentIngredients.Add(ingredient);
            OnAddedIngredient(ingredient);
        }

        public void RemoveIngredient(IngredientData ingredient)
        {
            if (IsCrafting) return;
            
            CurrentIngredients.Remove(ingredient);
            OnRemovedIngredient(ingredient);
        }

        public void ClearIngredients()
        {
            if (IsCrafting) return;
            
            CurrentIngredients.Clear();
            OnClearIngredients();
        }

        public void StartCraft()
        {
            if (IsCrafting || CurrentIngredients.Count == 0) return;
            
            _craftTask.Start();
            OnStartCraft();
        }

        protected abstract void OnAddedIngredient(IngredientData ingredient);

        protected abstract void OnRemovedIngredient(IngredientData ingredient);

        protected abstract void OnClearIngredients();

        protected abstract void OnStartCraft();
        
        protected abstract void OnCraftCompleted();

        protected abstract void OnSuccessCraft(RecipeData craftedRecipe);

        protected abstract void OnFailCraft();

        private void CheckCraft()
        {
            if (TryGetIngredientsResult(out RecipeData craftedRecipe))
                SuccessCraft(craftedRecipe);
            else
                FailCraft();
            
            OnCraftCompleted();
        }

        private void SuccessCraft(RecipeData craftedRecipe)
        {
            OnSuccessCraft(craftedRecipe);
            ClearIngredients();
        }

        private void FailCraft()
        {
            OnFailCraft();
            ClearIngredients();
        }

        private bool TryGetIngredientsResult(out RecipeData craftedRecipe)
        {
            foreach (RecipeData recipe in _cookbook.Recipes)
            {
                if (_useExactIngredients)
                    if (recipe.Ingredients.Length != CurrentIngredients.Count) continue;

                bool allIngredientsMatch = true;
                foreach (IngredientData ingredient in recipe.Ingredients)
                {
                    if (!CurrentIngredients.Contains(ingredient))
                    {
                        allIngredientsMatch = false;
                        break;
                    }
                }

                if (allIngredientsMatch)
                {
                    craftedRecipe = recipe;
                    return true;
                }
            }

            craftedRecipe = null;
            return false;
        }
    }
}