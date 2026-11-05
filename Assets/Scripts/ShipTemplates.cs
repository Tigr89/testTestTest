using UnityEngine;

[CreateAssetMenu(fileName = "ShipTemplates", menuName = "Scriptable Objects/ShipTemplates")]
public class ShipTemplates : ScriptableObject
{
    public Sprite shipGraphics;
    public float speed;
    public bool isPlayer;
    public WeaponSystem weaponSystem;

    public Vector3[] laserPylonOffset;


}
