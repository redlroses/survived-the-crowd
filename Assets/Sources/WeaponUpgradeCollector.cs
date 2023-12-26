using System;
using Sources.Data;
using Sources.Level;
using Sources.Player.Factory;
using Sources.Saves;
using Sources.Score;
using Sources.StaticData;
using UnityEngine;

namespace Sources
{
    public class WeaponUpgradeCollector : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private LoseDetector _loseDetector;
        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private WeaponUnlockStaticData _unlockData;
        private WeaponId _currentUnlockedWeapon;

        public event Action<WeaponId> NewWeaponUnlocked;

        private void OnEnable()
        {
            _loseDetector.Losed += OnLosed;
        }

        private void OnDisable()
        {
            _loseDetector.Losed += OnLosed;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            _currentUnlockedWeapon = progress.LastUnlockedWeapon;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            progress.LastChosenWeapon = _currentUnlockedWeapon;
        }

        private void OnLosed()
        {
            int score = _scoreCounter.CalculateScore();
            int weaponId = (int)_currentUnlockedWeapon;

            for (int i = 0; i < _unlockData.ScorePerUpdrades.Length; i++)
            {
                if (score >= _unlockData.ScorePerUpdrades[i])
                {
                    weaponId = i;
                }
            }

            if (weaponId > (int)_currentUnlockedWeapon)
            {
                _currentUnlockedWeapon = (WeaponId)weaponId;
                NewWeaponUnlocked?.Invoke(_currentUnlockedWeapon);
            }
        }
    }
}