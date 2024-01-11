using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBarFill : MonoBehaviour
{
    [SerializeField] private RectTransform _fillArea;
    [SerializeField] private RectTransform _fill;

    public void SetFill(float fraction)
    {
        fraction = Mathf.Clamp01(fraction);
        float width = _fillArea.rect.width * fraction;
        _fill.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

}
