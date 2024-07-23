namespace ProjectSA.Managers
{
    using VUDK.Features.Main.EventSystem;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Bases;
    using VUDK.Generic.Managers.Main.Interfaces.Casts;
    using ProjectSA.GameConstants;

    public class PSAUIManager : UIManagerBase, ICastSceneManager<PSASceneManager>
    {
        public PSASceneManager SceneManager => MainManager.Ins.SceneManager as PSASceneManager;

        private void OnEnable()
        {
            EventManager.Ins.AddListener(PSAEventKeys.OnPlayerSeat, OnPlayerSeat);
            EventManager.Ins.AddListener(PSAEventKeys.OnPlayerUnseat, OnPlayerUnseat);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener(PSAEventKeys.OnPlayerSeat, OnPlayerSeat);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnPlayerUnseat, OnPlayerUnseat);
        }

        private void Start()
        {
            if (SceneManager.IsCurrentSceneGame())
                OnGameScene();
            else if (SceneManager.IsCurrentSceneMenu())
                OnMenuScene();
        }
        
        private void OnPlayerSeat()
        {
            EnableCursor();
        }
  
        private void OnPlayerUnseat()
        {
            DisableCursor();
        }
        
        private void OnMenuScene()
        {
            EnableCursor();
        }
        
        private void OnGameScene()
        {
            DisableCursor();
        }
    }
}