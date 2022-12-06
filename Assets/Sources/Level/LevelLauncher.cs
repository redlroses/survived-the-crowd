using Import.Joystick.Scripts;
using Sources.Enemy;
using Sources.Fuel;
using Sources.Input;
using Sources.Vehicle;
using UnityEngine;

namespace Sources.Level
{
    public sealed class LevelLauncher : MonoBehaviour
    {
        [SerializeField] private LoseDetector _loseDetector;
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private FuelBarrelFactory _fuelFactory;
        [SerializeField] private PlayerInput _input;
        [SerializeField] private Car _car;
        [SerializeField] private Joystick _joystick;

        private void OnEnable()
        {
            _loseDetector.Lose += OnLose;
        }

        private void OnDisable()
        {
            _loseDetector.Lose -= OnLose;
        }

        public void Run()
        {
            Debug.Log(name + " Run level");
            _enemyFactory.Run();
            _fuelFactory.Run();
            _joystick.gameObject.SetActive(true);
            _car.StartEngine();
            _input.Activate();
        }

        public void Restart()
        {
            Debug.Log(name + " Restart level");
            _enemyFactory.KillAll();
            _fuelFactory.DisableAll();
            _fuelFactory.Stop();
        }

        private void OnLose()
        {
            _joystick.gameObject.SetActive(false);
            _input.Deactivate();
            _car.StopEngine();
        }
    }
}
