namespace ProjectSA.Player
{
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.GameConstants;

    public class PlayerResources : MonoBehaviour
    {
        [Header("Resources Settings")]
        [SerializeField, Min(0f)]
        private float _inkAmount = 100f;
        [SerializeField, Min(0f)]
        private float _bloodAmount = 100f;

        public float CurrentInkAmount { get; private set; }
        public float CurrentBloodAmount { get; private set; }
        
        private void Awake()
        {
            CurrentInkAmount = _inkAmount;
            CurrentBloodAmount = _bloodAmount;
        }

        private void Start()
        {
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnInkInit, CurrentInkAmount);
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnBloodInit, CurrentBloodAmount);
        }
        
        public bool TryConsumeInk(float amount)
        {
            if (CurrentInkAmount >= amount)
            {
                CurrentInkAmount -= amount;
                EventManager.Ins.TriggerEvent(PSAEventKeys.OnInkConsumed, amount);
                return true;
            }

            return false;
        }
        
        public bool TryConsumeBlood(float amount)
        {
            if (CurrentBloodAmount + 1 >= amount)
            {
                CurrentBloodAmount -= amount;
                EventManager.Ins.TriggerEvent(PSAEventKeys.OnBloodConsumed, amount);
                return true;
            }

            return false;
        }
        
        public void DamagePlayer(float damage)
        {
            CurrentBloodAmount -= damage;
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnPlayerDamaged, damage);

            if (CurrentBloodAmount <= 0)
            {
                CurrentBloodAmount = 0;
                EventManager.Ins.TriggerEvent(PSAEventKeys.OnPlayerDeath);
            }
        }
    }
}