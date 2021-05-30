using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionTrigger : MonoBehaviour
{
    [SerializeField] GameObject section;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            section.SetActive(true);
        }
    }
}
