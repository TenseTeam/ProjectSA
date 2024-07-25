namespace ProjectSA.Gameplay.MatchRequestSystem
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.CraftingSystem.Data.ScriptableObjects;
    using ProjectSA.GameConstants;

    public class RequestManager : MonoBehaviour
    {
        [Header("Request Settings")]
        [SerializeField]
        private List<CraftedItemDataBase> _requestedItems;
        
        private List<CraftedItemDataBase> _unsatisfiedItems;
        private CraftedItemDataBase _currentProvidedItem;
        
        private void Awake()
        {
            _unsatisfiedItems = new List<CraftedItemDataBase>(_requestedItems);
        }
        
        private void OnEnable()
        {
            EventManager.Ins.AddListener<RecipeData>(PSAEventKeys.OnCraftedRecipe, OnCraftedRecipe);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener<RecipeData>(PSAEventKeys.OnCraftedRecipe, OnCraftedRecipe);
        }

        [ContextMenu("Send Provided Item")] // TODO: Remove this, it's just for testing
        public void SendProvidedItem()
        {
            if (!_currentProvidedItem) return;
            
            CheckRequest(_currentProvidedItem);
        }
        
        private void OnCraftedRecipe(RecipeData recipe)
        { 
            _currentProvidedItem = recipe.Result;
        }
        
        private void CheckRequest(CraftedItemDataBase craftedItem)
        {
            if(_unsatisfiedItems.Contains(craftedItem))
                RequestSuccess(craftedItem);
            else
                RequestFail(craftedItem);
            
            _currentProvidedItem = null;
        }

        private void RequestSuccess(CraftedItemDataBase craftedItem)
        {
            _unsatisfiedItems.Remove(craftedItem);
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnRequestSuccess, craftedItem);
        }
        
        private void RequestFail(CraftedItemDataBase craftedItem)
        {
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnRequestFail, craftedItem);
        }
    }
}