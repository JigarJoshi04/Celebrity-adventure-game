using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {


	public GameManager Game;
	Vector3 startPosition;
	public float distance;
	public float speed;

	public Sprite[] mySprites;
	public float[] enemySpeed;
	public float[] enemyDistance;
	public SpriteRenderer mySpriteRenderer;

	int direction; //0 for left, 1 for right

	public int enemyType;
	// Use this for initialization
	void Start () {

	}


	void OnEnable()
	{
		//initialize enemy - assign type
		startPosition = this.transform.position;

		string typeOfEnemy = this.tag.ToString().Replace("EnemyType","");


		enemyType = int.Parse(typeOfEnemy);
		Debug.Log("Enemy type is " + enemyType); 

		mySpriteRenderer.sprite = mySprites[enemyType];

		//get speed of the enemy and the distance from game settings
		speed = enemySpeed[enemyType];
		distance = enemyDistance[enemyType];


	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.name.Contains("Player"))
		{
			col.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			col.GetComponent<Rigidbody2D>().gravityScale = 3;
			col.gameObject.GetComponent<Collider2D>().enabled = false;
			Game.endGame();
		}
		
	}


	// Update is called once per frame
	void Update () {

		//move enemy left and right
		if(direction == 0) //left
		{
			this.transform.Translate(new Vector2(-speed*Time.deltaTime,0));
			if((startPosition - this.transform.position).x > distance)
			{
				direction = 1;
				this.transform.localScale = new Vector3(-1,1,1);
			}

		}
		else if(direction == 1) //left
		{
			this.transform.Translate(new Vector2(speed*Time.deltaTime,0));
			if((startPosition - this.transform.position).x < -distance)
			{
				direction = 0;
				this.transform.localScale = new Vector3(1,1,1);
			}


		}

		//if the enemy hits the floor (goes off screen), destroy it - i.e. add it to the inactive objects pool
		if(Game.floor.transform.position.y > this.transform.position.y + 1)
		{
			Game.addInactiveEnemy(this.gameObject);
		}


		
	}
}
