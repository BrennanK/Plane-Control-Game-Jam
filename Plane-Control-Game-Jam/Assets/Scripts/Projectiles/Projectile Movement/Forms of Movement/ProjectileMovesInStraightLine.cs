using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovesInStraightLine : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GeneralProjectileMovementSettings _settings;

    // don't worry about position or rotation when you instantiate a projectile prefab
    public void Initialize(Rigidbody playerRigidbody, Transform player, Transform target)
    {
        ProjectileMovementProcessor.InitializePosVelRot(playerRigidbody, _rigidbody, player, target, _settings);
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(_rigidbody.mass * _settings.ForwardsAcceleration * _rigidbody.velocity.normalized);
    }
}
