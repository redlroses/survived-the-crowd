using System.Collections;
using System.Collections.Generic;
using Pool;
using UnityEngine;

namespace Fuel
{
    public sealed class FuelBarrelFactory : MonoBehaviour
    {
        [SerializeField] private float _spawnDelay = 30f;
        [SerializeField] private FuelBarrelPool _pool;
        [SerializeField] private Transform[] _spawnPoint;

        private IEnumerator Spawning()
        {
            
            yield return null;
        }
    }
}
