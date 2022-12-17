using Sources.Collectables;
using UnityEngine;

namespace Sources.Ui
{
    public class CarUnlockView : ProgressSlider
    {
        [SerializeField] private DetailsCollector _detailsCollector;

        protected override void OnEnable()
        {
            Init(_detailsCollector.CurrentProgressBar);
            base.OnEnable();
        }
    }
}