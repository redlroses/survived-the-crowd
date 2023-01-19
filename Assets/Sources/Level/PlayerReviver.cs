using Sources.Turret;
using Sources.Vehicle;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Level
{
    public class PlayerReviver : MonoBehaviour
    {
        [SerializeField] private float _enemyKillRadius;
        [SerializeField] private LayerMask _enemyMask;
        [SerializeField] private Button _revivalButton;

        private Weapon _playerWeapon;
        private Car _playerCar;
        private bool _isRevived;

        public void Init(Car playerCar)
        {
            _playerCar = playerCar;
        }

        public void Init(Weapon playerWeapon)
        {
            _playerWeapon = playerWeapon;
        }

        public void Revive()
        {
            if (_isRevived)
            {
                return;
            }

            _playerWeapon.TargetSeeker.OnRevived();
            _revivalButton.interactable = false;
            _isRevived = true;
            KillAroundEnemies();
            ResetHealth();
            ResetFuel();
        }

        public void Reset()
        {
            _isRevived = false;
            _revivalButton.interactable = true;
        }

        private void ResetHealth()
        {
            _playerCar.Health.Heal(_playerCar.Health.Max);
        }

        private void ResetFuel()
        {
            _playerCar.GasTank.Refuel(_playerCar.GasTank.MaxFuelLevel);
        }

        private void KillAroundEnemies()
        {
            Collider[] enemies = Physics.OverlapSphere(_playerCar.transform.position, _enemyKillRadius, _enemyMask);

            foreach (var enemy in enemies)
            {
                enemy.GetComponent<Enemy.Enemy>().Health.Damage(int.MaxValue);
            }
        }
    }
}
