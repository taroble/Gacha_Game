using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinHolder;

    public GameObject[] normalCoins;
    public GameObject[] rareCoins;
    public GameObject[] ultraRareCoins;

    public float minSpawnTimerLength;
    public float maxSpawnTimerLength;
    public int maxNumberOfCoinsOnScreen;

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

            if (coinHolder.transform.childCount < maxNumberOfCoinsOnScreen)
            {
                Vector2 spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];
                int d100 = Random.Range(0, 100);

                //Ultra rare (1%)
                if (d100 == 0)
                {
                    GameObject urCoin = Instantiate(ultraRareCoins[Random.Range(0, ultraRareCoins.Length)], spawnPosition, Quaternion.identity);
                    urCoin.transform.parent = coinHolder.transform;
                }

                //Rare (5%)
                else if (d100 > 0 && d100 < 6)
                {
                    GameObject rCoin = Instantiate(rareCoins[Random.Range(0, rareCoins.Length)], spawnPosition, Quaternion.identity);
                    rCoin.transform.parent = coinHolder.transform;
                }

                //Common (94%)
                else
                {
                    GameObject nCoin = Instantiate(normalCoins[Random.Range(0, normalCoins.Length)], spawnPosition, Quaternion.identity);
                    nCoin.transform.parent = coinHolder.transform;
                }
            }
        }
    }
}