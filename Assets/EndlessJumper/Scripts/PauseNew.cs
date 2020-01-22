using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseNew : MonoBehaviour
{
    GameObject[] pauseObjects;
    public Canvas pausenew;

    // Use this for initialization
    void Start()
    {
        pausenew.enabled = false;
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
    }

    // Update is called once per frame
    public void Pause()
    {

        //uses the p button to pause and unpause the game
        pausenew.enabled = true;

        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            Debug.Log("high");
            Time.timeScale = 1;
            hidePaused();
        }
    }


    //Reloads the Level
    public void Reload()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    //controls the pausing of the scene
    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }

    //shows objects with ShowOnPause tag
    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    //loads inputted level
    public void LoadLevel(int level)
    {
        Application.LoadLevel(level);
    }


    //////////////////////////////////////////////////////

    public void resume()
    {
        pausenew.enabled = false;
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            hidePaused();
        }
        else if (Time.timeScale == 0)
        {
            Debug.Log("high");
            Time.timeScale = 1;
            showPaused();
        }
    }
}
