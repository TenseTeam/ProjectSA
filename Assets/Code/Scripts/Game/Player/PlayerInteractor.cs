namespace ProjectSA.Player
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Features.Main.InteractSystem;
    using ProjectSA.Gameplay.InteractSystem;

    [RequireComponent(typeof(Interactor))]
    public class PlayerInteractor : MonoBehaviour
    {
        private Interactor _interactor;
        
        private void Awake()
        {
            TryGetComponent(out _interactor);
        }
        
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

        private void Interact()
        {
            if (_interactor.TryGetCurrentInteractable(out InteractableBase interactable))
                interactable.Interact();
        }
    }
}