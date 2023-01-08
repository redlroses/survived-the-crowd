using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.SDK
{
    public class InitializeSDK : MonoBehaviour
    {
        public event Action Initialized;

        private void Awake()
        {
#if !CRAZY_GAMES
            StartCoroutine(Init());
#endif

#if CRAZY_GAMES
        SceneManager.LoadScene(1);
#endif
        }

#if !CRAZY_GAMES
        private IEnumerator Init()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield return new WaitForSeconds(0.1f);

#elif YANDEX_GAMES
        while(Agava.YandexGames.YandexGamesSdk.IsInitialized == false)
        {
            yield return Agava.YandexGames.YandexGamesSdk.Initialize();
        }

#elif VK_GAMES
        while (Agava.VKGames.VKGamesSdk.Initialized == false)
        {
            yield return Agava.VKGames.VKGamesSdk.Initialize();
        }
#endif
            // GameAnalyticsSDK.GameAnalytics.Initialize();
            SceneManager.LoadScene(1);
        }
#endif
    }
}