using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : MonoBehaviour
{
    private EnemyAI _enemyAI;

    private void Awake()
    {
        _enemyAI = GetComponentInParent<EnemyAI>();
        if (_enemyAI == null)
            Debug.LogError("need enemyAI on this gameobject or a parent gameobject", gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        _enemyAI.OnHurtboxCollision(other);
    }

    private void OnTriggerStay(Collider other)
    {
        _enemyAI.OnHurtboxCollision(other);
    }
}
