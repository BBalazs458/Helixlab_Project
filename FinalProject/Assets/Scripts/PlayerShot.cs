using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [Header("Shot setting")]
    [SerializeField] float _shotRange = 50f;
    [SerializeField] GameObject impactEffect;

    private Camera _main;
    private bool isMouseLock = true;

    private void Start()
    {
        _main = GetComponent<Camera>();
    }

    private void Update()
    {
        LockMouse();
        Shot();
       
    }

    void Shot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _shotRange))
            {
                Debug.Log(hit.collider.gameObject.name);


                //Impact effect 
                GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 0.3f);
            }
        }
    }


    void LockMouse()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMouseLock = !isMouseLock;
        }

        if (isMouseLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }


}
