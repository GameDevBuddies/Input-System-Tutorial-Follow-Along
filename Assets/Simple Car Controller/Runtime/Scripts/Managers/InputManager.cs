using UnityEngine;

namespace GameDevBuddies.SimpleCarController
{
    /// <summary>
    /// Handles the keyboard input.
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        [Header("Settings:")]
        [SerializeField] private KeyCode _keyCodeAccelerator = default;
        [SerializeField] private KeyCode _keyCodeReverseAccelerator = default;
        [SerializeField] private KeyCode _keyCodeHandBrake = default;
        [SerializeField] private KeyCode _keyCodeSteerLeft = default;
        [SerializeField] private KeyCode _keyCodeSteerRight = default;
        [Space]
        [Header("Runtime Info:")]
        [SerializeField] private float _acceleratorValue = default;
        [SerializeField] private float _handBrakeValue = default;
        [SerializeField] private float _steerValue = default;

        public float AcceleratorValue => _acceleratorValue;
        public float HandBrakeValue => _handBrakeValue;
        public float SteerValue => _steerValue;

        #region MonoBehaviour Events
        private void Update()
        {
            _acceleratorValue = Input.GetKey(_keyCodeAccelerator) ? 1f : Input.GetKey(_keyCodeReverseAccelerator) ? -1f : 0;
            _handBrakeValue = Input.GetKey(_keyCodeHandBrake) ? 1f : 0f;
            _steerValue = Input.GetKey(_keyCodeSteerLeft) ? -1f : Input.GetKey(_keyCodeSteerRight) ? 1f : 0f;
        }
        #endregion
    }
}
