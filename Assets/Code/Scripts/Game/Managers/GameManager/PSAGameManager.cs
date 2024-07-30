namespace ProjectSA.Managers.GameManager
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Bases;
    using VUDK.Features.More.DialogueSystem;
    using VUDK.Features.More.DialogueSystem.Events;
    using VUDK.Generic.Managers.Main.Interfaces.Casts;
    using VUDK.Features.More.DialogueSystem.Components;
    using ProjectSA.Player;
    using ProjectSA.GameConstants;
    using ProjectSA.Managers.GameMachine;
    using ProjectSA.Gameplay.MatchRequestSystem;
    using ProjectSA.Managers.GameMachine.Data.Enums;
    using ProjectSA.Managers.GameManager.ElementsIngredientsManager;
    
    public class PSAGameManager : GameManagerBase, ICastGameStats<PSAGameStats>
    {
        [field: Header("Game Managers")]
        [field: SerializeField]
        public PSAGameMachine GameMachine { get; private set; }
        [field: SerializeField]
        public ElementsShopManager ElementsShopManager { get; private set; }
        [field: SerializeField]
        public RequestManager RequestManager { get; private set; }
        [field: SerializeField]
        public GameTimersManager GameTimersManager { get; private set; }
        [field: SerializeField]
        public DamagerManager DamagerManager { get; private set; }
        [field: SerializeField]
        public GameoverManager GameoverManager { get; private set; }

        [field: Header("Dialogues")]
        [field: SerializeField]
        public DSDialogueManager DialogueManager { get; private set; }
        [field: SerializeField]
        public DSDialogueTrigger FirstDialogueTrigger { get; private set; }
        [field: SerializeField]
        public DSDialogueListener FirstDialogueListener { get; private set; }

        public PlayerManager PlayerManager { get; private set; }
        public PSAGameStats GameStats => MainManager.Ins.GameStats as PSAGameStats;
        
        private bool _isPlayerSeated = false;

        private void Awake()
        {
            PlayerManager = FindObjectOfType<PlayerManager>();

            if (PlayerManager == null)
                Debug.LogError("PlayerManager not found in the scene.");
        }

        private void Start()
        {
            EnablePlayerMovementInputs(); // Just in case
            EnablePlayerInteractionInputs();
            GameMachine.Init();
            FirstDialogueTrigger.Trigger(); // Triggers the intro dialogue, at its end start the game
        }

        private void OnEnable()
        {
            DSEvents.OnEnable += OnDialogueStart;
            DSEvents.OnDisable += OnDialogueEnd;
            FirstDialogueListener.OnEnd.AddListener(OnFirstDialogueEnd);
            EventManager.Ins.AddListener(PSAEventKeys.OnPlayerDeath, OnPlayerDeath);
            EventManager.Ins.AddListener(PSAEventKeys.OnPlayerSeat, OnPlayerSeat);
            EventManager.Ins.AddListener(PSAEventKeys.OnPlayerUnseat, OnPlayerUnseat);
            EventManager.Ins.AddListener<string>(PSAEventKeys.OnGameover, OnGameover);
            EventManager.Ins.AddListener<string>(PSAEventKeys.OnGamevictory, OnGamevictory);
            InputsManager.Inputs.Dialogue.SkipSentence.performed += InputSkipSentence;
        }

        private void OnDisable()
        {
            DSEvents.OnEnable -= OnDialogueStart;
            DSEvents.OnDisable -= OnDialogueEnd;
            FirstDialogueListener.OnEnd.RemoveListener(OnFirstDialogueEnd);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnPlayerDeath, OnPlayerDeath);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnPlayerSeat, OnPlayerSeat);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnPlayerUnseat, OnPlayerUnseat);
            EventManager.Ins.RemoveListener<string>(PSAEventKeys.OnGameover, OnGameover);
            EventManager.Ins.RemoveListener<string>(PSAEventKeys.OnGamevictory, OnGamevictory);
            InputsManager.Inputs.Dialogue.SkipSentence.performed -= InputSkipSentence;
        }

        private void OnPlayerDeath()
        {
            string message = GameStats.GameoverDeathMessage;
            GameoverManager.SetGameoverMessage(message);
            GameMachine.ChangeState(GameStateKeys.GameoverState);
        }

        private void OnGameover(string message)
        {
            DisablePlayerMovementInputs();
            DisablePlayerInteractionInputs();
        }

        private void OnGamevictory(string message)
        {
            DisablePlayerMovementInputs();
            DisablePlayerInteractionInputs();
        }
        
        private void OnFirstDialogueEnd()
        {
            GameMachine.Run();
        }

        private void OnPlayerSeat()
        {
            _isPlayerSeated = true;
            DisablePlayerMovementInputs();
        }

        private void OnPlayerUnseat()
        {
            _isPlayerSeated = false;
            EnablePlayerMovementInputs();
        }

        private void OnDialogueStart()
        {
            DisablePlayerInteractionInputs();
            DisablePlayerMovementInputs();
        }

        private void OnDialogueEnd()
        {
            EnablePlayerInteractionInputs();
            
            if (!_isPlayerSeated)
                EnablePlayerMovementInputs();
        }

        private void InputSkipSentence(InputAction.CallbackContext obj)
        {
            DialogueManager.NextDialogueInput();
        }

        private void EnablePlayerMovementInputs()
        {
            InputsManager.Inputs.Camera.Look.Enable();
            InputsManager.Inputs.Player.Enable();
        }

        private void DisablePlayerMovementInputs()
        {
            InputsManager.Inputs.Camera.Look.Disable();
            InputsManager.Inputs.Player.Disable();
        }

        private void EnablePlayerInteractionInputs()
        {
            InputsManager.Inputs.Interaction.Enable();
        }

        private void DisablePlayerInteractionInputs()
        {
            InputsManager.Inputs.Interaction.Disable();
        }
    }
}