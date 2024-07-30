namespace ProjectSA.Player
{
    using UnityEngine.InputSystem;
    using VUDK.Features.Main.InputSystem;
    using ProjectSA.Gameplay.InteractSystem;
    
    public class PlayerInteractor : RayInteractor
    {
        private void OnEnable()
        {
            InputsManager.Inputs.Interaction.Interact.performed += InputInteract;
        }

        private void OnDisable()
        {
            InputsManager.Inputs.Interaction.Interact.performed -= InputInteract;
        }
        
        private void InputInteract(InputAction.CallbackContext context)
        { 
            Interact();
        }
    }
}