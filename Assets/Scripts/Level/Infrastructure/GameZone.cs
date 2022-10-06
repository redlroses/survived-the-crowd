﻿using System;
using UnityEngine;

namespace Level.Infrastructure
{
    public abstract class GameZone : MonoBehaviour
    {
        public event Action Left;
        public event Action Reached;

        private void OnTriggerEnter(Collider other)
        {
            if (CheckIsNotPlayer(other))
            {
                return;
            }

            Left?.Invoke();
            OnPlayerEnter();
        }

        private void OnTriggerExit(Collider other)
        {
            if (CheckIsNotPlayer(other))
            {
                return;
            }

            Reached?.Invoke();
            OnPlayerExit();
        }

        protected abstract void OnPlayerEnter();
        protected abstract void OnPlayerExit();

        private bool CheckIsNotPlayer(Collider other)
        {
            return other.TryGetComponent(out Player _);
        }
    }
}