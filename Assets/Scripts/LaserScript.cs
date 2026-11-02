using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public float speed;
    public bool rainBowLaser;
    SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if(rainBowLaser == true)
        {
            int randomColor = Random.Range(0, 6);
            switch (randomColor)
            {
                case 0:
                    sr.color = Color.red;
                    break;
                case 1:
                    sr.color = Color.blue;
                    break;
                case 3:
                    sr.color = Color.yellow;
                    break;
                case 4:
                    sr.color = Color.green;
                    break;
                case 5:
                    sr.color = Color.hotPink;
                    break;
                case 6:
                    sr.color = Color.turquoise;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
