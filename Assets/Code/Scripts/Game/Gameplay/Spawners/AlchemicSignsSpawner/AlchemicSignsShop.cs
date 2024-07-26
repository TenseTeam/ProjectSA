namespace ProjectSA.Gameplay.Spawners.AlchemicSignsSpawner
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Generic.Managers.Main.Interfaces.Casts;
    using ProjectSA.Player;
    using ProjectSA.Managers;
    using ProjectSA.GameConstants;
    using CraftingItems.Data.ScriptableObjects;

    public class AlchemicSignsShop : MonoBehaviour, ICastGameManager<PSAGameManager>
    {
        [Header("Shop Settings")]
        [SerializeField]
        private AlchemicSignSlot _alchemicSignSlot;
        
        public PSAGameManager GameManager => MainManager.Ins.GameManager as PSAGameManager;
        public PlayerResources PlayerResources => GameManager.PlayerManager.PlayerResources;
        
        private void OnEnable()
        {
            EventManager.Ins.AddListener<AlchemicSignIngredientData>(PSAEventKeys.OnClickedBuyIngredient, TrySpawnSign);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener<AlchemicSignIngredientData>(PSAEventKeys.OnClickedBuyIngredient, TrySpawnSign);
        }
        
        private void TrySpawnSign(AlchemicSignIngredientData data)
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
        
        private void SpawnSign(AlchemicSignIngredientData data)
        {
            _alchemicSignSlot.FillSlot(data);
        }
    }
}