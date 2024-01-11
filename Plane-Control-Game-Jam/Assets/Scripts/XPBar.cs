using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XPBar : MonoBehaviour
{
    [SerializeField] private UIBarFill _barFill;
    [SerializeField] private TMP_Text _text;

    private static XPBar _instance;

    private void Awake()
    {
        _instance = this;
    }

    public static void Change(int newLevel, int xp, int xpForNextLevelup, int xpForPriorLevelup)
    {
        _instance._text.text = "LEVEL: " + newLevel;
        float xpSinceLastLevelup = xp - xpForPriorLevelup;
        float xpBetweenLevelups = xpForNextLevelup - xpForPriorLevelup;
        float fraction = ((float)xpSinceLastLevelup) / xpBetweenLevelups;
        _instance._barFill.SetFill(fraction);
    }
}
