using System;
using Sources.HealthLogic;
using Sources.Player;
using Sources.Ui.Wrapper.Screens;
using Sources.Vehicle;
using UnityEngine;

namespace Sources.Level
{
    public sealed class LoseDetector : MonoBehaviour
    {
        private const string DefeatCauseCar = "Defeat Cause Car";
        private const string DefeatCauseFuel = "Defeat Cause Fuel";
        private const string PunishedByTheGods = "Punished by the gods";

        [SerializeField] [RequireInterface(typeof(IHealth))] private MonoBehaviour _playerHealth;
        [SerializeField] private GasTank _gasTank;
        [SerializeField] private LoseScreen _loseScreen;

        public event Action Lose;

        private IHealth PlayerHealth => (IHealth) _playerHealth;

        private void OnEnable()
        {
            PlayerHealth.Empty += OnEmptyPlayerHealth;
            _gasTank.Empty += OnEmptyGasTank;
        }

        private void OnDisable()
        {
            PlayerHealth.Empty -= OnEmptyPlayerHealth;
            _gasTank.Empty -= OnEmptyGasTank;
        }

        public void Init(GasTank gasTank, PlayerHealth playerHealth)
        {
            _gasTank = gasTank;
            _playerHealth = playerHealth;
        }

        public void RestartManual()
        {
            _loseScreen.SetLoseCause(PunishedByTheGods);
            ShowLoseScreen();
        }

        private void OnEmptyGasTank()
        {
            if (_loseScreen.IsActive)
            {
                return;
            }

            _loseScreen.SetLoseCause(Lean.Localization.LeanLocalization.GetTranslationText(DefeatCauseFuel));
            ShowLoseScreen();
        }

        private void OnEmptyPlayerHealth()
        {
            if (_loseScreen.IsActive)
            {
                return;
            }

            _loseScreen.SetLoseCause(Lean.Localization.LeanLocalization.GetTranslationText(DefeatCauseCar));
            ShowLoseScreen();
        }

        private void ShowAd()
        {
            Agava.YandexGames.InterstitialAd.Show();
        }

        private void ShowLoseScreen()
        {
            Lose?.Invoke();
            _loseScreen.Show(true);

#if !UNITY_EDITOR
            Invoke(nameof(ShowAd), 1f);
#endif
        }
    }
}
