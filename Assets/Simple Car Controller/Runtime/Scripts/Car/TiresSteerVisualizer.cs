using UnityEngine;

namespace GameDevBuddies.SimpleCarController
{
    /// <summary>
    /// <br>Visualizer handling tire rotation during steering.</br>
    /// <br>NOTE: Steering wheel maximum angle may not match the tires maximum rotation - but that's not an issue since we lock the tire rotation if steering maximum rotation is greater that visualization.</br>
    /// </summary>
    public class TiresSteerVisualizer : MonoBehaviour
    {
        [Header("Local References:")]
        [SerializeField] private Transform[] _steeringTires = default;
        [Space]
        [Header("Settings:")]
        [SerializeField] private float _maximumRotationAngle = 40f; // In degrees.

        private Quaternion[] _initialLocalRotations = default;

        private void Awake()
        {
            _initialLocalRotations = new Quaternion[_steeringTires.Length];
            for (int i = 0; i < _steeringTires.Length; i++)
            {
                _initialLocalRotations[i] = _steeringTires[i].localRotation;
            }
        }

        private void OnEnable()
        {
            SteeringWheel.SteeringAngleChanged += OnSteeringAngleChanged;
        }

        private void OnDisable()
        {
            SteeringWheel.SteeringAngleChanged -= OnSteeringAngleChanged;
        }

        /// <summary>
        /// <br>Callback received from <see cref="SteeringWheel"/>.</br>.
        /// </summary>
        /// <param name="steerAngleValue">Amount that steering wheel has turned.</param>
        private void OnSteeringAngleChanged(float steerAngleValue, float acceleratorValue)
        {
            steerAngleValue = steerAngleValue * ((acceleratorValue >= 0) ? 1f : -1f); // Accommodate for reverse movement.
            SetTireRotation(steerAngleValue);
        }

        /// <summary>
        /// Sets the tire rotation.
        /// </summary>
        /// <param name="angle">Tire rotation amount.</param>
        private void SetTireRotation(float angle)
        {
            angle = Mathf.Clamp(angle, -_maximumRotationAngle, _maximumRotationAngle);
            for (int i = 0; i < _steeringTires.Length; i++)
            {
                Transform steeringTire = _steeringTires[i];
                Quaternion initialLocalRotation = _initialLocalRotations[i];
                steeringTire.localRotation = initialLocalRotation * Quaternion.Euler(0, angle, 0);
            }
        }
    }
}
