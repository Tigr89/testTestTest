using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    GameObject enemyPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(enemySpawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator enemySpawner()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-7, 7), 6);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        yield return null;
    }
}
