using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UpgradeUI : MonoBehaviour
{
    //[SerializeField]
    //private List<GameObject> buttons;
    // Start is called before the first frame update

    private void Start()
    {
        //GameObject newButton = Instantiate(buttons[0],buttons[1].transform);
        //newButton.GetComponent<RectTransform>().anchoredPosition = buttons[1].GetComponent<RectTransform>().anchoredPosition;

    }
    public void HealthUpgrade()
    {
        Upgrade_Manager.instance.upgradeHealth();
    }

    public void SpeedUpgrade()
    {
        Upgrade_Manager.instance.upgradePlayerSpeed();
    }

    public void WeaponDamage()
    {
        Upgrade_Manager.instance.upgradeWeaponAtRandom();
    }

    public void addRandomWeapon()
    {
        Upgrade_Manager.instance.addWeapon();
    }
}
