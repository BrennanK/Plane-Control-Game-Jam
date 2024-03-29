using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon
{
    private Transform _player;
    private Rigidbody _playerRigidbody;
    private PlayerWeaponSettings _settings;
    private float _lastFireTime = float.NegativeInfinity;
    private bool _stopAfterFiredOnce;
    private float _addToFireInterval;

    public PlayerWeapon(Rigidbody playerRigidbody, PlayerWeaponSettings settings)
    {
        _player = playerRigidbody.transform;
        _playerRigidbody = playerRigidbody;
        _settings = settings;
        RandomizeAddToInterval();
    }

    public void Tick(bool onlyFireOnce)
    {
        if (_stopAfterFiredOnce)
            return;

        if (Time.time > _lastFireTime + _settings.FireInterval + _addToFireInterval)
        {
            bool fired = TryFire();
            if (fired && onlyFireOnce)
                _stopAfterFiredOnce = true;
            if (fired || !_settings.WaitForEnemyBeforeFinishFireInterval)
            {
                _lastFireTime = Time.time;
                RandomizeAddToInterval();
            }
        }
    }

    private void RandomizeAddToInterval()
    {
        _addToFireInterval = Random.Range(0, _settings.MaxRandomAdditionToFireInterval);
    }

    private bool TryFire()
    {
        Transform target = null;
        if (_settings.RequireTarget)
        {
            target = FindTarget();
            if (target == null)
                return false;
        }

        GameObject projectile = Object.Instantiate(_settings.ProjectilePrefab);

        if (projectile.TryGetComponent(out ProjectileMovesDirectlyToEnemy basicHoming))
        {
            basicHoming.Initialize(_playerRigidbody, _player, target, target.GetComponent<UnityEngine.AI.NavMeshAgent>());
        }
        else if (projectile.TryGetComponent(out ProjectileMovesLikeBoomerang boomerang))
        {
            boomerang.Initialize(_playerRigidbody, target);
        }
        else if (projectile.TryGetComponent(out ExplosionPositionInitializer explosion))
        {
            explosion.Initialize(_player);
        }
        else if (projectile.TryGetComponent(out MeleePositionInitializer melee))
        {
            melee.Initialize(_player, target);
        }
        else
        {
            throw new System.Exception("Implement initialization for the projectile prefab.");
        }

        return true;
    }

    // Find the closest enemy which the player is facing.
    // (Not necessarily looking straight at it, but within a maximum angle.)
    // Also, only if it's close enough.
    private Transform FindTarget() 
    {
        float sqrDetectionRadiusSetting = _settings.EnemyDetectionRadius * _settings.EnemyDetectionRadius;
        Vector3 forward3D = _player.forward;
        Vector2 forward = new Vector2(forward3D.x, forward3D.z);

        Transform result = null;
        float closestSqrDistance = float.PositiveInfinity;
        foreach (EnemyAI nextEnemy in EnemyAI.All)
        {
            Vector3 offset3D = nextEnemy.transform.position - _player.position;
            Vector2 offset2D = new Vector2(offset3D.x, offset3D.z);
            offset2D = ProjectileMovementProcessor.RotateVector(offset2D, _settings.EnemyDetectionAngleOffset * Mathf.Deg2Rad);

            float sqrDistance = offset2D.sqrMagnitude;

            if (sqrDistance > sqrDetectionRadiusSetting)
                continue;
            if (sqrDistance < closestSqrDistance)
            {
                float angle = Vector2.Angle(forward, offset2D);
                if (angle < _settings.EnemyDetectionMaxDegreesFromLookDirection)
                {
                    closestSqrDistance = sqrDistance;
                    result = nextEnemy.transform;
                }
            }
        }

        return result;
    }

    public PlayerWeaponSettings getSettings()
    {
        return _settings;
    }
}
