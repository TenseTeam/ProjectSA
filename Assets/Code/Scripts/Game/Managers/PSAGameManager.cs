namespace ProjectSA.Managers
{
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Generic.Managers.Main.Bases;
    using ProjectSA.GameConstants;
    using ProjectSA.Player;

    public class PSAGameManager : GameManagerBase
    {
        public PlayerManager PlayerManager { get; private set; }

        private void Awake()
        {
            PlayerManager = FindObjectOfType<PlayerManager>();
            
            if (PlayerManager == null)
                Debug.LogError("PlayerManager not found in the scene.");
        }

        private void OnEnable()
        {
            EventManager.Ins.AddListener(PSAEventKeys.OnPlayerSeat, OnPlayerSeat);
            EventManager.Ins.AddListener(PSAEventKeys.OnPlayerUnseat, OnPlayerUnseat);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener(PSAEventKeys.OnPlayerSeat, OnPlayerSeat);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnPlayerUnseat, OnPlayerUnseat);
        }
        
        private void OnPlayerSeat()
        {
            DisablePlayerMovementInputs();
        }
        
        private void OnPlayerUnseat()
        {
            EnablePlayerMovementInputs();
        }

        private void EnablePlayerMovementInputs()
        {
            InputsManager.Inputs.Player.Movement.Enable();
        }
        
        private void DisablePlayerMovementInputs()
        {
            InputsManager.Inputs.Player.Movement.Disable();
        }
    }
}