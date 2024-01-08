using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private TMP_Text Text;

    [SerializeField]
    private int spawnIntervalInSeconds;

    [SerializeField]
    private float healthMultiplier;

    private int timesToMultiply;

    private float gameTimeInSeconds;

    private GameObject playerCharacter;
    // Start is called before the first frame update
    void Start()
    {
        //Instantiate(enemy, gameObject.transform.position, gameObject.transform.rotation);
        playerCharacter=FindObjectOfType<PlayerMovement>().gameObject;
        if(playerCharacter!=null)
        {
            Debug.Log("Player Found");
        }
        timesToMultiply = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gameTimeInSeconds += Time.deltaTime;

        
        if(Text!=null)
        {
            Text.text = (int)gameTimeInSeconds + "";
        }
        

        if(((int)gameTimeInSeconds%spawnIntervalInSeconds)==0 && (int)gameTimeInSeconds>0)
        {
            gameTimeInSeconds = 0;
            spawnAnEnemy(enemy);
        }
    }

    void spawnAnEnemy(GameObject enemyToSpawn)
    {
        GameObject spawnedEnemy=Instantiate(enemyToSpawn, gameObject.transform.position, gameObject.transform.rotation);
        spawnedEnemy.GetComponent<EnemyAI>().setTarget(playerCharacter);
        if (timesToMultiply != 0)
        {
            spawnedEnemy.GetComponent<EnemyAI>().setHealth(healthMultiplier * timesToMultiply);
        }
    }

   public void spawnChampion()
    {
        GameObject spawnedEnemy = Instantiate(enemy, gameObject.transform.position, gameObject.transform.rotation);
        spawnedEnemy.GetComponent<EnemyAI>().makeChampion();
        spawnedEnemy.GetComponent<EnemyAI>().setTarget(playerCharacter);
    }

    public void IncrementTimesToMultiply()
    {
        timesToMultiply++;
    }
}
