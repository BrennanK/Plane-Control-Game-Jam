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
    [SerializeField] private ProjectileParticleEffects[] _particles;


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
        for (int i = 0; i < _particles.Length; i++)
            _particles[i].SpawnParticles(transform, ai.transform);

        // to do: multiply the damage based on player stats
        ai.takeDamage(_damageWithLowestPlayerStat, _knockbackScale, transform.position);
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

