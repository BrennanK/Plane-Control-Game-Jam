using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold_Script : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
