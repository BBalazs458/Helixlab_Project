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
    [SerializeField] GameObject ammoType;


    private Animator _playerAnim;

    [SerializeField]
    private int _currentAmmoInClip;
    private bool isReload = false;

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
            if (_currentAmmoInClip <= 0)
            {
                StartCoroutine(Reloading());
            }
        }
        else
        {
            Debug.Log("Out of ammo!");
        }
    }

    IEnumerator Reloading()
    {
        isReload = true;
        Debug.Log("Reloading...");
        _playerAnim.SetBool("Reloading", true);

        yield return new WaitForSeconds(3f);

        _currentAmmoInClip = _ammoCapacityPerClip;
        _inventoryAmmo -= _ammoCapacityPerClip;
        _playerAnim.SetBool("Reloading", false);

        isReload = false;
    }


    public void AddAmmoToInventory()
    {

    }


    #endregion
}
