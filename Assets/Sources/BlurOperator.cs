using DG.Tweening;
using UnityEngine;

namespace Sources
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    internal class BlurOperator : MonoBehaviour
    {
        private static readonly int Param = Shader.PropertyToID("_Param");

        [Tooltip("The BlurOperator Material created using the BlurOperator Script.")]
        public Material _blurMaterial;

        [Tooltip("The BlurOperator Shader attahed with this Asset.")]
        public Shader _blurShader;

        [Tooltip("Improves quality and reduces sharp edges (Downsamples the Image).")] [Range(0, 3)]
        public int _filter = 1;

        [Tooltip("Iterations of BlurOperator Calculation.")] [Range(0, 6)]
        public int _qualityIterations = 2;

        [Tooltip("Strength of the BlurOperator. Governs the Area of Pixels to Blend.")] [Range(0f, 10f)]
        public float _radius = 3.0f;
        private Tweener _disableBlurTween;

        private Tweener _enableBlurTween;

        private void Awake()
        {
            _enableBlurTween = DOTween.To(x => _radius = x, 0, 3f, 1.25f);
            _disableBlurTween = DOTween.To(x => _radius = x, 3f, 0, 1.25f).OnComplete(() => enabled = false);
        }

        private void OnRenderImage(RenderTexture sourcert, RenderTexture destinationrt)
        {
            if (_blurShader == null)
            {
                return;
            }

            float widthModification = 1.0f / (1.0f * (1 << _filter));

            _blurMaterial.SetVector(
                Param,
                new Vector4(_radius * widthModification, -_radius * widthModification, 0f, 0f));

            sourcert.filterMode = FilterMode.Bilinear;

            int rendertextureWidth = sourcert.width >> _filter;
            int rendertextureHeight = sourcert.height >> _filter;

            RenderTexture rendertexture =
                RenderTexture.GetTemporary(rendertextureWidth, rendertextureHeight, 0, sourcert.format);

            rendertexture.filterMode = FilterMode.Bilinear;
            Graphics.Blit(sourcert, rendertexture, _blurMaterial, 0);

            for (int i = 0; i < _qualityIterations; i++)
            {
                float iterationOffset = i * 1.0f;

                _blurMaterial.SetVector(
                    Param,
                    new Vector4(
                        _radius * widthModification + iterationOffset,
                        -_radius * widthModification - iterationOffset,
                        0.0f,
                        0.0f));

                RenderTexture rendertexturetemp =
                    RenderTexture.GetTemporary(rendertextureWidth, rendertextureHeight, 0, sourcert.format);

                rendertexturetemp.filterMode = FilterMode.Bilinear;
                Graphics.Blit(rendertexture, rendertexturetemp, _blurMaterial, 1);
                RenderTexture.ReleaseTemporary(rendertexture);
                rendertexture = rendertexturetemp;

                rendertexturetemp = RenderTexture.GetTemporary(
                    rendertextureWidth,
                    rendertextureHeight,
                    0,
                    sourcert.format);

                rendertexturetemp.filterMode = FilterMode.Bilinear;
                Graphics.Blit(rendertexture, rendertexturetemp, _blurMaterial, 2);
                RenderTexture.ReleaseTemporary(rendertexture);
                rendertexture = rendertexturetemp;
            }

            Graphics.Blit(rendertexture, destinationrt);
            RenderTexture.ReleaseTemporary(rendertexture);
        }

        public void Enable()
        {
            enabled = true;
            _disableBlurTween.Pause();
            _enableBlurTween.Restart();
        }

        public void Disable()
        {
            _enableBlurTween.Pause();
            _disableBlurTween.Restart();
        }
    }
}