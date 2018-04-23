using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Immobilizer))]
[CanEditMultipleObjects]
public class ImmobilizerEditor : Editor
{

    void OnSceneGUI()
    {
        Immobilizer fow = (Immobilizer)target;
        Handles.color = Color.cyan;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.radius);



    }
}
