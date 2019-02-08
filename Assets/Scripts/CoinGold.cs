using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGold : MonoBehaviour
{

    private AudioSource aSource;

	void Start()
	{
		GetComponent<SpriteRenderer>().color = Color.cyan;
		aSource = GetComponent<AudioSource>();
	}

	void Update()
	{

	}

	void OnMouseOver(){
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log("clicked");
			aSource.Play();

			GetComponent<SpriteRenderer>().color = Color.white;
			GameMaster.instance.quarters = GameMaster.instance.quarters + 5;
			Destroy(gameObject);
		}		
	}
}
