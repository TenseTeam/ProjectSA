namespace ProjectSA.Player.Cauldron
{
    using UnityEngine;
    using VUDK.Extensions;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces.Casts;
    using ProjectSA.Managers;
    using ProjectSA.Gameplay.InteractSystem.Interactables.Base;

    public class CauldronGraphicsController : GameInteractableGraphicsController, ICastGameStats<PSAGameStats>
    {
        [Header("Effects")]
        [SerializeField]
        private GameObject _cauldronEffect;
        
        [Header("UI Elements")]
        [SerializeField]
        private GameObject _cauldronUI;
        [SerializeField]
        private GameObject _inputsPanel;
        [SerializeField]
        private GameObject _ingredientsPanel;
        [SerializeField]
        private GameObject _ingredientUIPrefab;
        
        public PSAGameStats GameStats => MainManager.Ins.GameStats as PSAGameStats;

        private void Update()
        {
            _cauldronUI.transform.LookAt(GameStats.PlayerCamera.transform, Vector3.up);
        }

        public void AddIngredientUI()
        {
            Instantiate(_ingredientUIPrefab, _ingredientsPanel.transform);
        }
        
        public void RemoveIngredientUI()
        {
            if (_ingredientsPanel.transform.childCount > 0)
            
            Destroy(_ingredientsPanel.transform.GetChild(_ingredientsPanel.transform.childCount - 1).gameObject);
        }
        
        public void ClearIngredientsUI()
        { 
            _ingredientsPanel.transform.ClearChildren();
        }
        
        public void ShowCauldronEffect()
        {
            _cauldronEffect.SetActive(true);
        }
        
        public void HideCauldronEffect()
        {
            _cauldronEffect.SetActive(false);
        }
        
        public override void OnEnableInteractable()
        {
            _inputsPanel.SetActive(true);
        }

        public override void OnDisableInteractable()
        {
            _inputsPanel.SetActive(false);
        }
    }
}