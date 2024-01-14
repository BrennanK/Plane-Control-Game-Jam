using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    [SerializeField] private EnemyAI _ai;
    [SerializeField] private Collider[] _colliders;

    private static Dictionary<Collider, EnemyAI> _colliderToAI = new();

    private void Awake()
    {
        for (int i = 0; i < _colliders.Length; i++)
            _colliderToAI.Add(_colliders[i], _ai);
    }

    public static bool TryGetAI(Collider collider, out EnemyAI ai)
    {
        return _colliderToAI.TryGetValue(collider, out ai);
    }
}
