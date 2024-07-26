namespace ProjectSA.Gameplay.Spawners.AlchemicSignsSpawner
{
    using System;
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.GameConstants;
    using CraftingItems;
    using ProjectSA.Patterns.Factories;
    using CraftingItems.Data.ScriptableObjects;
    using UnityEngine.Rendering.Universal;

    public class AlchemicSignSlot : MonoBehaviour
    {
        [Header("Slot Settings")]
        [SerializeField]
        private Transform _slotPosition;
        [SerializeField]
        private DecalProjector _decal;
        
        private Material _decalMaterial;
        private AlchemicSignInteractable _currentSignInteractable;

        private void Awake()
        { 
            _decalMaterial = new Material(_decal.material);
            _decal.material = _decalMaterial;
        }

        private void OnEnable()
        {
            EventManager.Ins.AddListener<AlchemicSignInteractable>(PSAEventKeys.OnSignAddedToHand, OnSignAddedToHand);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener<AlchemicSignInteractable>(PSAEventKeys.OnSignAddedToHand, OnSignAddedToHand);
        }

        public void FillSlot(AlchemicSignIngredientData data)
        {
            if (_currentSignInteractable)
                _currentSignInteractable.Dispose();
            
            _decalMaterial.SetTexture("_BaseMap", data.SignDecalTexture);
            _currentSignInteractable = GameFactory.CreateAlchemicSign(data);
            _currentSignInteractable.transform.SetPositionAndRotation(_slotPosition.position, _slotPosition.rotation);
        }

        public void ReleaseSlot()
        {
            _currentSignInteractable = null;
        }
        
        private void OnSignAddedToHand(AlchemicSignInteractable signInteractable)
        {
            if (signInteractable == _currentSignInteractable)
                ReleaseSlot();
        }
    }
}