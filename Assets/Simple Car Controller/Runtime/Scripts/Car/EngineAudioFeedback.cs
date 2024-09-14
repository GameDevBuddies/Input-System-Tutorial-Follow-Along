using UnityEngine;

namespace GameDevBuddies.SimpleCarController
{
    /// <summary>
    /// Manages the car engine audio feedback.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class EngineAudioFeedback : MonoBehaviour
    {
        [Header("Local References:")]
        [SerializeField] private AudioSource _audioSourceEngine = default;
        [Space]
        [Header("Settings:")]
        [SerializeField] private float _defaultPitch = 0.9f;
        [SerializeField] private float _defaultVolume = 0.9f;
        [Space]
        [SerializeField] private float _velocityPitchMultiplier = 0.02f;
        [SerializeField] private float _velocityVolumeMultiplier = 0.02f;
        [Space]
        [Header("Runtime Info:")]
        [SerializeField] public float _currentPitchLevel = default;
        [SerializeField] public float _currentVolumeLevel = default;

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

        private void Update()
        {
            RefreshEngineAudio();
        }

        /// <summary>
        /// Basic pitch and volume calculations based on velocity.
        /// </summary>
        private void RefreshEngineAudio()
        {
            _currentPitchLevel = Rigidbody.velocity.sqrMagnitude * _velocityPitchMultiplier;
            _currentVolumeLevel = Rigidbody.velocity.sqrMagnitude * _velocityVolumeMultiplier;
            _audioSourceEngine.pitch = _defaultPitch + _currentPitchLevel;
            _audioSourceEngine.volume = _defaultVolume + _currentVolumeLevel;
        }
    }
}
