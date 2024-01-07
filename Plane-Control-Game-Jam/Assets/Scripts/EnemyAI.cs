using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private int health;

    [SerializeField]
    private float AIMoveSpeed;

    [SerializeField]
    private int attackDamage;

    
    private NavMeshAgent navAgent;

    private GameObject target;

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
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject==target)
        {
            Debug.Log("We collided with the player");
            // Will make call to damage the player once player health is implemented
        }
    }
}
