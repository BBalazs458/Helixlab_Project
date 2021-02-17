﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [Header("Shot setting")]
    //[SerializeField] float _shotRange = 50f;
    [SerializeField] GameObject impactEffect;
    [SerializeField] GameObject magazine;

    [SerializeField]
    private float _shotRange;

    Camera _main;
    private bool isMouseLock = true;

    Weapon m4;
    Weapon shotgun;

    public float ShotRange
    {
        //get { return _shotRange; }
        set { _shotRange = value; }
    }


    private void Start()
    {
        _main = GetComponent<Camera>();

        m4 = GameObject.Find("M4A1").GetComponent<Weapon>();
        shotgun = GameObject.Find("Puska").GetComponent<Weapon>();

        if (_main == null)
            throw new System.Exception("Missing is main camera!");

        if (m4 == null)
        {
            Debug.LogWarning("M4A1 is missing!");
        }
        else if (shotgun == null)
        {
            Debug.LogWarning("Shotgun is missing!");
        }
    }


    private void Update()
    {
        LockMouse();
        Shot();
       
    }

    void Shot()
    {
        if (Input.GetMouseButtonDown(0) && 
            (m4.GetReload == false || shotgun.GetReload == false))
        {
            Ray ray = _main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _shotRange))
            {
                Debug.Log(hit.collider.gameObject.name);
                #region M4A1 shot
                if (m4.gameObject.GetComponent<MeshRenderer>().enabled == true)
                {
                    m4.PlayMuzzleFlash();
                    m4.PlayeShotSound();
                    m4.DecreaseAmmo();
                    Zombie z = hit.transform.GetComponent<Zombie>();
                    if (z != null)
                    {
                        z.Damage(m4.GetDamage);
                    }
                }
                #endregion
                #region Shotgun shot
                if (shotgun.gameObject.GetComponent<MeshRenderer>().enabled == true)
                {
                    shotgun.PlayMuzzleFlash();
                    shotgun.PlayeShotSound();
                    shotgun.DecreaseAmmo();
                    Zombie z = hit.transform.GetComponent<Zombie>();
                    if (z != null)
                    {
                        z.Damage(shotgun.GetDamage);
                    }
                }
                #endregion
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
