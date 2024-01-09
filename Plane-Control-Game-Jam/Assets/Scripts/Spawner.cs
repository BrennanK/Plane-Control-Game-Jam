using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Spawner : MonoBehaviour
{
    [SerializeField]
    private List <GameObject> enemies;

    [SerializeField]
    private int FodderEnemyMin;

    [SerializeField]
    private int FodderEnemyMax;

    [SerializeField]
    private int MediumEnemyMin;

    [SerializeField]
    private int MediumEnemyMax;

    [SerializeField]
    private int HeavyEnemyMin;

    [SerializeField]
    private int HeavyEnemyMax;

    [SerializeField]
    private TMP_Text Text;

    [SerializeField]
    private int spawnIntervalInSeconds;

    [SerializeField]
    private int spawnRadius;

    [SerializeField]
    private float healthMultiplier;

    private int timesToMultiply;

    private float gameTimeInSeconds;

    private GameObject playerCharacter;

    [SerializeField]

    private GameObject planeGround;
    
    class numRange
    {
        [SerializeField]
        private int minNumber;

        [SerializeField]
        private int maxNumber;
    }

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
            spawnAnEnemy();
        }
    }

    void spawnAnEnemy()
    {
        GameObject spawnedEnemy=Instantiate(decideWhatToSpawn(), decideWhereToSpawn(), gameObject.transform.rotation);
        spawnedEnemy.GetComponent<EnemyAI>().setTarget(playerCharacter);
        if (timesToMultiply != 0)
        {
            //spawnedEnemy.GetComponent<EnemyAI>().setHealth(healthMultiplier * timesToMultiply);
        }
    }

   public void spawnChampion()
    {
        GameObject spawnedEnemy = Instantiate(decideWhatToSpawn(), decideWhereToSpawn(), gameObject.transform.rotation);
        spawnedEnemy.GetComponent<EnemyAI>().makeChampion();
        spawnedEnemy.GetComponent<EnemyAI>().setTarget(playerCharacter);
    }

    GameObject decideWhatToSpawn()
    {
        int decidingNumber = Random.Range(FodderEnemyMin, HeavyEnemyMax + 1);
        //Debug.Log(decidingNumber);

        if(decidingNumber>=FodderEnemyMin && decidingNumber<=FodderEnemyMax)
        {
            return enemies[0];
        }
        else if (decidingNumber >= MediumEnemyMin && decidingNumber <= MediumEnemyMax)
        {
            return enemies[1];
        }
        else if (decidingNumber >= HeavyEnemyMin && decidingNumber <= HeavyEnemyMax)
        {
            return enemies[2];
        }
        else
        {
            return enemies[0];
        }
    }

    public Vector3 decideWhereToSpawn()
    {

        float minX = planeGround.transform.position.x-planeGround.transform.localScale.x * 5;
        float maxX = planeGround.transform.position.x + planeGround.transform.localScale.x * 5;

        float minZ = planeGround.transform.position.z - planeGround.transform.localScale.z * 5;
        float maxZ = planeGround.transform.position.z + planeGround.transform.localScale.z * 5;



        Vector2 vec = Random.insideUnitCircle * spawnRadius;
        //Debug.Log(vec);
        Vector3 spawnOffset = new Vector3(vec.x,0,vec.y);

        Vector3 result = playerCharacter.transform.position + spawnOffset;

        result.x = Mathf.Clamp(result.x, minX, maxX);
        result.z = Mathf.Clamp(result.z, minZ, maxZ);

        return result;
    }

    public void IncrementTimesToMultiply()
    {
        timesToMultiply++;
    }
}
