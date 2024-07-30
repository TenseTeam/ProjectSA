namespace ProjectSA.UI.Player
{
    using UnityEngine;
    using ProjectSA.GameConstants;
    using VUDK.Features.Main.EventSystem;

    public class UIPlayerSeat : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField]
        private GameObject _uiSeatPanel;

        private void Awake()
        {
            _uiSeatPanel.SetActive(false);
        }

        private void OnEnable()
        {
            EventManager.Ins.AddListener(PSAEventKeys.OnPlayerSeat, OnPlayerSeat);
            EventManager.Ins.AddListener(PSAEventKeys.OnPlayerUnseat, OnPlayerUnseat);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener(PSAEventKeys.OnPlayerSeat, OnPlayerSeat);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnPlayerUnseat, OnPlayerUnseat);
        }
        
        private void OnPlayerSeat()
        {
            _uiSeatPanel.SetActive(true);
        }
        
        private void OnPlayerUnseat()
        {
            _uiSeatPanel.SetActive(false);
        }
    }
}