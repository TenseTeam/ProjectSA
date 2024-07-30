namespace ProjectSA.Gameplay.InteractSystem
{
    using UnityEngine;
    using ProjectSA.Gameplay.InteractSystem.Interactables.Base;

    public class RayInteractor : MonoBehaviour
    {
        [Header("Interactor Settings")]
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private LayerMask _interactLayer;
        [SerializeField]
        private float _interactDistance = 2f;

        private GameInteractable _currentInteractable;

        private void Update()
        {
            RaycastInteract();
        }

        public void Interact()
        {
            if (_currentInteractable)
                _currentInteractable.RayInteract();
        }
        
        private void RaycastInteract()
        {
            Debug.DrawRay(_camera.transform.position, _camera.transform.forward * _interactDistance, Color.red);
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, _interactDistance, _interactLayer))
            {
                if (hit.collider.TryGetComponent(out GameInteractable interactable))
                {
                    if (_currentInteractable)
                    {
                        if (_currentInteractable != interactable)
                        {
                            _currentInteractable.RayExit();
                            _currentInteractable = interactable;
                            _currentInteractable.RayEnter();
                        }
                    }
                    else
                    {
                        _currentInteractable = interactable;
                        _currentInteractable.RayEnter();
                    }
                }
                else
                {
                    if (_currentInteractable)
                    {
                        _currentInteractable.RayExit();
                        _currentInteractable = null;
                    }
                }
            }
            else
            {
                if (_currentInteractable)
                {
                    _currentInteractable.RayExit();
                    _currentInteractable = null;
                }
            }
        }
    }
}