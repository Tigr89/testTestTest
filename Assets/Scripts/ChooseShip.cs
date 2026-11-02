using UnityEngine;

public class ChooseShip : MonoBehaviour
{
    public PlayerBehaviour playerBehaviour;
    public void SelectShip(ShipTemplates _template)
    {
        playerBehaviour.template = _template;
        playerBehaviour.UpdateShip();
    }
}
