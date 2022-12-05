using Import.Joystick.Scripts;
using Sources.Enemy;
using Sources.Fuel;
using Sources.Input;
using Sources.Player;
using UnityEngine;

namespace Sources.Level
{
    public sealed class LevelLauncher : MonoBehaviour
    {
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private FuelBarrelFactory _fuelFactory;
        [SerializeField] private PlayerInput _inputService;
        [SerializeField] private PlayerMover _mover;
        [SerializeField] private Joystick _joystick;

        public void Run()
        {
            Debug.Log(name + "Run level");
            _enemyFactory.Run();
            _fuelFactory.Run();
            // _inputService.Activate();
            _joystick.gameObject.SetActive(true);
        }

        public void Restart()
        {
            Debug.Log(name + "Restart level");
            _enemyFactory.KillAll();
            _fuelFactory.DisableAll();
            _fuelFactory.Stop();
            // _inputService.Deactivate();
            _mover.gameObject.SetActive(true);
            // _mover.ResetPosition();
            _joystick.gameObject.SetActive(false);
        }
    }
}
