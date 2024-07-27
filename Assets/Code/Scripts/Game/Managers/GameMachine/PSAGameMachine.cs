namespace ProjectSA.Managers.GameMachine
{
    using VUDK.Generic.Managers.Main.Bases;
    using ProjectSA.Patterns.Factories;
    using ProjectSA.Managers.GameMachine.States;
    using ProjectSA.Managers.GameMachine.Contexts;

    public class PSAGameMachine : GameMachineBase
    {
        public PSAGameMachineContext Context { get; private set; }

        public override void Run()
        {
            ChangeState(GameStateKeys.BeginGameState);
        }
        
        public override void Init()
        {
            base.Init();
            Context = MachineFactory.Create();

            BeginGameState beginGameState = MachineFactory.Create<BeginGameState>(GameStateKeys.BeginGameState, this, Context);
            RequestState requestState = MachineFactory.Create<RequestState>(GameStateKeys.RequestState, this, Context);
            RequestSatisfiedState requestSatisfiedState = MachineFactory.Create<RequestSatisfiedState>(GameStateKeys.RequestSatisfiedState, this, Context);
            StunState stunState = MachineFactory.Create<StunState>(GameStateKeys.StunState, this, Context);
            GameoverState gameoverState = MachineFactory.Create<GameoverState>(GameStateKeys.GameoverState, this, Context);
            
            AddState(GameStateKeys.BeginGameState, beginGameState);
            AddState(GameStateKeys.RequestState, requestState);
            AddState(GameStateKeys.RequestSatisfiedState, requestSatisfiedState);
            AddState(GameStateKeys.StunState, stunState);
            AddState(GameStateKeys.GameoverState, gameoverState);
        }

        public override bool Check()
        {
            return Context != null;
        }
    }
}