using System.Collections;
using Sources.Level;
using Sources.Ui.Wrapper;
using UnityEngine;

namespace Sources.Timer
{
    public class TimerView : MonoBehaviour
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();

        [SerializeField] private LoseDetector _loseDetector;
        [SerializeField] private TextSetter _textSetter;

        private Coroutine _countTime;
        private bool _isCounting;

        private void OnEnable()
        {
            _loseDetector.Losed += StopCountTime;
            _stopwatch.Reset();
            StartCountTime();
        }

        private void OnDisable()
        {
            _loseDetector.Losed -= StopCountTime;
        }

        public void StartCountTime()
        {
            _isCounting = true;
            _countTime ??= StartCoroutine(CountTime());
        }

        public float GetTime()
            => _stopwatch.GetSeconds();

        private void StopCountTime()
        {
            if (_countTime == null)
            {
                return;
            }

            _isCounting = false;
            _countTime = null;
        }

        private IEnumerator CountTime()
        {
            while (_isCounting)
            {
                _stopwatch.Tick(Time.deltaTime);
                _textSetter.Set(_stopwatch.GetTime());

                yield return null;
            }
        }
    }
}