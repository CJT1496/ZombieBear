using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public bool alive = true;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "eyes")
        {
            other.transform.parent.GetComponent<MonsterBear>().checkSight();
        }
    }
}