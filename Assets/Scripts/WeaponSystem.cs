using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSystem", menuName = "Scriptable Objects/WeaponSystem")]
public class WeaponSystem : ScriptableObject
{
    public float weaponDamage;
    public GameObject weaponPrefab;
    public WeaponType weaponType; 

}

public enum WeaponType
{
    Laser,
    Bullet,
    Plasma
}
