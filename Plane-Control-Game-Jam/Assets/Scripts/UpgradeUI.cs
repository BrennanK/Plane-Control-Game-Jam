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
