using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject menu;

    public void openMenu()
    {
        menu.SetActive(true);
    }

    public void closeMenu()
    {
        menu.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}