using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerShot : MonoBehaviour
{
    [Header("Shot setting")]
    [SerializeField] GameObject impactEffect;
    [SerializeField] Camera _main;
    [SerializeField] float fireRate;

    [SerializeField]
    private float _shotRange;

    private bool _isShot = false;
    private bool canShot = true;
    private float nextFire = 0.0f;

    Weapon m4;
    Weapon shotgun;

    public float ShotRange
    { 
        //get { return _shotRange; }
        set { _shotRange = value; } 
    }


    private void Start()
    {
        m4 = GameObject.Find("M4A1").GetComponent<Weapon>();
        shotgun = GameObject.Find("Puska").GetComponent<Weapon>();

        if (_main == null)
            throw new System.Exception("Main camera is missing!");

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
         if (Input.GetButtonDown("Fire1") && (canShot == true && Time.time > nextFire) && shotgun.gameObject.GetComponent<MeshRenderer>().enabled == true)
         {
            _isShot = true;//Need play shot audio
            shotgun.PlayMuzzleFlash();
            shotgun.PlayeShotSound(_isShot);
            //if (shotgun.gameObject.GetComponent<MeshRenderer>().enabled == true)
            //{
                _shotRange = 25;
                nextFire = Time.time + shotgun.GetFireRate;
                Shot(shotgun);
                
                shotgun.DecreaseAmmo();
            //}
         }
         else
         {
             if (Input.GetButton("Fire1") && (canShot == true && Time.time > nextFire) && m4.gameObject.GetComponent<MeshRenderer>().enabled == true)
             {
                _isShot = true;//Need play shot audio
                m4.PlayMuzzleFlash();
                m4.PlayeShotSound(_isShot);
                //if (m4.gameObject.GetComponent<MeshRenderer>().enabled == true)
                //{
                        _shotRange = 40;
                        nextFire = Time.time + m4.GetFireRate;
                        Shot(m4);

                        m4.DecreaseAmmo();
                //}
             }
         }
        _isShot = false;//Need play shot audio
    }

    void Shot(Weapon w)
    {
        if (w.GetInventoryAmmo >= 0 && w.GetCurrentAmmo >= 0)
        {
            Ray ray = _main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            

            if (Physics.Raycast(ray, out hit, _shotRange))
            {
                Debug.Log(hit.collider.gameObject.name);
                
                Zombie z = hit.transform.GetComponent<Zombie>();
                if (z != null)
                {
                    z.Damage(w.GetDamage);
                }
                #region Shotgun shot
                //if (shotgun.gameObject.GetComponent<MeshRenderer>().enabled == true)
                //{
                //    shotgun.PlayeShotSound(_isShot);
                //    shotgun.PlayMuzzleFlash();
                //    shotgun.DecreaseAmmo();
                //    Zombie z = hit.transform.GetComponent<Zombie>();
                //    if (z != null)
                //    {
                //        z.Damage(shotgun.GetDamage);
                //    }
                //}
                #endregion
                //Impact effect 
                GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 0.5f);
            }
            
        }
        
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
}
