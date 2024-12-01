using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerVignette : MonoBehaviour
    {
        [SerializeField] private Image _vignetteImage;
        [SerializeField] private PlayerBell _playerBell;
        [SerializeField] private int _vignettePartCount;
        [SerializeField] private float _silenceTime;

        private float _silenceTimer;
        public float SilenceTime => _silenceTime;
        
        private int _currentPart;

        private void Start()
        {
            _currentPart = 0;
        }

        private void OnEnable()
        {
            _playerBell.Call += ChangeVignette;
        }

        private void OnDisable()
        {
            _playerBell.Call -= ChangeVignette;
        }

        private void Update()
        {
            if (_currentPart > 0)
            {
                _silenceTimer = Mathf.Clamp01(_silenceTimer - Time.deltaTime / _silenceTime);

                _vignetteImage.color =
                    new Color(_vignetteImage.color.r, _vignetteImage.color.g, _vignetteImage.color.b,
                        Mathf.Lerp(0f, 1f, _silenceTimer));

                if (_silenceTimer <= 0)
                {
                    _currentPart = 0;
                }
            }
        }

        private void ChangeVignette()
        {
            _currentPart = Mathf.Clamp(_currentPart + 1, 0, _vignettePartCount);

            var alpha = 1f / _vignettePartCount * _currentPart;
            _vignetteImage.color =
                new Color(_vignetteImage.color.r, _vignetteImage.color.g, _vignetteImage.color.b, alpha);

            _silenceTimer = alpha;
        }
    }
}