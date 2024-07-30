namespace ProjectSA.Gameplay.MatchRequestSystem
{
    using UnityEngine;
    using VUDK.Generic.Serializable;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.GameConstants;
    using ProjectSA.Patterns.Factories;
    using ProjectSA.Gameplay.CraftingItems;
    using ProjectSA.Player.Cauldron.EventArgs;
    using ProjectSA.Gameplay.CraftingItems.Data.ScriptableObjects;

    public class PotionsRequestSpawner : MonoBehaviour
    {
        [Header("Potions Spawner Settings")]
        [SerializeField]
        private SerializableDictionary<CraftedPotionData, Transform> _potionSpawnPoints;
        
        private void OnEnable()
        {
            EventManager.Ins.AddListener<CauldronCraftEventArgs>(PSAEventKeys.OnRequestSuccess, OnRequestSuccess);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener<CauldronCraftEventArgs>(PSAEventKeys.OnRequestSuccess, OnRequestSuccess);
        }

        private void OnRequestSuccess(CauldronCraftEventArgs args)
        {
            if (args.CraftedRecipe.Result is not CraftedPotionData potionData) return;

            Potion spawnedPotion = GameFactory.CreatePotionComboItem(potionData);
            
            if (!_potionSpawnPoints.ContainsKey(potionData)) return;
            Transform spawnPoint = _potionSpawnPoints[potionData];
            spawnedPotion.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
        }
    }
}