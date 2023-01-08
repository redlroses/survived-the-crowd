using DG.Tweening;
using UnityEngine;

namespace Sources.Ui.Animations
{
    public class LoadingAnimation : MonoBehaviour
    {
        [SerializeField] private RectTransform _rotor;
        [SerializeField] private float _duration;

        private Tween _rotation;

        private void Awake()
        {
            _rotation = _rotor.DORotate(new Vector3(0, 0, 360f), _duration, RotateMode.FastBeyond360).SetLoops(-1);
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