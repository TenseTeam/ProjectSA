namespace ProjectSA.Gameplay.CraftingItems.Elements
{
    using UnityEngine;
    using TMPro;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces.Casts;
    using ProjectSA.Managers;
    using ProjectSA.Gameplay.InteractSystem.Interactables.Base;
    using ProjectSA.Gameplay.CraftingItems.Data.ScriptableObjects;

    public class ElementInteractableGraphicsController : GameInteractableGraphicsController, ICastGameStats<PSAGameStats>
    {
        [Header("UI Elements")]
        [SerializeField]
        private TMP_Text _inputsText;
        
        private ElementIngredientData _ingredientData;
        public PSAGameStats GameStats => MainManager.Ins.GameStats as PSAGameStats;
        
        private void Update()
        {
            _inputsText.transform.LookAt(GameStats.PlayerCamera.transform, Vector3.up);
        }

        public void SetElementIngredientData(ElementIngredientData data)
        {
            _ingredientData = data;
            _inputsText.text = $"\"M1\" : Use as ingredient.\n";
            
            if (_ingredientData.UsableElementPoolKey)
                _inputsText.text += $"\"F\" : Pick up.";
        }
        
        public override void OnEnableInteractable()
        {
            _inputsText.gameObject.SetActive(true);
        }

        public override void OnDisableInteractable()
        {
            _inputsText.gameObject.SetActive(false);
        }
    }
}