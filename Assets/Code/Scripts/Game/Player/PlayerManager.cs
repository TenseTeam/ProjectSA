namespace ProjectSA.Player
{
    using UnityEngine;
    using VUDK.Features.Main.CharacterController.Movements;

    [RequireComponent(typeof(PlayerResources))]
    [RequireComponent(typeof(PlayerInteractor))]
    [RequireComponent(typeof(CharacterMovement))]
    public class PlayerManager : MonoBehaviour
    {
        public PlayerHand PlayerHand { get; private set; }
        public PlayerInteractor PlayerInteractor { get; private set; }
        public CharacterMovement CharacterMovement { get; private set; }
        public PlayerResources PlayerResources { get; private set; }
        
        private void Awake()
        {
            TryGetComponent(out CharacterMovement characterMovement);
            TryGetComponent(out PlayerInteractor playerInteractor);
            TryGetComponent(out PlayerResources playerResources);
            CharacterMovement = characterMovement;
            PlayerInteractor = playerInteractor;
            PlayerResources = playerResources;
            PlayerHand = FindObjectOfType<PlayerHand>();
            
            if (PlayerHand == null)
                Debug.LogError("PlayerHand not found in the scene!");
        }
    }
}