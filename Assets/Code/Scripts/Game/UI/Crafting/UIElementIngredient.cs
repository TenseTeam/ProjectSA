namespace ProjectSA.UI.Crafting
{
    using System.Globalization;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Serialization;
    using TMPro;
    using VUDK.Patterns.Initialization.Interfaces;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.Player;
    using ProjectSA.GameConstants;
    using ProjectSA.Gameplay.CraftingItems.Data.ScriptableObjects;

    public class UIElementIngredient : MonoBehaviour, IInit<ElementIngredientData>
    {
        [FormerlySerializedAs("_buyButton"),Header("UI Elements")]
        [SerializeField]
        private Button _buyBloodButton;
        [SerializeField]
        private Button _buyInkButton;
        [SerializeField]
        private Image _iconImage;
        [SerializeField]
        private TMP_Text _nameText;
        [SerializeField]
        private TMP_Text _inkCostText;
        [SerializeField]
        private TMP_Text _bloodCostText;
        
        public ElementIngredientData IngredientData { get; private set; }

        private void Awake()
        {
            EventManager.Ins.AddListener<PlayerConsumedEventArgs>(PSAEventKeys.OnInkConsumed, OnInkConsumed);
        }

        private void OnDestroy()
        {
            EventManager.Ins.RemoveListener<PlayerConsumedEventArgs>(PSAEventKeys.OnInkConsumed, OnInkConsumed);
        }

        private void OnEnable()
        {
            _buyInkButton.onClick.AddListener(BuyIngredientWithInkButton);
            _buyBloodButton.onClick.AddListener(BuyIngredientWithBloodButton);
        }
        
        private void OnDisable()
        {
            _buyInkButton.onClick.RemoveListener(BuyIngredientWithInkButton);
            _buyBloodButton.onClick.RemoveListener(BuyIngredientWithBloodButton);
        }

        public void Init(ElementIngredientData arg)
        {
            IngredientData = arg;
            SetUIElements();
        }
        
        public bool Check()
        {
            return IngredientData != null;
        }
        
        private void BuyIngredientWithBloodButton()
        {
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnClickedBuyIngredientWithBlood, IngredientData);
        }
        
        private void BuyIngredientWithInkButton()
        {
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnClickedBuyIngredientWithInk, IngredientData);
        }
        
        private void OnInkConsumed(PlayerConsumedEventArgs args)
        {
            _buyInkButton.interactable = args.PlayerResources.CurrentInkAmount >= IngredientData.InkCost;
        }
        
        private void SetUIElements()
        {
            if (!Check()) return;
            
            _iconImage.sprite = IngredientData.Icon;
            _nameText.text = IngredientData.Name;
            _inkCostText.text = IngredientData.InkCost.ToString(CultureInfo.InvariantCulture);
            _bloodCostText.text = IngredientData.BloodCost.ToString(CultureInfo.InvariantCulture);
        }
    }
}