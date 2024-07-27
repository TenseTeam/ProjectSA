namespace Code.Scripts.Game.Gameplay.EnvironmentPuzzles.SkullPuzzle
{
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.GameConstants;
    using ProjectSA.Gameplay.EnvironmentPuzzles.Base;
    using ProjectSA.Gameplay.UsableItems;

    public class SkullRevealerInteractable : PuzzleInteractableBase
    {
        [Header("Skull Puzzle Settings")]
        [SerializeField]
        private GameObject _objectToReveal;

        private void Start()
        {
            _objectToReveal.SetActive(false);
        }

        public override void Interact()
        {
            base.Interact();

            if (HasLife())
                Resolve();
        }

        protected override void Resolve()
        {
            _objectToReveal.SetActive(true);
            PlayerHand.RemoveElementFromHand();
            OnPuzzleSolved?.Invoke();
            DisableInteraction();
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnCrystalPuzzleSolved);
        }

        private bool HasLife()
        {
            if (PlayerHand.CurrentItem == null) return false;
            
            return PlayerHand.CurrentItem is LifeUsableItem;
        }
    }
}