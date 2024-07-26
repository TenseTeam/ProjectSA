namespace ProjectSA.Gameplay.UsableItems.LightItem
{
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.GameConstants;

    public class LightHideObject : MonoBehaviour
    {
        [Header("Light Hide Object Settings")]
        [SerializeField]
        private GameObject _hideObject;
        
        private void Awake()
        {
            HideObject();
        }

        private void OnEnable()
        {
            EventManager.Ins.AddListener(PSAEventKeys.OnLightOff, HideObject);
        }
        
        private void OnDisable()
        {
            EventManager.Ins.RemoveListener(PSAEventKeys.OnLightOff, HideObject);
        }

        public void ShowObject()
        {
            _hideObject.SetActive(true);
        }
        
        public void HideObject()
        {
            _hideObject.SetActive(false);
        }
    }
}