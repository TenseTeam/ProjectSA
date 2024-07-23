namespace ProjectSA.Player
{
    using UnityEngine;
    using VUDK.Features.Main.CharacterController.Movements;

    [RequireComponent(typeof(CharacterMovement))]
    public class PlayerManager : MonoBehaviour
    {
        private CharacterMovement _characterMovement;

        private void Awake()
        {
            TryGetComponent(out _characterMovement);
        }
    }
}