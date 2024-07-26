namespace ProjectSA.Gameplay.UsableItems.Base
{
    using System;
    using UnityEngine;
    using VUDK.Patterns.Initialization.Interfaces;
    using VUDK.Patterns.Pooling;
    using ProjectSA.Player;
    using ProjectSA.Gameplay.CraftingItems.Data.ScriptableObjects;
    using UnityEngine.InputSystem;
    using VUDK.Features.Main.InputSystem;

    public abstract class UsableItemBase : PooledObjectBase, IInit<PlayerHand, ElementIngredientData>
    {
        [Header("Usable Item Settings")]
        [SerializeField]
        private bool _canBeUsed = true;
        
        public ElementIngredientData IngredientData { get; private set; }
        protected PlayerHand PlayerHand { get; private set; }

        protected virtual void OnEnable()
        {
            InputsManager.Inputs.Interaction.UseItem.performed += UseInput;
        }

        protected virtual void OnDisable()
        {
            InputsManager.Inputs.Interaction.UseItem.performed -= UseInput;
        }

        public void Init(PlayerHand arg1, ElementIngredientData arg2)
        {
            PlayerHand = arg1;
            IngredientData = arg2;
        }
        
        public bool Check()
        {
            return PlayerHand != null && IngredientData != null;
        }
        
        public void EnableUsage()
        {
            _canBeUsed = true;
        }
        
        public void DisableUsage()
        {
            _canBeUsed = false;
        }
        
        public void Use()
        {
            if (!_canBeUsed) return;
            OnUse();
        }
        
        public void RemoveFromHand()
        { 
            PlayerHand.RemoveElementFromHand();
        }

        protected abstract void OnUse();
        
        private void UseInput(InputAction.CallbackContext obj)
        {
            Use();
        }
    }
}