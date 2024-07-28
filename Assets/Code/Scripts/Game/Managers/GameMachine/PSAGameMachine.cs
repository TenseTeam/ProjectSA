namespace ProjectSA.Managers.GameMachine
{
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Bases;
    using VUDK.Generic.Managers.Main.Interfaces.Casts;
    using ProjectSA.Patterns.Factories;
    using ProjectSA.Managers.GameManager;
    using ProjectSA.Managers.GameMachine.States;
    using ProjectSA.Managers.GameMachine.Contexts;
    using ProjectSA.Managers.GameMachine.Data.Enums;

    public class PSAGameMachine : GameMachineBase, ICastGameManager<PSAGameManager>, ICastGameStats<PSAGameStats>
    {
        public PSAGameMachineContext Context { get; private set; }
        public PSAGameManager GameManager => MainManager.Ins.GameManager as PSAGameManager;
        public PSAGameStats GameStats => MainManager.Ins.GameStats as PSAGameStats;

        public override void Run()
        {
            ChangeState(GameStateKeys.BeginGameState);
        }

        public override void Init()
        {
            base.Init();
            Context = MachineFactory.Create(GameManager, GameStats);

            BeginGameState beginGameState = MachineFactory.Create<BeginGameState>(GameStateKeys.BeginGameState, this, Context);
            RequestState requestState = MachineFactory.Create<RequestState>(GameStateKeys.RequestState, this, Context);
            StunState stunState = MachineFactory.Create<StunState>(GameStateKeys.StunState, this, Context);
            GameoverState gameoverState = MachineFactory.Create<GameoverState>(GameStateKeys.GameoverState, this, Context);
            GameVictoryState gameVictoryState = MachineFactory.Create<GameVictoryState>(GameStateKeys.GameVictoryState, this, Context);

            AddState(GameStateKeys.BeginGameState, beginGameState);
            AddState(GameStateKeys.RequestState, requestState);
            AddState(GameStateKeys.StunState, stunState);
            AddState(GameStateKeys.GameoverState, gameoverState);
            AddState(GameStateKeys.GameVictoryState, gameVictoryState);
        }

        public override bool Check()
        {
            return Context != null;
        }
        
// #if UNITY_EDITOR
//         public GameStateKeys statekey;
//         [ContextMenu("Check State Debug")]
//         public void CheckStateDebug()
//         {
//             Debug.Log("Is State? " + statekey + " = " + IsState(statekey));
//         }
// #endif
    }
}