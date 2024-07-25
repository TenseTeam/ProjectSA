namespace ProjectSA.Gameplay.InteractSystem.Interactables
{
    using GameConstants;
    using VUDK.Features.Main.EventSystem;

    public class InkBottleInteractable : GameInteractable
    {
        public override void Interact()
        {
            base.Interact();
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnInkBottleInteracted);
        }
    }
}