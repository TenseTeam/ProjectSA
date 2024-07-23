namespace ProjectSA.Managers
{
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Generic.Managers.Main.Bases;
    using ProjectSA.GameConstants;

    public class PSAGameManager : GameManagerBase
    {
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