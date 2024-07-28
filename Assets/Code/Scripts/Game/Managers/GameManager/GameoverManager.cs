namespace ProjectSA.Managers.GameManager
{
    using UnityEngine;
    using ProjectSA.GameConstants;
    using VUDK.Features.Main.EventSystem;

    public class GameoverManager : MonoBehaviour
    {
        private string _gameoverMessage;
        private string _gamevictoryMessage;
        
        public void Gamevictory()
        {
            Debug.Log("<color=yellow>Triggered Gamevictory with message: </color>" + _gamevictoryMessage);
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnGamevictory, _gamevictoryMessage);
        }
        
        public void Gameover()
        {
            Debug.Log("<color=yellow>Triggered Gameover with message: </color>" + _gameoverMessage);
            EventManager.Ins.TriggerEvent(PSAEventKeys.OnGameover, _gameoverMessage);
        }
        
        public void SetGameoverMessage(string message)
        {
            _gameoverMessage = message;
        }
        
        public void SetGamevictoryMessage(string message)
        {
            _gamevictoryMessage = message;
        }
    }
}