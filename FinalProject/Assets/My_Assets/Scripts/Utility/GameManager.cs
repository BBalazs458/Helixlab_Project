using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static Transform _playerCheckpoint;

    SpawnManager sm;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        sm = FindObjectOfType<SpawnManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (sm.GetPlayerSpawnPoint == null)
        {
            return;
        }
        else
        {
            sm.SetNewPlayerPos(_playerCheckpoint);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sm.GetPlayerSpawnPoint != null)
        {
            PositionChange(sm.GetPlayerSpawnPoint);
        }
        Debug.Log(_playerCheckpoint);
    }

    void PositionChange(Transform pos)
    {
        _playerCheckpoint = pos;
    }


}//class
