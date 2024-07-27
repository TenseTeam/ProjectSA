namespace ProjectSA.Gameplay.UsableItems.LightItem
{
    using GameConstants;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Generic.Serializable;
    using ProjectSA.Gameplay.UsableItems.Base;
    using VUDK.Features.Main.EventSystem;

    public class LightUsableItem : UsableItemBase
    {
        [Header("Light Settings")]
        [SerializeField, Min(1f)]
        private float _lightDuration = 10f;
        [SerializeField]
        private LightRevealerTrigger _lightRevealer;
        
        private DelayTask _lightDurationTask;
        
        public float LightRemainingTime => _lightDuration - _lightDurationTask.ElapsedTime;

        private void Awake()
        {
            _lightRevealer.gameObject.SetActive(false);
            _lightDurationTask = new DelayTask(_lightDuration);
            _lightDurationTask.Start();
            _lightDurationTask.Pause();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _lightDurationTask.OnTaskCompleted += OnLightFullyConsumed;
            InputsManager.Inputs.Interaction.UseItem.canceled += StopUseInput;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _lightDurationTask.OnTaskCompleted -= OnLightFullyConsumed;
            InputsManager.Inputs.Interaction.UseItem.performed -= StopUseInput;
        }

        private void Update()
        {
            if (_lightDurationTask.Process())
                EventManager.Ins.TriggerEvent(PSAEventKeys.OnLightConsuming, LightRemainingTime);
        }

        protected override void OnUse()
        {
            _lightDurationTask.Resume();
            _lightRevealer.gameObject.SetActive(true);
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnLightOn);
        }
        
        private void StopUse()
        {
            _lightDurationTask.Pause();
            _lightRevealer.gameObject.SetActive(false);
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnLightOff);
        }
        
        private void StopUseInput(InputAction.CallbackContext context)
        {
            StopUse();
        }
        
        private void OnLightFullyConsumed()
        { 
            PlayerHand.RemoveElementFromHand();
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnLightOff);
        }
    }
}