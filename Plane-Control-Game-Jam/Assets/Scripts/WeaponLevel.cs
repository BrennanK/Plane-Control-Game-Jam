using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponLevel : ScriptableObject
{
    public List<PlayerWeaponSettings> weaponsInLevel;

    public string displayName;
}
