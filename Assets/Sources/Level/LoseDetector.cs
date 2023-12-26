using System;
using Agava.YandexGames;
using Lean.Localization;
using Sources.Audio;
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

        [SerializeField] private AudioMixerOperator _audioMix;

        [SerializeField] private GasTank _gasTank;
        [SerializeField] private LoseScreen _loseScreen;

        [SerializeField] [RequireInterface(typeof(IHealth))]
        private MonoBehaviour _playerHealth;

        public event Action Losed;

        private IHealth PlayerHealth => (IHealth)_playerHealth;

        private void OnEnable()
        {
            PlayerHealth.Ended += OnEndedPlayerHealth;
            _gasTank.Ended += OnEndedGasTank;
        }

        private void OnDisable()
        {
            PlayerHealth.Ended -= OnEndedPlayerHealth;
            _gasTank.Ended -= OnEndedGasTank;
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

        private void OnEndedGasTank()
        {
            if (_loseScreen.IsActive)
            {
                return;
            }

            _loseScreen.SetLoseCause(LeanLocalization.GetTranslationText(DefeatCauseFuel));
            ShowLoseScreen();
        }

        private void OnEndedPlayerHealth()
        {
            if (_loseScreen.IsActive)
            {
                return;
            }

            _loseScreen.SetLoseCause(LeanLocalization.GetTranslationText(DefeatCauseCar));
            ShowLoseScreen();
        }

        private void ShowAd()
        {
            InterstitialAd.Show(
                () => { _audioMix.SetMaster(false); },
                b => { _audioMix.SetMaster(true); });
        }

        private void ShowLoseScreen()
        {
            Losed?.Invoke();
            _loseScreen.Show(true);

#if !UNITY_EDITOR
            Invoke(nameof(ShowAd), 1f);
#endif
        }
    }
}