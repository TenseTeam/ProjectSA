namespace ProjectSA.Managers.GameMachine.States
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.CraftingSystem.Data.ScriptableObjects;
    using ProjectSA.GameConstants;
    using Player.Cauldron.EventArgs;
    using ProjectSA.Managers.GameMachine.Contexts;
    using ProjectSA.Managers.GameMachine.Data.Enums;
    using ProjectSA.Gameplay.CraftingItems.Data.ScriptableObjects;

    public class RequestState : State<PSAGameMachineContext>
    {
        public RequestState(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnRequestState);
            
            Debug.Log("<color=green>Request State</color>");
            EventManager.Ins.AddListener(PSAEventKeys.OnRequestFail, OnRequestFail);
            EventManager.Ins.AddListener(PSAEventKeys.OnRequestTimerEnd, OnRequestTimerEnd);
            EventManager.Ins.AddListener<CauldronCraftEventArgs>(PSAEventKeys.OnRequestSuccess, OnRequestSuccess);
            Context.GameManager.GameTimersManager.StartRequestTimer();
        }

        public override void Exit()
        {
            EventManager.Ins.RemoveListener(PSAEventKeys.OnRequestFail, OnRequestFail);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnRequestTimerEnd, OnRequestTimerEnd);
            EventManager.Ins.RemoveListener<CauldronCraftEventArgs>(PSAEventKeys.OnRequestSuccess, OnRequestSuccess);
            Context.GameManager.GameTimersManager.StopRequestTimer();
        }

        public override void Process()
        {
        }

        public override void FixedProcess()
        {
        }
        
        private void OnRequestSuccess(CauldronCraftEventArgs args)
        {
            Context.GameManager.GameTimersManager.StopRequestTimer();
            
            if (Context.GameManager.RequestManager.AreAllRequestsSatisfied)
            {
                if (Context.GameManager.RequestManager.IsSecretItemCrafted)
                {
                    string message = Context.GameStats.GameVictoryMessage;
                    Context.GameManager.GameoverManager.SetGamevictoryMessage(message);
                    ChangeState(GameStateKeys.GameVictoryState);
                }
                else
                {
                    string message = Context.GameStats.GameoverCraftedAllPotionsMessage;
                    Context.GameManager.GameoverManager.SetGameoverMessage(message);
                    ChangeState(GameStateKeys.GameoverState);
                }

                return;
            }

            if (HasStunElement(args.UsedIngredients))
            {
                ChangeState(GameStateKeys.StunState);
                return;
            }
            
            ChangeState(GameStateKeys.BeginGameState);
        }
        
        private void OnRequestFail()
        {
            Debug.Log("<color=red>Request failed</color>");
            string message = Context.GameStats.GameoverFailedRequestMessage;
            Context.GameManager.GameoverManager.SetGameoverMessage(message);
            ChangeState(GameStateKeys.GameoverState);
        }
        
        private void OnRequestTimerEnd()
        {
            Context.GameManager.DamagerManager.DamagePlayer();
            ChangeState(GameStateKeys.BeginGameState);
        }

        private bool HasStunElement(List<IngredientData> usedIngredients)
        {
            foreach (var ingredient in usedIngredients)
            {
                if (ingredient is not ElementIngredientData elementIngredientData) continue;
                
                if (elementIngredientData.CanStun)
                    return true;
            }

            return false;
        }
    }
}