namespace ProjectSA.Gameplay.InteractSystem.Interactables
{
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.Main.InteractSystem;
    using ProjectSA.GameConstants;
    
    [RequireComponent(typeof(GameInteractableGraphicsController))]
    public class GameInteractable : InteractableBase
    {
        private GameInteractableGraphicsController _graphicsController;
        
        protected virtual void Awake()
        { 
            TryGetComponent(out _graphicsController);
            _graphicsController.CreateHighlightMaterial();
        }
        
        public override void Enable()
        {
            Debug.Log("Enabling interaction with " + gameObject.name);
            _graphicsController.EnableHighlight();
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