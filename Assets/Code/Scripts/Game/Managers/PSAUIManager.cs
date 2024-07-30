namespace ProjectSA.Managers
{
    using VUDK.Constants;
    using VUDK.Generic.Managers.Main;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Generic.Managers.Main.Bases;
    using VUDK.Generic.Managers.Main.Interfaces.Casts;
    using ProjectSA.GameConstants;

    public class PSAUIManager : UIManagerBase, ICastSceneManager<PSASceneManager>
    {
        private bool _wasCursorEnabled;
        
        public PSASceneManager SceneManager => MainManager.Ins.SceneManager as PSASceneManager;

        private void OnEnable()
        {
            EventManager.Ins.AddListener(PSAEventKeys.OnPlayerSeat, OnPlayerSeat);
            EventManager.Ins.AddListener(PSAEventKeys.OnPlayerUnseat, OnPlayerUnseat);
            EventManager.Ins.AddListener(EventKeys.PauseEvents.OnPauseEnter, OnPauseEnter);
            EventManager.Ins.AddListener(EventKeys.PauseEvents.OnPauseExit, OnPauseExit);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener(PSAEventKeys.OnPlayerSeat, OnPlayerSeat);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnPlayerUnseat, OnPlayerUnseat);
            EventManager.Ins.RemoveListener(EventKeys.PauseEvents.OnPauseEnter, OnPauseEnter);
            EventManager.Ins.RemoveListener(EventKeys.PauseEvents.OnPauseExit, OnPauseExit);
        }

        private void Start()
        {
            if (SceneManager.IsCurrentSceneGame())
                OnGameScene();
            else if (SceneManager.IsCurrentSceneMenu())
                OnMenuScene();
        }
        
        private void OnPauseEnter()
        {
            _wasCursorEnabled = IsCursorEnabled;
            EnableCursor();
        }
        
        private void OnPauseExit()
        {
            if (_wasCursorEnabled)
                EnableCursor();
            else
                DisableCursor();
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