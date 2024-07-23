namespace ProjectSA.Gameplay.InteractSystem
{
    using UnityEngine;
    using VUDK.Features.Main.InteractSystem;

    public class GameInteractable : InteractableBase
    {
        public override void Enable()
        {
            Debug.Log("Enabling interaction");
        }
        
        public override void Disable()
        {
            Debug.Log("Disabling interaction");
        }
        
        public override void Interact()
        {
            Debug.Log("Interacting with object");
        }
    }
}