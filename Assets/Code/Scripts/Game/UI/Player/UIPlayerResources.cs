namespace ProjectSA.UI.Player
{
    using System.Globalization;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces.Casts;
    using ProjectSA.Managers.GameManager;
    using ProjectSA.GameConstants;
    using ProjectSA.Player;
    
    public class UIPlayerResources : MonoBehaviour, ICastGameManager<PSAGameManager>
    {
        [Header("UI Elements")]
        [SerializeField]
        private TMP_Text _bloodAmountText;
        [SerializeField]
        private Image _bloodFillImage;
        
        public PSAGameManager GameManager => MainManager.Ins.GameManager as PSAGameManager;
        public PlayerResources PlayerResources => GameManager.PlayerManager.PlayerResources;

        private void Start()
        {
            UpdateResources();
        }
        
        private void OnEnable()
        {
            EventManager.Ins.AddListener<PlayerConsumedEventArgs>(PSAEventKeys.OnBloodConsumed, OnBloodConsumed);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener<PlayerConsumedEventArgs>(PSAEventKeys.OnBloodConsumed, OnBloodConsumed);
        }
        
        private void OnBloodConsumed(PlayerConsumedEventArgs args)
        {
            UpdateResources();
        }
        
        private void UpdateResources()
        {
            _bloodAmountText.text = PlayerResources.CurrentBloodAmount.ToString(CultureInfo.InvariantCulture);
            _bloodFillImage.fillAmount = PlayerResources.CurrentBloodAmount / PlayerResources.MaxBloodAmount;
        }
    }
}