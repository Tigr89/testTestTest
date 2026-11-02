using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class TutorialWindow : EditorWindow
{
    private TutorialData tutorialData;
    private int currentStepIndex = 0;
    private bool currentStepPassed = false;
    private Vector2 scroll;

    [MenuItem("Tutorial/Open Tutorial Window")]
    public static void ShowWindow()
    {
        var window = GetWindow<TutorialWindow>("Tutorial");
        window.minSize = new Vector2(340, 280);
    }

    private void OnEnable()
    {
        EditorApplication.hierarchyChanged += OnHierarchyChanged;
        EditorApplication.update += OnEditorUpdate;
    }

    private void OnDisable()
    {
        EditorApplication.hierarchyChanged -= OnHierarchyChanged;
        EditorApplication.update -= OnEditorUpdate;
    }

    private void OnHierarchyChanged()
    {
        RunValidation();
        Repaint();
    }

    private void OnEditorUpdate()
    {
        // Polls periodically so field/component changes are caught even without a hierarchy event.
        RunValidation();
    }

    private void OnGUI()
    {
        tutorialData = (TutorialData)EditorGUILayout.ObjectField(
            "Tutorial Asset", tutorialData, typeof(TutorialData), false);

        if (tutorialData == null)
        {
            EditorGUILayout.HelpBox("Assign a TutorialData asset above to begin.", MessageType.Info);
            return;
        }

        if (tutorialData.steps == null || tutorialData.steps.Count == 0)
        {
            EditorGUILayout.HelpBox("This tutorial has no steps yet.", MessageType.Warning);
            return;
        }

        currentStepIndex = Mathf.Clamp(currentStepIndex, 0, tutorialData.steps.Count - 1);
        var step = tutorialData.steps[currentStepIndex];

        EditorGUILayout.LabelField(tutorialData.tutorialTitle, EditorStyles.boldLabel);
        EditorGUILayout.LabelField(
            $"Step {currentStepIndex + 1} / {tutorialData.steps.Count}: {step.title}",
            EditorStyles.boldLabel);

        EditorGUILayout.Space();
        scroll = EditorGUILayout.BeginScrollView(scroll, GUILayout.Height(140));
        EditorGUILayout.LabelField(step.instructions, EditorStyles.wordWrappedLabel);
        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();

        if (step.validationType == StepValidationType.ManualConfirm)
        {
            currentStepPassed = GUILayout.Toggle(currentStepPassed, "I've done this");
        }
        else
        {
            var prevColor = GUI.color;
            GUI.color = currentStepPassed ? Color.green : Color.red;
            EditorGUILayout.LabelField(
                currentStepPassed ? "✓ Detected" : "✗ Not detected yet",
                EditorStyles.boldLabel);
            GUI.color = prevColor;
        }

        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();

        GUI.enabled = currentStepIndex > 0;
        if (GUILayout.Button("Previous"))
        {
            currentStepIndex--;
            RunValidation();
        }
        GUI.enabled = true;

        GUI.enabled = currentStepPassed;
        var isLastStep = currentStepIndex >= tutorialData.steps.Count - 1;
        if (GUILayout.Button(isLastStep ? "Finish" : "Next"))
        {
            if (!isLastStep)
            {
                currentStepIndex++;
                currentStepPassed = false;
                RunValidation();
            }
            else
            {
                Close();
            }
        }
        GUI.enabled = true;

        EditorGUILayout.EndHorizontal();
    }

    private void RunValidation()
    {
        if (tutorialData == null || tutorialData.steps == null || tutorialData.steps.Count == 0)
        {
            currentStepPassed = false;
            return;
        }

        currentStepIndex = Mathf.Clamp(currentStepIndex, 0, tutorialData.steps.Count - 1);
        var step = tutorialData.steps[currentStepIndex];

        switch (step.validationType)
        {
            case StepValidationType.ManualConfirm:
                // Left as-is; the student controls this via the toggle in OnGUI.
                break;

            case StepValidationType.ObjectExists:
                currentStepPassed = GameObject.Find(step.targetObjectName) != null;
                break;

            case StepValidationType.ComponentExists:
                currentStepPassed = FindComponentByTypeName(step.targetObjectName, step.componentTypeName) != null;
                break;

            case StepValidationType.FieldExists:
                currentStepPassed = ComponentHasMatchingField(
                    step.targetObjectName, step.componentTypeName, step.fieldNameContains);
                break;

            case StepValidationType.SpriteAssigned:
                currentStepPassed = CheckSpriteAssigned(step.targetObjectName);
                break;
        }
    }

    private Component FindComponentByTypeName(string objectName, string typeName)
    {
        var obj = GameObject.Find(objectName);
        if (obj == null) return null;

        return obj.GetComponents<Component>()
            .FirstOrDefault(c => c != null && c.GetType().Name == typeName);
    }

    private bool ComponentHasMatchingField(string objectName, string typeName, string nameContains)
    {
        var obj = GameObject.Find(objectName);
        if (obj == null) return false;

        // If a specific component type is given, check just that one; otherwise check every MonoBehaviour.
        var components = string.IsNullOrEmpty(typeName)
            ? obj.GetComponents<MonoBehaviour>().Cast<Component>()
            : obj.GetComponents<Component>().Where(c => c != null && c.GetType().Name == typeName);

        foreach (var comp in components)
        {
            if (comp == null) continue;
            var fields = comp.GetType().GetFields(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (fields.Any(f => f.Name.IndexOf(nameContains, StringComparison.OrdinalIgnoreCase) >= 0))
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckSpriteAssigned(string objectName)
    {
        var obj = GameObject.Find(objectName);
        if (obj == null) return false;

        var renderer = obj.GetComponent<SpriteRenderer>();
        return renderer != null && renderer.sprite != null;
    }
}
