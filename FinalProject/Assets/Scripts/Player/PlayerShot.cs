using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [Header("Shot setting")]
    [SerializeField] GameObject impactEffect;
    [SerializeField] Camera _main;

    [SerializeField]
    private float _shotRange;

    private bool isMouseLock = true;
    private bool _isShot = false;
    private bool canShot = true;

    Weapon m4;
    Weapon shotgun;

    public float ShotRange{ set { _shotRange = value; } }


    private void Start()
    {

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
        if ((m4.GetInventoryAmmo >= 0 && shotgun.GetInventoryAmmo >= 0) && 
            (m4.GetCurrentAmmo >= 0 && shotgun.GetCurrentAmmo >= 0))
        {
            Shot();
        }
       
    }

    void Shot()
    {
        // Át kell írni nyomva tartásra, és kell delay time 
        if (Input.GetMouseButtonDown(0) && canShot == true)
        {
            Ray ray = _main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            _isShot = true;//Need play shot audio

            if (Physics.Raycast(ray, out hit, _shotRange))
            {
                Debug.Log(hit.collider.gameObject.name);
                #region M4A1 shot
                if (m4.gameObject.GetComponent<MeshRenderer>().enabled == true)
                {
                    m4.PlayMuzzleFlash();
                    m4.PlayeShotSound(_isShot);
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
                    shotgun.PlayeShotSound(_isShot);
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
                Destroy(impact, 0.5f);
            }
        }
        _isShot = false;//Need play shot audio
    }

    //TODO: Animation EVENT
    public void StartReload()
    {
        canShot = false;
    }
    public void EndReload()
    {
        canShot = true;
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
