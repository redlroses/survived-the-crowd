using Sources.Audio;
using Sources.Input;
using Sources.Timer;
using Sources.Turret;
using Sources.Ui.Wrapper.Screens;
using Sources.Vehicle;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Level
{
    public class PlayerReviver : MonoBehaviour
    {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private TimerView _timerView;
        [SerializeField] private AudioMixerOperator _audioMixerOperator;
        [SerializeField] private float _enemyKillRadius;
        [SerializeField] private LayerMask _enemyMask;
        [SerializeField] private Button _revivalButton;
        [SerializeField] private LoseScreen _loseScreen;
        [SerializeField] private GameScreen _gameScreen;

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

            Time.timeScale = 0;
#if !UNITY_EDITOR
            Agava.YandexGames.VideoAd.Show(() =>
                {
                    _audioMixerOperator.SetMaster(false);
                },
                () =>
                {
                    KillAroundEnemies();
                    _playerWeapon.TargetSeeker.OnRevived();
                    _revivalButton.interactable = false;
                    _isRevived = true;
                    ResetFuel();
                    _timerView.StartCountTime();
                    _audioMixerOperator.SetMaster(true);
                    _loseScreen.Hide(false);
                    _gameScreen.Show(false);
                    _input.Activate();
                    Invoke(nameof(ResetHealth), 0.25f);
                    Time.timeScale = 1;
                },
                () =>
                    _audioMixerOperator.SetMaster(true));
#endif
#if UNITY_EDITOR
            KillAroundEnemies();
            _playerWeapon.TargetSeeker.OnRevived();
            _revivalButton.interactable = false;
            _isRevived = true;
            ResetFuel();
            _timerView.StartCountTime();
            _audioMixerOperator.SetMaster(true);
            _loseScreen.Hide(false);
            _gameScreen.Show(false);
            _input.Activate();
            Invoke(nameof(ResetHealth), 0.25f);
#endif
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