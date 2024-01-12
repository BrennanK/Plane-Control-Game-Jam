using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePositionInitializer : MonoBehaviour
{
    public void Initialize(Transform player, Transform target)
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);

        Vector3 direction = ProjectileMovementProcessor.DirectionToTargetInPlane(transform.position, target);
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, direction);
    }
}
