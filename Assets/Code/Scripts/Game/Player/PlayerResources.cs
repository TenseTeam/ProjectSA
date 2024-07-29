namespace ProjectSA.Player
{
    using System;
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.GameConstants;
    using UnityEngine.Serialization;

    public class PlayerResources : MonoBehaviour
    {
        [field: Header("Resources Settings")]
        [field: SerializeField, Min(0f)]
        public float MaxInkAmount { get; private set; } = 100f;
        [field: SerializeField, Min(0f)]
        public float MaxBloodAmount { get; private set; } = 100f;

        public float CurrentInkAmount { get; private set; }
        public float CurrentBloodAmount { get; private set; }

        private void Awake()
        {
            CurrentInkAmount = MaxInkAmount;
            CurrentBloodAmount = MaxBloodAmount;
        }

        public bool TryConsumeInk(float amount)
        {
            if (CurrentInkAmount >= amount)
            {
                CurrentInkAmount -= amount;
                PlayerConsumedEventArgs playerConsumedEventArgs = new PlayerConsumedEventArgs(this, amount);
                EventManager.Ins.TriggerEvent<PlayerConsumedEventArgs>(PSAEventKeys.OnInkConsumed, playerConsumedEventArgs);
                return true;
            }

            return false;
        }

        public bool TryConsumeBlood(float amount)
        {
            DamagePlayer(amount);
            
            PlayerConsumedEventArgs playerConsumedEventArgs = new PlayerConsumedEventArgs(this, amount);
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnBloodConsumed, playerConsumedEventArgs);
            return CurrentBloodAmount >= amount;
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

    public class PlayerConsumedEventArgs : EventArgs
    {
        public PlayerResources PlayerResources { get; private set; }
        public float ConsumedAmount { get; private set; }
        
        public PlayerConsumedEventArgs(PlayerResources playerResources, float consumedAmount)
        {
            PlayerResources = playerResources;
            ConsumedAmount = consumedAmount;
        }
    }
}