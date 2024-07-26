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
            EventManager.Ins.AddListener<RecipeData>(PSAEventKeys.OnCraftedRecipeSuccess, OnCraftedRecipe);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener<RecipeData>(PSAEventKeys.OnCraftedRecipeSuccess, OnCraftedRecipe);
        }
        
        public void SendProvidedItem()
        {
            if (!_currentProvidedItem) return;
            
            CheckRequest(_currentProvidedItem);
        }
        
        private void OnCraftedRecipe(RecipeData recipe)
        { 
            _currentProvidedItem = recipe.Result;
            SendProvidedItem();
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
            Debug.Log("Request Success");
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnRequestSuccess, craftedItem);

            if (_unsatisfiedItems.Count == 0)
            {
                Debug.Log("<color=green>All requests satisfied</color>");
                EventManager.Ins.TriggerEvent(PSAEventKeys.OnAllRequestsSatisfied);
            }
        }
        
        private void RequestFail(CraftedItemDataBase craftedItem)
        {
            Debug.Log("Request Fail");
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnRequestFail, craftedItem);
        }
    }
}