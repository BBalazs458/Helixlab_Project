using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthSelector : MonoBehaviour
{
    public Slider enemyHealthBar = null;
    public GameObject selectedEnemy = null;
    public float maxSelectionDistance = 100f;

    public GameObject fillArea;

    private void Awake()
    {
        UpdateselectedObject(null);
    }

    public void UpdateselectedObject(GameObject obj)
    {
        selectedEnemy = obj;

        if (enemyHealthBar == null) return;

        if (selectedEnemy != null)
        {
            enemyHealthBar.value = selectedEnemy.GetComponent<ZombieAI>().GetHealth();
            fillArea.gameObject.SetActive(true);

        }
        else
        {
            fillArea.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxSelectionDistance))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                UpdateselectedObject(hit.transform.gameObject);
            }
            else
            {
                if (selectedEnemy != null)
                    UpdateselectedObject(null);
            }
        }
        else
        {
            if (selectedEnemy != null)
                UpdateselectedObject(null);
        }

    }


}//class
