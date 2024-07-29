namespace ProjectSA.Gameplay.EnvironmentPuzzles.Base
{
    using UnityEngine.Events;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces.Casts;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.Player;
    using ProjectSA.GameConstants;
    using ProjectSA.Managers.GameMachine;
    using ProjectSA.Managers.GameManager;
    using ProjectSA.Managers.GameMachine.Data.Enums;
    using ProjectSA.Gameplay.InteractSystem.Interactables.Base;
    using UnityEngine;

    public abstract class PuzzleInteractableBase : GameInteractable, ICastGameManager<PSAGameManager>
    {
        public PSAGameManager GameManager => MainManager.Ins.GameManager as PSAGameManager;
        public PlayerHand PlayerHand => GameManager.PlayerManager.PlayerHand;
        public PSAGameMachine GameMachine => GameManager.GameMachine;

        public bool CanInteractPuzzle => GameMachine.IsState(GameStateKeys.StunState);
        
        public UnityEvent OnPuzzleSolved;

        protected abstract void Resolve();

        public override void Interact()
        {
            if (!CanInteractPuzzle)
            {
                Debug.Log("Can't interact with puzzle while not in stun state");
                EventManager.Ins.TriggerEvent(PSAEventKeys.OnCantInteractPuzzle);
                return;
            }
            
            base.Interact();
        }
    }
}