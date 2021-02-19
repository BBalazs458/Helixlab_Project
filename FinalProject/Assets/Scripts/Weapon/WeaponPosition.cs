using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPosition : MonoBehaviour
{
    [Header("Crouch weapon position")]
    public Vector3 newPos;
    public Vector3 newRot;
    [Header("IK hand new position")]
    public Vector3 newIKPos;

    private PlayerMovement playerCrouch;

    public Transform IKControlLeft;

    private Vector3 _originalPos;
    private Vector3 _originalRot;


    // Start is called before the first frame update
    void Start()
    {
        _originalPos = transform.localPosition;
        _originalRot = transform.localEulerAngles;
        playerCrouch = GameObject.Find("Player").GetComponent<PlayerMovement>();


        IKControlLeft.transform.localPosition = new Vector3(-0.04f,-0.038f,0.331f);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCrouch == null)
            Debug.Log("Missing Component");

        if (playerCrouch.IsCrouch)
        {
            gameObject.transform.localPosition = newPos;
            gameObject.transform.localEulerAngles = newRot;

            IKControlLeft.transform.localPosition = newIKPos;
        }
        else
        {
            gameObject.transform.localPosition = _originalPos;
            gameObject.transform.localEulerAngles = _originalRot;

            IKControlLeft.transform.localPosition = new Vector3(-0.04f, -0.038f, 0.331f);
        }
    }

    
}
