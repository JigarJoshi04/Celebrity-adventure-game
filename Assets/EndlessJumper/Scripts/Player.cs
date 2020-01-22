using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public bool isMouseControl;
	public GameManager Game;
	public GameObject jumpBlue;
	public GameObject jumpOrange;

	public Sprite leftSprite;
	public Sprite rightSprite;

	public SoundManager SFXManager;
	float leftBorder; 
	float rightBorder;

	int currentDirection;
	float timeDirection;

	SpriteRenderer thisRenderer;
	// Use this for initialization
	void Start () {


		thisRenderer = this.GetComponent<SpriteRenderer>();

		#if UNITY_IOS
			Application.targetFrameRate = 60;
		#endif

		Vector3 playerSize = GetComponent<Renderer>().bounds.size;
		// Here is the definition of the boundary in world point
		float distance = (transform.position - Camera.main.transform.position).z;
		leftBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance)).x + (playerSize.x/2);
		rightBorder = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance)).x - (playerSize.x/2);
		timeDirection = Time.time;

	}

	void OnMouseDown()
	{
		#if UNITY_EDITOR
		Screen.lockCursor =true;
		Cursor.visible = false;
		#endif
	}




	Vector3 mousePosition;
	// Update is called once per frame
	void Update () {

		if(!Game.isGameOver && !Game.isGamePaused)
		{
		if (Input.GetKeyDown("escape")) //If escape is pressed, mouse is released
		{
			Screen.lockCursor = false;
			Game.endGame();
			Cursor.visible = true;
		}

			//Get mouse position
		mousePosition = Input.mousePosition;           
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		mousePosition.y = 0f;

		#if UNITY_EDITOR || UNITY_WEBPLAYER
			Vector3 diff;
			if(isMouseControl){
				//If Mouse control is being used, the player follows the mouse
				diff =  Vector3.MoveTowards(this.transform.localPosition, mousePosition,(0.1f * Time.time));}
			else
			{
				//Keyboard Control - use arrow keys to move the player
				Vector3 acc = Vector3.zero;
				if(Input.GetKey(KeyCode.LeftArrow))
				{
					acc.x = -0.1f;
					thisRenderer.sprite = leftSprite;
				}
				if(Input.GetKey(KeyCode.RightArrow))
				{
					acc.x = 0.1f;
					thisRenderer.sprite = rightSprite;
				}



				diff = Vector3.MoveTowards(this.transform.localPosition,this.transform.localPosition + acc,(0.5f * Time.time));

			}
		#else

			//Accelerometer Control - default for mobile builds

			Vector3 acc = Input.acceleration;
			acc.y = 0f;
			acc.z = 0f;
			Vector3 diff =  Vector3.MoveTowards(this.transform.localPosition,this.transform.localPosition + acc,(0.5f * Time.time));
		#endif

		diff.y = this.transform.localPosition.y;
		diff.z = 0f;

		if(diff.x<leftBorder-1f)
		{
			diff.x = rightBorder;
		}
		else if(diff.x>rightBorder+1f)
		{
			diff.x = leftBorder;
		}

		this.transform.localPosition = diff;
		}

	}

	public void switchOn(string name)
	{
		if(name.Contains("j2")) //Switch on the jump sprite behind the player
			jumpBlue.SetActive(true);
		else
			jumpOrange.SetActive(true);
	}
	public void switchOff(string name)
	{
		if(name.Contains("j2"))//Switch on the jump sprite behind the player
			jumpBlue.SetActive(false);
		else
			jumpOrange.SetActive(false);
	}

	public void jump(float x)
	{
		//Jump (12*x) force - change force for lower jump or change tile jump in the tile.cs script
		this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f,12f*x),ForceMode2D.Impulse);

		SFXManager.playSFX(0);//Play Jump Sound
	}



	void OnTriggerEnter2D(Collider2D col)
	{

		if(col.name.Contains("platform"))
		{
			//Jump if the object is platform
			Game.player.jump(1);
		}

		else if(this.gameObject.GetComponent<Rigidbody2D>().velocity.y <= 0 )
		{
			if(col.gameObject.name.Contains("Floor"))
			{
				//If the player hits the floor object, then it means he has fallen - end game
				Game.endGame();
			}
		}
	}
}
