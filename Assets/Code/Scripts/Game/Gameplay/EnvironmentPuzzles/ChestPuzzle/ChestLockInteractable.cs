namespace ProjectSA.Gameplay.EnvironmentPuzzles.ChestPuzzle
{
    using UnityEngine;
    using UnityEngine.Events;
    using ProjectSA.GameConstants;
    using ProjectSA.Gameplay.UsableItems;
    using ProjectSA.Gameplay.EnvironmentPuzzles.Base;
    using VUDK.Generic.Serializable;
    using VUDK.Features.Main.EventSystem;

    public class ChestLockInteractable : PuzzleInteractableBase
    {
        [Header("Chest Lock Settings")]
        [SerializeField]
        private DelayTask _acidTask;
        
        public float UnlockRemainingTime => _acidTask.RemainingTime;
        
        public UnityEvent OnChestStartedUnlocking;
        public UnityEvent OnChestUnlocked;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            _acidTask.OnTaskCompleted += OnAcidTaskCompleted;
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            _acidTask.OnTaskCompleted -= OnAcidTaskCompleted;
        }

        private void Update()
        {
            _acidTask.Process();
        }

        protected override void OnInteract()
        {
            base.OnInteract();
            if (HasAcid())
                Resolve();
        }

        protected override void Resolve()
        {
            _acidTask.Start();
            OnChestStartedUnlocking?.Invoke();
            OnPuzzleSolved?.Invoke();
            PlayerHand.RemoveElementFromHand();
            DisableInteraction();
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnChestPuzzleSolved);
        }

        private bool HasAcid()
        {
            if (PlayerHand.CurrentItem == null) return false;

            return PlayerHand.CurrentItem is AcidUsableItem;
        }
        
        private void OnAcidTaskCompleted()
        {
            OnChestUnlocked?.Invoke();
        }
    }
}