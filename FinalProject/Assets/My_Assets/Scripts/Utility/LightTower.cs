using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTower : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MissionManager ms = FindObjectOfType<MissionManager>();
            ms.SetNextObjectives = true;
            Destroy(gameObject);
        }
    }
}
