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
            EventManager.Ins.AddListener<ElementIngredientData>(PSAEventKeys.OnClickedBuyIngredientWithBlood, TrySpawnSignWithBlood);
            EventManager.Ins.AddListener<ElementIngredientData>(PSAEventKeys.OnClickedBuyIngredientWithInk, TrySpawnSignWithInk);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener<ElementIngredientData>(PSAEventKeys.OnClickedBuyIngredientWithBlood, TrySpawnSignWithBlood);
            EventManager.Ins.RemoveListener<ElementIngredientData>(PSAEventKeys.OnClickedBuyIngredientWithInk, TrySpawnSignWithInk);
        }
        
        private void TrySpawnSignWithBlood(ElementIngredientData data)
        {
            if (PlayerResources.TryConsumeBlood(data.BloodCost))
            {
                EventManager.Ins.TriggerEvent(PSAEventKeys.OnBoughtIngredient, data);
                SpawnSign(data);
            }
        }
        
        private void TrySpawnSignWithInk(ElementIngredientData data)
        {
            if (PlayerResources.TryConsumeInk(data.InkCost))
            {
                EventManager.Ins.TriggerEvent(PSAEventKeys.OnBoughtIngredient, data);
                SpawnSign(data);
            }
        }
        
        private void SpawnSign(ElementIngredientData data)
        {
            _elementSlot.FillSlot(data);
        }
    }
}