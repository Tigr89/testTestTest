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
        if (this.GetComponent<PlayerBehaviour>().template != template) UpdateShip();

        if (Input.GetKeyDown(KeyCode.Space)) FireWeapon();

        float horizontalMovement = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * template.speed * horizontalMovement * Time.deltaTime);
    }

    public void UpdateShip()
    {
        sr.sprite = template.shipGraphics;

        
        
    }

    public void FireWeapon()
    {
        if (template.weaponSystem.weaponType == WeaponType.Laser)
        {
            foreach(Vector3 pylon in template.laserPylonOffset)
            {
                Instantiate(template.weaponSystem.weaponPrefab, transform.position + pylon, Quaternion.identity);
            }
        }
        else if(template.weaponSystem.weaponType == WeaponType.Plasma)
        {

        }
        else if(template.weaponSystem.weaponType == WeaponType.Bullet)
        {

        }
    }
}
