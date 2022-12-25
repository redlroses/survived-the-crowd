using Sources.Tools;
using Sources.Tools.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Ui.Indication
{
    public class DirectionIndicator : MonoBehaviour
    {
        [Header("Transforms")]
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _centerTransform;

        [Space] [Header("Icon")]
        [SerializeField] private Image _indicatorIcon;
        [SerializeField] private float _iconOffset;

        [Space] [Header("Arrow")]
        [SerializeField] private Image _indicatorArrow;
        [SerializeField] private float _arrowOffset;

        private Vector2 _indicationDirection;

        private void LateUpdate()
        {
            _indicationDirection = GetDirection();
            SetIndicatorPosition(_indicatorIcon, _iconOffset);
            SetIndicatorPosition(_indicatorArrow, _arrowOffset);
            RotateIndicator(_indicatorArrow);
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
            float angle = Mathf.Atan2(_indicationDirection.y, _indicationDirection.x) * Mathf.Rad2Deg - Constants.HalfPiDegrees;
            indicator.rectTransform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
