namespace ProjectSA.Gameplay.InteractSystem.Interactables
{
    using System;
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.Main.InteractSystem;
    using ProjectSA.GameConstants;
    using ProjectSA.Gameplay.InteractSystem.Data;
    using VUDK.Generic.Managers.Main.Bases;

    [RequireComponent(typeof(GameInteractableGraphicsController))]
    public class GameInteractable : InteractableBase
    {
        [field: Header("Interactable Settings")]
        [field: SerializeField]
        public InteractionType InteractionType { get; private set; } = InteractionType.Ray;

        private GameInteractableGraphicsController _graphicsController;

        protected virtual void Awake()
        {
            TryGetComponent(out _graphicsController);
            _graphicsController.CreateHighlightMaterial();
        }

        private void OnMouseEnter()
        {
            if (InteractionType == InteractionType.Mouse && UIManagerBase.IsCursorEnabled)
                Enable();
        }

        private void OnMouseExit()
        {
            if (InteractionType == InteractionType.Mouse && UIManagerBase.IsCursorEnabled)
                Disable();
        }

        private void OnMouseDown()
        {
            if (InteractionType == InteractionType.Mouse && UIManagerBase.IsCursorEnabled)
                Interact();
        }

        public void RayEnter()
        {
            if (InteractionType == InteractionType.Ray)
                Enable();
        }

        public void RayExit()
        {
            if (InteractionType == InteractionType.Ray)
                Disable();
        }

        public void RayInteract()
        {
            if (InteractionType == InteractionType.Ray)
                Interact();
        }

        public override void Enable()
        {
            Debug.Log("Enabling interaction with " + gameObject.name);
            _graphicsController.EnableHighlight();
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnEnableInteractable, this);
        }

        public override void Disable()
        {
            Debug.Log("Disabling interaction with " + gameObject.name);
            _graphicsController.DisableHighlight();
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnDisableInteractable, this);
        }

        public override void Interact()
        {
            Debug.Log("Interacting with " + gameObject.name);
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnInteractInteractable, this);
        }
    }
}