using UnityEngine;

namespace GameDevBuddies.SimpleCarController
{
    /// <summary>
    /// Sets center of mass on the <see cref="UnityEngine.Rigidbody"/> 
    /// component that lives on the same <see cref="GameObject"/> as this component.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class CenterOfMassController : MonoBehaviour
    {
        [Header("Local References:")]
        [SerializeField] private Transform _centerOfMass = default;

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

        /// <inheritdoc/>
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawSphere(Rigidbody.worldCenterOfMass, 0.05f);
        }

        /// <inheritdoc/>
        private void Awake()
        {
            Rigidbody.centerOfMass = _centerOfMass.localPosition;
        }
    }
}
