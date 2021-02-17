using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon settings")]
    [SerializeField] int _damage;
    [SerializeField] int _shotRange;
    [SerializeField] int _ammoCapacityPerClip;
    [SerializeField] int _inventoryAmmo;
    [SerializeField] ParticleSystem _muzzleFlash;
    [SerializeField] AudioSource audioSource;


    private Animator _playerAnim;

    [SerializeField]
    private int _currentAmmoInClip;
    private bool isReload = false;

    //private WeaponHolder _weaponIndex;


    void Start()
    {
        _currentAmmoInClip = _ammoCapacityPerClip;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            return;

        PlayerShot playerShot = FindObjectOfType<PlayerShot>();
        playerShot.ShotRange = _shotRange;

        _playerAnim = GameObject.Find("Player").GetComponent<Animator>();
    }


    private void OnEnable()
    {
        isReload = false;
    }

    void Update()
    {
        
    }

    public int GetDamage { get { return _damage; } }
    public bool GetReload { get { return isReload; } }
    #region Functions
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
        _playerAnim.SetBool("Reloading", true);

        yield return new WaitForSeconds(3f);

        _currentAmmoInClip = _ammoCapacityPerClip;
        _playerAnim.SetBool("Reloading", false);

        isReload = false;
    }
    #endregion
}
