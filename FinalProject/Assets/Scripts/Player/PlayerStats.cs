using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class PlayerStats : MonoBehaviour
{
    [SerializeField] Slider _healthSlider;
    [SerializeField] int _currentHP;

    private int _maximumHP = 100;
    private bool _gameOver = false;
    private Animator _playerAnimator;

    public bool GetGameOver { get { return _gameOver; } }

    
    void Start()
    {
        _currentHP = _maximumHP;
        _healthSlider.maxValue = _currentHP;
        _healthSlider.value = _currentHP;

        _playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
        if (_playerAnimator == null)
            Debug.LogWarning("Player Animator missing from PlayerStats script!");
    }

    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamagePlayer(10);
        }

        if (_currentHP <= 0)
        {
            _gameOver = true;
            _playerAnimator.SetBool("IsDead", true);
        }
    }

    public void TakeDamagePlayer(int dmg)
    {
        _currentHP -= dmg;
        _healthSlider.value = _currentHP;
        _playerAnimator.SetTrigger("Hit");
    }

    public void AddHealth(int addHP)
    {
        _currentHP += addHP;
        _healthSlider.value = _currentHP;
    }
}
