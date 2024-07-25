namespace ProjectSA.Player
{
    using UnityEngine;
    using VUDK.Features.Main.CharacterController.Movements;

    [RequireComponent(typeof(PlayerResources))]
    [RequireComponent(typeof(PlayerInteractor))]
    [RequireComponent(typeof(CharacterMovement))]
    public class PlayerManager : MonoBehaviour
    {
        private PlayerInteractor _playerInteractor;
        private CharacterMovement _characterMovement;

        public PlayerResources PlayerResources { get; private set; }
        
        private void Awake()
        {
            TryGetComponent(out _characterMovement);
            TryGetComponent(out _playerInteractor);
            TryGetComponent(out PlayerResources playerResources);
            PlayerResources = playerResources;
        }
    }
}