using System;
using Sources.Level;
using Sources.Player.Factory;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Ui.Wrapper.Screens
{
    public class SetupScreen : Screen
    {
        [SerializeField] private Button _applyButton;
        [SerializeField] private GameObject _lockedMark;
        [SerializeField] private UnlockProvider _unlockProvider;
        [SerializeField] private PlayerFactory _playerFactory;

        protected PlayerFactory PlayerFactory => _playerFactory;
        protected UnlockProvider UnlockProvider => _unlockProvider;

        protected int LastUnlockedId;

        protected virtual void OnEnable()
        {
            UnlockProvider.Updated += UpdateUnlock;
        }

        protected virtual void OnDisable()
        {
            UnlockProvider.Updated -= UpdateUnlock;
        }

        protected virtual void UpdateUnlock()
        {
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
    }
}
