namespace VUDK.Features.Main.Camera.CameraViews
{
    using System;
    using UnityEngine;
    using VUDK.Features.Main.CharacterController;

    public class FirstPersonCamera : CameraFreeLook
    {
        [SerializeField, Header("First Person Settings"), Tooltip("Target Character Controller")]
        private CharacterController3DBase _targetCharacter;
        [SerializeField]
        private Vector3 _targetPositionOffset;
        [SerializeField]
        private float _followSmoothTime = 0.1f;
        
        [Header("Camera Bobbing Settings")]
        [SerializeField]
        private bool _useCameraBobbing;
        [SerializeField, Min(0f)]
        private float _bobbingAmplitude = 1f;
        [SerializeField, Min(0f)]
        private float _bobbingFrequency = 0.1f;

        private Transform _currentTarget;
        private Vector3 _currentCameraVelocity;
        private float _currentSmoothTime;
        private Vector3 _bobbingOffset;
        
        private Vector3 TargetPosition => _currentTarget.position + _targetPositionOffset + _bobbingOffset;
        private Quaternion TargetRotation => _currentTarget.rotation;

        private void OnValidate()
        {
            if (!_targetCharacter) return;
            
            transform.position = _targetCharacter.transform.position + _targetPositionOffset;
        }

        protected override void Awake()
        {
            base.Awake();

            if (!_targetCharacter)
            {
                Debug.LogError($"Target Character Controller in {gameObject.name} is null.");
                return;
            }
            
            SetTarget(_targetCharacter.transform, _followSmoothTime);
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            LockToTargetPosition();
            
            if (_useCameraBobbing)
                CameraBobbing();
        }

        public virtual void SetTarget(Transform target, float smoothTime = 0f, bool canLook = true)
        {
            _currentTarget = target;
            _currentSmoothTime = smoothTime;
            
            if (canLook)
                Enable();
            else
                Disable();
        }
        
        public virtual void ResetTarget()
        {
            SetTarget(_targetCharacter.transform, _followSmoothTime, true);
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
            transform.position = Vector3.SmoothDamp(transform.position, TargetPosition, ref _currentCameraVelocity, Time.deltaTime * _currentSmoothTime);

            if (!CanLook)
                transform.rotation = Quaternion.Slerp(transform.rotation, TargetRotation, Time.deltaTime * _currentSmoothTime);
        }
        
        private void CameraBobbing()
        {
            _bobbingOffset = Vector3.up * Mathf.Sin(Time.time * _bobbingFrequency) * _bobbingAmplitude;
        }
    }
}