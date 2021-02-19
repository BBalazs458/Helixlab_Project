using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class PlayerStats : MonoBehaviour
{
    [SerializeField] int _currentHP;


    private int _maximumHP = 100;
    private bool _gameOver = false;

    private Animator _playerAnimator;
    private UIManager _uiManager;

    public bool GameOver { get { return _gameOver; } set { _gameOver = value; } }
    public int GetCurrentHP { get { return _currentHP; } }

    
    void Start()
    {
        _currentHP = _maximumHP;

        _uiManager = FindObjectOfType<UIManager>();
        if (_uiManager == null) return;

        _uiManager.SetMaximumHealth(_currentHP);
        _uiManager.UpdateHelathBar(_currentHP);

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

        _uiManager.SetPainHudTransparent(_currentHP);
    }

    public void TakeDamagePlayer(int dmg)
    {
        _currentHP -= dmg;
   
        _uiManager.UpdateHelathBar(_currentHP);

        _playerAnimator.SetTrigger("Hit");
    }

    public void AddHealth(int addHP)
    {
        _currentHP += addHP;
        _uiManager.UpdateHelathBar(_currentHP);
    }

}
