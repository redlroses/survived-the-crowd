using QFSW.QC;
using Sources.Player;
using Sources.Vehicle;
using UnityEngine;

namespace Sources.CheatConsole
{
    public class PlayerHealthCommands : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private GasTank _gasTank;

        [Command]
        private void HealCar(int value)
        {
            _playerHealth.Heal(value);
        }

        [Command]
        private void RefuelGasTank(float value)
        {
            _gasTank.Refuel(value);
        }
    }
}