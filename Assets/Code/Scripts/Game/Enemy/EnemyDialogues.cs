namespace ProjectSA.Enemy
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

    public class EnemyDialogues : MonoBehaviour
    {
        [Header("Dialogues")]
        [SerializeField]
        SerializableDictionary<CraftedPotionData, DSDialogueContainerData> _potionsDialogues;
        [SerializeField]
        private List<DSDialogueContainerData> _failedRequestDialogues;
        [SerializeField]
        private List<DSDialogueContainerData> _cantResolvePuzzleDialogues;

        private void OnEnable()
        {
            EventManager.Ins.AddListener(PSAEventKeys.OnRequestFail, OnRequestFail);
            EventManager.Ins.AddListener(PSAEventKeys.OnCantInteractPuzzle, OnCantInteractPuzzle);
            EventManager.Ins.AddListener<CauldronCraftEventArgs>(PSAEventKeys.OnRequestSuccess, OnRequestSuccess);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener(PSAEventKeys.OnRequestFail, OnRequestFail);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnCantInteractPuzzle, OnCantInteractPuzzle);
            EventManager.Ins.RemoveListener<CauldronCraftEventArgs>(PSAEventKeys.OnRequestSuccess, OnRequestSuccess);
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