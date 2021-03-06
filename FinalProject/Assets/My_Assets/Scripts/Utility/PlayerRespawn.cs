using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] int spawnPointNumber;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //set the spawn point
            SpawnManager spawnManager = FindObjectOfType<SpawnManager>();
            if (spawnManager == null) return;

            spawnManager.SetPlayerSpawn(spawnPointNumber);
            Debug.Log("Spawn point is set!");
            Destroy(this.gameObject);
        }
    }
}
