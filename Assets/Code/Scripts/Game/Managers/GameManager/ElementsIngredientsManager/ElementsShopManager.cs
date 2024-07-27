namespace ProjectSA.Managers.GameManager.ElementsIngredientsManager
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Generic.Managers.Main.Interfaces.Casts;
    using ProjectSA.Player;
    using ProjectSA.GameConstants;
    using ProjectSA.Gameplay.CraftingItems.Data.ScriptableObjects;

    [RequireComponent(typeof(ElementSlot))]
    public class ElementsShopManager : MonoBehaviour, ICastGameManager<PSAGameManager>
    {
        [field: Header("Elements Shop Settings")]
        [field: SerializeField]
        public List<ElementIngredientData> ElementIngredients { get; private set; }
        
        private ElementSlot _elementSlot;
        
        public PSAGameManager GameManager => MainManager.Ins.GameManager as PSAGameManager;
        public PlayerResources PlayerResources => GameManager.PlayerManager.PlayerResources;

        private void Awake()
        { 
            TryGetComponent(out _elementSlot);
        }

        private void OnEnable()
        {
            EventManager.Ins.AddListener<ElementIngredientData>(PSAEventKeys.OnClickedBuyIngredient, TrySpawnSign);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener<ElementIngredientData>(PSAEventKeys.OnClickedBuyIngredient, TrySpawnSign);
        }
        
        private void TrySpawnSign(ElementIngredientData data)
        {
            if (TryBuySign(data.InkCost, data.BloodCost))
            {
                EventManager.Ins.TriggerEvent(PSAEventKeys.OnBoughtIngredient, data);
                SpawnSign(data);
            }
        }
        
        private bool TryBuySign(float inkCost, float bloodCost)
        {
            if (!PlayerResources.TryConsumeInk(inkCost))
                return PlayerResources.TryConsumeBlood(bloodCost);

            return true;
        }
        
        private void SpawnSign(ElementIngredientData data)
        {
            _elementSlot.FillSlot(data);
        }
    }
}