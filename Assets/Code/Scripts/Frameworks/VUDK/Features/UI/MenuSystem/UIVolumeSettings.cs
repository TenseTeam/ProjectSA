namespace VUDK.Features.UI.MenuSystem
{
    using UnityEngine;
    using UnityEngine.Audio;
    using UnityEngine.UI;
    using VUDK.Extensions;
    using VUDK.Features.UI.MenuSystem.MenuPreferences;

    public class UIVolumeSettings : MonoBehaviour
    {
        [SerializeField, Header("Mixer Group")]
        private AudioMixer _mixer;
        [SerializeField]
        private float _maxVolume = 0f;
        [SerializeField]
        private float _minVolume = -60f;

        [SerializeField, Header("Sliders")]
        private Slider _masterSlider;
        [SerializeField]
        private Slider _musicSlider;
        [SerializeField]
        private Slider _effectsSlider;

        private void Start() // Need to do it on start cause the mixer is loaded after the awake
        {
            if (MenuPrefsSaver.Audio.LoadVolume(out float master, out float music, out float sfx))
            {
                _mixer.SetFloat("Master", MathExtension.DenormalizeInRange(master, _minVolume, _maxVolume));
                _mixer.SetFloat("Music", MathExtension.DenormalizeInRange(music, _minVolume, _maxVolume));
                _mixer.SetFloat("Effects", MathExtension.DenormalizeInRange(sfx, _minVolume, _maxVolume));

                Debug.Log("Loaded volume settings: " + master + " " + music + " " + sfx);
                _masterSlider.value = master;
                _musicSlider.value = music;
                _effectsSlider.value = sfx;
            }
            else
            {
                // If there were no saved values then we set the volume to 0 (that means +0dB)
                _mixer.SetFloat("Master", _maxVolume);
                _mixer.SetFloat("Music", _maxVolume);
                _mixer.SetFloat("Effects", _maxVolume);
                
                _masterSlider.value = 1f;
                _musicSlider.value = 1f;
                _effectsSlider.value = 1f;
            }
        }

        private void OnEnable()
        {
            _masterSlider.onValueChanged.AddListener(OnMasterChanged);
            _musicSlider.onValueChanged.AddListener(OnMusicChanged);
            _effectsSlider.onValueChanged.AddListener(OnEffectsChanged);
        }
        
        private void OnDisable()
        {
            _masterSlider.onValueChanged.RemoveListener(OnMasterChanged);
            _musicSlider.onValueChanged.RemoveListener(OnMusicChanged);
            _effectsSlider.onValueChanged.RemoveListener(OnEffectsChanged);
        }

        /// <summary>
        /// Sets the master volume.
        /// </summary>
        public void SetMaster()
        {
            float volume = MathExtension.NormalizeInRange(_masterSlider.value, _minVolume, _maxVolume);
            _mixer.SetFloat("Master", volume);
            MenuPrefsSaver.Audio.SaveVolume(_masterSlider.value, _musicSlider.value, _effectsSlider.value);
        }

        /// <summary>
        /// Sets the music volume.
        /// </summary>
        public void SetMusic()
        {
            float volume = MathExtension.NormalizeInRange(_musicSlider.value, _minVolume, _maxVolume);
            _mixer.SetFloat("Music", volume);
            MenuPrefsSaver.Audio.SaveVolume(_masterSlider.value, _musicSlider.value, _effectsSlider.value);
        }

        /// <summary>
        /// Sets the effects volume.
        /// </summary>
        public void SetEffects()
        {
            float volume = MathExtension.NormalizeInRange(_effectsSlider.value, _minVolume, _maxVolume);
            _mixer.SetFloat("Effects", volume);
            MenuPrefsSaver.Audio.SaveVolume(_masterSlider.value, _musicSlider.value, _effectsSlider.value);
        }
        
        private void OnMasterChanged(float arg0)
        {
            SetMaster();
        }
        
        private void OnMusicChanged(float arg0)
        {
            SetMusic();
        }
        
        private void OnEffectsChanged(float arg0)
        {
            SetEffects();
        }
    }
}