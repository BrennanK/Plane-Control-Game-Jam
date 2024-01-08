using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    private int health;

    [SerializeField]
    private float AIMoveSpeed;

    [SerializeField]
    private int attackDamage;

    [SerializeField]
    private float attackInterval = 1;

    private float lastAttackTime = float.NegativeInfinity;

    
    private NavMeshAgent navAgent;

    private GameObject target;

    public static HashSet<EnemyAI> All { get; private set; } = new();

    private void OnEnable() => All.Add(this);
    private void OnDisable() => All.Remove(this); 
    // ^ technically should do this immediately when dies to avoid being in this until the end of frame, I think.

    // Start is called before the first frame update
    void Start()
    {
        //target= FindObjectOfType<PlayerMovement>().gameObject;
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        navAgent.speed = AIMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(target!=null)
        {
            navAgent.SetDestination(target.transform.position);
        }
        else
        {
            Debug.Log("We have no target");
        }
    }

    public void setTarget(GameObject player)
    {
        target = player;
    }

    public void takeDamage(int damageDealt)
    {
        health -= damageDealt;

        if(health<=0)
        {
            Destroy(gameObject);
            Game_Manager.instance.updateScore();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnCollision(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        OnCollision(collision);
    }

    private void OnCollision(Collision collision)
    {
        if (collision.gameObject == target)
        {
            if (Time.time > lastAttackTime + attackInterval)
            {
                Debug.Log("damage player " + attackDamage);
                lastAttackTime = Time.time;
                PlayerStats.Instance.GetStat("health").Change(-attackDamage);
            }
        }
    }

    public void makeChampion()
    {
        health *= 5;
        Debug.Log("We have a champion: " + gameObject.name + "with health of: " + health);
    }

    public void setHealth(float multiplierToUse)
    {
        health = (int)(maxHealth * multiplierToUse);
        //Debug.Log("Current health is: " + health);
    }
}
