using UnityEngine;

public enum StepValidationType
{
    ManualConfirm,      // Student ticks a checkbox - no automatic check (use for behavior that's hard to detect)
    ObjectExists,       // A GameObject with a given name exists in the scene
    ComponentExists,    // A GameObject with a given name has a component of a given type name
    FieldExists,        // A component on the object has a field whose name contains a given string
    SpriteAssigned      // A GameObject's SpriteRenderer has a non-null sprite assigned
}

[CreateAssetMenu(fileName = "NewTutorialStep", menuName = "Tutorial/Tutorial Step")]
public class TutorialStepData : ScriptableObject
{
    public string title = "New Step";

    [TextArea(4, 12)]
    public string instructions = "Describe what the student should do here.";

    public StepValidationType validationType = StepValidationType.ManualConfirm;

    [Header("Used by ObjectExists / ComponentExists / FieldExists / SpriteAssigned")]
    public string targetObjectName = "Player";

    [Header("Used by ComponentExists / FieldExists (leave empty to check all MonoBehaviours)")]
    public string componentTypeName = "";

    [Header("Used by FieldExists - matches field names containing this text (case-insensitive)")]
    public string fieldNameContains = "health";
}
