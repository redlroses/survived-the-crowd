using System;
using DG.Tweening;
using Sources.Vehicle;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Ui
{
    public sealed class LowFuelAnimation : MonoBehaviour
    {
        private const int Infinite = -1;

        [SerializeField] private Image _icon;
        [SerializeField] private GasTank _gasTank;
        [SerializeField] [Range(0, 100f)] private float _levelTrigger = 20f;
        [SerializeField] private float _duration;

        private Sequence _animationSequence;
        private bool _isPlaying;
        private Tween _animation;

        private void Start()
        {
            _animationSequence = DOTween.Sequence();
            _animationSequence.Append(_icon.DOFade(1f, _duration));
            _animationSequence.Append(_icon.DOFade(0f, _duration));
            _animationSequence.OnComplete(TryPlayAnimation);
        }

        private void Update()
        {
            bool isLowLevel = _gasTank.FuelLevelPercent < _levelTrigger;

            if (_isPlaying == false && isLowLevel)
            {
                _isPlaying = true;
                TryPlayAnimation();
            }
            else if (_isPlaying && isLowLevel == false)
            {
                _isPlaying = false;
            }
        }

        private void TryPlayAnimation()
        {
            if (_isPlaying)
            {
                Debug.Log("complete");
                _animationSequence.Restart();
            }
            else
            {
                _animationSequence.Pause();
            }
        }
    }
}
