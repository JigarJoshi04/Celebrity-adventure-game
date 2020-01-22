using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {


	//This script assigns behavior to tiles - when you add a new tile, you need to define its behavior here

	public GameManager Game;
	public int tileType;

	Vector3 startPosition;
	float distance;
	float speed;
	int direction; //0 for left, 1 for right or 0 for up and 1 for down

	public SpriteRenderer mySpriteRenderer;
	public Sprite[] mySprites;

	public SoundManager SFXManager;

	// Use this for initialization
	void Start () {
	

	}

	void OnEnable()
	{
		if(this.tag == "TileType0")
			tileType = 0;
		if(this.tag == "TileType1")
			tileType = 1;
		if(this.tag == "TileType2")
			tileType = 2;
		if(this.tag == "TileType3")
			tileType = 3;
		if(this.tag == "TileType4")
			tileType = 4;
		if(this.tag == "TileType5")
			tileType = 5;
		
		
		switch(tileType)
		{
		case 0: //Normal Tile
			mySpriteRenderer.sprite = mySprites[0];
			break;
		case 1: //Broken Tile
			mySpriteRenderer.sprite = mySprites[1];
			break;
		case 2: //One Time Tile
			mySpriteRenderer.sprite = mySprites[2];
			break;
		case 3: //Spring Tile
			mySpriteRenderer.sprite = mySprites[3];
			break;
		case 4: //Moving Horizontally
			mySpriteRenderer.sprite = mySprites[4];
			startPosition = this.transform.position;
			distance = Game.Advanced.movingTilesHorizontal.distance;
			speed = Game.Advanced.movingTilesHorizontal.speed;
			break;
		case 5: //Moving Vertically
			mySpriteRenderer.sprite = mySprites[5];
			startPosition = this.transform.position;
			distance = Game.Advanced.movingTilesVertical.distance;
			speed = Game.Advanced.movingTilesVertical.speed;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		switch(tileType)
		{
		case 0: //Normal Tile
		break;
		case 1: //Broken Tile
		break;
		case 2: //One Time Tile
		break;
		case 3: //Spring Tile
		break;
		case 4: //Moving Horizontally
			if(direction == 0) //left
			{
				this.transform.Translate(new Vector2(-speed*Time.deltaTime,0));
				if((startPosition - this.transform.position).x > distance)
					direction = 1;
			}
			else if(direction == 1) //left
			{
				this.transform.Translate(new Vector2(speed*Time.deltaTime,0));
				if((startPosition - this.transform.position).x < -distance)
					direction = 0;
			}

		break;
		case 5: //Moving Vertically
			if(direction == 0) //up
			{
				this.transform.Translate(new Vector2(0,-speed*Time.deltaTime));
				if((startPosition - this.transform.position).y > distance)
					direction = 1;
			}
			else if(direction == 1) //down
			{
				this.transform.Translate(new Vector2(0,speed*Time.deltaTime));
				if((startPosition - this.transform.position).y < -distance)
					direction = 0;
			}
		break;
		}

		if(Game.floor.transform.position.y > this.transform.position.y + 1)
		{
			this.GetComponent<Rigidbody2D>().gravityScale = 0;
			Game.addInactiveTile(this.gameObject);
			
		}

	}


	void OnTriggerEnter2D(Collider2D col)
	{

		//(col.gameObject.rigidbody2D.velocity.y <= 0 ) Checks if the player is falling down only then the tile is in effect
		switch(tileType)
		{
		case 0: //Normal Tile
			if(col.name.Contains("Player") && (col.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0 ))
			{
				Game.player.jump(1);
			}

			break;
		case 1: //Broken Tile

			if(col.name.Contains("Player") && (col.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0 ))
			{
				this.GetComponent<Rigidbody2D>().gravityScale = 1; //Make the tile fall down as soon as player touches it
				SFXManager.playSFX(2);//Play Broken Sound
			}

			break;
		case 2: //One Time Tile
		
			if(col.name.Contains("Player") && (col.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0 ))
			{
				Game.player.jump(1);
				this.GetComponent<Rigidbody2D>().gravityScale = 1; //Make the tile fall down as soon as player touches it
			}
			break;
		case 3: //Spring Tile

			if(col.name.Contains("Player") && (col.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0 ))
			{
				Game.player.jump(1.5f);

			}
			break;
		case 4: //Moving Horizontally
			if(col.name.Contains("Player") && (col.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0 ))
			{
				Game.player.jump(1);
			}
			
			break;
		case 5: //Moving Vertically
			if(col.name.Contains("Player") && (col.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0 ))
			{
				Game.player.jump(1);
			}
			break;
		}

	}


}
