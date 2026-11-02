using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float moveSpeed;
    public float moveDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdatePosition();
        StartCoroutine(EnemyMovement());
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);

        


        if(transform.position.y < -6f)
        {
            UpdatePosition();
        }
    }

    void UpdatePosition()
    {
        float randomPosition = Random.Range(-10, 10);
        Debug.Log("Teleporting");
        transform.position = new Vector2(randomPosition, 6);

    }

    IEnumerator EnemyMovement()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            transform.position = transform.position + Vector3.down * moveDistance;
        }
    }
}
