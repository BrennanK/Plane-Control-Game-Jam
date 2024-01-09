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
    private float receivedKnockbackScale = 1;
    [SerializeField]
    private float knockbackEndSpeed = .1f;
    [SerializeField]
    private float knockbackMaxDuration = 1f;

    [SerializeField]
    private float AIMoveSpeed;

    [SerializeField]
    private int attackDamage;

    [SerializeField]
    private float attackInterval = 1;

    private float lastAttackTime = float.NegativeInfinity;

    [SerializeField]
    private GameObject enemyDeathVFX;
    private NavMeshAgent navAgent;
    private Rigidbody _rigidbody;

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
        _rigidbody = gameObject.GetComponent<Rigidbody>();
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

    public void takeDamage(int damageDealt, Transform knockbackSource, float knockbackPower)
    {
        Vector3 knockbackDirection = transform.position - knockbackSource.position;
        knockbackDirection.y = 0;
        knockbackDirection.Normalize();
        Vector3 knockback = knockbackPower * receivedKnockbackScale / _rigidbody.mass * knockbackDirection;
        StartCoroutine(ApplyKnockback(knockback));

        Debug.Log("Health before: "+ maxHealth);
        maxHealth -= damageDealt;
        Debug.Log("Health after: " + maxHealth);
        if(maxHealth <=0)
        {
            Destroy(gameObject);
            Game_Manager.instance.updateScore();
            GameObject deathVFX = Instantiate(enemyDeathVFX,gameObject.transform.position,gameObject.transform.rotation);
            Destroy(deathVFX, 2);
        }
    }

    private IEnumerator ApplyKnockback(Vector3 knockback)
    {
        // based on https://www.youtube.com/watch?v=0NH5obeOb7I

        // switch from the navMeshAgent to the rigidbody during knockback
        navAgent.enabled = false;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(knockback, ForceMode.Impulse);

        float startTime = Time.time;

        yield return new WaitForFixedUpdate(); // let the velocity update
        while (Time.time < startTime + knockbackMaxDuration)
        {
            if (_rigidbody.velocity.magnitude < knockbackEndSpeed)
            {
                Debug.Log("finished speed");
                break;
            }
            yield return null;
        }
        if (!(Time.time < startTime + knockbackMaxDuration))
            Debug.Log("finished time");

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.isKinematic = true;
        navAgent.Warp(transform.position);
        navAgent.enabled = true;
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
