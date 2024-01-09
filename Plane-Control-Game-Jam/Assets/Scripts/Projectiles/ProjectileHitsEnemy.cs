using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We might have multiple colliders on each projectile or on each enemy, so it could collide multiple times.

public class ProjectileHitsEnemy : MonoBehaviour
{
    [SerializeField] private int _damageWithLowestPlayerStat;
    [SerializeField] private float _knockback;

    private bool _alreadyHit;

    public void DetectCollision(Collider other)
    {
        if (_alreadyHit)
            return;

        // to do: get an enemy health script or something, and deal damage instead.
        EnemyAI ai = other.GetComponent<EnemyAI>();
        if (ai == null)
            return;

        // to do: multiply the damage based on player stats
        ai.takeDamage(_damageWithLowestPlayerStat, transform, _knockback);

        // destroy the projectile, and prevent hitting more times (since destroy only
        // happens at end of the frame)
        _alreadyHit = true;
        Destroy(gameObject);
    }
}
