namespace ProjectSA.GameConstants
{
    public class PSAEventKeys
    {
        // Gameover Events
        public const string OnGameover = "OnGameover";
        public const string OnGamevictory = "OnGamevictory";
        
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
        public const string OnPlayerFirstSeat = "OnPlayerFirstSeat";
        public const string OnElementAddedToHand = "OnElementAddedToHand";
        public const string OnElementRemovedFromHand = "OnElementRemovedFromHand";
        
        // Player Resources Events
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
        public const string OnSkullPuzzleSolved = "OnSkullPuzzleSolved";
        public const string OnCantInteractPuzzle = "OnCantInteractPuzzle";
        
        // Items Events
        public const string OnLightOn = "OnLightOn";
        public const string OnLightOff = "OnLightOff";
        public const string OnLightConsumingTick = "OnLightConsuming";
        
        // Crafting Events
        public const string OnAddedCraftIngredient = "OnAddedCraftIngredient";
        public const string OnRemovedCraftIngredient = "OnRemovedCraftIngredient";
        public const string OnClearCraftIngredients = "OnClearCraftIngredients";
        public const string OnCraftStarted = "OnStartCraft";
        public const string OnCraftCompleted = "OnCraftComplete";
        public const string OnCraftedRecipeSuccess = "OnCraftedRecipe";
        public const string OnCraftedRecipeFail = "OnFailedCraft";
        public const string OnSecretItemCrafted = "OnSecretItemCrafted";
        public const string OnTriedCraftWhileStunned = "OnTriedCraftWhileStunned";
        
        // Match Request Events
        public const string OnRequestSuccess = "OnRequestSuccess";
        public const string OnRequestFail = "OnRequestFail";
        public const string OnAllRequestsSatisfied = "OnAllRequestsSatisfied";
        
        // UI Events
        public const string OnOpenElementsPanel = "OnOpenShopPanel";
        public const string OnCloseElementsPanel = "OnCloseShopPanel";
        public const string OnClickedBuyIngredientWithInk = "OnClickedBuyIngredientWithInk";
        public const string OnClickedBuyIngredientWithBlood = "OnClickedBuyIngredientWithBlood";
        
        // State Machine Events
        public const string OnRequestState = "OnRequestState";
        public const string OnStunState = "OnStunState";
    }
}