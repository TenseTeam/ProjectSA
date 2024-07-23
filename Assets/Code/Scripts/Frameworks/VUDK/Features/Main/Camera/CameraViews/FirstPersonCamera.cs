namespace VUDK.Features.Main.Camera.CameraViews
{
    using UnityEngine;
    using VUDK.Features.Main.CharacterController;

    public class FirstPersonCamera : CameraFreeLook
    {
        [SerializeField, Header("First Person Settings"), Tooltip("Target Character Controller")]
        private CharacterController3DBase _targetCharacter;
        [SerializeField]
        private Vector3 _targetPositionOffset;
        
        [Header("Camera Bobbing Settings")]
        [SerializeField]
        private bool _useCameraBobbing;
        [SerializeField, Min(0f)]
        private float _bobbingAmplitude = 1f;
        [SerializeField, Min(0f)]
        private float _bobbingFrequency = 0.1f;

        private Vector3 TargetPosition => _targetCharacter.transform.position + _targetPositionOffset;

        protected virtual void OnValidate()
        {
            if (!_targetCharacter) return;
            transform.position = TargetPosition;
        }

        protected override void Awake()
        {
            base.Awake();

            if (_targetCharacter) return;
            Debug.LogError($"Target Character Controller in {gameObject.name} is null.");
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            LockToTargetPosition();
            
            if (_useCameraBobbing)
                CameraBobbing();
        }

        protected override void LookRotate()
        {
            base.LookRotate();
            RotateTarget();
        }

        private void RotateTarget()
        {
            _targetCharacter.MoveRotation(Quaternion.Euler(0f, transform.eulerAngles.y, 0f));
        }

        private void LockToTargetPosition()
        {
            transform.position = TargetPosition;
        }
        
        private void CameraBobbing()
        {
            transform.position += Vector3.up * Mathf.Sin(Time.time * _bobbingFrequency) * _bobbingAmplitude;
        }
    }
}