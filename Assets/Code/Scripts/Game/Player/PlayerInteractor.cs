namespace ProjectSA.Player
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Features.Main.InteractSystem;
    using ProjectSA.Gameplay.InteractSystem;

    [RequireComponent(typeof(RayInteractor))]
    public class PlayerInteractor : MonoBehaviour
    {
        private RayInteractor _rayInteractor;
        
        private void Awake()
        {
            TryGetComponent(out _rayInteractor);
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
            if (_rayInteractor.TryGetCurrentInteractable(out InteractableBase interactable))
                interactable.Interact();
        }
    }
}