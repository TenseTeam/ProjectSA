namespace ProjectSA.Gameplay.EnvironmentPuzzles.CrystalPuzzle
{
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.GameConstants;
    using ProjectSA.Gameplay.UsableItems;
    using ProjectSA.Gameplay.EnvironmentPuzzles.Base;

    public class CrystalSlotInteractable : PuzzleInteractableBase
    {
        [Header("Crystal Slot Settings")]
        [SerializeField]
        private GameObject _crystal;

        private void Start()
        {
            _crystal.SetActive(false);
        }

        protected override void OnInteract()
        {
            base.OnInteract();

            if (HasCrystal())
                Resolve();
        }

        protected override void Resolve()
        {
            _crystal.SetActive(true);
            PlayerHand.RemoveElementFromHand();
            OnPuzzleSolved?.Invoke();
            DisableInteraction();
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnCrystalPuzzleSolved);
        }

        private bool HasCrystal()
        {
            if (PlayerHand.CurrentItem == null) return false;

            return PlayerHand.CurrentItem is CrystalUsableItem;
        }
    }
}