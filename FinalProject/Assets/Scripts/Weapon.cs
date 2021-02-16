using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] int _damage;
    [SerializeField] int _shotRange;
    [SerializeField] int _ammoCapacityPerClip;
    [SerializeField] ParticleSystem _muzzleFlash;
    [SerializeField] AudioSource audioSource;



    //[SerializeField]
    //private int _selectedIndex;
    private int _currentAmmoInClip;
    private bool isReload = false;

    //private WeaponHolder _weaponIndex;


    void Start()
    {
        _currentAmmoInClip = _ammoCapacityPerClip;

        audioSource = GetComponent<AudioSource>();

        //_weaponIndex = GameObject.Find("WEAPONHOLDER").GetComponent<WeaponHolder>();
        //_weaponIndex.selectedWeapon = _selectedIndex;

        PlayerShot playerShot = FindObjectOfType<PlayerShot>();
        playerShot.ShotRange = _shotRange;
    }


    private void OnEnable()
    {
        isReload = false;
    }

    void Update()
    {
        
    }

    public int GetDamage { get { return _damage; } }

    public void PlayMuzzleFlash() => _muzzleFlash.Play();

    public void PlayeShotSound()
    {
        audioSource.Play(0);
    }

    public void DecreaseAmmo()
    {
        _currentAmmoInClip--;
        if (_currentAmmoInClip <= 0)
        {
            StartCoroutine(Reloading());
        }
    }

    IEnumerator Reloading()
    {
        isReload = true;
        Debug.Log("Reloading...");
        yield return new WaitForSeconds(0.5f);
        _currentAmmoInClip = _ammoCapacityPerClip;
        isReload = false;
    }

}
