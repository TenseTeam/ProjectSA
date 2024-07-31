namespace ProjectSA.Gameplay.Enemy
{
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;
    using ProjectSA.GameConstants;
    using UnityEngine.Animations;

    public class ShadowGraphicsController : MonoBehaviour
    {
        [Header("Graphics Settings")]
        [SerializeField]
        private LookAtConstraint _lookAtConstraint;
        [SerializeField]
        private SpriteRenderer _shadowSpriteRenderer;
        [SerializeField]
        private Sprite _requestPhaseSprite;
        [SerializeField]
        private Sprite _stunPhaseSprite;

        private void OnEnable()
        {
            EventManager.Ins.AddListener(PSAEventKeys.OnStunTimerStart, SetStunImage);
            EventManager.Ins.AddListener(PSAEventKeys.OnRequestTimerStart, SetRequestImage);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener(PSAEventKeys.OnStunTimerStart, SetStunImage);
            EventManager.Ins.RemoveListener(PSAEventKeys.OnRequestTimerStart, SetRequestImage);
        }
        
        private void SetStunImage()
        {
            _lookAtConstraint.enabled = false;
            _shadowSpriteRenderer.sprite = _stunPhaseSprite;
        }
        
        private void SetRequestImage()
        {
            _lookAtConstraint.enabled = true;
            _shadowSpriteRenderer.sprite = _requestPhaseSprite;
        }
    }
}