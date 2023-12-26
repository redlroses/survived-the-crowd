using Sources.Tools;
using Sources.Tools.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Ui.Indication
{
    public class DirectionIndicator : MonoBehaviour
    {
        [SerializeField] private float _arrowOffset;
        [Header("Transforms")]
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _centerTransform;
        [SerializeField] private float _iconOffset;

        [Space] [Header("Arrow")]
        [SerializeField] private Image _indicatorArrow;

        [Space] [Header("Icon")]
        [SerializeField] private Image _indicatorIcon;
        [SerializeField] private Transform _target;

        private Vector2 _indicationDirection;

        public float DistanceToTarget
            => Vector3.Distance(_target.position, _centerTransform.position);

        private void LateUpdate()
        {
            _indicationDirection = GetDirection();
            SetIndicatorPosition(_indicatorIcon, _iconOffset);
            SetIndicatorPosition(_indicatorArrow, _arrowOffset);
            RotateIndicator(_indicatorArrow);
        }

        public void Activate(Transform target)
        {
            _target = target;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        private Vector3 GetDirection()
        {
            Vector3 direction = (_target.position - _centerTransform.position).normalized;

            return direction.ToInputFormat().RotateVector2(_cameraTransform.rotation.eulerAngles.y);
        }

        private void SetIndicatorPosition(Graphic indicator, float offset)
        {
            indicator.rectTransform.anchoredPosition = _indicationDirection * offset;
        }

        private void RotateIndicator(Graphic indicator)
        {
            float angle = Mathf.Atan2(_indicationDirection.y, _indicationDirection.x) * Mathf.Rad2Deg
                          - Constants.HalfPiDegrees;

            indicator.rectTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}