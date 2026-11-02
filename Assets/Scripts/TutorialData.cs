using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTutorial", menuName = "Tutorial/Tutorial Sequence")]
public class TutorialData : ScriptableObject
{
    public string tutorialTitle = "Assignment Tutorial";
    public List<TutorialStepData> steps = new List<TutorialStepData>();
}
