using UnityEditor;
using UnityEngine;

public class AxevallaTutorial : EditorWindow
{
    [MenuItem("Tutorial/My Tutorial")]


    public static void ShowWindow()
    {
        GetWindow<AxevallaTutorial>("Tutorial");
        EditorApplication.ExecuteMenuItem("Window/General/Hierarchy");
    }

    private void OnGUI()
    {
        GUILayout.Label("Welcome to the tutorial!");
        GUILayout.Label("Create a new GameObject.");
    }

    private void OnEnable()
    {
        EditorApplication.hierarchyChanged += CheckForNewObject;
    }

    private void OnDisable()
    {
        EditorApplication.hierarchyChanged -= CheckForNewObject;
    }

    private void CheckForNewObject()
    {
        Debug.Log("Hierarchy changed!");
        Close();
    }
}