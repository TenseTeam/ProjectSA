namespace ProjectSA.Managers.GameMachine.States
{
    using System;
    using Data.Enums;
    using GameConstants;
    using VUDK.Patterns.StateMachine;
    using ProjectSA.Managers.GameMachine.Contexts;
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;

    public class BeginGameState : State<PSAGameMachineContext>
    {
        public BeginGameState(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey, relatedStateMachine, context)
        {
        }
        
        public override void Enter()
        {
            Debug.Log("<color=green>Begin Game State</color>");
            ChangeState(GameStateKeys.RequestState);
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