using UnityEngine;
using UnityEngine.U2D;

public class PlayerBehaviour : MonoBehaviour
{
    public ShipTemplates template;
    SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = template.shipGraphics;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateShip()
    {
        sr.sprite = template.shipGraphics;
        
    }
}
