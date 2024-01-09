using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XP_Script : MonoBehaviour
{
    [SerializeField]
    private int xpPerObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Game_Manager.instance.updateXP(xpPerObject);
            Destroy(gameObject);
        }
    }
}
