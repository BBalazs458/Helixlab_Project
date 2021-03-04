using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemy Spawn Settings")]
    [SerializeField] GameObject[] zombies = new GameObject[3];
    [SerializeField] List<Transform> zombiesSpawnPoints;
    
    [Header("Player Spawn Settings")]
    [SerializeField] GameObject player;
    [SerializeField] List<Transform> playerSpawnPoints;

    [Header("Ammo and Health Spawn Settings")]
    [SerializeField] GameObject[] ammoAndHP;
    [SerializeField] List<Transform> ammoAndHPSpawnPoints;



    private void Awake()
    {
        if (zombies.Length == 0) return;
        if (zombiesSpawnPoints.Count == 0) return;
        if (playerSpawnPoints.Count == 0) return;
        if (ammoAndHP.Length == 0) return;
        if (ammoAndHPSpawnPoints.Count == 0) return;
        if (player == null) return;

    }

    private void Start()
    {
        SpawnItems();
        SpawnEnemy();
    }

    private void FixedUpdate()
    {

    }


    void SpawnItems()
    {

        for (int i = 0; i < ammoAndHPSpawnPoints.Count; i++)
        {
            int randomItem = Random.Range(0, ammoAndHP.Length);
            Transform pos = ammoAndHPSpawnPoints[i];
            GameObject newItem = Instantiate(ammoAndHP[randomItem], pos);
        }


    }

    void SpawnEnemy()
    {

        for (int i = 0; i < zombiesSpawnPoints.Count; i++)
        {
            int randomEnemy = Random.Range(0, zombies.Length);
            Transform pos = zombiesSpawnPoints[i];
            //Vector3 newPos = new Vector3(pos.position.x + Random.Range(-5,5),0, pos.position.z + Random.Range(-5, 5));
            GameObject newEnemy = Instantiate(zombies[randomEnemy],pos);
        }
    }

}//class
