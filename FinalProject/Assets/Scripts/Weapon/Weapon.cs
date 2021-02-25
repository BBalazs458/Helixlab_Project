using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//--

public class Weapon : MonoBehaviour
{
    [Header("Weapon settings")]
    [SerializeField] int _damage;
    [SerializeField] int _range;
    [Range(0f,5f),SerializeField] float _fireRate;
    [SerializeField] int _ammoCapacityPerClip;
    [SerializeField] int _thisWeaponAllAmmo;
    [SerializeField] AmmoType _weaponAmmoType;

    [Header("Components")]
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject icon;
    [SerializeField] Text _ammoCounterText;

    private Animator _playerAnim;
    private PlayerShot _playerShot;

    [SerializeField]
    private int _currentAmmoInClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) return;


        _currentAmmoInClip = _ammoCapacityPerClip;
        UpdateAmmoCounter(_thisWeaponAllAmmo);


        //TODO: setting the range in PlayerShot script
        _playerShot = GameObject.Find("Player").GetComponent<PlayerShot>();
        _playerShot.ShotRange = _range;

        _playerAnim = GameObject.Find("Player").GetComponent<Animator>();

        icon.SetActive(false);

        
    }


    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R) && _currentAmmoInClip <= 0)
        {
            StartCoroutine(Reloading());
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
    public int GetDamage { get { return _damage; } }
    public int GetCurrentAmmo { get { return _currentAmmoInClip; } }
    public int GetInventoryAmmo { get { return _thisWeaponAllAmmo; } }
    public float GetFireRate { get { return _fireRate; } }
    #endregion


    #region Methods
    public void PlayMuzzleFlash() => muzzleFlash.Play();

    public void PlayeShotSound(bool shot)
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

    public void DecreaseAmmo()
    {
        _currentAmmoInClip--;
        if (_thisWeaponAllAmmo > 0 && _currentAmmoInClip <= 0)
        {
            _currentAmmoInClip = 0;
            StartCoroutine(Reloading());
        }
        else
        {
            Debug.Log("Out of ammo!");
        }
    }

    IEnumerator Reloading()
    {
        Debug.Log("Reloading...");
        _playerAnim.SetBool("Reloading", true);

        yield return new WaitForSeconds(3f);
        _currentAmmoInClip = 0;

        if (_thisWeaponAllAmmo < _ammoCapacityPerClip)
        {
            _currentAmmoInClip += _thisWeaponAllAmmo;
            _thisWeaponAllAmmo -= _currentAmmoInClip;
        }
        else
        {
           _currentAmmoInClip += _ammoCapacityPerClip;
           _thisWeaponAllAmmo -= _ammoCapacityPerClip;
        }

        UpdateAmmoCounter(_thisWeaponAllAmmo);
        _playerAnim.SetBool("Reloading", false);
    }


    public void AddAmmoToInventory(int addAmmo, AmmoType typeAmmo)
    {

        if (_weaponAmmoType == typeAmmo)
        {
            _thisWeaponAllAmmo += addAmmo;
            UpdateAmmoCounter(_thisWeaponAllAmmo);
        }
    }

    void UpdateAmmoCounter(int inventory)
    {
        _ammoCounterText.text = inventory.ToString();
    }
    #endregion
}
