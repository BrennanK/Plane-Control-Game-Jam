using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRepetitiveCollisionDetector : MonoBehaviour
{
    [SerializeField] private bool _hitImmediatelyWhenEnter;
    [SerializeField] private float _repeatInterval;

    private ProjectileHitsEnemy _hitter;
    private Dictionary<EnemyAI, float> _nextHitTimes = new();

    private void Awake()
    {
        _hitter = GetComponentInParent<ProjectileHitsEnemy>();
        if (_hitter == null)
        {
            Debug.LogError("ProjectileCollisionDetector: Need a ProjectileHitsEnemy script " +
                "component on this gameObject or a parent.");
        }
    }

    private void OnTriggerEnter(Collider other) => OnTrigger(other);

    private void OnTriggerStay(Collider other) => OnTrigger(other);

    private void OnTrigger(Collider other)
    {
        if (!EnemyHitbox.TryGetAI(other, out EnemyAI ai))
            return;

        if (!_nextHitTimes.ContainsKey(ai))
        {
            _nextHitTimes.Add(ai, Time.fixedTime + _repeatInterval);
            if (_hitImmediatelyWhenEnter)
                _hitter.DetectCollision(other);
        }
        else
        {
            if (Time.fixedTime < _nextHitTimes[ai])
                return;
            _nextHitTimes[ai] = Time.fixedTime + _repeatInterval;
            _hitter.DetectCollision(other);
        }
    }
}
