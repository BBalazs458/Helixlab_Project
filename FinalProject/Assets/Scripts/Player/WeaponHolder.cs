using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    public int selectedWeapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        SelectedWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount -1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }
        if (previousWeapon != selectedWeapon)
        {
            SelectedWeapon();
        }
    }

    void SelectedWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.GetComponent<MeshRenderer>().enabled = true;
                weapon.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
            }
            else
            {
                weapon.gameObject.GetComponent<MeshRenderer>().enabled = false;
                weapon.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            }
            i++;
        }
    }

}
