using UnityEngine;
using System.Collections;

public class JumpPowerup : MonoBehaviour {

	bool jump;
	GameObject player;
	public float[] itemPower;
	public GameManager Game;
	public int itemType;
	public SpriteRenderer mySpriteRenderer;
	public Sprite[] mySprites;
	public GameObject[] myObjects;

	public SoundManager SFXManager;

	
	void OnEnable()
	{

		//Assign the type - which item is it (0 or 1) - 0 = small jump - 1 = large jump

		
		string typeOfPowerup = this.tag.ToString().Replace("ItemType","");
		
		
		itemType = int.Parse(typeOfPowerup);
		Debug.Log("Item type is " + itemType); 

		mySpriteRenderer.sprite = mySprites[itemType];
	
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.name.Contains("Player"))
		{
			if(itemType==2)//Item type is 2 = coin
			{
				//Add to score
				Game.score+=20;
				Game.addInactiveItem(this.gameObject);
			}
			else
			{
				SFXManager.playSFX(1);//Play Electric Sound
				player = col.gameObject;//Assign player
				player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;//Cancel out downwards force of the player
				jump = true; //Make jump var true 
				player.GetComponent<Rigidbody2D>().isKinematic = true; //disable gravity
				player.GetComponent<Collider2D>().enabled = false; //disable collider while using powerup jump
				myObjects[itemType].SetActive(true); //make the jump sprite active
				StartCoroutine("stopJumping"); //stop jump after certain seconds - depending on the type - edit freely
				mySpriteRenderer.enabled=false; 
			}
		}
		
	}

	IEnumerator stopJumping()
	{
		//Stop jumping - reset state
		yield return new WaitForSeconds(itemPower[itemType]);

		player.GetComponent<Rigidbody2D>().isKinematic = false;
		player.GetComponent<Collider2D>().enabled = true;

		myObjects[itemType].SetActive(false);
		jump = false;

		SFXManager.stopSFX();


	}

	void Update()
	{
		if(jump)
		{
			//Jump - move player object at a constact speed upwards
			player.transform.Translate(new Vector2(0,12*Time.deltaTime));
		}

		else if(Game.floor.transform.position.y > this.transform.position.y + 1)
		{
			Game.addInactiveItem(this.gameObject);
		}
	}
}