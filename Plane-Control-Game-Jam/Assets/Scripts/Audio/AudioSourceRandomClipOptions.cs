using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AudioSourceRandomClipOptions : ScriptableObject
{
    [field: SerializeField] public AudioClip[] Clips { get; private set; }
}
