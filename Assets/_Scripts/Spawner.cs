using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public GameObject[] platforms;
    public float spawnrate;
    public float xRange, yRange;

    private float delayTime;

	// Use this for initialization
	void Start () {
        delayTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
	    
        if(Time.time - delayTime >= spawnrate)
        {
            SpawnPlatform();
        }

	}

    private void SpawnPlatform()
    {
        float x = Random.Range(-xRange, xRange) + transform.position.x;
        float y = Random.Range(-yRange, yRange) + transform.position.y;
        Instantiate(platforms[Random.Range(0, platforms.Length - 1)], new Vector3(x, y, 0), Quaternion.identity);
        delayTime = Time.time;
    }
}
