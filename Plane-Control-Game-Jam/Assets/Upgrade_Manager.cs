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

    private WeaponUpgradePair pairings;

    private List<PlayerWeaponSettings> weaponsToAttach;

    private List<PlayerWeapon> attachedWeapons;

    private HashSet<WeaponLevel> levelsOfCurrentWeapons= new HashSet<WeaponLevel>();

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

    private WeaponLevel upgradableWeapon;
    private WeaponLevel oldWeapon;
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

        pairings = gameObject.GetComponent<WeaponUpgradePair>();

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
        for (int i = 0; i < attachedWeapons.Count; i++)
        {
            Debug.Log(attachedWeapons[i].getSettings());
        }
        attachedWeapons = player.GetComponentInChildren<PlayerWeapons>().getCurrentWeaponsOnPlayer();
        deactivateUpgradeMenu();
    }

    public WeaponLevel checkForUpgradableWeapon()
    {
        attachedWeapons = player.GetComponentInChildren<PlayerWeapons>().getCurrentWeaponsOnPlayer();
        levelsOfCurrentWeapons.Clear();
        for(int i=0;i<attachedWeapons.Count;i++)
        {
            //Debug.Log("This is levels: "+attachedWeapons[i].getSettings().levelAssociated);
            
            if(attachedWeapons[i].getSettings().levelAssociated!=null)
            {
                levelsOfCurrentWeapons.Add(attachedWeapons[i].getSettings().levelAssociated);
            }
           
            //Blah
        }

        foreach (WeaponLevel lv in levelsOfCurrentWeapons)
        {
            Debug.Log("This is lv: "+lv);
        }

        for (int i = 0; i < attachedWeapons.Count; i++)
        {
            //Debug.Log(attachedWeapons[i].getSettings());
        }

        if (levelsOfCurrentWeapons.Count==0)
        {
            return null;
        }

        int index = Random.Range(0, levelsOfCurrentWeapons.Count);

        foreach(WeaponLevel lv in levelsOfCurrentWeapons)
        {
           // Debug.Log("This is lv: "+lv);
            if(index==0)
            {
                oldWeapon = lv;
                upgradableWeapon = pairings.returnUpgradedWeapon(lv);
               Debug.Log("This the match: "+upgradableWeapon);
                return upgradableWeapon;
            }
            else
            {
                index--;
            }
        }

        return null;
    }

    public void removeAndAddWeapons()
    {
        for(int i=0;i<oldWeapon.weaponsInLevel.Count;i++)
        {
            player.GetComponentInChildren<PlayerWeapons>().removeWeaponFromPlayer(oldWeapon.weaponsInLevel[i]);
        }

        for (int i = 0; i < upgradableWeapon.weaponsInLevel.Count; i++)
        {
            
            player.GetComponentInChildren<PlayerWeapons>().AddWeapon(upgradableWeapon.weaponsInLevel[i]);
        }
       /*
        attachedWeapons= player.GetComponentInChildren<PlayerWeapons>().getCurrentWeaponsOnPlayer();
        for (int i=0;i<attachedWeapons.Count;i++)
        {
            if(!weaponsToAttach.Contains(attachedWeapons[i].getSettings()))
            {
                weaponsToAttach.Add(attachedWeapons[i].getSettings());
            }
        }
       */
        deactivateUpgradeMenu();
    }

    public string getupgradeLevelName()
    {
        return upgradableWeapon.name;
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
