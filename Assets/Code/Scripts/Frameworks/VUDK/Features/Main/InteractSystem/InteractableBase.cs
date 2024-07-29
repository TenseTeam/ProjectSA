namespace VUDK.Features.Main.InteractSystem
{
    using UnityEngine;
    using VUDK.Features.Main.InteractSystem.Interfaces;

    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        public abstract void Disable();

        public abstract void Enable();

        public virtual void Interact()
        {
            OnInteract();
        }
        
        protected abstract void OnInteract();
    }
}
