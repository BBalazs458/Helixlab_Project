using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeSpawn : MonoBehaviour
{
    public List<Transform> points;

    public List<GameObject> horde;
    public int counterLimit = 2;

    private void Start()
    {
        if (points.Count == 0)
        {
            return;
        }
        if (horde.Count == 0)
        {
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int counter = 0;
            while (counter < counterLimit)
            {
                counter++;
                SpawnHorde();
            }
            Destroy(gameObject);
        }
    }

    void SpawnHorde()
    {
        for (int i = 0; i < points.Count; i++)
        {
            int randomEnemy = Random.Range(0, horde.Count);
            Transform pos = points[i];
            Vector3 newPos = new Vector3(pos.position.x + Random.Range(-5, 5), 0, pos.position.z + Random.Range(-5, 5));
            GameObject newEnemy = Instantiate(horde[randomEnemy], newPos, Quaternion.Euler(0,180,0));
        }
    }

}//
