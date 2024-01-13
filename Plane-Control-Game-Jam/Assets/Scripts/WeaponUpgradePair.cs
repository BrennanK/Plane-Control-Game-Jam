using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class WeaponPair
{
    [SerializeField]
    private WeaponLevel baseVer;

    [SerializeField]
    private WeaponLevel newVer;


    public WeaponLevel GetOriginalVersion()
    {
        return baseVer;
    }
    
    public WeaponLevel getNewVersion()
    {
        return newVer;
    }
}
public class WeaponUpgradePair: MonoBehaviour
{

    // List of Possible Weapons to upgrade
    // *NOTE* Due to time constraints keep the elements paired for weapons and upgradeableWeapons as in if Bommerang is index 0 its upgradable should be index 0
    [SerializeField]
    private List<WeaponPair> upgradeableWeapons;

    

    public WeaponLevel returnUpgradedWeapon(WeaponLevel baseW)
    {
        for(int i=0;i<upgradeableWeapons.Count;i++)
        {
           if(baseW==upgradeableWeapons[i].GetOriginalVersion())
            {
               return upgradeableWeapons[i].getNewVersion();
            }
        }
        return null;
    }
}
