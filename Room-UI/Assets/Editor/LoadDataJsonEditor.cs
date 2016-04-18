using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoadDataJson))]
public class LoadDataJsonEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("PressMeToLoadJson"))
        {
            Debug.Log(" Loading.... ");
            LoadDataJsonController ld = new LoadDataJsonController();
            ld.Execute();
        }
    }
}
