using UnityEngine;
using System.Collections;

public class PlayerPrefBackground : MonoBehaviour {

    public Camera mainCamera;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("Vote"))
        {
            switch (PlayerPrefs.GetString("Vote"))
            {
                case "Trump":
                    mainCamera.backgroundColor = Color.red;
                    break;
                case "Clinton":
                    mainCamera.backgroundColor = Color.blue;
                    break;
                default:
                    break;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
