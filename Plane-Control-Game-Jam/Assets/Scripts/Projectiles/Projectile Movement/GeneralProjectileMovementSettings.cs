using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// maybe split this up into e.g. a VelocitySettings subclass, so the names have more context and can be shorter.

[System.Serializable]
public class GeneralProjectileMovementSettings
{
    [field: SerializeField, Tooltip("Min initial distance from the player")]
    public float MinSpawnDistanceFromPlayer { get; private set; } = 1;
    [field: SerializeField, Tooltip("Max initial distance from the player")]
    public float MaxSpawnDistanceFromPlayer { get; private set; } = 1;
    [field: SerializeField, Tooltip("Spawn in the direction the player is looking. " +
        "Else, spawn in the direction of the target")]
    public bool SpawnInFrontOfPlayer { get; private set; } = true;
    [field: SerializeField, Tooltip("The maximum angle offset from the spawn direction." +
        " E.g. if spawn in front of the player, can spawn some degrees around that direction.")]
    public float MaxRandomSpawnDirectionAngleOffset { get; private set; } = 0;
    [field: SerializeField]
    public float MinInitialVelocity { get; private set; } = 2;
    [field: SerializeField]
    public float MaxInitialVelocity { get; private set; } = 2;
    [field: SerializeField]
    public bool InitialVelocityIsTowardsEnemyNotAwayFromPlayer { get; private set; } = false;
    [field: SerializeField]
    public float ForwardsAcceleration { get; private set; } = 0;
}
