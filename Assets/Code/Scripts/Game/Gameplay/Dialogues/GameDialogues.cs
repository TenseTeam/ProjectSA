namespace ProjectSA.Gameplay.Dialogues
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Generic.Serializable;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.More.DialogueSystem.Data;
    using VUDK.Features.More.DialogueSystem.Events;
    using ProjectSA.GameConstants;
    using ProjectSA.Player.Cauldron.EventArgs;
    using ProjectSA.Gameplay.CraftingItems.Data.ScriptableObjects;

    public class GameDialogues : MonoBehaviour
    {
        [Header("Dialogues")]
        [SerializeField]
        SerializableDictionary<CraftedPotionData, DSDialogueContainerData> _potionsDialogues;
        [SerializeField]
        private List<DSDialogueContainerData> _failedRequestDialogues;
        [SerializeField]
        private List<DSDialogueContainerData> _cantResolvePuzzleDialogues;
        [SerializeField]
        private DSDialogueContainerData _firstSeatDialogue;
        [SerializeField]
        private DSDialogueContainerData _triedCraftWhileStunnedDialogue;
        [SerializeField]
        private DSDialogueContainerData _requestTimerEndDialogue;
        [SerializeField]
        private DSDialogueContainerData _stunnedDialogue;
        [SerializeField]
        private DSDialogueContainerData _stunTimerEndDialogue;
        
        private void OnEnable()
        {
            EventManager.Ins.AddListener(PSAEventKeys.OnRequestFail, OnRequestFail);
            EventManager.Ins.AddListener(PSAEventKeys.OnCantInteractPuzzle, OnCantInteractPuzzle);
            EventManager.Ins.AddListener<CauldronCraftEventArgs>(PSAEventKeys.OnRequestSuccess, OnRequestSuccess);
            EventManager.Ins.AddListener(PSAEventKeys.OnTriedCraftWhileStunned, OnTriedCraftWhileStunned);
            EventManager.Ins.AddListener(PSAEventKeys.OnPlayerFirstSeat, OnPlayerFirstSeat);
            EventManager.Ins.AddListener(PSAEventKeys.OnRequestTimerEnd, OnRequestTimerEnd);
            EventManager.Ins.AddListener(PSAEventKeys.OnStunState, OnStunState);
            EventManager.Ins.AddListener(PSAEventKeys.OnStunTimerEnd, OnStunTimerEnd);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener(PSAEventKeys.OnRequestFail, OnRequestFail);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnCantInteractPuzzle, OnCantInteractPuzzle);
            EventManager.Ins.RemoveListener<CauldronCraftEventArgs>(PSAEventKeys.OnRequestSuccess, OnRequestSuccess);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnTriedCraftWhileStunned, OnTriedCraftWhileStunned);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnPlayerFirstSeat, OnPlayerFirstSeat);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnRequestTimerEnd, OnRequestTimerEnd);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnStunState, OnStunState);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnStunTimerEnd, OnStunTimerEnd);
        }
        
        private void OnStunTimerEnd()
        {
            if (!_stunTimerEndDialogue) return;
            
            OnStartDialogueEventArgs dialogueArgs = new OnStartDialogueEventArgs(_stunTimerEndDialogue, null, false, false);
            DSEvents.DialogueStartHandler?.Invoke(this, dialogueArgs);
        }
        
        private void OnStunState()
        {
            if (!_stunnedDialogue) return;
            
            OnStartDialogueEventArgs dialogueArgs = new OnStartDialogueEventArgs(_stunnedDialogue, null, false, false);
            DSEvents.DialogueStartHandler?.Invoke(this, dialogueArgs);
        }
        
        private void OnRequestTimerEnd()
        {
            if (!_requestTimerEndDialogue) return;
            
            OnStartDialogueEventArgs dialogueArgs = new OnStartDialogueEventArgs(_requestTimerEndDialogue, null, false, false);
            DSEvents.DialogueStartHandler?.Invoke(this, dialogueArgs);
        }
        
        private void OnTriedCraftWhileStunned()
        {
            if (!_triedCraftWhileStunnedDialogue) return;
            
            OnStartDialogueEventArgs dialogueArgs = new OnStartDialogueEventArgs(_triedCraftWhileStunnedDialogue, null, false, false);
            DSEvents.DialogueStartHandler?.Invoke(this, dialogueArgs);
        }

        private void OnRequestSuccess(CauldronCraftEventArgs args)
        {
            if (args.CraftedRecipe.Result is not CraftedPotionData potionData) return;

            TriggerSuccessDialogue(potionData);
        }

        private void OnRequestFail()
        {
            TriggerFailedRequestDialogue();
        }

        private void OnCantInteractPuzzle()
        {
            TriggerCantResolvePuzzleDialogue();
        }

        private void OnPlayerFirstSeat()
        {
            TriggerFirstSeatDialogue();
        }
        
        private void TriggerFirstSeatDialogue()
        {
            if (!_firstSeatDialogue) return;
            
            OnStartDialogueEventArgs dialogueArgs = new OnStartDialogueEventArgs(_firstSeatDialogue, null, false, false);
            DSEvents.DialogueStartHandler?.Invoke(this, dialogueArgs);
        }

        private void TriggerSuccessDialogue(CraftedPotionData potionData)
        {
            if (!_potionsDialogues.ContainsKey(potionData)) return;

            OnStartDialogueEventArgs dialogueArgs = new OnStartDialogueEventArgs(_potionsDialogues[potionData], null, false, false);
            DSEvents.DialogueStartHandler?.Invoke(this, dialogueArgs);
        }

        private void TriggerFailedRequestDialogue()
        {
            if (_failedRequestDialogues.Count == 0) return;
            
            DSDialogueContainerData dialogue = _failedRequestDialogues[Random.Range(0, _failedRequestDialogues.Count)];
            OnStartDialogueEventArgs dialogueArgs = new OnStartDialogueEventArgs(dialogue, null, false, false);
            DSEvents.DialogueStartHandler?.Invoke(this, dialogueArgs);
        }

        private void TriggerCantResolvePuzzleDialogue()
        {
            if (_cantResolvePuzzleDialogues.Count == 0) return;
            
            DSDialogueContainerData dialogue = _cantResolvePuzzleDialogues[Random.Range(0, _cantResolvePuzzleDialogues.Count)];
            OnStartDialogueEventArgs dialogueArgs = new OnStartDialogueEventArgs(dialogue, null, false, false);
            DSEvents.DialogueStartHandler?.Invoke(this, dialogueArgs);
        }
    }
}