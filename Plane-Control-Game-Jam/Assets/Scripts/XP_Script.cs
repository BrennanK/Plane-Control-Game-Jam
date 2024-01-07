using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP_Script : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
