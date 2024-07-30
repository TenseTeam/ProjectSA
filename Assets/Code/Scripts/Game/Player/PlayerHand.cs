namespace ProjectSA.Player
{
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.GameConstants;
    using ProjectSA.Patterns.Factories;
    using ProjectSA.Gameplay.UsableItems.Base;
    using ProjectSA.Gameplay.CraftingItems.Data.ScriptableObjects;

    public class PlayerHand : MonoBehaviour
    {
        [Header("Player Hand Settings")]
        [SerializeField]
        private Transform _handTransform;
        
        public UsableItemBase CurrentItem { get; private set; }
        
        private void OnEnable()
        {
            EventManager.Ins.AddListener<ElementIngredientData>(PSAEventKeys.OnElementSecondaryInteracted, OnElementGrabbed);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener<ElementIngredientData>(PSAEventKeys.OnElementSecondaryInteracted, OnElementGrabbed);
        }
        
        public void AddElementToHand(ElementIngredientData elementData)
        {
            if (CurrentItem)
                RemoveElementFromHand();

            CurrentItem = GameFactory.CreateUsableItem(elementData, this);
            CurrentItem.transform.SetPositionAndRotation(_handTransform.position, _handTransform.rotation);
            CurrentItem.transform.SetParent(_handTransform, true);
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnElementAddedToHand, CurrentItem);
        }
        
        public void RemoveElementFromHand()
        {
            if (!CurrentItem) return;
            
            CurrentItem.Dispose();
            CurrentItem = null;
            
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnElementRemovedFromHand);
        }
        
        private void OnElementGrabbed(ElementIngredientData elementData)
        {
            AddElementToHand(elementData);
        }
    }
}