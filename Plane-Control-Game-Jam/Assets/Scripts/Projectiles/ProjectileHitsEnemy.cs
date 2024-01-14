using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We might have multiple colliders on each projectile or on each enemy, so it could collide multiple times.

public class ProjectileHitsEnemy : MonoBehaviour
{


    [SerializeField] private bool _hitMultipleEnemies;
    [SerializeField] private bool _canHitSameEnemyMultipleTimes;
    [SerializeField] private int _damageWithLowestPlayerStat;
    [SerializeField] private int _originalDamage;
    [SerializeField] private float _knockbackScale = 1f;
    [Header("Particle Effects")]
    [SerializeField] private GameObject _particleEffects;
    [SerializeField] private int _minParticleInstancesCount = 1;
    [SerializeField] private int _maxParticleInstancesCount = 1;
    [SerializeField] private float _particlesSpawnRadius;
    [SerializeField] private bool _randomizeParticleRotation;
    [SerializeField] private bool _spawnParticlesAtEnemyNotProjectile;
    [SerializeField] private bool _includeOneWithNoSpawnRadius;


    private HashSet<EnemyAI> _alreadyHit = new();

    public void DetectCollision(Collider other)
    {
        if (!_hitMultipleEnemies && _alreadyHit.Count > 0)
            return;

        if (!EnemyHitbox.TryGetAI(other, out EnemyAI ai))
            return;
        if (!_canHitSameEnemyMultipleTimes && _alreadyHit.Contains(ai))
            return;

        Hit(ai);

        _alreadyHit.Add(ai);
        if (!_hitMultipleEnemies)
            Destroy(gameObject);
    }
    private void Hit(EnemyAI ai)
    {
        if (_particleEffects != null)
            SpawnParticles(ai.transform);

        // to do: multiply the damage based on player stats
        ai.takeDamage(_damageWithLowestPlayerStat, _knockbackScale, transform.position);
    }

    private void SpawnParticles(Transform enemy)
    {
        Transform spawnAround = _spawnParticlesAtEnemyNotProjectile ? enemy : transform;

        int count = Random.Range(_minParticleInstancesCount, _maxParticleInstancesCount + 1);
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = spawnAround.position + _particlesSpawnRadius * Random.insideUnitSphere;
            if (_includeOneWithNoSpawnRadius && i == 0)
                pos = spawnAround.position;

            Quaternion rotation = spawnAround.rotation;
            if (_randomizeParticleRotation)
                rotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);

            GameObject instantiated = Instantiate(_particleEffects, pos, rotation);
            Destroy(instantiated, 5);
        }
    }


    public int getDamageDealt()
    {
        return _damageWithLowestPlayerStat;
    }

    public void setDamageDealt(int amountToAdd)
    {
        _damageWithLowestPlayerStat = amountToAdd;
    }

    public void resetToOriginalDamage()
    {
        _damageWithLowestPlayerStat = _originalDamage;
    }
}

