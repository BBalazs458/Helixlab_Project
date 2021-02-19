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

    [SerializeField] AmmoType _type;

    private Weapon weapon;

    private void Start()
    {

        switch (_type)
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
            
        }
    }

}
