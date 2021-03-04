using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDirk : MonoBehaviour
{
    private Animator _playerStabAnim;

    [SerializeField] GameObject _icon;

    void Start()
    {
        _playerStabAnim = GameObject.Find("Player").GetComponent<Animator>();
        if (_playerStabAnim == null)
            Debug.LogWarning("Player animator is missing from CombatDirk script!");

        _icon.SetActive(false);
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && gameObject.GetComponent<MeshRenderer>().enabled == true)
        {
            _playerStabAnim.SetTrigger("Stabbing");
        }

        if (gameObject.GetComponent<MeshRenderer>().enabled == true)
        {
            _icon.SetActive(true);
            gameObject.GetComponent<MeshCollider>().enabled = true;
        }
        else
        {
            _icon.SetActive(false);
            gameObject.GetComponent<MeshCollider>().enabled = false;
        }
    }
}
