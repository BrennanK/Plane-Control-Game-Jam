using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProjectileMovesDirectlyToEnemy : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private GeneralProjectileMovementSettings _settings;
    [SerializeField] private float _turningAccelerationScale = 2.5f;
    [SerializeField] private float _maxTurningAcceleration = float.PositiveInfinity;

    private NavMeshAgent _targetNav;
    private Transform _target;

    // don't worry about position or rotation when you instantiate a projectile prefab
    public void Initialize(Rigidbody playerRigidbody, Transform player, Transform target, NavMeshAgent targetNav)
    {
        _target = target;
        _targetNav = targetNav;
        ProjectileMovementProcessor.InitializePosVelRot(playerRigidbody, _rigidbody, player, target, _settings);
    }

    private void FixedUpdate()
    {
        Vector3 acceleration = Vector3.zero;

        // rotate velocity towards the target (technically a collision course with the target)
        if (_target != null)
        {
            Vector3 relPos3D = _target.position - _rigidbody.transform.position;
            Vector3 relVel3D = _targetNav.velocity - _rigidbody.velocity;
            Vector2 relPos = new Vector2(relPos3D.x, relPos3D.z);
            Vector2 relVel = new Vector2(relVel3D.x, relVel3D.z);
            Vector2 propNav = ProportionalNavigationAccel(relPos, relVel);

            if (propNav.sqrMagnitude > _maxTurningAcceleration * _maxTurningAcceleration)
                propNav = propNav.normalized * _maxTurningAcceleration;

            Vector3 propNav3D = new Vector3(propNav.x, 0, propNav.y);
            acceleration += propNav3D;
        }

        // accelerate forwards
        Vector3 rotatedVelocity = _rigidbody.velocity + acceleration * Time.fixedDeltaTime;
        acceleration += _settings.ForwardsAcceleration * rotatedVelocity.normalized;

        _rigidbody.AddForce(_rigidbody.mass * acceleration);

        // Make the projectile point in the direction of its velocity.
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, rotatedVelocity);
        _rigidbody.MoveRotation(rotation);
    }

    private Vector2 ProportionalNavigationAccel(Vector2 relPos, Vector2 relVel)
    {
        // This is used irl to guide things towards collisions, e.g. for missiles.
        // It only accelerates perpendicularly to the velocity.
        float k = _turningAccelerationScale * (relPos.x * relVel.y - relPos.y * relVel.x) / relPos.sqrMagnitude;
        return new Vector2(k * relVel.y, k * -relVel.x);
    }
}
