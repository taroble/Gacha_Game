using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void sceneHome(){
		SceneManager.LoadScene("HomeScene");
	}

	public void sceneMap(){
		SceneManager.LoadScene("MapScene");
	}

	public void sceneFountain(){
		SceneManager.LoadScene("FountainScene");
	}

	public void sceneStore(){
		SceneManager.LoadScene("StoreScene");
	}

}
