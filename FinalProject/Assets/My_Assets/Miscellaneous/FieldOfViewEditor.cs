using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

        Vector3 viewAngel01 = DirectionFromAngel(fov.transform.eulerAngles.y, -fov.angel / 2);
        Vector3 viewAngel02 = DirectionFromAngel(fov.transform.eulerAngles.y, fov.angel / 2);


        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngel01 * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngel02 * fov.radius);


        if (fov.canSeePlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.playerRef.transform.position);
        }
        
    }

    private Vector3 DirectionFromAngel(float eulerY,float angelInDegrees)
    {
        angelInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angelInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angelInDegrees * Mathf.Deg2Rad));
    }

}
