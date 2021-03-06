﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemy Spawn Settings")]
    [SerializeField] GameObject[] zombies = new GameObject[3];
    [SerializeField] List<Transform> zombiesSpawnPoints;
    public int counterLimit = 2;
    
    [Header("Player Spawn Settings")]
    [SerializeField]  GameObject player;
    [SerializeField]  List<Transform> playerSpawnPoints;

    [Header("Ammo and Health Spawn Settings")]
    [SerializeField] GameObject[] ammoAndHP;
    [SerializeField] List<Transform> ammoAndHPSpawnPoints;

    private Transform _startPosition = null;
    private static Transform _newSpawnPoint;

    public Transform GetPlayerSpawnPoint { get { return _newSpawnPoint; } }

    private void Awake()
    {
        DontDestroyOnLoad(this);

        _startPosition = playerSpawnPoints[0];

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
        //SpawnEnemy();
        StartCoroutine(EnemySpawnSequence(0.5f));
        if (_newSpawnPoint == null)
        {
            player.transform.position = _startPosition.position;

        }
        else
        {
            SetNewPlayerPos(_newSpawnPoint);
        }
    }


    private void Update()
    {
        Debug.Log(_newSpawnPoint);
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
            Vector3 newPos = new Vector3(pos.position.x + Random.Range(-5, 5), 0, pos.position.z + Random.Range(-5, 5));
            GameObject newEnemy = Instantiate(zombies[randomEnemy], newPos,Quaternion.identity);
        }
    }

    IEnumerator EnemySpawnSequence(float time)
    {
        int counter = 0;
        while (counter < counterLimit)
        {
            counter++;
            SpawnEnemy();
            yield return new WaitForSeconds(time);
        }
    }


    public void SetPlayerSpawn(int pos)
    {

        for (int i = 0; i < playerSpawnPoints.Count; i++)
        {
            if (i == pos)
            {
                _newSpawnPoint = playerSpawnPoints[pos];
            }
        }
    }

    public void SetNewPlayerPos(Transform playerPos)
    {
        player.transform.position = playerPos.position;
    }

}//class
