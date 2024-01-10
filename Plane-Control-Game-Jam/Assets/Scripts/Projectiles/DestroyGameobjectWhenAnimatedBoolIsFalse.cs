using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameobjectWhenAnimatedBoolIsFalse : MonoBehaviour
{
    [SerializeField] private bool _dontDestroy = true;

    private void LateUpdate()
    {
        if (!_dontDestroy)
        {
            Destroy(gameObject);
        }
    }
}
