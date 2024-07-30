namespace ProjectSA.Player.Cauldron
{
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.GameConstants;
    using ProjectSA.Gameplay.InteractSystem.Interactables.Base;

    [RequireComponent(typeof(PlayerCauldron))]
    public class PlayerCauldronInteractable : GameInteractable
    {
        private PlayerCauldron _playerCauldron;

        protected override void Awake()
        {
            base.Awake();
            TryGetComponent(out _playerCauldron);
        }
        
        public override void Interact()
        {
            base.Interact();
            _playerCauldron.StartCraft();
        }

        public override void SecondaryInteract()
        {
            base.SecondaryInteract();
            _playerCauldron.ClearIngredients();
        }
    }
}