using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Import.Joystick.Scripts
{
    [RequireComponent(typeof(RectTransform))]
    public sealed class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        private const string JoystickIsOutTheCanvas = "Joystick is out the canvas";

        private readonly float _diameter2Radius = 0.5f;
        private readonly Vector2 _center = new Vector2(0.5f, 0.5f);
        private readonly Camera _camera = null;

        [SerializeField] private RectTransform _background;
        [SerializeField] private float _handleRange = 1;
        [SerializeField] private float _deadZone;
        [SerializeField] private RectTransform _handle;
        [SerializeField] private AxisOptions _axisOptions = AxisOptions.Both;

        private RectTransform _baseRect;
        private Canvas _canvas;
        private Vector2 _input = Vector2.zero;

        public event Action<float, float> StickDeviated;
        public event Action StickDown;
        public event Action StickUp;

        public float Horizontal => _input.x;
        public float Vertical => _input.y;
        public Vector2 Direction => new Vector2(Horizontal, Vertical);

        private void Start()
        {
            _baseRect = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();

            if (_canvas == null)
            {
                throw new NullReferenceException(JoystickIsOutTheCanvas);
            }

            _background.pivot = _center;
            _handle.anchorMin = _center;
            _handle.anchorMax = _center;
            _handle.pivot = _center;
            _handle.anchoredPosition = Vector2.zero;
            _background.gameObject.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            _background.gameObject.SetActive(true);
            OnDrag(eventData);
            StickDown?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            var position = RectTransformUtility.WorldToScreenPoint(_camera, _background.position);
            var radius = _background.sizeDelta * _diameter2Radius;
            _input = (eventData.position - position) / (radius * _canvas.scaleFactor);
            FormatInput();
            HandleInput(_input.magnitude, _input.normalized);
            _handle.anchoredPosition = _input * radius * _handleRange;
            StickDeviated?.Invoke(Horizontal, Vertical);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _background.gameObject.SetActive(false);
            _input = Vector2.zero;
            _handle.anchoredPosition = Vector2.zero;
            StickUp?.Invoke();
        }

        private void HandleInput(float magnitude, Vector2 normalised)
        {
            if (magnitude > _deadZone)
            {
                if (magnitude > 1)
                {
                    _input = normalised;
                }
            }
            else
            {
                _input = Vector2.zero;
            }
        }

        private Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_baseRect, screenPosition, _camera,
                out var localPoint) == false)
            {
                return Vector2.zero;
            }

            var sizeDelta = _baseRect.sizeDelta;
            var pivotOffset = _baseRect.pivot * sizeDelta;
            return localPoint - _background.anchorMax * sizeDelta + pivotOffset;
        }

        private void FormatInput()
        {
            _input = _axisOptions switch
            {
                AxisOptions.Horizontal => new Vector2(_input.x, 0f),
                AxisOptions.Vertical => new Vector2(0f, _input.y),
                _ => _input
            };
        }
    }

    internal enum AxisOptions
    {
        Both,
        Horizontal,
        Vertical
    }
}