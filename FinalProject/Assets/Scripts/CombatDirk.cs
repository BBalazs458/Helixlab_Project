using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDirk : MonoBehaviour
{
    private Animator _playerStabAnim;

    // Start is called before the first frame update
    void Start()
    {
        _playerStabAnim = GameObject.Find("Player").GetComponent<Animator>();
        if (_playerStabAnim == null)
            Debug.LogWarning("Player animator is missing from CombatDirk script!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _playerStabAnim.SetTrigger("Stabbing");
        }
    }
}
