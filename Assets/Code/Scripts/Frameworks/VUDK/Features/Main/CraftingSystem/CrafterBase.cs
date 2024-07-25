namespace VUDK.Features.CraftingSystem
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using VUDK.Generic.Serializable;
    using VUDK.Features.CraftingSystem.Data.ScriptableObjects;

    public abstract class CrafterBase : MonoBehaviour
    {
        [Header("Crafter Settings")]
        [SerializeField]
        private CookbookData _cookbook;
        [SerializeField]
        private DelayTask _craftTask;

        private List<IngredientData> _currentIngredients = new List<IngredientData>();

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
            _currentIngredients.Add(ingredient);
            OnAddedIngredient(ingredient);
        }

        public void RemoveIngredient(IngredientData ingredient)
        {
            _currentIngredients.Remove(ingredient);
            OnRemovedIngredient(ingredient);
        }

        public void ClearIngredients()
        {
            _currentIngredients.Clear();
            OnClearIngredients();
        }

        public void StartCraft()
        {
            _craftTask.Start();
            OnStartCraft();
        }

        protected abstract void OnAddedIngredient(IngredientData ingredient);

        protected abstract void OnRemovedIngredient(IngredientData ingredient);

        protected abstract void OnClearIngredients();

        protected abstract void OnStartCraft();

        protected abstract void OnSuccessCraft(RecipeData craftedRecipe);

        protected abstract void OnFailCraft();

        private void CheckCraft()
        {
            if (TryGetIngredientsResult(out RecipeData craftedRecipe))
                SuccessCraft(craftedRecipe);
            else
                FailCraft();
        }

        private void SuccessCraft(RecipeData craftedRecipe)
        {
            Debug.Log($"Crafted recipe {craftedRecipe.name}...");
            ClearIngredients();
            OnSuccessCraft(craftedRecipe);
        }

        private void FailCraft()
        {
            Debug.Log("Failed to craft recipe...");
            ClearIngredients();
            OnFailCraft();
        }

        private bool TryGetIngredientsResult(out RecipeData craftedRecipe)
        {
            foreach (RecipeData recipe in _cookbook.Recipes)
            {
                if (recipe.Ingredients.Length != _currentIngredients.Count) continue;

                bool allIngredientsMatch = true;
                foreach (IngredientData ingredient in _currentIngredients)
                {
                    if (!recipe.Ingredients.Contains(ingredient))
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