using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundpopuper : MonoBehaviour
{
    public Canvas soundcanvas ;
    public Canvas startscreen;

    // Start is called before the first frame update
    void Start()
    {
        soundcanvas.enabled = false ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Popup()
    {
        soundcanvas.enabled = true;
        startscreen.enabled = false;
    }
}
