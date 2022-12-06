using System;
using QFSW.QC;
using Sources.HealthLogic;
using Sources.Ui.Wrapper.Screens;
using Sources.Vehicle;
using UnityEngine;

namespace Sources.Level
{
    public sealed class LoseDetector : MonoBehaviour
    {
        private const string RanOutOfFuel = "Ran out of fuel";
        private const string CarIsBroken = "Car is broken";

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
            Lose?.Invoke();
            _loseScreen.Show(true);
        }
    }
}
