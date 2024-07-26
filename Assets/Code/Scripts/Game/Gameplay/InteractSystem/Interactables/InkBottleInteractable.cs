namespace ProjectSA.Gameplay.InteractSystem.Interactables
{
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.Gameplay.InteractSystem.Interactables.Base;
    using ProjectSA.GameConstants;

    public class InkBottleInteractable : GameInteractable
    {
        public override void Interact()
        {
            base.Interact();
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnInkBottleInteracted);
        }
    }
}