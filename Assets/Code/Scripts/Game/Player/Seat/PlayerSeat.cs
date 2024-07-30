namespace ProjectSA.Player.Seat
{
    using Managers.GameMachine.Data.Enums;
    using Managers.GameManager;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using VUDK.Features.Main.Camera.CameraViews;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.GameConstants;
    using ProjectSA.Gameplay.InteractSystem.Interactables.Base;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces.Casts;

    public class PlayerSeat : GameInteractable
    {
        [Header("Seat Settings")]
        [SerializeField]
        private Transform _target;
        [SerializeField]
        private FirstPersonCamera _playerCamera;
        [SerializeField]
        private float _smoothTime = 10f;
        [SerializeField]
        private bool _canLook;

        private bool _isSeated;
        
        private void OnValidate()
        {
            if (!_playerCamera) return;
            _playerCamera = FindObjectOfType<FirstPersonCamera>();
        }

        protected override void Awake()
        {
            base.Awake();
            if (!_playerCamera)
            {
                Debug.LogError($"Could not find FirstPersonCamera in {gameObject.name}.");
                return;
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            InputsManager.Inputs.Interaction.LeaveInteraction.performed += LeaveInteraction;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            InputsManager.Inputs.Interaction.LeaveInteraction.performed -= LeaveInteraction;
        }

        protected override void OnInteract()
        {
            base.OnInteract();
            SeatPlayer();
        }

        public void SeatPlayer()
        {
            if (_isSeated) return;
            
            _isSeated = true;
            _playerCamera.SetTarget(_target, _smoothTime, _canLook);
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnPlayerSeat);
        }
        
        public void LeaveSeat()
        {
            if (!_isSeated) return;
            
            _isSeated = false;
            _playerCamera.ResetTarget();
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnPlayerUnseat);
        }
        
        private void LeaveInteraction(InputAction.CallbackContext context)
        {
            LeaveSeat();
        }
    }
}