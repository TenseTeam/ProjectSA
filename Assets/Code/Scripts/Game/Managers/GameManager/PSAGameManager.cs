namespace ProjectSA.Managers.GameManager
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Generic.Managers.Main.Bases;
    using VUDK.Features.More.DialogueSystem;
    using VUDK.Features.More.DialogueSystem.Events;
    using ProjectSA.Player;
    using ProjectSA.GameConstants;
    using ProjectSA.Managers.GameMachine;
    using ProjectSA.Gameplay.MatchRequestSystem;
    using ProjectSA.Managers.GameManager.ElementsIngredientsManager;

    public class PSAGameManager : GameManagerBase
    {
        [field: Header("Game Managers")]
        [field: SerializeField]
        public PSAGameMachine GameMachine { get; private set; }
        [field: SerializeField]
        public ElementsShopManager ElementsShopManager { get; private set; }
        [field: SerializeField]
        public RequestManager RequestManager { get; private set; }
        [field: SerializeField]
        public DSDialogueManager DialogueManager { get; private set; }
        
        public PlayerManager PlayerManager { get; private set; }

        private void Awake()
        {
            PlayerManager = FindObjectOfType<PlayerManager>();
            
            if (PlayerManager == null)
                Debug.LogError("PlayerManager not found in the scene.");
        }

        private void Start()
        {
            GameMachine.Init();
            GameMachine.Run();
        }
        
        private void OnEnable()
        {
            DSEvents.OnEnable += OnDialogueStart;
            DSEvents.OnDisable += OnDialogueEnd;
            EventManager.Ins.AddListener(PSAEventKeys.OnPlayerSeat, OnPlayerSeat);
            EventManager.Ins.AddListener(PSAEventKeys.OnPlayerUnseat, OnPlayerUnseat);
            InputsManager.Inputs.Dialogue.SkipSentence.performed += InputSkipSentence;
        }

        private void OnDisable()
        {
            DSEvents.OnEnable -= OnDialogueStart;
            DSEvents.OnDisable -= OnDialogueEnd;
            EventManager.Ins.RemoveListener(PSAEventKeys.OnPlayerSeat, OnPlayerSeat);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnPlayerUnseat, OnPlayerUnseat);
            InputsManager.Inputs.Dialogue.SkipSentence.performed -= InputSkipSentence;
        }
        
        private void OnPlayerSeat()
        {
            DisablePlayerInputs();
        }
        
        private void OnPlayerUnseat()
        {
            EnablePlayerInputs();
        }
        
        private void OnDialogueStart()
        {
            DisablePlayerInputs();
        }

        private void OnDialogueEnd()
        {
            EnablePlayerInputs();
        }
        
        private void EnablePlayerInputs()
        {
            InputsManager.Inputs.Camera.Look.Enable();
            InputsManager.Inputs.Player.Enable();
        }
        
        private void DisablePlayerInputs()
        {
            InputsManager.Inputs.Camera.Look.Disable();
            InputsManager.Inputs.Player.Disable();
        }
        
        private void InputSkipSentence(InputAction.CallbackContext obj)
        {
            DialogueManager.NextDialogueInput();
        }
    }
}