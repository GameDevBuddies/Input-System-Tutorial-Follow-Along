using System;
using UnityEngine;

namespace GameDevBuddies.SimpleCarController
{
    /// <summary>
    /// Handles the steering values.
    /// </summary>
    [Serializable]
    public class SteeringWheel : PartBase
    {
        [Header("Settings:")]
        [SerializeField] private float _maximumSteeringAngle = 35f;
        [SerializeField] private AnimationCurve _steerAngleBasedOnVelocity = default; // Will return from 0 to 1 values.
        [Space]
        [Header("Runtime Info:")]
        [SerializeField] private float _currentMaximumSteeringAngle = default;
        [SerializeField] private float _steeringAngle = default;

        public delegate void SteeringWheelEventHandler(float steerAngleValue, float acceleratorValue);
        public static event SteeringWheelEventHandler SteeringAngleChanged;

        public override float Update(float deltaTime, params object[] extraParameters)
        {
            float inputSteerValue = (float)extraParameters[0];
            float acceleratorValue = (float)extraParameters[1];
            float velocity = (float)extraParameters[2];

            float steerAngleBasedOnVelocity = _steerAngleBasedOnVelocity.Evaluate(velocity);
            _currentMaximumSteeringAngle = _maximumSteeringAngle * steerAngleBasedOnVelocity;
            _steeringAngle = inputSteerValue * _currentMaximumSteeringAngle;
            _steeringAngle = _steeringAngle * ((acceleratorValue >= 0) ? 1f : -1f);
            SteeringAngleChanged?.Invoke(_steeringAngle, acceleratorValue);
            return _steeringAngle;
        }
    }
}
