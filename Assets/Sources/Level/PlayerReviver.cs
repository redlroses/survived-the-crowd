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
        private const float ResetHealthDelay = 0.25f;

        [SerializeField] private AudioMixerOperator _audioMixerOperator;
        [SerializeField] private BlurOperator _blurOperator;
        [SerializeField] private float _enemyKillRadius;
        [SerializeField] private LayerMask _enemyMask;
        [SerializeField] private GameScreen _gameScreen;
        [SerializeField] private InputProvider _input;
        [SerializeField] private LoseScreen _loseScreen;
        [SerializeField] private Button _revivalButton;
        [SerializeField] private TimerView _timerView;

        private bool _isRevived;
        private Car _playerCar;
        private Weapon _playerWeapon;

        public void Reset()
        {
            _isRevived = false;
            _revivalButton.interactable = true;
        }

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
                    _input.Input.Activate();
                    Invoke(nameof(ResetHealth), 0.25f);
                },
                () =>
                {
                    Time.timeScale = 1;
                    _blurOperator.Disable();
                    _audioMixerOperator.SetMaster(true);
                });
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
            _input.Input.Activate();
            Invoke(nameof(ResetHealth), ResetHealthDelay);
            Time.timeScale = 1;
            _blurOperator.Disable();
            _audioMixerOperator.SetMaster(true);
#endif
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

            foreach (Collider enemy in enemies)
            {
                enemy.GetComponent<Enemy.Enemy>().Health.Damage(int.MaxValue);
            }
        }
    }
}