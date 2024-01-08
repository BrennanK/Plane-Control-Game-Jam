using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovementTest : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _player;

    private float _timer;

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer > 0)
            return;
        _timer = 1f;

        EnemyAI spawnedEnemy = FindObjectOfType<EnemyAI>();

        if (spawnedEnemy == null)
            return;

        Rigidbody playerRigidbody = _player.GetComponent<Rigidbody>();

        GameObject projectile = Instantiate(_projectilePrefab);
        projectile.TryGetComponent(out ProjectileMovesInStraightLine straightLine);
        projectile.TryGetComponent(out ProjectileMovesDirectlyToEnemy basicHoming);

        if (straightLine != null)
            straightLine.Initialize(playerRigidbody, _player, spawnedEnemy.transform);
        else if (basicHoming != null)
            basicHoming.Initialize(playerRigidbody, _player, spawnedEnemy.transform, spawnedEnemy.GetComponent<UnityEngine.AI.NavMeshAgent>());
    }
}
