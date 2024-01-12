using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProjectileMovementProcessor
{
    // Initializes a projectile's position, velocity, and rotation.
    public static void InitializePosVelRot(Rigidbody playerRigidbody, Rigidbody projectileRigidbody
        , Transform player, Transform target
        , GeneralProjectileMovementSettings settings)
    {
        Vector3 posOffsetDirection = PositionOffsetDirection(player, target
            , settings.MaxRandomSpawnDirectionAngleOffset
            , settings.SpawnDirectionAngleOffset
            , settings.SpawnInFrontOfPlayer);

        Vector3 pos = InitialPosition(player, settings.MinSpawnDistanceFromPlayer
            , settings.MaxSpawnDistanceFromPlayer, posOffsetDirection);

        
        Vector3 vel = InitialVelocity(pos, target, settings.MinInitialVelocity, settings.MaxInitialVelocity
            , settings.InitialVelocityIsTowardsEnemyNotAwayFromPlayer, posOffsetDirection, playerRigidbody);

        Quaternion rot = InitialRotation(vel);

        projectileRigidbody.position = pos;
        projectileRigidbody.transform.position = pos;

        projectileRigidbody.velocity = vel;

        projectileRigidbody.rotation = rot;
        projectileRigidbody.transform.rotation = rot;
    }

    private static Vector3 PositionOffsetDirection(Transform source, Transform target
       , float maxRandomOffsetDegrees, float extraOffsetDegrees, bool spawnInFrontOfPlayer)
    {
        Vector3 offsetDirectionBeforeRotate = DirectionToTargetInPlane(source.position, target);
        if (spawnInFrontOfPlayer)
            offsetDirectionBeforeRotate = DirectionInFront(source);

        float offsetAngle = (extraOffsetDegrees + Random.Range(-maxRandomOffsetDegrees, maxRandomOffsetDegrees)) * Mathf.Deg2Rad;
        return RotateVectorOnPlane(offsetDirectionBeforeRotate, offsetAngle);
    }

    private static Vector3 InitialPosition(Transform source
       , float minDistance, float maxDistance, Vector3 positionOffsetDirection)
    {
        return source.position + Random.Range(minDistance, maxDistance) * positionOffsetDirection;
    }

    private static Vector3 InitialVelocity(Vector3 initialPosition, Transform target, float minSpeed, float maxSpeed
        , bool towardsEnemyNotAwayFromPlayer, Vector3 positionOffsetDirection
        , Rigidbody playerRigidbody)
    {
        if (towardsEnemyNotAwayFromPlayer)
        {
            return Random.Range(minSpeed, maxSpeed)
                * DirectionToTargetInPlane(initialPosition, target);
        }
        else
        {
            Vector3 playerVel = playerRigidbody.velocity;
            playerVel.y = 0;
            return playerVel + Random.Range(minSpeed, maxSpeed) * positionOffsetDirection;
        }
    }

    public static Quaternion InitialRotation(Vector3 initialVelocity)
    {
        return Quaternion.FromToRotation(Vector3.forward, initialVelocity);
    }



    public static Vector3 DirectionToTargetInPlane(Vector3 from, Transform target)
    {
        Vector3 direction = target.position - from;
        direction.y = 0; // no vertical component
        return direction.normalized;
    }

    public static Vector3 DirectionInFront(Transform inFrontOf)
    {
        Vector3 direction = inFrontOf.forward;
        direction.y = 0;
        return direction.normalized;
    }

    private static Vector3 RotateVectorOnPlane(Vector3 v, float radians)
    {
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        float x = cos * v.x - sin * v.z;
        float z = sin * v.x + cos * v.z;
        return new Vector3(x, v.y, z);
    }

    public static Vector2 RotateVector(Vector2 v, float radians)
    {
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);
        float x = cos * v.x - sin * v.y;
        float y = sin * v.x + cos * v.y;
        return new Vector2(x, y);
    }
}
