using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlueRedFade : MonoBehaviour {

    private Image background;
    private float speed = 1.0f;


    // Use this for initialization
    void Start () {
        background = gameObject.GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {

        float t = (Mathf.Sin(Time.time * speed) + 1) / 2.0f;

        background.color = Color.Lerp(Color.red, Color.blue, t);

    }
}
