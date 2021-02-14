using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Setting")]
    [SerializeField] float _moveSpeed = 10f;
    [SerializeField] float _jumpHeight = 5f;
    [SerializeField] float _gravity = 2f;
    [Range(0, 10), SerializeField] float _airControl = 5f;
    
    [Header("Mouse horizontal setting")]
    public float turnSpeed = 10f;


    private CharacterController _cc;
    private Animator _anim;

    private Vector3 moveDirection = Vector3.zero;
    private bool _isCrouch = false;

    private float _axisY = 0;
    private Quaternion _bodyStartRotation;
    private Transform _cam;

    private void Start()
    {
        _cc = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();

        _cam = GetComponentInChildren<Camera>().transform;
        _bodyStartRotation = transform.localRotation;

    }


    private void Update()
    {
        CalculateMove();
        Crouch();
        MouseRotation();

    }

    void CalculateMove()
    {
        float hor = Input.GetAxis("Horizontal") * _moveSpeed;
        float ver = Input.GetAxis("Vertical") * _moveSpeed;

        Vector3 movement = new Vector3(hor, 0, ver);
        movement = transform.TransformDirection(movement);
        #region Jump Action
        if (_cc.isGrounded)
        {
            moveDirection = movement;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(2 * _gravity + _jumpHeight);
                _anim.SetTrigger("JumpAction");
            }
            else
            {
                moveDirection.y = 0;
            }
        }
        else
        {
            movement.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, movement, _airControl * Time.deltaTime);
        }
        #endregion
        moveDirection.y -= _gravity * Time.deltaTime;

        _anim.SetFloat("MoveX", hor);
        _anim.SetFloat("MoveY", ver);

        _cc.Move(movement * Time.deltaTime);
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

    void MouseRotation()
    {
        //Mouse input
        float horizontalMouse = Input.GetAxis("Mouse X") * Time.deltaTime * turnSpeed;

        //Update rotation
        _axisY += horizontalMouse;
        
        //Compute rotation Y-axis
        Quaternion bodyRotation = Quaternion.AngleAxis(_axisY, Vector3.up);
        
        //Convert rotation to world
        Quaternion worldRotation = bodyRotation * _bodyStartRotation;
        
        //Creat new rotation
        transform.localRotation = worldRotation;
    }


}
