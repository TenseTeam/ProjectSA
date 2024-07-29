namespace Code.Scripts.Game.UI.Gameover
{
    using UnityEngine;
    using TMPro;
    using ProjectSA.Managers.GameManager;

    public class UIGameover : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField]
        private TMP_Text _gameoverMessageText;

        private void Awake()
        {
            SetGameoverMessage(GameoverManager.GameoverMessage);
        }

        private void SetGameoverMessage(string message)
        {
            _gameoverMessageText.text = message;
        }
    }
}