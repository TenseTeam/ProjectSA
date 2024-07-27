namespace ProjectSA.Managers.GameMachine.States
{
    using System;
    using VUDK.Patterns.StateMachine;
    using ProjectSA.Managers.GameMachine.Contexts;

    public class RequestState : State<PSAGameMachineContext>
    {
        public RequestState(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void Process()
        {
        }

        public override void FixedProcess()
        {
        }
    }
}