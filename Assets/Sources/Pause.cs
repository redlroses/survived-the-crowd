using Agava.WebUtility;
using UnityEngine;

namespace Sources
{
    public class Pause : MonoBehaviour
    {
        private void OnEnable()
        {
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChangeEvent;
        }

        private void OnDisable()
        {
            WebApplication.InBackgroundChangeEvent -= OnInBackgroundChangeEvent;
        }

        private void OnApplicationFocus(bool pauseStatus)
        {
            PauseTime(!pauseStatus);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            PauseTime(pauseStatus);
        }

        private void OnInBackgroundChangeEvent(bool inBackground)
        {
            PauseTime(inBackground);
        }

        private void PauseTime(bool isPause)
        {
            Time.timeScale = isPause ? 0 : 1;
        }
    }
}