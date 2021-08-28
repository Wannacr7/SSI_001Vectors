using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Walker))]
public class WalkerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        bool isClicked = GUILayout.Button("Manual Update");
        if (isClicked)
        {
            //Debug.Log("Do something");
            Walker mywalker = target as Walker;
            mywalker.UpdatePosition();
        }
    }
}
