namespace ProjectSA.Patterns.Factories
{
    using VUDK.Patterns.StateMachine;
    using ProjectSA.Managers.GameMachine.Contexts;
    using ProjectSA.Managers.GameMachine.States;

    /// <summary>
    /// Responsible for the creation of all necessary objects related to state machines for the game.
    /// </summary>
    public static class MachineFactory
    {
        public static PSAGameMachineContext Create()
        {
            return new PSAGameMachineContext();
        }
        
        /// <summary>
        /// Creates a state for the game state machine.
        /// </summary>
        /// <param name="stateKey">The key of the state to be created.</param>
        /// <param name="relatedStateMachine">The state machine that the state will be related to.</param>
        /// <param name="context">The game context object.</param>
        /// <typeparam name="T">The type of the state to be created.</typeparam>
        /// <returns>The created state object.</returns>
        public static T Create<T>(GameStateKeys stateKey, StateMachine relatedStateMachine, PSAGameMachineContext context) where T : State<PSAGameMachineContext>
        {
            switch (stateKey)
            {
                case GameStateKeys.BeginGameState:
                    return new BeginGameState(stateKey, relatedStateMachine, context) as T;
                case GameStateKeys.RequestState:
                    return new RequestState(stateKey, relatedStateMachine, context) as T;
                case GameStateKeys.RequestSatisfiedState:
                    return new RequestSatisfiedState(stateKey, relatedStateMachine, context) as T;
                case GameStateKeys.StunState:
                    return new StunState(stateKey, relatedStateMachine, context) as T;
                case GameStateKeys.GameoverState:
                    return new GameoverState(stateKey, relatedStateMachine, context) as T;
            }
        
            return null;
        }
    }
}