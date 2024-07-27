namespace ProjectSA.Managers.GameManager
{
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Generic.Serializable;
    using ProjectSA.GameConstants;

    public class GameTimersManager : MonoBehaviour
    {
        [Header("Game Timers Settings")]
        [SerializeField]
        private DelayTask _requestDelayTask;
        [SerializeField]
        private DelayTask _stunDelayTask;

        private void OnEnable()
        {
            _requestDelayTask.OnTaskCompleted += OnRequestTimerEnd;
            _stunDelayTask.OnTaskCompleted += OnStunTimerEnd;
        }
        
        private void OnDisable()
        {
            _requestDelayTask.OnTaskCompleted -= OnRequestTimerEnd;
            _stunDelayTask.OnTaskCompleted -= OnStunTimerEnd;
        }

        private void Update()
        {
            if (_requestDelayTask.Process())
                EventManager.Ins.TriggerEvent(PSAEventKeys.OnRequestTimerTick, _requestDelayTask.RemainingTime);
                
            if(_stunDelayTask.Process())
                EventManager.Ins.TriggerEvent(PSAEventKeys.OnStunTimerTick, _requestDelayTask.RemainingTime);
        }

        public void StartRequestTimer()
        {
            _requestDelayTask.Start();
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnRequestTimerStart);
        }
        
        public void StartStunTimer()
        {
            _stunDelayTask.Start();
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnStunTimerStart);
        }
        
        public void StopRequestTimer()
        {
            _requestDelayTask.Stop();
        }
        
        public void StopStunTimer()
        {
            _stunDelayTask.Stop();
        }
        
        private void OnRequestTimerEnd()
        {
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnRequestTimerEnd);
        }
        
        private void OnStunTimerEnd()
        {
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnStunTimerEnd);
        }
    }
}