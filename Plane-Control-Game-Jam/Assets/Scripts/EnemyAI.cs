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

    [SerializeField]
    private GameObject enemyDeathVFX;
    private NavMeshAgent navAgent;

    private GameObject target;

    private Rigidbody rigidBody;

    [SerializeField]
    private float knockBackForce;

    [SerializeField]
    private float durationOFKnockback;

    [SerializeField]
    private float StillThreshold = .05f;
    private bool inKnockBack;

    [SerializeField]
    private GameObject xpPrefab;

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

        rigidBody = gameObject.GetComponent<Rigidbody>();
        inKnockBack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(target!=null && inKnockBack==false)
        {
            navAgent.SetDestination(target.transform.position);
        }
        else
        {
            //Debug.Log("We have no target");
        }
    }

    public void setTarget(GameObject player)
    {
        target = player;
    }

    public void takeDamage(int damageDealt, Vector3 projectilePosition)
    {
        //Debug.Log("Health before: "+ maxHealth);
        maxHealth -= damageDealt;
        // Debug.Log("Health after: " + maxHealth);
        StartCoroutine(KnockBack(projectilePosition));
        if(maxHealth <=0)
        {
            Destroy(gameObject);
            Instantiate(xpPrefab,gameObject.transform.position,Quaternion.identity);
            Game_Manager.instance.updateScore();
            GameObject deathVFX = Instantiate(enemyDeathVFX,gameObject.transform.position,gameObject.transform.rotation);
            Destroy(deathVFX, 2);
        }
    }

    IEnumerator KnockBack(Vector3 projPosit)
    {
        //Get direction for knockback
        Vector3 directionOfKnockback = gameObject.transform.position - projPosit;
        directionOfKnockback.y = 0;
        directionOfKnockback = directionOfKnockback.normalized;
        // Debug.Log(directionOfKnockback * knockBackForce);
        // rigidBody.AddForce(directionOfKnockback * knockBackForce,ForceMode.Impulse);

        yield return null;
        navAgent.enabled = false;
        inKnockBack = true;
        yield return new WaitForEndOfFrame();
        rigidBody.useGravity = true;
        rigidBody.isKinematic = false;
        rigidBody.AddForce(directionOfKnockback * knockBackForce,ForceMode.Impulse);

        yield return new WaitForFixedUpdate();
        yield return new WaitUntil(() => rigidBody.velocity.magnitude < StillThreshold);
        yield return new WaitForSeconds(durationOFKnockback);

        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
        navAgent.Warp(transform.position);
        navAgent.enabled = true;
        inKnockBack = false;
        yield return null;

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
