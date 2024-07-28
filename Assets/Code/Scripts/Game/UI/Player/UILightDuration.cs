namespace ProjectSA.UI.Player
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.GameConstants;

    public class UILightDuration : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField]
        private GameObject _lightPanel;
        [SerializeField]
        private Image _lightDurationFillImage;
        
        private float _lightDuration;
        
        private void Awake()
        {
            _lightPanel.SetActive(false);
        }
        
        private void OnEnable()
        {
            EventManager.Ins.AddListener<float>(PSAEventKeys.OnLightOn, OnLightOn);
            EventManager.Ins.AddListener(PSAEventKeys.OnLightOff, OnLightOff);
            EventManager.Ins.AddListener<float>(PSAEventKeys.OnLightConsumingTick, OnLightConsumingTick);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener<float>(PSAEventKeys.OnLightOn, OnLightOn);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnLightOff, OnLightOff);
            EventManager.Ins.RemoveListener<float>(PSAEventKeys.OnLightConsumingTick, OnLightConsumingTick);
        }
        
        private void OnLightOn(float lightDuration)
        {
            _lightDuration = lightDuration;
            _lightPanel.SetActive(true);
        }
        
        private void OnLightOff()
        {
            _lightPanel.SetActive(false);
        }
        
        private void OnLightConsumingTick(float remainingTime)
        {
            UpdateLightDuration(remainingTime);
        }
        
        private void UpdateLightDuration(float remainingTime)
        {
            _lightDurationFillImage.fillAmount = remainingTime / _lightDuration;
        }
    }
}