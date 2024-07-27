namespace ProjectSA.Player.Cauldron.EventArgs
{
    using System;
    using System.Collections.Generic;
    using VUDK.Features.CraftingSystem.Data.ScriptableObjects;

    public class CauldronCraftEventArgs : EventArgs
    {
        public RecipeData CraftedRecipe;
        public List<IngredientData> UsedIngredients;
        
        public CauldronCraftEventArgs(RecipeData craftedRecipe, List<IngredientData> usedIngredients) 
        {
            CraftedRecipe = craftedRecipe;
            UsedIngredients = usedIngredients;
        }
    }
}