namespace ProjectSA.Managers.GameMachine.States
{
    using System;
    using UnityEngine;
    using VUDK.Patterns.StateMachine;
    using ProjectSA.Managers.GameMachine.Contexts;

    public class GameVictoryState : State<PSAGameMachineContext>
    {
        public GameVictoryState(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey, relatedStateMachine, context)
        {
        }
        
        public override void Enter()
        {
            Debug.Log("<color=green>Gamevictory State</color>");
            Context.GameManager.GameoverManager.Gamevictory();
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