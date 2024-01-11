using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP_Script : MonoBehaviour
{
    [SerializeField]
    private GameObject pickupSound;
    [SerializeField]
    private int xpPerObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Game_Manager.instance.updateXP(xpPerObject);
            Instantiate(pickupSound, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
