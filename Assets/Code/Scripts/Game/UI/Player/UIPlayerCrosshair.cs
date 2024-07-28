namespace ProjectSA.UI.Player
{
    using System;
    using GameConstants;
    using Gameplay.InteractSystem.Interactables.Base;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using UnityEngine.Serialization;
    using VUDK.Features.Main.EventSystem;

    public class UIPlayerCrosshair : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField]
        private GameObject _crosshairPanel;
        [SerializeField]
        private TMP_Text _interactionText;
        [SerializeField]
        private Image _crosshairImage;
        [SerializeField]
        private Sprite _crosshairSprite;
        [FormerlySerializedAs("_crosshairHitSprite"),SerializeField]
        private Sprite _crosshairInteractSprite;
        
        private void Awake()
        {
            _crosshairPanel.SetActive(true);
            _interactionText.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            EventManager.Ins.AddListener(PSAEventKeys.OnPlayerSeat, OnPlayerSeat);
            EventManager.Ins.AddListener(PSAEventKeys.OnPlayerUnseat, OnPlayerUnseat);
            EventManager.Ins.AddListener<GameInteractable>(PSAEventKeys.OnEnableInteractable, OnEnableInteractable);
            EventManager.Ins.AddListener<GameInteractable>(PSAEventKeys.OnDisableInteractable, OnDisableInteractable);
        }
        
        private void OnDisable()
        {
            EventManager.Ins.RemoveListener(PSAEventKeys.OnPlayerSeat, OnPlayerSeat);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnPlayerUnseat, OnPlayerUnseat);
            EventManager.Ins.RemoveListener<GameInteractable>(PSAEventKeys.OnEnableInteractable, OnEnableInteractable);
            EventManager.Ins.RemoveListener<GameInteractable>(PSAEventKeys.OnDisableInteractable, OnDisableInteractable);
        }
        
        private void OnDisableInteractable(GameInteractable interactable)
        {
            SetCrosshairDefaultSprite();
            _interactionText.gameObject.SetActive(false);
        }
        
        private void OnEnableInteractable(GameInteractable interactable)
        {
            SetCrosshairInteractSprite();
            _interactionText.gameObject.SetActive(true);
        }
        
        private void OnPlayerUnseat()
        {
            _crosshairPanel.SetActive(true);
        }
        
        private void OnPlayerSeat()
        {
            _crosshairPanel.SetActive(false);
        }
        
        private void SetCrosshairDefaultSprite()
        {
            _crosshairImage.sprite = _crosshairSprite;
        }
        
        private void SetCrosshairInteractSprite()
        {
            _crosshairImage.sprite = _crosshairInteractSprite;
        }
    }
}