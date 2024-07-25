namespace VUDK.Generic.Managers.Main.Bases
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using VUDK.Features.Main.SceneManagement;
    using VUDK.Generic.Attributes;

    public abstract class SceneManagerBase : SceneSwitcher
    {
        [Header("Main Scenes")]
        [Scene, SerializeField]
        private string _menuScene;
        [Scene, SerializeField]
        private string _gameScene;
        
        public int CurrentSceneIndex => SceneManager.GetActiveScene().buildIndex;

        /// <summary>
        /// Loads the next scene in the build index settings.
        /// </summary>
        public void LoadNextScene()
        {
            WaitChangeScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        /// <summary>
        /// Reloads the current scene.
        /// </summary>
        public void ReloadScene()
        {
            WaitChangeScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        /// <summary>
        /// Checks if the current scene is the menu scene.
        /// </summary>
        /// <returns>True if the current scene is the menu scene.</returns>
        public bool IsCurrentSceneMenu()
        {
            return SceneManager.GetActiveScene().path == _menuScene;
        }
        
        /// <summary>
        /// Checks if the current scene is the game scene.
        /// </summary>
        /// <returns>True if the current scene is the game scene.</returns>
        public bool IsCurrentSceneGame()
        {
            return SceneManager.GetActiveScene().path == _gameScene;
        }
    }
}
