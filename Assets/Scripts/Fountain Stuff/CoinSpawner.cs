using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject[] coins;
    public float minSpawnTimerLength;
    public float maxSpawnTimerLength;
    float spawnTimer;



    void Start()
    {
        GameMaster.instance.UpdateCoinCounter();
        spawnTimer = Random.Range(minSpawnTimerLength, maxSpawnTimerLength);
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer = Random.Range(minSpawnTimerLength, maxSpawnTimerLength);
            float rarity = Random.Range(0f, 100f);
            if (rarity % 5 != 0)
            {
                Instantiate(coins[0], new Vector2(-15f + Random.Range(0, 30f), -8f + Random.Range(0, 17f)), Quaternion.identity);


            }
            else
            {
                Instantiate(coins[1], new Vector2(-15f + Random.Range(0, 30f), -8f + Random.Range(0, 17f)), Quaternion.identity);

            }
        }
    }
}
