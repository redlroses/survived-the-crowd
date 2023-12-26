using Sources.Player.Factory;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Ui.Wrapper.Screens
{
    public class SetupScreen : Screen
    {
        [SerializeField] private Button _applyButton;
        [SerializeField] private GameObject _lockedMark;

        protected int LastUnlockedId;

        [field: SerializeField]
        protected PlayerFactory PlayerFactory { get; }

        [field: SerializeField]
        protected UnlockProvider UnlockProvider { get; }

        protected virtual void OnEnable()
        {
            UnlockProvider.Updated += UpdateUnlock;
        }

        protected virtual void OnDisable()
        {
            UnlockProvider.Updated -= UpdateUnlock;
        }

        protected void OnChanged(int id)
        {
            if (id > LastUnlockedId)
            {
                _applyButton.interactable = false;
                _lockedMark.SetActive(true);
            }
            else
            {
                _applyButton.interactable = true;
                _lockedMark.SetActive(false);
            }
        }

        protected virtual void UpdateUnlock()
        {
        }
    }
}