namespace ProjectSA.Managers.GameManager
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces.Casts;
    using ProjectSA.Player;

    public class DamagerManager : MonoBehaviour, ICastGameManager<PSAGameManager>
    {
        [Header("Damager Settings")]
        [SerializeField, Min(0f)]
        private float _playerDamageOnFailedRequest = 10f;
        
        public PSAGameManager GameManager => MainManager.Ins.GameManager as PSAGameManager;
        public PlayerResources PlayerResources => GameManager.PlayerManager.PlayerResources;
        
        public void DamagePlayer()
        {
            Debug.Log("<color=yellow>Damaging Player</color>");
            PlayerResources.DamagePlayer(_playerDamageOnFailedRequest);
        }
    }
}