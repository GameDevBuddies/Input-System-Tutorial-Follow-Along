using UnityEngine;

namespace GameDevBuddies.SimpleCarController
{
    /// <summary>
    /// Handles visibility of skid-marks that are visualized by <see cref="TrailRenderer"/> component on rear-left and rear-right wheels.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class SkidmarkEffect : MonoBehaviour
    {
        [Header("Settings:")]
        [SerializeField] private float _velocityThreshold = 0.1f;
        [SerializeField] private float _skidmarkEffectThreshold = 2.5f;
        [Space]
        [Header("Local References:")]
        [SerializeField] private CarController _carController = default;
        [SerializeField] private TrailRenderer _leftTrailRenderer = default;
        [SerializeField] private TrailRenderer _rightTrailRenderer = default;

        private Rigidbody _rigidbody = default;

        private Rigidbody Rigidbody
        {
            get
            {
                if (_rigidbody == null)
                {
                    _rigidbody = GetComponent<Rigidbody>();
                }
                return _rigidbody;
            }
        }

        public delegate void SkidmarkEffectEventHandler(float skidmarkEffectValue);
        public event SkidmarkEffectEventHandler EffectValueUpdated;
        public delegate void SkidMarkEffectVisibilityEventHandler(bool isActive);
        public event SkidMarkEffectVisibilityEventHandler VisibilityUpdated;

        /// <inheritdoc/>
        private void FixedUpdate()
        {
            float skidmarkEffectValue = CalculateSkidmarkEffectValue();
            bool isEmitting = skidmarkEffectValue < _skidmarkEffectThreshold && Rigidbody.velocity.magnitude > _velocityThreshold;
            SetTrailRendererEmitting(isEmitting);
            EffectValueUpdated?.Invoke(skidmarkEffectValue);
            VisibilityUpdated(isEmitting);
        }

        /// <summary>
        /// <br>Calculates the dot product based on the <see cref="Rigidbody"/> object orientation and velocity.</br>
        /// <br>Calculated dot product is the result with strength of how much skid should be shown.</br>
        /// <br>Later in code, <see cref="_skidmarkEffectThreshold"/> is used to determine if skid-mark should be visible or not.</br>
        /// </summary>
        /// <returns>Dot product that determines the value of direction where body is oriented opposed to the velocity it has.</returns>
        private float CalculateSkidmarkEffectValue()
        {
            Vector3 rigidbodyForward = Rigidbody.rotation * ((_carController.Engine.CurrentPowerOutput >= 0f) ? Vector3.forward : Vector3.back);
            rigidbodyForward = rigidbodyForward.normalized;
            Vector3 velocityDirection = Rigidbody.velocity;
            velocityDirection = velocityDirection.normalized;
            float dotProduct = Vector3.Dot(rigidbodyForward, velocityDirection);
            return dotProduct;
        }

        /// <summary>
        /// Sets the <see cref="TrailRenderer.emitting"/> property to <paramref name="isEmitting"/> value.
        /// </summary>
        /// <param name="isEmitting">Should <see cref="TrailRenderer"/> be visible?</param>
        private void SetTrailRendererEmitting(bool isEmitting)
        {
            _leftTrailRenderer.emitting = isEmitting;
            _rightTrailRenderer.emitting = isEmitting;
        }
    }
}
