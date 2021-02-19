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

    private Weapon _m4;
    private Weapon _shotgun;

    private void Start()
    {
        _m4 = GameObject.Find("M4A1").GetComponent<Weapon>();
        _shotgun = GameObject.Find("Puska").GetComponent<Weapon>();

        switch (type)
        {
            case AmmoType.M4A1:
                _ammo = Random.Range(5, 26);
                break;
            case AmmoType.SHOTGUN:
                _ammo = 5;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_m4.GetComponent<MeshRenderer>().enabled == true)
            {
                _m4.AddAmmoToInventory(_ammo, type);
                Debug.Log("M4 ammo add.");
                Destroy(gameObject);
            }
            else if (_shotgun.GetComponent<MeshRenderer>().enabled == true)
            {
                _shotgun.AddAmmoToInventory(_ammo, type);
                Debug.Log("Puska ammo add.");
                Destroy(gameObject);
            }
            
        }
    }

}
