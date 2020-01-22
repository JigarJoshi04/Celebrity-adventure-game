using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backbutton : MonoBehaviour
{
    public Canvas soundsettingcanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void back()
    {
        soundsettingcanvas.enabled = false;
    }
}
