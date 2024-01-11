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
   //[SerializeField]
  // private TMP_Text currentPlayerHealthValue;

    //[SerializeField]
   // private TMP_Text currentPlayerSpeed;

   // [SerializeField]
   // private TMP_Text curNumWeapons;

    // Weapons Possible for the Player to Have
    [SerializeField]
    private List<PlayerWeaponSettings> weapons;

    private List<PlayerWeaponSettings> weaponsToAttach;

    private List<PlayerWeapon> attachedWeapons;

   // [SerializeField]
   // private TMP_Text currentWeaponDamage;

    [SerializeField]
    private int healthUpgradeAmount;

    [SerializeField]
    private int speedUpgradeAmount;

    [SerializeField]
    private int weaponDamageUpgradeAmount;

    [SerializeField]
    private GameObject UpgradeMenu;

    [SerializeField]
    private GameObject addWeaponButton;

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
        weaponsToAttach = weapons;
       // currentPlayerHealthValue.text = "Player Health: " + playerHealth.Value;
        // playerHealth.Change(4);
        // currentPlayerHealthValue.text += playerHealth.Value;

        //currentWeaponDamage.text += weapons[0].GetComponent<ProjectileHitsEnemy>().getDamageDealt();

        //weapons[0].GetComponent<ProjectileHitsEnemy>().setDamageDealt(-24);

        playerSpeed = player.GetComponentsInChildren<OneStat>()[1];

        // playerSpeed.Change(100);

        attachedWeapons = player.GetComponentInChildren<PlayerWeapons>().getCurrentWeaponsOnPlayer();

       // curNumWeapons.text = "Number of Weapons on Player: " + attachedWeapons.Count;

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
       // currentPlayerHealthValue.text ="Player Health: "+ playerHealth.Value;
        deactivateUpgradeMenu();
    }

    public void upgradePlayerSpeed()
    {
        playerSpeed.Change(speedUpgradeAmount);
      //  currentPlayerSpeed.text = "Player Speed: " + playerSpeed.Value;
        deactivateUpgradeMenu();
    }

    // NOTE BE SURE TO RESET THE DAMAGE ON AWAKE OTHERWISE PREFAB WILL RETAIN UPGRADE
    //public void upgradeWeaponDamage()

    public void upgradeWeaponAtRandom()
    {
        attachedWeapons = player.GetComponentInChildren<PlayerWeapons>().getCurrentWeaponsOnPlayer();
        GameObject projectile = attachedWeapons[Random.Range(0, attachedWeapons.Count)].getSettings().ProjectilePrefab;
        projectile.GetComponent<ProjectileHitsEnemy>().setDamageDealt(projectile.GetComponent<ProjectileHitsEnemy>().getDamageDealt() + weaponDamageUpgradeAmount);
        deactivateUpgradeMenu();
    }

    public void addWeapon()
    {
        
        if(weaponsToAttach.Count==0)
        {
            deactivateUpgradeMenu();
            return;
        }

        attachedWeapons = player.GetComponentInChildren<PlayerWeapons>().getCurrentWeaponsOnPlayer();

        for(int i=0;i<weaponsToAttach.Count;i++)
        {
            for(int j=0;j<attachedWeapons.Count;j++)
            {
                if(weaponsToAttach[i].ProjectilePrefab == attachedWeapons[j].getSettings().ProjectilePrefab)
                {
                    weaponsToAttach.Remove(attachedWeapons[j].getSettings());
                   // Debug.Log("Weapons left"+weaponsToAttach.Count);
                }
            }
        }
       
        if (weaponsToAttach.Count!=0)
        {
            player.GetComponentInChildren<PlayerWeapons>().AddWeapon(weaponsToAttach[Random.Range(0,weaponsToAttach.Count)]);
            //weaponsToAttach = weapons;
        }

        attachedWeapons = player.GetComponentInChildren<PlayerWeapons>().getCurrentWeaponsOnPlayer();

        for (int i = 0; i < weaponsToAttach.Count; i++)
        {
            for (int j = 0; j < attachedWeapons.Count; j++)
            {
                if (weaponsToAttach[i].ProjectilePrefab == attachedWeapons[j].getSettings().ProjectilePrefab)
                {
                    weaponsToAttach.Remove(attachedWeapons[j].getSettings());
                    // Debug.Log("Weapons left"+weaponsToAttach.Count);
                }
            }
        }
        Debug.Log("Weapons left" + weaponsToAttach.Count);
       // curNumWeapons.text = "Number of Weapons on Player: " + attachedWeapons.Count;
        deactivateUpgradeMenu();
    }

    public void activateUpgradeMenu()
    {
        Time.timeScale = 0;
        if (weaponsToAttach.Count == 0)
        {
            addWeaponButton.SetActive(false);
        }
        UpgradeMenu.SetActive(true);
    }

    void deactivateUpgradeMenu()
    {
        Time.timeScale = 1;
        UpgradeMenu.SetActive(false);
    }

    
}
