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
    private HashSet<EnemyAI> _alreadyHit = new();

    public void DetectCollision(Collider other)
    {
        if (!_hitMultipleEnemies && _alreadyHit.Count > 0)
            return;

        // to do: get an enemy health script or something, and deal damage instead.
        EnemyAI ai = other.GetComponent<EnemyAI>();
        if (ai == null)
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

