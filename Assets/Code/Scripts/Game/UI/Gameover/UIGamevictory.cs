namespace Code.Scripts.Game.UI.Gameover
{
    using UnityEngine;
    using TMPro;
    using ProjectSA.Managers.GameManager;
    
    public class UIGamevictory : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField]
        private TMP_Text _gamevictoryMessageText;

        private void Awake()
        {
            SetGamevictoryMessage(GameoverManager.GamevictoryMessage);
        }

        private void SetGamevictoryMessage(string message)
        {
            _gamevictoryMessageText.text = message;
        }
    }
}