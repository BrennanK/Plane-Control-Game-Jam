using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionDetector : MonoBehaviour
{
    private ProjectileHitsEnemy _hitter;

    private void Awake()
    {
        _hitter = GetComponentInParent<ProjectileHitsEnemy>();
        if (_hitter == null)
        {
            Debug.LogError("ProjectileCollisionDetector: Need a ProjectileHitsEnemy script " +
                "component on this gameObject or a parent.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _hitter.DetectCollision(other);
    }
}
