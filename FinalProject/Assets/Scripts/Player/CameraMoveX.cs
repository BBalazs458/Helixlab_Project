using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveX : MonoBehaviour
{
    [Header("Mouse Setting")]
    public float turnSpeedX = 100;
    public float upperLimit = 85f;
    public float lowerLimit = -80f;
    public bool inversLook = true;

    public Transform spine;

    private Transform _cam;
    private Quaternion _startRotation;
    private float _axisX = 0;

    // Start is called before the first frame update
    void Start()
    {
        _cam = GetComponent<Camera>().transform;
        _startRotation = _cam.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        LookXAxis();
    }

    void LookXAxis()
    {
        float verticalMouse = Input.GetAxis("Mouse Y") * turnSpeedX * Time.deltaTime;

        if (inversLook)
        {
            _axisX += verticalMouse;
        }
        else
        {
            _axisX += verticalMouse * -1;
        }

        _axisX = Mathf.Clamp(_axisX, lowerLimit, upperLimit);

        Quaternion rotation = Quaternion.AngleAxis(_axisX, Vector3.right);

        Quaternion world = rotation * _startRotation;

        _cam.transform.localRotation = world;
    }
}
