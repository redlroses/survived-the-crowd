using Import.Joystick.Scripts;
using QFSW.QC;
using Sources.Enemy;
using Sources.Fuel;
using Sources.Player;
using UnityEngine;

namespace Sources.Level
{
    public sealed class LevelLauncher : MonoBehaviour
    {
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private FuelBarrelFactory _fuelFactory;
        [SerializeField] private PlayerMover _mover;
        [SerializeField] private Joystick _joystick;

        public void Run()
        {
            Debug.Log(name + "Run level");
            _enemyFactory.Run();
            _fuelFactory.Run();
            _joystick.gameObject.SetActive(true);
        }

        public void Restart()
        {
            Debug.Log(name + "Restart level");
            _enemyFactory.KillAll();
            _fuelFactory.DisableAll();
            _fuelFactory.Stop();
            _mover.gameObject.SetActive(true);
            _joystick.gameObject.SetActive(false);
        }
    }
}
