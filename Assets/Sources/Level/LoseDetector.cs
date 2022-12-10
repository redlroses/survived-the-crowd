using System;
using QFSW.QC;
using Sources.HealthLogic;
using Sources.Player;
using Sources.Ui.Wrapper.Screens;
using Sources.Vehicle;
using UnityEngine;

namespace Sources.Level
{
    public sealed class LoseDetector : MonoBehaviour
    {
        private const string RanOutOfFuel = "Ran out of fuel";
        private const string CarIsBroken = "Car is broken";
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
            _loseScreen.SetLoseCause(RanOutOfFuel);
            ShowLoseScreen();
        }

        [Command("Restart", "force restart screen")]
        private void OnEmptyPlayerHealth()
        {
            _loseScreen.SetLoseCause(CarIsBroken);
            ShowLoseScreen();
        }

        private void ShowLoseScreen()
        {
            if (_loseScreen.IsActive)
            {
                return;
            }

            Lose?.Invoke();
            _loseScreen.Show(true);
        }
    }
}
