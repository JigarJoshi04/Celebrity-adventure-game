using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundstartbutton : MonoBehaviour
{
    public Canvas start;
    public Canvas soundsetting;
    /// <summary>
    /// ///////////////////////////
    /// </summary>
    public GameObject soundControlButton;
    public Sprite audioOffsprite;
    public Sprite audioOnsprite;
   
    // Start is called before the first frame update
    void Start()
    {
        
        if (AudioListener.pause == true)
        {
            soundControlButton.GetComponent<Image>().sprite = audioOffsprite;
           
        }
        else
        {
            soundControlButton.GetComponent<Image>().sprite = audioOffsprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void start_disappear()
    {
        start.enabled = false;
    }

    public void SoundControl()
    {
        
        if (AudioListener.pause == true)
        {
            AudioListener.pause = false;
            soundControlButton.GetComponent<Image>().sprite = audioOnsprite;

        }

        else
        {
            AudioListener.pause = true;
            soundControlButton.GetComponent<Image>().sprite = audioOffsprite;
        }
    }

}
