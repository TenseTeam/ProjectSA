namespace ProjectSA.Managers.GameManager
{
    using UnityEngine;
    using ProjectSA.GameConstants;
    using VUDK.Features.Main.EventSystem;

    public class GameoverManager : MonoBehaviour
    {
        public static string GameoverMessage { get; private set; }
        public static string GamevictoryMessage { get; private set; }
        
        public void Gamevictory()
        {
            Debug.Log("<color=yellow>Triggered Gamevictory with message: </color>" + GamevictoryMessage);
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnGamevictory, GamevictoryMessage);
        }
        
        public void Gameover()
        {
            Debug.Log("<color=yellow>Triggered Gameover with message: </color>" + GameoverMessage);
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnGameover, GameoverMessage);
        }
        
        public void SetGameoverMessage(string message)
        {
            GameoverMessage = message;
        }
        
        public void SetGamevictoryMessage(string message)
        {
            GamevictoryMessage = message;
        }
    }
}