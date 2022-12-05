using QFSW.QC;
using Sources.HealthLogic;
using UnityEngine;

namespace Sources.Level
{
    public sealed class LoseDetector : MonoBehaviour
    {
        [SerializeField] [RequireInterface(typeof(IHealth))] private MonoBehaviour _playerHealth;
        [SerializeField] private GameObject _playerObject;
        [SerializeField] private GameObject _loseScreen;

        private IHealth PlayerHealth => (IHealth) _playerHealth;

        private void OnEnable()
        {
            PlayerHealth.Empty += OnEmptyPlayerHealth;
        }

        private void OnDisable()
        {
            PlayerHealth.Empty -= OnEmptyPlayerHealth;
        }

        [Command("Restart", "force restart screen")]
        private void OnEmptyPlayerHealth()
        {
            _playerObject.SetActive(false);
            _loseScreen.SetActive(true);
        }
    }
}
