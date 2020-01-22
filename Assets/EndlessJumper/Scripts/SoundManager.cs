using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	//plays sfx - SFX provided by Freesfx.co.uk (Free)
	public AudioClip[] sfx;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playSFX(int id)
	{
		//play sound effect by id
		this.GetComponent<AudioSource>().PlayOneShot(sfx[id]);
	}

	public void stopSFX()
	{
		this.GetComponent<AudioSource>().Stop();
	}
}
