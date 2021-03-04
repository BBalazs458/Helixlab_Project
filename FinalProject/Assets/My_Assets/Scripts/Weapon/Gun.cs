using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Gun : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected int range;
    [Range(0f,5f)]
    [SerializeField] protected float fireRate;
    [SerializeField] protected int ammoCapacityPerClip;
    [SerializeField] protected AmmoType ammoType;

    [SerializeField] protected ParticleSystem muzzleFlash;
    [SerializeField] protected AudioSource audioSource;
    [SerializeField] protected GameObject icon;
    [SerializeField] protected Text ammoCounter;

    protected Animator _playerAnimator;
    protected PlayerShot _playerShot;

    protected int _currentAmmoInClip;
    [SerializeField]
    protected int _thisWeaponAllAmmo;

    protected void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) return;

        _currentAmmoInClip = ammoCapacityPerClip;

        //TODO: setting the range in PlayerShot script
        _playerShot = GameObject.Find("Player").GetComponent<PlayerShot>();
        _playerShot.ShotRange = range;

        _playerAnimator = GameObject.Find("Player").GetComponent<Animator>();

        icon.SetActive(false);
    }

    #region PROPERTY
    public virtual int GetDamage { get { return damage; } }
    public virtual float GetFireRate { get { return fireRate; } }
    #endregion

    public abstract void PlayMuzzleFlash();
    public abstract void PlayShotSound(bool shot);
    public abstract void DecreaseAmmo(int minusAmmo);
    public abstract void AddAmmoToInventory(int addAmmo, AmmoType typeAmmo);
    protected abstract void UpdateAmmo(int inventory);

}//class
