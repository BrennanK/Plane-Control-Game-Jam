using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectileParticleEffects
{
    [Header("Particle Effects")]
    [SerializeField] private GameObject _particleEffects;
    [SerializeField] private int _minParticleInstancesCount = 1;
    [SerializeField] private int _maxParticleInstancesCount = 1;
    [SerializeField] private float _particlesSpawnRadius;
    [SerializeField] private bool _randomizeParticleRotation;
    [SerializeField] private bool _spawnParticlesAtEnemyNotProjectile;
    [SerializeField] private bool _includeOneWithNoSpawnRadius;


    public void SpawnParticles(Transform projectile, Transform enemy)
    {
        Transform spawnAround = _spawnParticlesAtEnemyNotProjectile ? enemy : projectile;

        int count = Random.Range(_minParticleInstancesCount, _maxParticleInstancesCount + 1);
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = spawnAround.position + _particlesSpawnRadius * Random.insideUnitSphere;
            if (_includeOneWithNoSpawnRadius && i == 0)
                pos = spawnAround.position;

            Quaternion rotation = spawnAround.rotation;
            if (_randomizeParticleRotation)
                rotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);

            GameObject instantiated = Object.Instantiate(_particleEffects, pos, rotation);
            Object.Destroy(instantiated, 10);
        }
    }
}
