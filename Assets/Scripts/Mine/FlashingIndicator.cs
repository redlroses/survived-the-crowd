using System.Collections;
using UnityEngine;

namespace Mine
{
    public sealed class FlashingIndicator : MonoBehaviour
    {
        [SerializeField] private Mine _mine;
        [SerializeField] private float _baseFlickRate;
        [SerializeField] private float _minFlickRate;
        [SerializeField] private float _linearityOfTime = 0.2f;
        [SerializeField] private Material _baseMaterial;
        [SerializeField] private Material _secondMaterial;
        [SerializeField] private Renderer _renderer;

        private bool _isBaseMaterial = true;

        private void OnEnable()
        {
            _mine.FuseActivated += ActivateFlash;
        }

        private void OnDisable()
        {
            _mine.FuseActivated -= ActivateFlash;
        }

        private void ActivateFlash()
        {
            StartCoroutine(Flash(_mine.DetonationDelay));
            _mine.FuseActivated -= ActivateFlash;
        }

        private IEnumerator Flash(float detonationDelay)
        {
            float elapsedTime = 0;
            float elapsedFlickTime = 0;
            float flickTime = 0;

            while (elapsedTime < detonationDelay)
            {
                elapsedFlickTime += Time.deltaTime;
                elapsedTime += Time.deltaTime;

                if (elapsedFlickTime >= flickTime)
                {
                    InvertMaterial();
                    float lerpedTime = Mathf.InverseLerp(0f, detonationDelay, elapsedTime);
                    flickTime = Mathf.Lerp(_baseFlickRate, _minFlickRate, Mathf.Pow(lerpedTime, _linearityOfTime));
                    elapsedFlickTime = 0;
                }

                yield return null;
            }
        }

        private void InvertMaterial()
        {
            _renderer.material = _isBaseMaterial ? _secondMaterial : _baseMaterial;
            _isBaseMaterial = !_isBaseMaterial;
        }
    }
}