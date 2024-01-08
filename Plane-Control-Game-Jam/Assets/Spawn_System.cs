using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_System : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> enemiesSpawnable;

    [SerializeField]
    private List<GameObject> spawners;

    [SerializeField]
    private int intervalInSecondsForChampionSpawn;

    [SerializeField]
    private int intervalInSecondsForenemyHealthIncrease;

    private float gameTimeForChampionSpawn;
    private float gameTimeForEnemyHealthIcrease;
    // Start is called before the first frame update
    void Start()
    {
        //spawners[0].GetComponent<Spawner>().spawnChampion();
        //Debug.Log(spawners.Count);
    }

    // Update is called once per frame
    void Update()
    {
        gameTimeForChampionSpawn += Time.deltaTime;
        gameTimeForEnemyHealthIcrease+=Time.deltaTime;
       
        if ((int)gameTimeForChampionSpawn==intervalInSecondsForChampionSpawn)
        {
            gameTimeForChampionSpawn = 0;
            spawners[Random.Range(0,spawners.Count)].GetComponent<Spawner>().spawnChampion();
        }

        if((int)gameTimeForEnemyHealthIcrease==intervalInSecondsForenemyHealthIncrease)
        {
            gameTimeForEnemyHealthIcrease = 0;
            foreach (GameObject spawner in spawners)
            {
                spawner.GetComponent<Spawner>().IncrementTimesToMultiply();
            }
        }
        
    }
}
