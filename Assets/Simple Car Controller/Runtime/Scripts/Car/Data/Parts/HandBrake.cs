using System;
using UnityEngine;

namespace GameDevBuddies.SimpleCarController
{
    /// <summary>
    /// <br>Handles handbrake value.</br>
    /// <br>Basically just forwards the input value at the moment, but easily expandable for different purposes.</br>
    /// </summary>
    [Serializable]
    public class HandBrake : PartBase
    {
        [Header("Settings:")]
        [SerializeField, Range(0f, 1f)] private float _power = 0.5f;
        [Header("Runtime Values:")]
        [SerializeField] private float _handBrakeValue = default;
        public override float Update(float deltaTime, params object[] extraParameters)
        {
            _handBrakeValue = (float)extraParameters[0];
            return _handBrakeValue * _power;
        }
    }
}
