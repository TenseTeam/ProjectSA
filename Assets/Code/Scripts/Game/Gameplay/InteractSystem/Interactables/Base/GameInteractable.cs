namespace ProjectSA.Gameplay.InteractSystem.Interactables.Base
{
    using System;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Generic.Managers.Main.Bases;
    using VUDK.Features.Main.InteractSystem;
    using ProjectSA.GameConstants;
    using ProjectSA.Gameplay.InteractSystem.Data;

    [RequireComponent(typeof(GameInteractableGraphicsController))]
    public class GameInteractable : InteractableBase
    {
        [field: Header("Interactable Settings")]
        [field: SerializeField]
        public InteractionType InteractionType { get; private set; } = InteractionType.Ray;
        
        public GameInteractableGraphicsController GraphicsController { get; private set; }

        private bool _isInteractionEnabled = true;
        private bool _canBeSecondaryInteracted = false;
        private bool _wasEnabled = false;

        protected virtual void Awake()
        {
            TryGetComponent(out GameInteractableGraphicsController graphicsController);
            GraphicsController = graphicsController;
            GraphicsController.CreateHighlightMaterial();
            
            EventManager.Ins.AddListener(PSAEventKeys.OnOpenElementsPanel, OnOpenElementsPanel);
            EventManager.Ins.AddListener(PSAEventKeys.OnCloseElementsPanel, OnCloseElementsPanel);
        }

        protected virtual void OnDestroy()
        {
            EventManager.Ins.RemoveListener(PSAEventKeys.OnOpenElementsPanel, OnOpenElementsPanel);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnCloseElementsPanel, OnCloseElementsPanel);
        }

        protected virtual void OnEnable()
        {
            InputsManager.Inputs.Interaction.SecondaryInteract.performed += InputSecondaryInteract;
            EventManager.Ins.AddListener(PSAEventKeys.OnPlayerUnseat, OnPlayerUnseat);
        }

        protected virtual void OnDisable()
        {
            InputsManager.Inputs.Interaction.SecondaryInteract.performed -= InputSecondaryInteract;
            EventManager.Ins.RemoveListener(PSAEventKeys.OnPlayerUnseat, OnPlayerUnseat);
        }

        private void OnMouseEnter()
        {
            if (InteractionType == InteractionType.Mouse && UIManagerBase.IsCursorEnabled && _isInteractionEnabled)
            {
                Enable();
                _canBeSecondaryInteracted = true;
            }
        }

        private void OnMouseExit()
        {
            if (InteractionType == InteractionType.Mouse && UIManagerBase.IsCursorEnabled && _isInteractionEnabled)
            {
                Disable();
                _canBeSecondaryInteracted = false;
            }
        }

        private void OnMouseDown()
        {
            if (InteractionType == InteractionType.Mouse && UIManagerBase.IsCursorEnabled && _isInteractionEnabled)
                Interact();
        }

        public void RayEnter()
        {
            if (InteractionType == InteractionType.Ray && _isInteractionEnabled)
                Enable();
        }

        public void RayExit()
        {
            if (InteractionType == InteractionType.Ray && _isInteractionEnabled)
                Disable();
        }

        public void RayInteract()
        {
            if (InteractionType == InteractionType.Ray && _isInteractionEnabled)
                Interact();
        }

        public override void Enable()
        {
            GraphicsController.Enable();
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnEnableInteractable, this);
        }

        public override void Disable()
        {
            GraphicsController.Disable();
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnDisableInteractable, this);
        }

        public void EnableInteraction(bool triggerEnable = true)
        {
            if (triggerEnable)
                Enable();
            _isInteractionEnabled = true;
        }

        public void DisableInteraction(bool triggerDisable = true)
        {
            if (triggerDisable)
                Disable();
            _isInteractionEnabled = false;
        }

        protected override void OnInteract()
        {
            Debug.Log("Interacting with " + gameObject.name);
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnInteractInteractable, this);
        }

        public virtual void SecondaryInteract()
        {
            Debug.Log("Secondary interacting with " + gameObject.name);
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnSecondaryInteractInteractable, this);
        }

        private void InputSecondaryInteract(InputAction.CallbackContext obj)
        {
            if (_canBeSecondaryInteracted)
                SecondaryInteract();
        }
        
        private void OnPlayerUnseat()
        {
            Disable();
            _canBeSecondaryInteracted = false;
        }
        
        private void OnOpenElementsPanel()
        {
            _wasEnabled = _isInteractionEnabled;
            DisableInteraction();
        }
        
        private void OnCloseElementsPanel()
        {
            if (_wasEnabled)
                EnableInteraction(false);
        }
    }
}