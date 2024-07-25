namespace ProjectSA.GameConstants
{
    public class PSAEventKeys
    {
        // Interaction Events
        public const string OnInteractInteractable = "OnPlayerInteract";
        public const string OnEnableInteractable = "OnEnableInteractable";
        public const string OnDisableInteractable = "OnDisableInteractable";
        public const string OnInkBottleInteracted = "OnInkBottleInteracted";
        public const string OnAlchemicSignInteracted = "OnAlchemicSignInteracted";
        
        // Player Events
        public const string OnPlayerSeat = "OnPlayerSeat";
        public const string OnPlayerUnseat = "OnPlayerUnseat";
        public const string OnPlayerDeath = "OnPlayerDeath";
        public const string OnPlayerDamaged = "OnPlayerRevive";
        public const string OnSignAddedToHand = "OnSignAddedToHand";
        public const string OnSignRemovedFromHand = "OnSignRemovedFromHand";
        
        // Player Resources Events
        public const string OnInkInit = "OnInkInit";
        public const string OnBloodInit = "OnBloodInit";
        public const string OnInkConsumed = "OnInkConsumed";
        public const string OnBloodConsumed = "OnBloodConsumed";
        public const string OnBoughtIngredient = "OnBoughtIngredient";
        
        // Crafting Events
        public const string OnAddedCraftIngredient = "OnAddedCraftIngredient";
        public const string OnRemovedCraftIngredient = "OnRemovedCraftIngredient";
        public const string OnClearCraftIngredients = "OnClearCraftIngredients";
        public const string OnStartCraft = "OnStartCraft";
        public const string OnCraftedRecipe = "OnCraftedRecipe";
        public const string OnFailedCraft = "OnFailedCraft";
        
        // Match Request Events
        public const string OnRequestSuccess = "OnRequestSuccess";
        public const string OnRequestFail = "OnRequestFail";
        
        // UI Events
        public const string OnClickedBuyIngredient = "OnClickedBuyIngredient";
    }
}