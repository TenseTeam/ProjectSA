namespace Code.Scripts.Game.UI.Timer
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.GameConstants;

    public class UIGamePhasesTimers : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField]
        private GameObject _timerPanel;
        [SerializeField]
        private TMP_Text _timerText;
        [SerializeField]
        private Image _phaseImage;
        [SerializeField]
        private Sprite _requestPhaseSprite;
        [SerializeField]
        private Sprite _stunPhaseSprite;

        private void Awake()
        {
            _timerPanel.SetActive(false);
        }

        private void OnEnable()
        {
            EventManager.Ins.AddListener(PSAEventKeys.OnStunTimerStart, SetStunImage);
            EventManager.Ins.AddListener(PSAEventKeys.OnRequestTimerStart, SetRequestImage);
            EventManager.Ins.AddListener<float>(PSAEventKeys.OnRequestTimerTick, OnTimerTick);
            EventManager.Ins.AddListener<float>(PSAEventKeys.OnStunTimerTick, OnTimerTick);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener(PSAEventKeys.OnStunTimerStart, SetStunImage);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnRequestTimerStart, SetRequestImage);
            EventManager.Ins.RemoveListener<float>(PSAEventKeys.OnRequestTimerTick, OnTimerTick);
            EventManager.Ins.RemoveListener<float>(PSAEventKeys.OnStunTimerTick, OnTimerTick);
        }

        private void OnTimerTick(float remainingTime)
        {
            UpdateTimer(remainingTime);
        }
        
        private void UpdateTimer(float time)
        {
            _timerText.text = TimeSpan.FromSeconds(time).ToString("mm':'ss");
        }
        
        private void SetStunImage()
        {
            _timerPanel.SetActive(true);
            _phaseImage.sprite = _stunPhaseSprite;
        }
        
        private void SetRequestImage()
        {
            _timerPanel.SetActive(true);
            _phaseImage.sprite = _requestPhaseSprite;
        }
    }
}