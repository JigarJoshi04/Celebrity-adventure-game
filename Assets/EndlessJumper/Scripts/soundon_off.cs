using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundon_off : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject soundControlButton;
    public Sprite audioOffsprite;
    public Sprite audioOnsprite;
    public Canvas pausenew;
    void Start()
    {
        pausenew.enabled = false;
        if(AudioListener.pause==true)
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

    public void SoundControl()
    {
        pausenew.enabled = true;
        if(AudioListener.pause==true)
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
