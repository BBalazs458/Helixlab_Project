using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerShot : MonoBehaviour
{
    [Header("Shot setting")]
    [SerializeField] GameObject impactEffect;
    [SerializeField] Camera _main;

    [SerializeField]
    private float _shotRange;
    [SerializeField]
    private bool canShot = true;
    private float nextFire = 0.0f;

    Gun m4;
    Gun shotgun;


    public float ShotRange
    { 
        //get { return _shotRange; }
        set { _shotRange = value; } 
    }
    public bool CanShot
    {
        get { return canShot; }
        set { canShot = value; }
    }

    private void Start()
    {
        m4 = GameObject.Find("M4A1").GetComponent<M4>();
        shotgun = GameObject.Find("Puska").GetComponent<Shotgun>();


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
            _shotRange = 25;
            shotgun.PlayShotSound(true);
            shotgun.PlayMuzzleFlash();
            Shot(shotgun);
            shotgun.DecreaseAmmo(1);
            nextFire = Time.time + shotgun.GetFireRate;
        }
        else
        {
            if (Input.GetButton("Fire1") && (canShot == true && Time.time > nextFire) && m4.gameObject.GetComponent<MeshRenderer>().enabled == true)
            {
                _shotRange = 40;
                m4.PlayShotSound(true);
                m4.PlayMuzzleFlash();
                Shot(m4);
                m4.DecreaseAmmo(3);
                nextFire = Time.time + m4.GetFireRate;
            }
        }

        
    }

    void Shot(Gun w)
    {
            Ray ray = _main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _shotRange))
            {
                Debug.Log(hit.collider.gameObject.name);
                
                ZombieAI z = hit.transform.GetComponent<ZombieAI>();
                if (z != null)
                {
                    z.Damage(w.GetDamage);
                    //Blood effect or something else
                }
                else
                {
                    //Impact effect 
                    GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impact, 2f);
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
