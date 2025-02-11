namespace VUDK.Features.UI.MenuSystem
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using VUDK.Features.UI.MenuSystem.MenuPreferences;

    public class UIScreenSettings : MonoBehaviour
    {
        [SerializeField, Header("Dropdown")]
        private TMP_Dropdown _dropResolution;
        [SerializeField]
        private TMP_Dropdown _dropFPS;

        [SerializeField, Header("Toggle")]
        private Toggle _toggleFullscreen;

        public bool Fullscreen { get; private set; }

        private void Awake()
        {
            QualitySettings.vSyncCount = 0; // Disable V-Sync to allow the FPS Cap

            InitFullscreen();
            InitDropdownResolutions();
            InitFPSDropdown();
        }
        
        private void InitFullscreen()
        {
            _toggleFullscreen.SetIsOnWithoutNotify(MenuPrefsSaver.Screen.LoadFullscreen());
            Screen.fullScreen = _toggleFullscreen.isOn;
        }

        private void OnEnable()
        {
            _toggleFullscreen.onValueChanged.AddListener(OnFullscreenChanged);
            
            if (_dropResolution)
                _dropResolution.onValueChanged.AddListener(OnResolutionChanged);
            
            if (_dropFPS)
                _dropFPS.onValueChanged.AddListener(OnRefreshRateChanged);
        }

        private void OnDisable()
        {
            _toggleFullscreen.onValueChanged.RemoveListener(OnFullscreenChanged);
            
            if (_dropResolution)
                _dropResolution.onValueChanged.RemoveListener(OnResolutionChanged);
            
            if (_dropFPS)
                _dropFPS.onValueChanged.RemoveListener(OnRefreshRateChanged);
        }

        private void InitFPSDropdown()
        {
            if (!_dropFPS) return;
            
            if (MenuPrefsSaver.Screen.LoadRefreshRate(out int hz, out int selectedHz))
            {
                Application.targetFrameRate = hz;
                _dropFPS.value = selectedHz;
            }
        }

        private void InitDropdownResolutions()
        {
            if (!_dropResolution) return;
            
            foreach (string resolution in GetCurrentResolutions())
            {
                _dropResolution.options.Add(new TMP_Dropdown.OptionData(resolution));
            }

            if (MenuPrefsSaver.Screen.LoadResolution(out int w, out int h, out int sel))
            {
                _dropResolution.value = sel;
                Screen.SetResolution(w, h, _toggleFullscreen.isOn);
            }
            else
            {
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, _toggleFullscreen.isOn);
            }
        }

        /// <summary>
        /// Gets the current available resolutions
        /// as array of strings.
        /// </summary>
        /// <returns>Available resolutions.</returns>
        private string[] GetCurrentResolutions()
        {
            Resolution[] resolutions = Screen.resolutions;
            System.Array.Reverse(resolutions);

            List<string> resList = new List<string>(); // Creating a list of strings

            foreach (Resolution res in resolutions) // In this list of string only add the witdth and the height
            {
                resList.Add(res.width + "x" + res.height);
            }

            return resList.ToArray().Distinct().ToArray(); // Distinct() because there are resolutions' duplicates due the refresh rate
        }
        
        private void OnFullscreenChanged(bool arg0)
        {
            SetFullScreen(arg0);
        }

        private void OnResolutionChanged(int arg0)
        {
            SetResolution();
        }
        
        private void OnRefreshRateChanged(int arg0)
        {
            SetRefreshRate();
        }
        
        #region Setter

        /// <summary>
        /// Sets the resolution of the screen.
        /// </summary>
        public void SetResolution()
        {
            string[] res = _dropResolution.options[_dropResolution.value].text.Split('x');

            int width = int.Parse(res[0]);
            int height = int.Parse(res[1]);

            MenuPrefsSaver.Screen.SaveResolution(width + ":" + height + ":" + _dropResolution.value);

            Screen.SetResolution(width, height, Fullscreen);
        }

        /// <summary>
        /// Sets the refresh rate of the screen.
        /// </summary>
        public void SetRefreshRate()
        {
            string hz = _dropFPS.options[_dropFPS.value].text;
            hz = System.Text.RegularExpressions.Regex.Replace(hz, "[^0-9]", ""); // Regular expression

            int fps = int.Parse(hz);

            MenuPrefsSaver.Screen.SaveRefreshRate(fps, _dropFPS.value);
            Application.targetFrameRate = fps;
        }
        
        public void SetFullScreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
            MenuPrefsSaver.Screen.SaveFullscreen(Screen.fullScreen);
        }

        #endregion
    }
}
