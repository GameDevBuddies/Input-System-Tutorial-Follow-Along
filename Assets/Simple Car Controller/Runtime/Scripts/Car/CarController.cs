using UnityEngine;

namespace GameDevBuddies.SimpleCarController
{
    /// <summary>
    /// Main controller that samples all values and applies values to the <see cref="_rigidbody"/> component.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class CarController : MonoBehaviour
    {
        [Header("Scene References:")]
        [SerializeField] private InputManager _inputManager = default;
        [Space]
        [Header("Parts:")]
        [SerializeField] private Engine _engine = default;
        [SerializeField] private HandBrake _handBrake = default;
        [SerializeField] private SteeringWheel _steeringWheel = default;
        [SerializeField] private TireTraction _tireTraction = default;
        [Space]
        [Header("Project References:")]
        [SerializeField] private PhysicMaterial _physicMaterialTire = default;
        [Header("Settings:")]
        [SerializeField] private float _handlingMultiplier = 0.5f;
        [SerializeField] private AnimationCurve _angularRotationStrenthBasedOnVelocity = default;
        [Space]
        [SerializeField] private float _maximumVelocity = 5;
        [Space]
        [Header("Runtime Info:")]
        [SerializeField] private Vector3 _forceDirection = default;
        [SerializeField] private float _currentVelocity = default;

        private Rigidbody _rigidbody = default;

        public Engine Engine
        {
            get
            {
                return _engine;
            }
        }

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

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, _forceDirection);
        }

        private void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;
            float enginePowerOutput = _engine.Update(deltaTime, _inputManager.AcceleratorValue);
            float handBrakeOutput = _handBrake.Update(deltaTime, _inputManager.HandBrakeValue);
            float steerAngle = _steeringWheel.Update(deltaTime, _inputManager.SteerValue, _inputManager.AcceleratorValue, Rigidbody.velocity.magnitude);
            float tireTraction = _tireTraction.Update(deltaTime, Rigidbody);

            if (Mathf.Approximately(handBrakeOutput, 0))
            {
                _physicMaterialTire.dynamicFriction = 0.3f;
                _physicMaterialTire.staticFriction = 0.3f;
            }
            else
            {
                _physicMaterialTire.dynamicFriction = 0f;
                _physicMaterialTire.staticFriction = 0f;
            }

            // Steering.
            Quaternion steeringRotation = Quaternion.Euler(0, steerAngle * _angularRotationStrenthBasedOnVelocity.Evaluate(Rigidbody.velocity.magnitude), 0);
            steeringRotation = Rigidbody.rotation * steeringRotation;
            _forceDirection = steeringRotation * Vector3.forward;

            Vector3 calculatedForce = _forceDirection * enginePowerOutput * tireTraction * (1 - handBrakeOutput);
            Rigidbody.AddForce(calculatedForce);

            Vector3 angularVelocity = Rigidbody.rotation * Vector3.up;
            angularVelocity *= steerAngle * _handlingMultiplier;
            angularVelocity *= _angularRotationStrenthBasedOnVelocity.Evaluate(Rigidbody.velocity.magnitude);
            Rigidbody.angularVelocity = angularVelocity;

            _currentVelocity = Rigidbody.velocity.magnitude;
            _currentVelocity = Mathf.Clamp(_currentVelocity, 0f, _maximumVelocity);
            Rigidbody.velocity = Rigidbody.velocity.normalized * _currentVelocity;
        }
    }
}
