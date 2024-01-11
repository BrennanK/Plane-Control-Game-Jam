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
    private GameObject addWeaponPanel;

    private PlayerWeaponSettings chosenWeapon;

    public int getHealthUpgrade()
    {
        return healthUpgradeAmount;
    }

    public int getSpeedUpgrade()
    {
        return speedUpgradeAmount;
    }

    public int getWeaponDamgeUpgrade()
    {
        return weaponDamageUpgradeAmount;
    }

    public string getWeaponName()
    {
        return chosenWeapon.ProjectilePrefab.name;
    }
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
        for(int i=0;i< weapons.Count;i++)
        {
            weapons[i].ProjectilePrefab.GetComponent<ProjectileHitsEnemy>().resetToOriginalDamage();
        }
    }

   public void upgradeHealth()
    {
        playerHealth.IncreaseMaxAndValue(healthUpgradeAmount);
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

    public void chooseWeapon()
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
            //player.GetComponentInChildren<PlayerWeapons>().AddWeapon(weaponsToAttach[Random.Range(0,weaponsToAttach.Count)]);
            //weaponsToAttach = weapons;
            chosenWeapon = weaponsToAttach[Random.Range(0, weaponsToAttach.Count)];
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

    public void addWeapon()
    {
        player.GetComponentInChildren<PlayerWeapons>().AddWeapon(chosenWeapon);
        deactivateUpgradeMenu();
    }

    public void activateUpgradeMenu()
    {
        chooseWeapon();
        Time.timeScale = 0;
        if (weaponsToAttach.Count == 0)
        {
            addWeaponPanel.SetActive(false);
        }
        UpgradeMenu.SetActive(true);
    }

    void deactivateUpgradeMenu()
    {
        Time.timeScale = 1;
        UpgradeMenu.SetActive(false);
    }

    
}
