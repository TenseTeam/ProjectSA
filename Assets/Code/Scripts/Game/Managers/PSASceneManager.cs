namespace ProjectSA.Managers
{
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Generic.Attributes;
    using VUDK.Generic.Managers.Main.Bases;
    using ProjectSA.GameConstants;

    public class PSASceneManager : SceneManagerBase
    {
        [Scene, SerializeField]
        private string _gameoverScene;
        [Scene, SerializeField]
        private string _gamevictoryScene;
        [Scene, SerializeField]
        private string _creditsScene;
        
        protected override void OnEnable()
        {
            base.OnEnable();
            EventManager.Ins.AddListener<string>(PSAEventKeys.OnGameover, OnGameover);
            EventManager.Ins.AddListener<string>(PSAEventKeys.OnGamevictory, OnGamevictory);
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            EventManager.Ins.RemoveListener<string>(PSAEventKeys.OnGameover, OnGameover);
            EventManager.Ins.RemoveListener<string>(PSAEventKeys.OnGamevictory, OnGamevictory);
        }
        
        private void OnGameover(string message)
        {
            LoadGameoverScene();
        }
        
        private void OnGamevictory(string message)
        {
            LoadGamevictoryScene();
        }
        
        public void LoadGameoverScene()
        {
            ChangeScene(_gameoverScene);
        }
        
        public void LoadGamevictoryScene()
        {
            ChangeScene(_gamevictoryScene);
        }
        
        public void LoadCreditsScene()
        {
            ChangeScene(_creditsScene);
        }
    }
}