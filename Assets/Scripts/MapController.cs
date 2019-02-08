using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
	public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openMenu(){
    	menu.SetActive(true);
	}

	public void closeMenu(){
		menu.SetActive(false);
	}

	public void quitGame(){
		Application.Quit();
			}
}
