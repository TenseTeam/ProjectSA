namespace ProjectSA.GameConstants
{
    public class PSAEventKeys
    {
        // Interaction Events
        public const string OnInteractInteractable = "OnPlayerInteract";
        public const string OnSecondaryInteractInteractable = "OnPlayerSecondaryInteract";
        public const string OnEnableInteractable = "OnEnableInteractable";
        public const string OnDisableInteractable = "OnDisableInteractable";
        public const string OnInkBottleInteracted = "OnInkBottleInteracted";
        public const string OnElementInteracted = "OnElementInteracted";
        public const string OnElementSecondaryInteracted = "OnElementSecondaryInteracted";
        
        // Player Events
        public const string OnPlayerSeat = "OnPlayerSeat";
        public const string OnPlayerUnseat = "OnPlayerUnseat";
        public const string OnPlayerDeath = "OnPlayerDeath";
        public const string OnPlayerDamaged = "OnPlayerRevive";
        public const string OnElementAddedToHand = "OnElementAddedToHand";
        public const string OnElementRemovedFromHand = "OnElementRemovedFromHand";
        
        // Player Resources Events
        public const string OnInkInit = "OnInkInit";
        public const string OnBloodInit = "OnBloodInit";
        public const string OnInkConsumed = "OnInkConsumed";
        public const string OnBloodConsumed = "OnBloodConsumed";
        public const string OnBoughtIngredient = "OnBoughtIngredient";
        
        // Game Timers Events
        public const string OnRequestTimerStart = "OnRequestTimerStart";
        public const string OnStunTimerStart = "OnStunTimerStart";
        public const string OnRequestTimerEnd = "OnRequestTimerEnd";
        public const string OnStunTimerEnd = "OnStunTimerEnd";
        public const string OnRequestTimerTick = "OnRequestTimerTick";
        public const string OnStunTimerTick = "OnStunTimerTick";
        
        // Puzzle Events
        public const string OnCrystalPuzzleSolved = "OnCrystalPuzzleSolved";
        public const string OnChestPuzzleSolved = "OnChestPuzzleSolved";
        
        // Items Events
        public const string OnLightOn = "OnLightOn";
        public const string OnLightOff = "OnLightOff";
        public const string OnLightConsuming = "OnLightConsuming";
        
        // Crafting Events
        public const string OnAddedCraftIngredient = "OnAddedCraftIngredient";
        public const string OnRemovedCraftIngredient = "OnRemovedCraftIngredient";
        public const string OnClearCraftIngredients = "OnClearCraftIngredients";
        public const string OnCraftStarted = "OnStartCraft";
        public const string OnCraftCompleted = "OnCraftComplete";
        public const string OnCraftedRecipeSuccess = "OnCraftedRecipe";
        public const string OnCraftedRecipeFail = "OnFailedCraft";
        
        // Match Request Events
        public const string OnRequestSuccess = "OnRequestSuccess";
        public const string OnRequestFail = "OnRequestFail";
        public const string OnAllRequestsSatisfied = "OnAllRequestsSatisfied";
        
        // UI Events
        public const string OnClickedBuyIngredient = "OnClickedBuyIngredient";
    }
}