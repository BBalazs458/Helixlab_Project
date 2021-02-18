using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class PlayerStats : MonoBehaviour
{
    [SerializeField] Slider _healthSlider;
    [SerializeField] int _currentHP;
    [SerializeField] Image _painHud;

    private int _maximumHP = 100;
    private bool _gameOver = false;
    private Animator _playerAnimator;

    public bool GetGameOver { get { return _gameOver; } }
    public int GetCurrentHP { get { return _currentHP; } }

    
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

        SetPainHudTransparent(_currentHP);
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

    void SetPainHudTransparent(int health)
    {
        if (health <= 70 && health > 50)
        {
            _painHud.color = new Color(_painHud.color.r, _painHud.color.g, _painHud.color.b, 0.2f);
        }
        else if (health <= 50 && health > 20)
        {
            _painHud.color = new Color(_painHud.color.r, _painHud.color.g, _painHud.color.b, 0.5f);
        }
        else if (health <= 20)
        {
            _painHud.color = new Color(_painHud.color.r, _painHud.color.g, _painHud.color.b, 1);
        }
        else
        {
            _painHud.color = new Color(_painHud.color.r, _painHud.color.g, _painHud.color.b, 0);
        }
    }


}
