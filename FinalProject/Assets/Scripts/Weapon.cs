using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header("Weapon settings")]
    [SerializeField] int _damage;
    [SerializeField] int _shotRange;
    [SerializeField] int _ammoCapacityPerClip;
    [SerializeField] int _inventoryAmmo;
    [SerializeField] AmmoType _weaponAmmoType;

    [Header("Components")]
    [SerializeField] ParticleSystem _muzzleFlash;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject _icon;
    [SerializeField] Text _allAmmoText;

    private Animator _playerAnim;

    [SerializeField]
    private int _currentAmmoInClip;
    private bool isReload = false;

    void Start()
    {
        _currentAmmoInClip = _ammoCapacityPerClip;
        _allAmmoText.text = _inventoryAmmo.ToString();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            return;

        //TODO: setting the range in PlayerShot script
        PlayerShot playerShot = FindObjectOfType<PlayerShot>();
        playerShot.ShotRange = _shotRange;

        _playerAnim = GameObject.Find("Player").GetComponent<Animator>();

        _icon.SetActive(false);

        
    }


    void Update()
    {
        if (gameObject.GetComponent<MeshRenderer>().enabled == true)
        {
            _icon.SetActive(true);
        }
        else
        {
            _icon.SetActive(false);
        }

    }

    #region PROPERTY
    public int GetDamage { get { return _damage; } }
    public bool GetReload { get { return isReload; } }
    public GameObject SetIcon { get { return _icon; } set { _icon = value; } }
    public int GetInventoryAmmo { get { return _inventoryAmmo; } }
    public int GetCurrentAmmo { get { return _currentAmmoInClip; } }
    #endregion


    #region Methods
    public void PlayMuzzleFlash() => _muzzleFlash.Play();

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
        if (_inventoryAmmo > 0)
        {
            if (_currentAmmoInClip == 0)
            {
                isReload = true;
                StartCoroutine(Reloading());
                isReload = false;
            }
        }
        else
        {
            Debug.Log("Out of ammo!");
        }
    }

    IEnumerator Reloading()
    {
        //isReload = true;
        Debug.Log("Reloading...");
        _playerAnim.SetBool("Reloading", true);

        yield return new WaitForSeconds(3f);

        if (_inventoryAmmo < _ammoCapacityPerClip)
        {
            _currentAmmoInClip = _inventoryAmmo;
            _inventoryAmmo -= _currentAmmoInClip;
        }
        else
        {
            _currentAmmoInClip = _ammoCapacityPerClip;
            _inventoryAmmo -= _ammoCapacityPerClip;
        }
        _allAmmoText.text = _inventoryAmmo.ToString();
        _playerAnim.SetBool("Reloading", false);

        //isReload = false;
    }


    public void AddAmmoToInventory(int addAmmo)
    {
        Ammo ammo = FindObjectOfType<Ammo>();
        if (ammo.AmmoType == _weaponAmmoType)
        {
            _inventoryAmmo += addAmmo;
        }
    }


    #endregion
}
