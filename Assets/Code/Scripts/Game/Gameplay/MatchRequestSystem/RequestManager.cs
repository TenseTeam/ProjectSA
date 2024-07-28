namespace ProjectSA.Gameplay.MatchRequestSystem
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.CraftingSystem.Data.ScriptableObjects;
    using ProjectSA.GameConstants;
    using ProjectSA.Player.Cauldron.EventArgs;

    public class RequestManager : MonoBehaviour
    {
        [Header("Request Settings")]
        [SerializeField]
        private List<CraftedItemDataBase> _requestedItems;
        [SerializeField]
        private CraftedItemDataBase _secretItem;
        
        private List<CraftedItemDataBase> _unsatisfiedItems;
        private CauldronCraftEventArgs _currentCauldronArgs;
        
        public bool IsSecretItemCrafted { get; private set; } = false;
        public bool AreAllRequestsSatisfied => _unsatisfiedItems.Count == 0;
        
        private void Awake()
        {
            _unsatisfiedItems = new List<CraftedItemDataBase>(_requestedItems);
        }
        
        private void OnEnable()
        {
            EventManager.Ins.AddListener<CauldronCraftEventArgs>(PSAEventKeys.OnCraftedRecipeSuccess, OnCraftedRecipe);
            EventManager.Ins.AddListener(PSAEventKeys.OnCraftedRecipeFail, RequestFail);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener<CauldronCraftEventArgs>(PSAEventKeys.OnCraftedRecipeSuccess, OnCraftedRecipe);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnCraftedRecipeFail, RequestFail);
        }
        
        private void OnCraftedRecipe(CauldronCraftEventArgs args)
        {
            _currentCauldronArgs = args;
            CheckRequest(_currentCauldronArgs.CraftedRecipe);
        }
        
        private void CheckRequest(RecipeData recipeData)
        {
            if (_secretItem == recipeData.Result)
            {
                Debug.Log("Secret item crafted");
                EventManager.Ins.TriggerEvent(PSAEventKeys.OnSecretItemCrafted);
                EventManager.Ins.TriggerEvent(PSAEventKeys.OnRequestSuccess, _currentCauldronArgs);
                IsSecretItemCrafted = true;
                _currentCauldronArgs = null;
                return;
            }
            
            if(_unsatisfiedItems.Contains(recipeData.Result))
                RequestSuccess();
            else
                RequestFail();
            
            _currentCauldronArgs = null;
        }

        private void RequestSuccess()
        {
            Debug.Log("Request Success");
            _unsatisfiedItems.Remove(_currentCauldronArgs.CraftedRecipe.Result);
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnRequestSuccess, _currentCauldronArgs);

            if (_unsatisfiedItems.Count == 0)
            {
                Debug.Log("<color=green>All requests satisfied</color>");
                EventManager.Ins.TriggerEvent(PSAEventKeys.OnAllRequestsSatisfied);
            }
        }
        
        private void RequestFail()
        {
            Debug.Log("Request Fail");
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnRequestFail);
        }
    }
}