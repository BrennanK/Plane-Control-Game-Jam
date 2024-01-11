using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameobjectWhenAudioSourceFinishes : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void LateUpdate()
    {
        if (!_audioSource.isPlaying)
            Destroy(gameObject);
    }
}
