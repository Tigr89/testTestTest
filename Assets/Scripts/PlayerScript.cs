using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int playerSpeed = 3;
    public float fireRate = 0.5f;
    float fireRateMax;
    bool canFire = true;
    public GameObject laserShot;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector2(0, -4);
        fireRateMax = fireRate;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * playerSpeed * Time.deltaTime);
            //transform.position = transform.position + Vector3.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * playerSpeed * Time.deltaTime);
            //transform.position = transform.position + Vector3.left;
        }

        if (Input.GetKey(KeyCode.Space) && canFire == true)
        {
            canFire = false;
            Instantiate(laserShot, transform.position, Quaternion.identity);
        }
        if (fireRate > 0 && canFire == false)
        {
            fireRate -= Time.deltaTime;
        }
        else
        {
            canFire = true;
            fireRate = fireRateMax;
            
        }
    }


}
