namespace ProjectSA.Managers.GameMachine.Contexts
{
    using GameManager;
    using VUDK.Patterns.StateMachine;

    public class PSAGameMachineContext : StateContext
    {
        public PSAGameManager GameManager { get; private set; }
        public PSAGameStats GameStats { get; private set; }
        
        public PSAGameMachineContext(PSAGameManager gameManager, PSAGameStats gameStats) : base()
        {
            GameManager = gameManager;
            GameStats = gameStats;
        }
    }
}