using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGAme : MonoBehaviour
{
    public GameObject endGame;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;

            AudioListener listener = FindObjectOfType<AudioListener>().GetComponent<AudioListener>();
            listener.enabled = false;

            endGame.SetActive(true);
            Destroy(gameObject);
        }
    }
}
