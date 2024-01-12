using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePositionInitializer : MonoBehaviour
{
    [SerializeField] private float _spawnPositionOffset;

    public void Initialize(Transform player, Transform target)
    {
        Vector3 offset = player.transform.right;
        offset.y = 0;
        offset.Normalize();
        offset *= _spawnPositionOffset;
        transform.position = offset + new Vector3(player.position.x, transform.position.y, player.position.z);

        Vector3 direction = ProjectileMovementProcessor.DirectionToTargetInPlane(transform.position, target);
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, direction);
    }
}
