using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerDoorOpen : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Animator container = GameObject.Find("Meglepi").GetComponent<Animator>();
            container.SetTrigger("openDoor");
        }
    }
}
