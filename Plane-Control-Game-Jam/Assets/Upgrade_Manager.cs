using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrade_Manager : MonoBehaviour
{
    public static Upgrade_Manager instance { get; private set; }

    [SerializeField]
    private GameObject player;

    
    private OneStat playerHealth;
    private OneStat playerSpeed;
    [SerializeField]
    private TMP_Text currentPlayerHealthValue;

    [SerializeField]
    private TMP_Text currentPlayerSpeed;

    [SerializeField]
    private TMP_Text curNumWeapons;

    // Weapons Possible for the Player to Have
    [SerializeField]
    private List<PlayerWeaponSettings> weapons;

    private List<PlayerWeapon> attachedWeapons;

    [SerializeField]
    private TMP_Text currentWeaponDamage;

    [SerializeField]
    private int healthUpgradeAmount;

    [SerializeField]
    private int speedUpgradeAmount;

    [SerializeField]
    private int weaponDamageUpgradeAmount;

    [SerializeField]
    private GameObject UpgradeMenu;

    void Awake()
    {
        if(instance!=null && instance!=this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        playerHealth = player.GetComponentInChildren<OneStat>();

        currentPlayerHealthValue.text = "Player Health: " + playerHealth.Value;
        // playerHealth.Change(4);
        // currentPlayerHealthValue.text += playerHealth.Value;

        //currentWeaponDamage.text += weapons[0].GetComponent<ProjectileHitsEnemy>().getDamageDealt();

        //weapons[0].GetComponent<ProjectileHitsEnemy>().setDamageDealt(-24);

        playerSpeed = player.GetComponentsInChildren<OneStat>()[1];

        // playerSpeed.Change(100);

        attachedWeapons = player.GetComponentInChildren<PlayerWeapons>().getCurrentWeaponsOnPlayer();

        //curNumWeapons.text = "Number of Weapons on Player: " + attachedWeapons.Count;

        resetWeaponDamage();
    }

    void resetWeaponDamage()
    {
        foreach(PlayerWeaponSettings weaponSetting in weapons)
        {
            weaponSetting.ProjectilePrefab.GetComponent<ProjectileHitsEnemy>().resetToOriginalDamage();
        }
    }

   public void upgradeHealth()
    {
        playerHealth.Change(healthUpgradeAmount);
        currentPlayerHealthValue.text ="Player Health: "+ playerHealth.Value;
        deactivateUpgradeMenu();
    }

    public void upgradePlayerSpeed()
    {
        playerSpeed.Change(speedUpgradeAmount);
        currentPlayerSpeed.text = "Player Speed: " + playerSpeed.Value;
        deactivateUpgradeMenu();
    }

    // NOTE BE SURE TO RESET THE DAMAGE ON AWAKE OTHERWISE PREFAB WILL RETAIN UPGRADE
    //public void upgradeWeaponDamage()

    public void upgradeWeaponAtRandom()
    {
        GameObject projectile = attachedWeapons[Random.Range(0, attachedWeapons.Count)].getSettings().ProjectilePrefab;
        projectile.GetComponent<ProjectileHitsEnemy>().setDamageDealt(projectile.GetComponent<ProjectileHitsEnemy>().getDamageDealt() + weaponDamageUpgradeAmount);
        deactivateUpgradeMenu();
    }

    public void activateUpgradeMenu()
    {
        Time.timeScale = 0;
        UpgradeMenu.SetActive(true);
    }

    void deactivateUpgradeMenu()
    {
        Time.timeScale = 1;
        UpgradeMenu.SetActive(false);
    }

    
}
