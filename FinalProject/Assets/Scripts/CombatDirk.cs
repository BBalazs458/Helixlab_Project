using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDirk : MonoBehaviour
{
    private Animator _playerStabAnim;

    [SerializeField] GameObject _icon;

    // Start is called before the first frame update
    void Start()
    {
        _playerStabAnim = GameObject.Find("Player").GetComponent<Animator>();
        if (_playerStabAnim == null)
            Debug.LogWarning("Player animator is missing from CombatDirk script!");

        _icon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
