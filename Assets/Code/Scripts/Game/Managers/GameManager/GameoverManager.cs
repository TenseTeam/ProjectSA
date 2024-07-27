namespace ProjectSA.Managers.GameManager
{
    using UnityEngine;
    using ProjectSA.GameConstants;
    using VUDK.Features.Main.EventSystem;

    public class GameoverManager : MonoBehaviour
    {
        public void Gamevictory()
        {
            Debug.Log("<color=yellow>Triggered Gamevictory</color>");
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnGamevictory);
        }
        
        public void Gameover()
        {
            Debug.Log("<color=yellow>Triggered Gameover</color>");
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnGameover);
        }
    }
}