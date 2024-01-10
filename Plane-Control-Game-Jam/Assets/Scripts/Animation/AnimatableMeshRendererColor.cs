using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatableMeshRendererColor : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer _meshRenderer;
    [SerializeField]
    private Color _color = Color.white;

    private MaterialPropertyBlock _properties;
    private int _colorPropertyID;

    private void Awake()
    {
        _properties = new MaterialPropertyBlock();
        _colorPropertyID = Shader.PropertyToID("_BaseColor");
    }

    private void LateUpdate()
    {
        _properties.SetColor(_colorPropertyID, _color);
        _meshRenderer.SetPropertyBlock(_properties);
    }
}
