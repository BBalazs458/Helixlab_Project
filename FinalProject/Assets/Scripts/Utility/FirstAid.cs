using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid : MonoBehaviour
{
    [SerializeField] int _healing = 10;
    private PlayerStats ps;

    private void Start()
    {
        ps = GameObject.Find("Player").GetComponent<PlayerStats>();
        if (ps == null)
            Debug.LogError("Missing PlayerStats on FirstAid!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && ps.GetCurrentHP != 100)
        {
            Debug.Log("Trigger");
            ps.AddHealth(_healing);
            Destroy(this.gameObject);
        }
    }
}
