using System.Collections;
using System.Collections.Generic;
using Sources.Level;
using UnityEngine;
using UnityEngine.UI;
using Screen = Sources.Ui.Wrapper.Screens.Screen;

namespace Sources.Ui.Animations
{
    public class KeysAnimation : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private Screen _gameScreen;
        [SerializeField] private Image _image;
        [SerializeField] private List<Sprite> _keys;
        [SerializeField] private LevelLauncher _levelLauncher;
        [SerializeField] private Screen _startScreen;

        private bool _isAnimate;
        private Coroutine _routine;
        private WaitForSeconds _waitForNextSprite;

        private void Awake()
        {
            _waitForNextSprite = new WaitForSeconds(_duration);
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKey(KeyCode.A) || UnityEngine.Input.GetKey(KeyCode.W)
                                                    || UnityEngine.Input.GetKey(KeyCode.D))
            {
                _levelLauncher.Run();
                _startScreen.Hide(false);
                _gameScreen.Show(false);
                gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            StartAnimation();
        }

        private void OnDisable()
        {
            StopAnimation();
        }

        private void StartAnimation()
        {
            _isAnimate = true;
            _routine ??= StartCoroutine(ChangeSprites());
        }

        private void StopAnimation()
        {
            if (_routine == null)
            {
                return;
            }

            StopCoroutine(_routine);
            _isAnimate = false;
            _routine = null;
        }

        private IEnumerator ChangeSprites()
        {
            while (_isAnimate)
            {
                foreach (Sprite key in _keys)
                {
                    yield return _waitForNextSprite;

                    _image.sprite = key;
                }
            }
        }
    }
}