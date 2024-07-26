namespace ProjectSA.UI.Crafting
{
    using System.Globalization;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using VUDK.Patterns.Initialization.Interfaces;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.GameConstants;
    using Gameplay.CraftingItems.Data.ScriptableObjects;

    public class UIAlchemicSign : MonoBehaviour, IInit<ElementIngredientData>
    {
        [Header("UI Elements")]
        [SerializeField]
        private Image _iconImage;
        [SerializeField]
        private TMP_Text _nameText;
        [SerializeField]
        private TMP_Text _inkCostText;
        [SerializeField]
        private TMP_Text _bloodCostText;
        
        public ElementIngredientData IngredientData { get; private set; }
        
        public void Init(ElementIngredientData arg)
        {
            IngredientData = arg;
            SetUIElements();
        }
        
        public bool Check()
        {
            return IngredientData != null;
        }
        
        public void TriggerBuyIngredient()
        {
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnClickedBuyIngredient, IngredientData);
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