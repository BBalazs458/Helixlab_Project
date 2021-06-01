using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class END : MonoBehaviour
{
    [SerializeField] GameObject endPanel;
    [SerializeField] GameObject ship;
    [SerializeField] GameObject cutSceneCam;
    [SerializeField] GameObject Player;


    private void Start()
    {
        endPanel.SetActive(false);
        cutSceneCam.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cutSceneCam.SetActive(true);
            Player.SetActive(false);
            ship.GetComponent<Animator>().SetTrigger("Start");
            endPanel.SetActive(true);
        }
    }

}//class
