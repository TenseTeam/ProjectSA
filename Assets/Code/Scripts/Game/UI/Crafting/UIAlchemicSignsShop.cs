namespace ProjectSA.UI.Crafting
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Extensions;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.Gameplay.Items.Data.ScriptableObjects;
    using ProjectSA.GameConstants;

    public class UIAlchemicSignsShop : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField]
        private GameObject _craftingPanel;
        [SerializeField]
        private GridLayoutGroup _grid;
        
        [Header("Signs Settings")]
        [SerializeField]
        private GameObject _uiSignPrefab;
        [SerializeField]
        private AlchemicSignIngredientData[] _craftableSigns;

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
            
            foreach (AlchemicSignIngredientData sign in _craftableSigns)
            {
                GameObject signGO = Instantiate(_uiSignPrefab, _grid.transform);
                if (signGO.TryGetComponent(out UIAlchemicSign uiSign))
                    uiSign.Init(sign);
            }
        }
        
        public void OpenCraftingPanel()
        {
            _craftingPanel.SetActive(true);
        }
        
        public void CloseCraftingPanel()
        {
            _craftingPanel.SetActive(false);
        }
    }
}