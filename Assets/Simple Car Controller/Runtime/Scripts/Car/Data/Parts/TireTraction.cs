using System;
using UnityEngine;

namespace GameDevBuddies.SimpleCarController
{
    /// <summary>
    /// Handles tire traction based on dot product from <see cref="Rigidbody"/> velocity and <see cref="Rigidbody"/> forward direction.
    /// </summary>
    [Serializable]
    public class TireTraction : PartBase
    {
        [Header("Settings:")]
        [SerializeField] private float _maximumTireTraction = 1f;
        [SerializeField] private float _minimumTireTraction = 0.5f;
        [SerializeField] private float _minimumVelocityThreshold = 0.1f;
        [Space]
        [Header("Runtime Info:")]
        [SerializeField] private float _dot = default;
        [SerializeField, Range(-1f, 1f)] private float _traction = default;

        public override float Update(float deltaTime, params object[] extraParameters)
        {
            Rigidbody rigidbody = (Rigidbody)extraParameters[0];
            Vector3 rigidbodyForward = (rigidbody.rotation * Vector3.forward).normalized;
            Vector3 velocity = rigidbody.velocity;
            Vector3 velocityForward = velocity.normalized;
            if (velocity.magnitude < _minimumVelocityThreshold)
            {
                velocityForward = rigidbodyForward;
            }
            _dot = Vector3.Dot(rigidbodyForward, velocityForward);
            _traction = Mathf.Abs(_dot);
            _traction = Mathf.Clamp(_traction, _minimumTireTraction, _maximumTireTraction);
            return _traction;
        }
    }
}
