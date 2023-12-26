using Lean.Localization;
using UnityEngine;

namespace Sources.Ui.Wrapper
{
    public class LocalizationSetter : MonoBehaviour
    {
        [SerializeField] private LeanLocalization _leanLocalization;

        private void Start()
        {
#if !UNITY_EDITOR
            PlayerAccount.GetProfileData(result =>
            {
                switch (result.lang)
                {
                    case "ru":
                        OnLanguageChanged((int) Language.Russian);
                        break;

                    case "en":
                        OnLanguageChanged((int) Language.English);
                        break;

                    case "tr":
                        OnLanguageChanged((int) Language.Turkish);
                        break;
                }
            });
#endif
        }

        public void OnLanguageChanged(int index)
        {
            Language language = (Language)index;

            switch (language)
            {
                case Language.Russian:
                    _leanLocalization.SetCurrentLanguage(Language.Russian.ToString());

                    break;

                case Language.English:
                    _leanLocalization.SetCurrentLanguage(Language.English.ToString());

                    break;

                case Language.Turkish:
                    _leanLocalization.SetCurrentLanguage(Language.Turkish.ToString());

                    break;

                default:
                    _leanLocalization.SetCurrentLanguage(Language.English.ToString());

                    break;
            }
        }
    }
}