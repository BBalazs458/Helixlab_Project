using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4 : Gun
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        UpdateAmmo(_thisWeaponAllAmmo);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && (_playerShot.CanShot == false && _thisWeaponAllAmmo > 0))
        {
            if (gameObject.GetComponent<MeshRenderer>().enabled)
            {
                StartCoroutine(Reloading());
            }
        }

        if (gameObject.GetComponent<MeshRenderer>().enabled == true)
        {
            icon.SetActive(true);
        }
        else
        {
            icon.SetActive(false);
        }
    }

    #region PROPERTY
    public override int GetDamage { get { return damage; } }
    public override float GetFireRate { get { return fireRate; } }
    #endregion


    IEnumerator Reloading()
    {
        Debug.Log("Reloading...");
        _playerAnimator.SetBool("Reloading", true);

        yield return new WaitForSeconds(3f);

        if (_thisWeaponAllAmmo != 0 && (ammoCapacityPerClip > _thisWeaponAllAmmo))
        {
            _currentAmmoInClip += _thisWeaponAllAmmo;
            _thisWeaponAllAmmo -= _thisWeaponAllAmmo;
        }
        else
        {
            _currentAmmoInClip += ammoCapacityPerClip;
            _thisWeaponAllAmmo -= ammoCapacityPerClip;
        }

        _playerAnimator.SetBool("Reloading", false);
        UpdateAmmo(_thisWeaponAllAmmo);
    }

    public override void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    public override void PlayShotSound(bool shot)
    {
        if (shot && !audioSource.isPlaying)
        {
            audioSource.Play(0);
        }
        if (!shot)
        {
            audioSource.Stop();
        }
    }

    public override void DecreaseAmmo(int minusAmmo)
    {
        if (gameObject.GetComponent<MeshRenderer>().enabled)
        {
            if (_currentAmmoInClip > 0)
            {
                _currentAmmoInClip -= minusAmmo;
                if (_currentAmmoInClip == 0)
                {
                    _playerShot.CanShot = false;
                    Debug.Log(_playerShot.CanShot);
                }
            }
        }
    }

    public override void AddAmmoToInventory(int addAmmo, AmmoType typeAmmo)
    {
        if (ammoType == typeAmmo)
        {
            _thisWeaponAllAmmo += addAmmo;
            UpdateAmmo(_thisWeaponAllAmmo);
        }
    }

    protected override void UpdateAmmo(int inventory)
    {
        ammoCounter.text = inventory.ToString();
    }
}//class
