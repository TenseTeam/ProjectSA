namespace ProjectSA.Player
{
    using GameConstants;
    using Gameplay.Items;
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;

    public class PlayerHand : MonoBehaviour
    {
        [Header("Player Hand Settings")]
        [SerializeField]
        private Transform _handTransform;
        
        private AlchemicSign _currentAlchemicSign;
        
        private void OnEnable()
        {
            EventManager.Ins.AddListener<AlchemicSign>(PSAEventKeys.OnAlchemicSignInteracted, OnAlchemicSignInteracted);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener<AlchemicSign>(PSAEventKeys.OnAlchemicSignInteracted, OnAlchemicSignInteracted);
        }
        
        public void AddAlchemiSignToHand(AlchemicSign alchemicSign)
        {
            if (_currentAlchemicSign)
                _currentAlchemicSign.Dispose();
            
            _currentAlchemicSign = alchemicSign;
            _currentAlchemicSign.transform.SetParent(_handTransform);
            _currentAlchemicSign.transform.SetPositionAndRotation(_handTransform.position, _handTransform.rotation);
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnSignAddedToHand, _currentAlchemicSign);
        }
        
        public void RemoveAlchemicSignFromHand()
        {
            if (_currentAlchemicSign)
            {
                _currentAlchemicSign.Dispose();
                EventManager.Ins.TriggerEvent(PSAEventKeys.OnSignRemovedFromHand);
                _currentAlchemicSign = null;
            }
        }
        
        private void OnAlchemicSignInteracted(AlchemicSign alchemicSign)
        {
            AddAlchemiSignToHand(alchemicSign);
        }
    }
}