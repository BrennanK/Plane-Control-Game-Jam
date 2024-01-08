using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOnStatChange
{
    void OnStatChange(OneStat stat, int newValue);
}
