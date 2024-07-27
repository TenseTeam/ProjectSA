namespace ProjectSA.Managers.GameMachine.States
{
    using UnityEngine;
    using System;
    using VUDK.Patterns.StateMachine;
    using ProjectSA.Managers.GameMachine.Contexts;

    public class GameoverState : State<PSAGameMachineContext>
    {
        public GameoverState(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey, relatedStateMachine, context)
        {
        }
        
        public override void Enter()
        {
            Debug.Log("<color=green>Gameover State</color>");
            Context.GameManager.GameoverManager.Gameover();
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