using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameobjectWhenAnimatedBoolIsFalse : MonoBehaviour
{
    [SerializeField] private GameObject _toDestroy;
    [SerializeField] private bool _dontDestroy = true;

    private void LateUpdate()
    {
        if (!_dontDestroy)
        {
            if (_toDestroy == null)
                Destroy(gameObject);
            else
                Destroy(_toDestroy);
        }
    }
}
