using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 10;
    [SerializeField] float _jumpHeight = 5;

    private CharacterController _cc;
    private Animator _anim;

    private bool _isCrouch = false;

    private void Start()
    {
        _cc = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
    }


    private void Update()
    {
        CalculateMove();
        Crouch();
    }

    void CalculateMove()
    {
        float hor = Input.GetAxis("Horizontal") * _moveSpeed;
        float ver = Input.GetAxis("Vertical") * _moveSpeed;

        Vector3 movement = new Vector3(hor, 0, ver);
        movement *= Time.deltaTime;
        transform.TransformDirection(movement);

        _anim.SetFloat("MoveX", hor);
        _anim.SetFloat("MoveY", ver);

        _cc.Move(movement);
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C) && _isCrouch == false)
        {
            _anim.SetBool("Crouch", true);
            _isCrouch = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && _isCrouch)
        {
            _anim.SetBool("Crouch", false);
            _isCrouch = false;
        }
    }
}
