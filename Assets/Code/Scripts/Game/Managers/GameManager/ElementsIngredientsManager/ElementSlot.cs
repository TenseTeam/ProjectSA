namespace ProjectSA.Managers.GameManager.ElementsIngredientsManager
{
    using Gameplay.UsableItems.Base;
    using UnityEngine;
    using UnityEngine.Rendering.Universal;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.GameConstants;
    using ProjectSA.Patterns.Factories;
    using ProjectSA.Gameplay.CraftingItems.Elements;
    using ProjectSA.Gameplay.CraftingItems.Data.ScriptableObjects;

    public class ElementSlot : MonoBehaviour
    {
        [Header("Slot Settings")]
        [SerializeField]
        private Transform _slotPosition;
        [SerializeField]
        private DecalProjector _decal;

        private Material _decalMaterial;
        private ElementIngredientInteractable _currentElement;

        private void Awake()
        {
            _decalMaterial = new Material(_decal.material);
            _decal.material = _decalMaterial;
            _decal.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            EventManager.Ins.AddListener<ElementIngredientData>(PSAEventKeys.OnElementInteracted, OnElementInteracted);
            EventManager.Ins.AddListener<UsableItemBase>(PSAEventKeys.OnElementAddedToHand, OnElementAddedToHand);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener<ElementIngredientData>(PSAEventKeys.OnElementInteracted, OnElementInteracted);
            EventManager.Ins.RemoveListener<UsableItemBase>(PSAEventKeys.OnElementAddedToHand, OnElementAddedToHand);
        }

        public void FillSlot(ElementIngredientData data)
        {
            ClearSlot();

            SetDecalTexture(data.SignDecalTexture);
            _currentElement = GameFactory.CreateElementIngredient(data);
            _currentElement.transform.SetPositionAndRotation(_slotPosition.position, _slotPosition.rotation);
        }

        public void ClearSlot()
        {
            if (!_currentElement) return;

            ClearDecalTexture();
            _currentElement.Dispose();
            _currentElement = null;
        }

        private void OnElementInteracted(ElementIngredientData ingredientData)
        {
            ClearSlot();
        }

        private void OnElementAddedToHand(UsableItemBase usableItem)
        {
            if (usableItem.ElementIngredientData == _currentElement.IngredientData)
                ClearSlot();
        }

        private void SetDecalTexture(Texture2D decalTexture)
        {
            _decal.gameObject.SetActive(true);
            _decalMaterial.SetTexture("Base_Map", decalTexture);
        }

        private void ClearDecalTexture()
        {
            _decal.gameObject.SetActive(false);
            _decalMaterial.SetTexture("Base_Map", null);
        }
    }
}