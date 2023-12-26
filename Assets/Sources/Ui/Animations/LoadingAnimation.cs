using DG.Tweening;
using UnityEngine;

namespace Sources.Ui.Animations
{
    public class LoadingAnimation : MonoBehaviour
    {
        private readonly Vector3 _rotatedVector = new Vector3(0, 0, 360f);

        [SerializeField] private float _duration;
        [SerializeField] private RectTransform _rotor;

        private Tween _rotation;

        private void Awake()
        {
            _rotation = _rotor.DORotate(_rotatedVector, _duration, RotateMode.FastBeyond360).SetLoops(-1);
        }

        public void Play()
        {
            _rotor.gameObject.SetActive(true);
            _rotation.Restart();
        }

        public void Stop()
        {
            _rotation.Pause();
            _rotor.gameObject.SetActive(false);
        }
    }
}