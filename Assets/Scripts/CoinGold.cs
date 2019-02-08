using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGold : MonoBehaviour
{

	[HideInInspector]
	public GameMaster gameMaster;

	void Start()
	{
		GetComponent<SpriteRenderer>().color = Color.cyan;

	}

	void Update()
	{

	}

	void OnMouseOver(){
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log("clicked");

			GetComponent<SpriteRenderer>().color = Color.white;
			gameMaster.quarters = gameMaster.quarters + 5;
			Destroy(gameObject);
		}		
	}
}
