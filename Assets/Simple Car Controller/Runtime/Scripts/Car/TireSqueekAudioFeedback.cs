using UnityEngine;

namespace GameDevBuddies.SimpleCarController
{

    /// <summary>
    /// <br>Handles the car skid audio feedback effect.</br>
    /// <br>The skid audio will play only when there's skid mark visible.</br>
    /// </summary>
    [RequireComponent(typeof(SkidmarkEffect))]
    public class TireSqueekAudioFeedback : MonoBehaviour
    {
        [Header("Local References:")]
        [SerializeField] private AudioSource _skidAudioSource = default;
        [Space]
        [Header("Settings:")]
        [SerializeField] private float _filterThreshold = 0.1f;

        private SkidmarkEffect _skidmarkEffect = default;
        private float _skidmarkEffectValue = default;

        private SkidmarkEffect SkidmarkEffect
        {
            get
            {
                if (_skidmarkEffect == null)
                {
                    _skidmarkEffect = GetComponent<SkidmarkEffect>();
                }
                return _skidmarkEffect;
            }
        }

        private void OnEnable()
        {
            SkidmarkEffect.EffectValueUpdated += OnEffectValueUpdated;
            SkidmarkEffect.VisibilityUpdated += OnVisibilityUpdated;
        }

        private void OnDisable()
        {
            SkidmarkEffect.EffectValueUpdated -= OnEffectValueUpdated;
            SkidmarkEffect.VisibilityUpdated -= OnVisibilityUpdated;
        }

        /// <summary>
        /// Callback from <see cref="SimpleCarController.SkidmarkEffect"/> when skid mark visibility changes.
        /// </summary>
        /// <param name="isActive">Is skid mark on the ground effect visible?</param>
        private void OnVisibilityUpdated(bool isActive)
        {
            if (isActive && _skidmarkEffectValue > _filterThreshold)
            {
                if (!_skidAudioSource.isPlaying)
                {
                    _skidAudioSource.Play();
                }
            }
            else
            {
                _skidAudioSource.Stop();
            }
        }

        /// <summary>
        /// Callback from <see cref="SimpleCarController.SkidmarkEffect"/> when skid mark effect value changes.
        /// </summary>
        /// <param name="skidmarkEffectValue">Skid mark effect/strength value range from 0 to 1.</param>
        private void OnEffectValueUpdated(float skidmarkEffectValue)
        {
            _skidmarkEffectValue = skidmarkEffectValue;
        }
    }
}
