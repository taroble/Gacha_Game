using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{

	public float coinSpawnTimerMinLength;
	public float coinSpawnTimerMaxLength;
	float coinSpawnTimerLength;
	float coinSpawnTimer;

	public int maxActiveCoins;
	[HideInInspector]
	public int currentNumberOfCoins;
	public GameObject[] coinPrefabs;

	// Start is called before the first frame update
	void Start()
	{
		coinSpawnTimerLength = Random.Range(coinSpawnTimerMinLength, coinSpawnTimerMaxLength);
		coinSpawnTimer = coinSpawnTimerLength;
	}

	// Update is called once per frame
	void Update()
	{
		coinSpawnTimer -= Time.deltaTime;
		if (coinSpawnTimer <= 0)
		{
			if (currentNumberOfCoins < maxActiveCoins)
			{
				currentNumberOfCoins++;
				float randomValue = Random.Range(0f, 100f);

				if (randomValue < 90)
				{
					GameObject newCoin = Instantiate(coinPrefabs[0],
					new Vector2(-10f + Random.Range(0, 3f), 7f - Random.Range(0, 4f)),
					Quaternion.identity);
					newCoin.GetComponent<CoinGold>().coinSpawner = this;
				}
				else
				{
					GameObject newCoin = Instantiate(coinPrefabs[3],
					new Vector2(-10f + Random.Range(0, 3f), 7f - Random.Range(0, 4f)),
					Quaternion.identity);
					newCoin.GetComponent<CoinSilver>().coinSpawner = this;
				}
			}

			coinSpawnTimerLength = Random.Range(coinSpawnTimerMinLength, coinSpawnTimerMaxLength);
			coinSpawnTimer += coinSpawnTimerLength;
		}
	}
}
