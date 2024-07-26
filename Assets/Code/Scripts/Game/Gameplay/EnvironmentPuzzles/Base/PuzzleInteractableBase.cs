namespace ProjectSA.Gameplay.EnvironmentPuzzles.Base
{
    using UnityEngine.Events;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces.Casts;
    using ProjectSA.Player;
    using ProjectSA.Managers.GameManager;
    using ProjectSA.Gameplay.InteractSystem.Interactables.Base;

    public abstract class PuzzleInteractableBase : GameInteractable, ICastGameManager<PSAGameManager>
    {
        public PSAGameManager GameManager => MainManager.Ins.GameManager as PSAGameManager;
        public PlayerHand PlayerHand => GameManager.PlayerManager.PlayerHand;
        
        public UnityEvent OnPuzzleSolved;
        
        protected abstract void Resolve();
    }
}