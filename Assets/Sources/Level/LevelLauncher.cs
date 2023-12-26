using Import.Joystick.Scripts;
using Sources.Collectables;
using Sources.Enemy;
using Sources.Fuel;
using Sources.Input;
using Sources.Pool;
using UnityEngine;

namespace Sources.Level
{
    public sealed class LevelLauncher : MonoBehaviour
    {
        [SerializeField] private BlurOperator _blurOperator;
        [SerializeField] private CarDetailsFactory _carDetailsFactory;
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private FuelBarrelFactory _fuelFactory;
        [SerializeField] private GateAnimation _gateAnimation;
        [SerializeField] private InputProvider _input;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private LoseDetector _loseDetector;
        [SerializeField] private PlayerReviver _playerReviver;
        [SerializeField] private Vector3 _starPlayerPosition;
        [SerializeField] private Vector3 _starPlayerRotation;

        private void OnEnable()
        {
            _loseDetector.Losed += OnLosed;
        }

        private void OnDisable()
        {
            _loseDetector.Losed -= OnLosed;
        }

        public void Run()
        {
            _enemyFactory.Run();
            _fuelFactory.Run();
            _carDetailsFactory.Run();
            _input.Input.Activate();
            _gateAnimation.Open();
            _loseDetector.enabled = true;
            _playerReviver.Reset();
        }

        public void Restart()
        {
            _enemyFactory.KillAll();
            _enemyFactory.Stop();
            _fuelFactory.DisableAll();
            _fuelFactory.Stop();
            _carDetailsFactory.DisableAll();
            _carDetailsFactory.Stop();
            _input.gameObject.SetActive(false);
            _input.gameObject.SetActive(true);
            _input.transform.position = _starPlayerPosition;
            _input.transform.rotation = Quaternion.Euler(_starPlayerRotation);
            _loseDetector.enabled = false;
            _gateAnimation.Reset();
            _enemyPool.ResetDeadEnemyCounter();
        }

        private void OnLosed()
        {
            _input.Input.Deactivate();
            _joystick.Disable();
            _blurOperator.Enable();
        }
    }
}