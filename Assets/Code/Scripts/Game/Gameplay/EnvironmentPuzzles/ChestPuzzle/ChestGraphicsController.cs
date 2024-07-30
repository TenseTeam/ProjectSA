namespace ProjectSA.Gameplay.EnvironmentPuzzles.ChestPuzzle
{
    using System;
    using UnityEngine;
    using TMPro;
    using ProjectSA.Gameplay.InteractSystem.Interactables.Base;

    [RequireComponent(typeof(ChestLockInteractable))]
    public class ChestGraphicsController : GameInteractableGraphicsController
    {
        [Header("UI Elements")]
        [SerializeField]
        private GameObject _uiChestPanel;
        [SerializeField]
        private TMP_Text _chestTimeText;

        private ChestLockInteractable _chestLockInteractable;

        private void Awake()
        {
            TryGetComponent(out _chestLockInteractable);
            _uiChestPanel.SetActive(false);
        }

        private void OnEnable()
        {
            _chestLockInteractable.OnChestStartedUnlocking.AddListener(OnChestStartedUnlocking);
            _chestLockInteractable.OnChestUnlocked.AddListener(OnChestUnlocked);
        }

        private void OnDisable()
        {
            _chestLockInteractable.OnChestStartedUnlocking.RemoveListener(OnChestStartedUnlocking);
            _chestLockInteractable.OnChestUnlocked.RemoveListener(OnChestUnlocked);
        }

        private void Update()
        {
            if (_uiChestPanel.activeSelf)
                _chestTimeText.text = TimeSpan.FromSeconds(_chestLockInteractable.UnlockRemainingTime).ToString("mm':'ss");
        }

        private void OnChestStartedUnlocking()
        {
            _uiChestPanel.SetActive(true);
        }
        
        private void OnChestUnlocked()
        {
            _uiChestPanel.SetActive(false);
        }
    }
}