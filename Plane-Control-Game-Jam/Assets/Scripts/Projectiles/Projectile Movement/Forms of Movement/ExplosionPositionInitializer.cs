using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPositionInitializer : MonoBehaviour
{
    [SerializeField] private float _minDistanceFromPlayer;
    [SerializeField] private float _maxDistanceFromPlayer;

    public void Initialize(Transform player)
    {
        float y = transform.position.y;
        Vector2 xz;
        if (_minDistanceFromPlayer == _maxDistanceFromPlayer)
            xz = _minDistanceFromPlayer * Random.insideUnitCircle.normalized;
        else
        {
            do
            {
                xz = _maxDistanceFromPlayer * Random.insideUnitCircle;
            } while (xz.magnitude < _minDistanceFromPlayer);
        }

        Vector2 playerPos = new Vector2(player.position.x, player.position.z);
        xz += playerPos;
        transform.position = new Vector3(xz.x, y, xz.y);
    }
}
