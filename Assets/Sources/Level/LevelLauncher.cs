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


        [SerializeField] private Vector3 _starPlayerPosition;
        [SerializeField] private Vector3 _starPlayerRotation;

        private void OnEnable()
        {
            _loseDetector.Lose += OnLose;
        }

        private void OnDisable()
        {
            _loseDetector.Lose -= OnLose;
        }

        public void Init(Car car)
        {
            _car = car;
        }

        public void Run()
        {
            Debug.Log(name + " Run level");
            _enemyFactory.Run();
            _fuelFactory.Run();
            _input.Activate();
            _loseDetector.enabled = true;
        }

        public void Restart()
        {
            Debug.Log(name + " Restart level");
            _enemyFactory.KillAll();
            _fuelFactory.DisableAll();
            _fuelFactory.Stop();
            _input.gameObject.SetActive(false);
            _input.gameObject.SetActive(true);
            _input.transform.position = _starPlayerPosition;
            _input.transform.rotation = Quaternion.Euler(_starPlayerRotation);
            _loseDetector.enabled = false;
        }

        private void OnLose()
        {
            _input.Deactivate();
        }
    }
}
