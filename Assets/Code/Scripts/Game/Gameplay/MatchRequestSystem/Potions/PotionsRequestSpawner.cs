namespace ProjectSA.Gameplay.MatchRequestSystem
{
    using UnityEngine;
    using VUDK.Generic.Serializable;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.CraftingSystem.Data.ScriptableObjects;
    using ProjectSA.GameConstants;
    using ProjectSA.Patterns.Factories;
    using ProjectSA.Gameplay.Items;
    using ProjectSA.Gameplay.Items.Data.ScriptableObjects;

    public class PotionsRequestSpawner : MonoBehaviour
    {
        [Header("Potions Spawner Settings")]
        [SerializeField]
        private SerializableDictionary<CraftedPotionData, Transform> _potionSpawnPoints;
        
        private void OnEnable()
        {
            EventManager.Ins.AddListener<CraftedItemDataBase>(PSAEventKeys.OnRequestSuccess, OnRequestSuccess);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener<CraftedItemDataBase>(PSAEventKeys.OnRequestSuccess, OnRequestSuccess);
        }

        private void OnRequestSuccess(CraftedItemDataBase requestedItem)
        {
            if (requestedItem is not CraftedPotionData potionData) return;

            Potion spawnedPotion = GameFactory.CreatePotionComboItem(potionData);
            
            if (!_potionSpawnPoints.ContainsKey(potionData)) return;
            Transform spawnPoint = _potionSpawnPoints[potionData];
            spawnedPotion.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        }
    }
}