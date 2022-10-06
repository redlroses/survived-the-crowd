using System.Collections.Generic;
using Turret;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
public sealed class ShootView : MonoBehaviour
{
    [SerializeField] private Shooter _shooter;
    [SerializeField] private ParticleSystem[] _shootEffects;

    private void Awake()
    {
        if (_shooter == null)
        {
            _shooter = GetComponent<Shooter>();
        }
    }

    private void OnEnable()
    {
        _shooter.ShotOff += OnShotOff;
    }

    private void OnDisable()
    {
        _shooter.ShotOff += OnShotOff;
    }

    private void OnShotOff(int shootPointIndex)
    {
        _shootEffects[shootPointIndex].Play();
    }
}
