namespace ProjectSA.Gameplay.MatchRequestSystem.Potions
{
    using VUDK.Patterns.Initialization.Interfaces;
    using VUDK.Patterns.Pooling;
    using VUDK.Features.More.DialogueSystem.Events;
    using ProjectSA.Gameplay.CraftingItems.Data.ScriptableObjects;

    public class Potion : PooledObjectBase, IInit<CraftedPotionData>
    {
        public CraftedPotionData PotionData { get; private set; }
        
        public void Init(CraftedPotionData arg)
        {
            PotionData = arg;
            TriggerDialogue();
        }

        public bool Check()
        {
            return PotionData != null;
        }
        
        private void TriggerDialogue()
        {
            if (!Check() || !PotionData.DialogueToTrigger) return;
            
            // TODO: Change this with a ShadowDialgues script with a dictionary of dialogues to trigger based on the potion
            DSEvents.DialogueStartHandler?.Invoke(
                this, 
                new OnStartDialogueEventArgs(PotionData.DialogueToTrigger, PotionData.DialogueToTrigger.StartingDialogues[0], false, false)
            );
        }
    }
}