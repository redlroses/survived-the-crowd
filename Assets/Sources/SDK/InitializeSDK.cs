using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.SDK
{
    public class InitializeSDK : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(Init());
        }

#if !CRAZY_GAMES
        private IEnumerator Init()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            yield return new WaitForSeconds(0.1f);
#else
        while(Agava.YandexGames.YandexGamesSdk.IsInitialized == false)
        {
            yield return Agava.YandexGames.YandexGamesSdk.Initialize();
        }
#endif
            // GameAnalyticsSDK.GameAnalytics.Initialize();
            SceneManager.LoadScene(1);
            yield return null;
        }
#endif
    }
}