using UnityEngine;

namespace Sources.Ui.Indication
{
    public class IndicatorScaler : MonoBehaviour
    {
        [SerializeField] private DirectionIndicator _directionIndicator;
        [SerializeField] private Transform _icon;
        [SerializeField] private Transform _arrow;
        [SerializeField] private float _minSize;
        [SerializeField] private float _maxSize;
        [SerializeField] private float _distanceMaxSize;
        [SerializeField] private float _distanceMinSize;

        private void Update()
        {
            Resize(_directionIndicator.DistanceToTarget);
        }

        private void Resize(float distanceToTarget)
        {
            float normalizedSize = Mathf.InverseLerp(_distanceMinSize, _distanceMaxSize, distanceToTarget);
            float size = Mathf.Lerp(_maxSize, _minSize, normalizedSize);
            _icon.localScale = new Vector2(size, size);
            _arrow.localScale = new Vector2(size, _arrow.localScale.y);
        }
    }
}