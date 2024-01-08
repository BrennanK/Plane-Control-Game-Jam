using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// You may need to lock the inspector to navigate folders to the projectile prefab, so you can assign it.
// There's a lock icon at the top right of the inspector window.

[CreateAssetMenu]
public class PlayerWeaponSettings : ScriptableObject
{
    [field: SerializeField] public GameObject ProjectilePrefab { get; private set; }
    [field: SerializeField] public float FireInterval { get; private set; } = 1;
    [field: SerializeField] public float EnemyDetectionRadius { get; private set; } = 5;
    [field: SerializeField] public float EnemyDetectionMaxDegreesFromLookDirection { get; private set; } = 90;
    [field: SerializeField] public bool WaitForEnemyBeforeFinishFireInterval { get; private set; } = true;
}
