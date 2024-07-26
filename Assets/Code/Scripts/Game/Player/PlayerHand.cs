namespace ProjectSA.Player
{
    using GameConstants;
    using Gameplay.CraftingItems;
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;

    public class PlayerHand : MonoBehaviour
    {
        [Header("Player Hand Settings")]
        [SerializeField]
        private Transform _handTransform;

        // TODO: Spawn a game usable object in the player's hand
        // private AlchemicSignInteractable _currentAlchemicSignInteractable;
        
        private void OnEnable()
        {
            EventManager.Ins.AddListener<AlchemicSignInteractable>(PSAEventKeys.OnAlchemicSignInteracted, OnAlchemicSignInteracted);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener<AlchemicSignInteractable>(PSAEventKeys.OnAlchemicSignInteracted, OnAlchemicSignInteracted);
        }
        
        public void AddAlchemiSignToHand(AlchemicSignInteractable alchemicSignInteractable)
        {
            // if (_currentAlchemicSignInteractable)
            //     _currentAlchemicSignInteractable.Dispose();
            //
            // _currentAlchemicSignInteractable = alchemicSignInteractable;
            // _currentAlchemicSignInteractable.transform.SetParent(_handTransform);
            // _currentAlchemicSignInteractable.transform.SetPositionAndRotation(_handTransform.position, _handTransform.rotation);
            // EventManager.Ins.TriggerEvent(PSAEventKeys.OnSignAddedToHand, _currentAlchemicSignInteractable);
        }
        
        public void RemoveAlchemicSignFromHand()
        {
            // if (_currentAlchemicSignInteractable)
            // {
            //     _currentAlchemicSignInteractable.Dispose();
            //     EventManager.Ins.TriggerEvent(PSAEventKeys.OnSignRemovedFromHand);
            //     _currentAlchemicSignInteractable = null;
            // }
        }
        
        private void OnAlchemicSignInteracted(AlchemicSignInteractable alchemicSignInteractable)
        {
            // AddAlchemiSignToHand(alchemicSignInteractable);
        }
    }
}