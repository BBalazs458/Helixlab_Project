using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid : MonoBehaviour
{
    [SerializeField] int _healing = 10;
    private Rigidbody _rb;
    private PlayerStats ps;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        ps = FindObjectOfType<PlayerStats>();
        if (ps == null)
            Debug.LogError("Missing PlayerStats on FirstAid!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            ps.AddHealth(_healing);
            Destroy(this.gameObject);
        }
    }
}
