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

    private Weapon _w;
    [SerializeField] AmmoType _type = AmmoType.M4A1;

    public AmmoType AmmoType { get { return _type; } }

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
        

        _w = FindObjectOfType<Weapon>();
        if (_w == null)
            Debug.LogError("Weapon missing!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _w.AddAmmoToInventory(_ammo);
        }
    }

}
