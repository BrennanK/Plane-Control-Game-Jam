using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameobjectAfterDuration : MonoBehaviour
{
    [SerializeField] private float _seconds = 10;

    private float _startTime;

    private void Start()
    {
        _startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time > _startTime + _seconds)
            Destroy(gameObject);
    }
}
