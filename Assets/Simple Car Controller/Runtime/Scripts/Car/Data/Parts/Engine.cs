using System;
using UnityEngine;

namespace GameDevBuddies.SimpleCarController
{
    /// <summary>
    /// Handles the engine power that drives the <see cref="Rigidbody"/>.
    /// </summary>
    [Serializable]
    public class Engine : PartBase
    {
        [Header("Settings:")]
        [SerializeField] private float _maximumPowerOutput = 2500;
        [SerializeField] private AnimationCurve _throttleOutputRatio = default;
        [Space]
        [Header("Runtime Values:")]
        [SerializeField] private float _currentPowerOutput = default;

        public float CurrentPowerOutput
        {
            get
            {
                return _currentPowerOutput;
            }
        }

        public override float Update(float deltaTime, params object[] extraParameters)
        {
            float acceleratorValue = (float)extraParameters[0];
            _currentPowerOutput = _throttleOutputRatio.Evaluate(acceleratorValue) * _maximumPowerOutput;
            return _currentPowerOutput;
        }
    }
}
