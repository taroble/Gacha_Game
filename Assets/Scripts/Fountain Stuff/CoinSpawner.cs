using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject normalCoin;
    public GameObject rainbowCoin;

    public float minSpawnTimerLength;
    public float maxSpawnTimerLength;

    float spawnTimer;
    Vector2[] spawnPositions;



    void Start()
    {
        GameMaster.instance.UpdateCoinCounter();    //Just slappin this here lol
        spawnTimer = Random.Range(minSpawnTimerLength, maxSpawnTimerLength);

        spawnPositions = new Vector2[transform.childCount];
        int index = 0;
        foreach (Transform child in transform)
        {
            spawnPositions[index] = new Vector2(child.position.x, child.position.y);
            index++;
        }
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer += Random.Range(minSpawnTimerLength, maxSpawnTimerLength);
            Vector2 spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];

            if (Random.Range(0, 100) < 5)
                Instantiate(rainbowCoin, spawnPosition, Quaternion.identity);
            else
                Instantiate(normalCoin, spawnPosition, Quaternion.identity);
        }
    }
}