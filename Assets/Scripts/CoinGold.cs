using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGold : MonoBehaviour
{
	
	bool clicked;
	public int coinValue;

	public float minLifeLength;
	public float maxLifeLength;
	float lifeTimerLength;
	float lifeTimer;

	[HideInInspector]
	public CoinSpawner coinSpawner;



	void Start()
	{
		lifeTimerLength = Random.Range(minLifeLength, maxLifeLength);
		lifeTimer = lifeTimerLength;

		transform.localScale = new Vector2(0.2f, 0.2f);
		GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
		
		
		GetComponent<Renderer>().material.color = Color.cyan;

	}

	void Update()
	{
		if (!clicked)
		{
			GetComponent<Renderer>().material.color = Color.white;
			lifeTimer -= Time.deltaTime;
			if (lifeTimer <= 0)
			{
				lifeTimer = 999;	//bluh
				coinSpawner.currentNumberOfCoins--;
				Destroy(gameObject);
			}
		}
	}


	
}
