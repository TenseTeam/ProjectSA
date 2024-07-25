namespace ProjectSA.Gameplay.Spawners.AlchemicSignsSpawner
{
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.GameConstants;
    using ProjectSA.Gameplay.Items;
    using ProjectSA.Patterns.Factories;
    using ProjectSA.Gameplay.Items.Data.ScriptableObjects;

    public class AlchemicSignSlot : MonoBehaviour
    {
        [Header("Slot Settings")]
        [SerializeField]
        private Transform _slotPosition;
        
        private AlchemicSign _currentSign;

        private void OnEnable()
        {
            EventManager.Ins.AddListener<AlchemicSign>(PSAEventKeys.OnSignAddedToHand, OnSignAddedToHand);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener<AlchemicSign>(PSAEventKeys.OnSignAddedToHand, OnSignAddedToHand);
        }

        public void FillSlot(AlchemicSignIngredientData data)
        {
            if (_currentSign)
                _currentSign.Dispose();
            
            _currentSign = GameFactory.CreateAlchemicSign(data);
            _currentSign.transform.SetPositionAndRotation(_slotPosition.position, _slotPosition.rotation);
        }

        public void ReleaseSlot()
        {
            _currentSign = null;
        }
        
        private void OnSignAddedToHand(AlchemicSign sign)
        {
            if (sign == _currentSign)
                ReleaseSlot();
        }
    }
}