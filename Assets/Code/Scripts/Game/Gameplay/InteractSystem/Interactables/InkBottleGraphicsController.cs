namespace ProjectSA.Gameplay.InteractSystem.Interactables
{
    using System.Globalization;
    using UnityEngine;
    using TMPro;
    using VUDK.Generic.Managers.Main;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Generic.Managers.Main.Interfaces.Casts;
    using ProjectSA.Player;
    using ProjectSA.GameConstants;
    using ProjectSA.Managers.GameManager;
    using ProjectSA.Gameplay.InteractSystem.Interactables.Base;

    public class InkBottleGraphicsController : GameInteractableGraphicsController, ICastGameManager<PSAGameManager>
    {
        [Header("UI Elements")]
        [SerializeField]
        private GameObject _inkBottleUI;
        [SerializeField]
        private TMP_Text _inkAmountText;

        public PSAGameManager GameManager => MainManager.Ins.GameManager as PSAGameManager;

        private void Start()
        {
            UpdateInkAmountText();
            _inkBottleUI.SetActive(false);
        }

        private void OnEnable()
        {
            EventManager.Ins.AddListener(PSAEventKeys.OnPlayerSeat, OnPlayerSeat);
            EventManager.Ins.AddListener(PSAEventKeys.OnPlayerUnseat, OnPlayerUnseat);
            EventManager.Ins.AddListener<PlayerConsumedEventArgs>(PSAEventKeys.OnInkConsumed, OnInkConsumed);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener(PSAEventKeys.OnPlayerSeat, OnPlayerSeat);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnPlayerUnseat, OnPlayerUnseat);
            EventManager.Ins.RemoveListener<PlayerConsumedEventArgs>(PSAEventKeys.OnInkConsumed, OnInkConsumed);
        }

        private void Update()
        {
            _inkBottleUI.transform.LookAt(GameManager.PlayerManager.PlayerHand.transform);
        }

        private void OnPlayerSeat()
        {
            _inkBottleUI.SetActive(true);
        }

        private void OnPlayerUnseat()
        {
            _inkBottleUI.SetActive(false);
        }
        
        private void OnInkConsumed(PlayerConsumedEventArgs args)
        {
            UpdateInkAmountText();
        }
        
        private void UpdateInkAmountText()
        {
            _inkAmountText.text = GameManager.PlayerManager.PlayerResources.CurrentInkAmount.ToString(CultureInfo.InvariantCulture) + " ml";
        }
    }
}