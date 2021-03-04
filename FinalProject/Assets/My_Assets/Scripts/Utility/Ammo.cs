using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmmoType
{
    M4A1,
    SHOTGUN
}

public class Ammo : MonoBehaviour
{
    private int _ammo;

    [SerializeField] AmmoType type;

    private Gun _m4;
    private Gun _shotgun;

    private void Start()
    {
        _m4 = GameObject.Find("M4A1").GetComponent<M4>();
        _shotgun = GameObject.Find("Puska").GetComponent<Shotgun>();

        switch (type)
        {
            case AmmoType.M4A1:
                _ammo = Random.Range(5, 26);
                break;
            case AmmoType.SHOTGUN:
                _ammo = Random.Range(5, 15);
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _m4.AddAmmoToInventory(_ammo, type);
            Debug.Log("M4 ammo add.");
            Destroy(gameObject);


            _shotgun.AddAmmoToInventory(_ammo, type);
            Debug.Log("Puska ammo add.");
            Destroy(gameObject);


        }
    }

}
