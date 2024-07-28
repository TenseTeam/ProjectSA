namespace ProjectSA.UI.Crafting
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Extensions;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces.Casts;
    using ProjectSA.GameConstants;
    using ProjectSA.Managers.GameManager;
    using ProjectSA.Gameplay.CraftingItems.Data.ScriptableObjects;
    using UnityEngine.Serialization;

    public class UIElementsIngredientsShop : MonoBehaviour, ICastGameManager<PSAGameManager>
    {
        [Header("UI Elements")]
        [SerializeField]
        private GameObject _craftingPanel;
        [SerializeField]
        private GridLayoutGroup _grid;
        
        [FormerlySerializedAs("_uiSignPrefab"),Header("Signs Settings")]
        [SerializeField]
        private GameObject _uiElementPrefab;
        
        public PSAGameManager GameManager => MainManager.Ins.GameManager as PSAGameManager;
        public List<ElementIngredientData> CraftableSigns => GameManager.ElementsShopManager.ElementIngredients;
        
        private void Awake()
        {
            CloseCraftingPanel();
        }

        private void OnEnable()
        {
            EventManager.Ins.AddListener(PSAEventKeys.OnInkBottleInteracted, OpenCraftingPanel);
            EventManager.Ins.AddListener(PSAEventKeys.OnPlayerUnseat, CloseCraftingPanel);
        }
        
        private void OnDisable()
        {
            EventManager.Ins.RemoveListener(PSAEventKeys.OnInkBottleInteracted, OpenCraftingPanel);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnPlayerUnseat, CloseCraftingPanel);
        }

        private void Start()
        {
            GenerateCraftableSigns();
        }
        
        public void GenerateCraftableSigns()
        {
            _grid.transform.ClearChildren();
            
            foreach (ElementIngredientData sign in CraftableSigns)
            {
                GameObject signGO = Instantiate(_uiElementPrefab, _grid.transform);
                if (signGO.TryGetComponent(out UIElementIngredient uiSign))
                    uiSign.Init(sign);
            }
        }
        
        public void OpenCraftingPanel()
        {
            _craftingPanel.SetActive(true);
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnOpenElementsPanel);
        }
        
        public void CloseCraftingPanel()
        {
            _craftingPanel.SetActive(false);
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnCloseElementsPanel);
        }
    }
}