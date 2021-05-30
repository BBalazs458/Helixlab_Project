using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stuffs : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            MissionManager ms = FindObjectOfType<MissionManager>();
            if (ms == null) Debug.LogWarning("Mission manager is missing!");
            ms.AddStuff(1);
            Destroy(gameObject);
        }
    }
}
