using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradeUI : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> panel;
    // Start is called before the first frame update

    [SerializeField]
    private List<Sprite> weaponIcons;

    private void Start()
    {
        //GameObject newButton = Instantiate(buttons[0],buttons[1].transform);
        //newButton.GetComponent<RectTransform>().anchoredPosition = buttons[1].GetComponent<RectTransform>().anchoredPosition;

        panel[0].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "HP +" + Upgrade_Manager.instance.getHealthUpgrade();
        panel[1].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "Speed +" + Upgrade_Manager.instance.getSpeedUpgrade();
        panel[2].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "Damage +" + Upgrade_Manager.instance.getWeaponDamgeUpgrade();

       // Upgrade_Manager.instance.chooseWeapon();

        //panel[3].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = Upgrade_Manager.instance.getWeaponName();

    }

    private void OnEnable()
    {
        panel[3].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = Upgrade_Manager.instance.getWeaponName();

        if(Upgrade_Manager.instance.getWeaponName()=="Boomerang")
        {
            panel[3].transform.GetChild(2).gameObject.GetComponent<Image>().sprite = weaponIcons[0];
        }
        if(Upgrade_Manager.instance.getWeaponName()=="Puddle")
        {
            panel[3].transform.GetChild(2).gameObject.GetComponent<Image>().sprite = weaponIcons[1];
        }
        if(Upgrade_Manager.instance.getWeaponName()=="Explosion")
        {
            panel[3].transform.GetChild(2).gameObject.GetComponent<Image>().sprite = weaponIcons[2];
        }
        if (Upgrade_Manager.instance.getWeaponName() == "Melee")
        {
            panel[3].transform.GetChild(2).gameObject.GetComponent<Image>().sprite = weaponIcons[3];
        }

        WeaponLevel level = Upgrade_Manager.instance.checkForUpgradableWeapon();
        if(level!=null)
        {
            panel[4].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = Upgrade_Manager.instance.getupgradeLevelName();
        }
        else
        {
            panel[4].gameObject.SetActive(false);
        }

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

    public void upgradeWeaponLevel()
    {
        Upgrade_Manager.instance.removeAndAddWeapons();
    }
}
