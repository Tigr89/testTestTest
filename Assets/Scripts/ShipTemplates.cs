using UnityEngine;

[CreateAssetMenu(fileName = "ShipTemplates", menuName = "Scriptable Objects/ShipTemplates")]
public class ShipTemplates : ScriptableObject
{
    public Sprite shipGraphics;
    public bool isPlayer;
}
