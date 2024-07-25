namespace ProjectSA.Player.Seat
{
    using GameConstants;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using VUDK.Features.Main.Camera.CameraViews;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.Gameplay.InteractSystem.Interactables;

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

        private void OnEnable()
        {
            InputsManager.Inputs.Interaction.LeaveInteraction.performed += LeaveInteraction;
        }

        private void OnDisable()
        {
            InputsManager.Inputs.Interaction.LeaveInteraction.performed -= LeaveInteraction;
        }
        
        public override void Interact()
        {
            base.Interact();
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