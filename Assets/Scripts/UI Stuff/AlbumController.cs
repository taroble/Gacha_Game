using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumController : MonoBehaviour
{
	public GameObject[] pages;

    public void OpenPage(int page)
	{
		for (int i = 0; i < pages.Length; i++)
		{
			if (i == page) {
				pages[i].SetActive(true);
			}
			else {
				pages[i].SetActive(false);
			}
		}
	}

	public void CloseAll()
	{
		for (int i = 0; i < pages.Length; i++)
		{
			pages[i].SetActive(false);
		}
	}
}
