namespace ProjectSA.Gameplay.InteractSystem
{
    using UnityEngine;
    using VUDK.Features.Main.InteractSystem;

    public class RayInteractor : MonoBehaviour
    {
        [Header("Interactor Settings")]
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private LayerMask _interactLayer;
        [SerializeField]
        private float _interactDistance = 2f;

        private InteractableBase _currentInteractable;

        private void Update()
        {
            RaycastInteract();
        }

        public bool TryGetCurrentInteractable(out InteractableBase interactable)
        {
            interactable = _currentInteractable;
            return interactable;
        }
        
        private void RaycastInteract()
        {
            Debug.DrawRay(_camera.transform.position, _camera.transform.forward * _interactDistance, Color.red);
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, _interactDistance, _interactLayer))
            {
                if (hit.collider.TryGetComponent(out InteractableBase interactable))
                {
                    if (_currentInteractable)
                    {
                        if (_currentInteractable != interactable)
                        {
                            _currentInteractable.Disable();
                            _currentInteractable = interactable;
                            _currentInteractable.Enable();
                        }
                    }
                    else
                    {
                        _currentInteractable = interactable;
                        _currentInteractable.Enable();
                    }
                }
                else
                {
                    if (_currentInteractable)
                    {
                        _currentInteractable.Disable();
                        _currentInteractable = null;
                    }
                }
            }
            else
            {
                if (_currentInteractable)
                {
                    _currentInteractable.Disable();
                    _currentInteractable = null;
                }
            }
        }
    }
}