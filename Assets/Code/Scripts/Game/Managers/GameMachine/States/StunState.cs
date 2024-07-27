namespace ProjectSA.Managers.GameMachine.States
{
    using System;
    using UnityEngine;
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.GameConstants;
    using ProjectSA.Managers.GameMachine.Contexts;
    using ProjectSA.Managers.GameMachine.Data.Enums;
    
    
    public class StunState : State<PSAGameMachineContext>
    {
        public StunState(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
            Debug.Log("<color=green>Stun State</color>");
            EventManager.Ins.AddListener(PSAEventKeys.OnStunTimerEnd, OnStunTimerEnd);
            Context.GameManager.GameTimersManager.StartStunTimer();
        }

        public override void Exit()
        {
            EventManager.Ins.RemoveListener(PSAEventKeys.OnStunTimerEnd, OnStunTimerEnd);
        }

        public override void Process()
        {
        }

        public override void FixedProcess()
        {
        }
        
        private void OnStunTimerEnd()
        {
            ChangeState(GameStateKeys.RequestState);
        }
    }
}