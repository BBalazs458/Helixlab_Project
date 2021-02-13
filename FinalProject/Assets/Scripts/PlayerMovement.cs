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

    private void Start()
    {
        _cc = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
    }


    private void Update()
    {
        CalculateMove();
    }

    void CalculateMove()
    {
        float hor = Input.GetAxis("Horizontal") * _moveSpeed;
        float ver = Input.GetAxis("Vertical") * _moveSpeed;

        Vector3 movement = new Vector3(hor, 0, ver);
        movement *= Time.deltaTime;
        transform.TransformDirection(movement);


        _cc.Move(movement);
    }


}
