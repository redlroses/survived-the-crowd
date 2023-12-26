using UnityEngine;
using UnityEngine.UI;

namespace Sources.Ui
{
    public class LogInButton : MonoBehaviour
    {
        [SerializeField] private Button _logIn;

        private void OnEnable()
        {
#if !UNITY_EDITOR
            if (PlayerAccount.IsAuthorized)
            {
                _logIn.gameObject.SetActive(false);
            }
#endif
        }
    }
}