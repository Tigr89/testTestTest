using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class AxevallaAssignment1Setup
{
    private const string FolderPath = "Assets/Tutorial/Assignment1";

    [MenuItem("Tutorial/Create Assignment 1 Tutorial Asset")]
    public static void CreateAssignment1Tutorial()
    {
        if (!AssetDatabase.IsValidFolder("Assets/Tutorial"))
            AssetDatabase.CreateFolder("Assets", "Tutorial");
        if (!AssetDatabase.IsValidFolder(FolderPath))
            AssetDatabase.CreateFolder("Assets/Tutorial", "Assignment1");

        var step1 = CreateStep(
            "Skapa spelaren",
            "Skapa ett nytt GameObject och döp det till \"Player\".",
            StepValidationType.ObjectExists,
            targetObjectName: "Player");

        var step2 = CreateStep(
            "Förflyttning",
            "Lägg till ett script på \"Player\" som låter spelaren röra sig i X- och Y-led vid input, " +
            "med en wraparound-effekt när spelaren lämnar spelytan.\n\n" +
            "(Detta steg markeras manuellt eftersom själva rörelsebeteendet är svårt att verifiera automatiskt.)",
            StepValidationType.ManualConfirm,
            targetObjectName: "Player");

        var step3 = CreateStep(
            "Skjutmekanik",
            "Skapa ett laser-objekt som skapas när spelaren trycker på mellanslag, som förstör sig själv " +
            "efter ett antal sekunder, och som har en cooldown så spelaren inte kan spamma.\n\n" +
            "(Detta steg markeras manuellt.)",
            StepValidationType.ManualConfirm,
            targetObjectName: "Player");

        var step4 = CreateStep(
            "Hälsa/liv",
            "Lägg till variabler för hälsa och extra liv i ditt spelar-script.",
            StepValidationType.FieldExists,
            targetObjectName: "Player",
            fieldNameContains: "health");

        var step5 = CreateStep(
            "Sprite",
            "Lägg till en Sprite Renderer på \"Player\" och applicera en sprite.",
            StepValidationType.SpriteAssigned,
            targetObjectName: "Player");

        var tutorial = ScriptableObject.CreateInstance<TutorialData>();
        tutorial.tutorialTitle = "Uppgift 1: Skapa spelaren";
        tutorial.steps = new List<TutorialStepData> { step1, step2, step3, step4, step5 };

        var tutorialPath = $"{FolderPath}/Assignment1Tutorial.asset";
        AssetDatabase.CreateAsset(tutorial, tutorialPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Selection.activeObject = tutorial;
        EditorGUIUtility.PingObject(tutorial);
        Debug.Log($"Created tutorial asset at {tutorialPath}");
    }

    private static TutorialStepData CreateStep(
        string title,
        string instructions,
        StepValidationType type,
        string targetObjectName = "",
        string componentTypeName = "",
        string fieldNameContains = "")
    {
        var step = ScriptableObject.CreateInstance<TutorialStepData>();
        step.title = title;
        step.instructions = instructions;
        step.validationType = type;
        step.targetObjectName = targetObjectName;
        step.componentTypeName = componentTypeName;
        step.fieldNameContains = fieldNameContains;

        var safeName = title.Replace(" ", "_").Replace("/", "_");
        var path = $"{FolderPath}/Step_{safeName}.asset";
        AssetDatabase.CreateAsset(step, path);
        return step;
    }
}
