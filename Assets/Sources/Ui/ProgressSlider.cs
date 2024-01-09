using System;
using System.Linq;
using Sources.Level;
using Sources.Tools;
using Sources.Ui.Wrapper;
using UnityEngine;

namespace Sources.Ui
{
    public class ProgressSlider : MonoBehaviour
    {
        private const string TheSliderIsAlreadyLinkedToTheProgressBarObject =
            "The slider is already linked to the progress bar object";

        [SerializeField] private bool _isSmoothly = true;
        [SerializeField] private TextSetter _count;
        [SerializeField] private RectTransform _grid;

        [SerializeField] private SliderSetter _slider;
        private bool _isSelfUpdated;

        private ProgressBar _progressBar;

        protected virtual void OnEnable()
        {
            if (_isSelfUpdated)
            {
                _progressBar.Updated += UpdateProgress;
            }
        }

        protected virtual void OnDisable()
        {
            if (_isSelfUpdated)
            {
                _progressBar.Updated += UpdateProgress;
            }
        }

        public void Init(ProgressBar associateWith)
        {
            ComponentTool.CheckNull(associateWith);
            _progressBar = associateWith;
            _isSelfUpdated = true;
            _slider.SetBounds(_progressBar.LowValue, _progressBar.HighValue);
            _slider.SetValue(_progressBar.Amount);
            SetGrid(_progressBar.Stages);
            _count.Set(_progressBar.Stages);
        }

        public void UpdateProgress(float value, bool isSmoothly = false)
        {
            if (_isSelfUpdated)
            {
                throw new InvalidOperationException(TheSliderIsAlreadyLinkedToTheProgressBarObject);
            }

            _slider.SetValue(value, isSmoothly);
        }

        private void UpdateProgress()
        {
            _slider.SetValue(_progressBar.Amount, _isSmoothly);
        }

        private void SetGrid(int size)
        {
            int currentSize = _grid.Cast<RectTransform>().Count(cells => cells.gameObject.activeSelf);

            if (size == currentSize)
            {
                return;
            }

            if (size < currentSize)
            {
                for (int i = size; i < currentSize; i++)
                {
                    _grid.GetChild(i).gameObject.SetActive(false);
                }
            }
            else
            {
                for (int i = currentSize; i < size; i++)
                {
                    _grid.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
    }
}