using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControlObjects : MonoBehaviour
{
    public Sprite hover, basic;
    SpriteRenderer sr;
    

    // Start is called before the first frame update
    void Start()
    {
    	sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
		sr.sprite = hover;
		Debug.Log("Mouse is over GameObject.");

    }

    void OnMouseExit()
    {
    	sr.sprite = basic;
    	Debug.Log("Mouse is no longer on GameObject.");

    }
}
