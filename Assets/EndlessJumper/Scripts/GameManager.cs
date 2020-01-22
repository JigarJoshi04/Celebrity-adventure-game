using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GUIControl SGUI;
	public Player player;
	public float ScreenDistance;
	public bool isGamePaused = true;
	public bool isGameOver;
	public int score;
	public GameObject floor;
	public GameSettings Advanced;
	int initialSize = 40;
	float currentPosition = 2f;
	float totalWeight;
	float lastTime;

	public GameObject defaultTile;
	public GameObject defaultItem;
	public GameObject defaultEnemy;
	public GUIStyle scoreLabel;

	Vector3 initCameraPosition;
	Queue<GameObject> tilePool = new Queue<GameObject>();
	Queue<GameObject> enemyPool = new Queue<GameObject>();
	Queue<GameObject> itemPool = new Queue<GameObject>();

	// Use this for initialization
	void Start () {
		getTotalProbabilityWeight();
		generateTilePool(); //Generate atleast 10-20 tiles at first and resuse them (pooling)
		generateEnemyPool(); //Generate atleast 10-20 enemies at first and resuse them (pooling)
		generateItemPool(); //Generate atleast 10-20 items at first and resuse them (pooling)

		Debug.Log("Total enemies " + enemyPool.Count + " " + " Total items : " +  itemPool.Count);

		for(int i = 0;i<initialSize; i++)
		{
			generateTile(); //Use a tile from the pool and add to scene (make it visible at the right position)
			generateItem(); //Use an item from the pool and add to scene (make it visible at the right position)
			generateEnemy(); //Use a enemy from the pool and add to scene (make it visible at the right position)
		}

		pauseGame(); //Pause game at the start to show the start panel
		initCameraPosition = Camera.main.transform.position; //Store the initial position of the camera - reset it at game over
	}


	public void pauseGame()
	{
		isGamePaused = true; //Pause the game (using one variable)
	}

	public void resumeGame()
	{
		player.gameObject.SetActive(true); //Resume game - make the player object active
		isGamePaused = false; //Resume game - make the variable false
	}

	public void endGame()
	{
		Camera.main.GetComponent<PlayerCamera>().enabled = false; //Disable the script that moves the camera
		isGameOver = true; //Make game over using one variable
		SGUI.showGameOverScreen(); //Use the GUI Manager script to show the game over screen panel
		Camera.main.transform.position = initCameraPosition; //reset camera to initial/original position to show game over screen


		//Scoring - if a score already exists that is less than current, make it the best score
		if(PlayerPrefs.HasKey("bestScore"))
		{
			if(PlayerPrefs.GetInt("bestScore") < score)
			{
				PlayerPrefs.SetInt("bestScore",score);
			}
		}
		else
		{
			PlayerPrefs.SetInt("bestScore",score);
		}

		//Update GUI Text to show the score on Game Over Screen
		GameObject.Find("lblScore").GetComponent<GUIText>().text = "Score : " + score;
		GameObject.Find("lblBest").GetComponent<GUIText>().text = "Best : " + PlayerPrefs.GetInt("bestScore");


	}

	//This is simply a function that creates one tile - used when a tile is destroyed.
	public void createTile()
	{
		generateTile();
		generateItem();
		generateEnemy();
		score+=5;

		increaseDifficulty(10);//Checks the time, increases difficulty by a set factor after certain seconds
	}


	void increaseDifficulty(float seconds)
	{
		if(Time.time-lastTime>seconds)
		{
			Debug.Log("Increasing Diffuclty now");
			lastTime= Time.time;
			Advanced.enemyProbability +=0.01f;
			//Change settings to your liking. Increase the one time tiles by increasing their probability weight, reduce normal tiles, items etc.
		}

	}
	//This function creates an item (orange jump, blue jump) gameobject based on the probability 0 to 1 in Game Settings

	void generateItem()
	{

		
		Vector2 tmpPos = new Vector2(Random.Range(-ScreenDistance,ScreenDistance),currentPosition);//Generate a vector with a random x and next y
		float rand = Random.Range(0f,1f);

		//Generate Item
		if(rand < Advanced.itemAppearProbability)
		{
			GameObject gObj = getInactiveItem();
			//Assign type to item - very important
			gObj.tag = "ItemType" + Random.Range(0,3);
			//Set position of item and make it active
			gObj.transform.position = tmpPos;
			gObj.SetActive(true);
		}
	}



	//This function creates an enemy gameobject based on the probability 0 to 1 in Game Settings
	void generateEnemy()
	{
		float rand = Random.Range(0f,1f);
		Vector2 tmpPos = new Vector2(Random.Range(-ScreenDistance,ScreenDistance),currentPosition);//Generate a vector with a random x and next y
		if(rand < Advanced.enemyProbability)
		{
			GameObject gObj = getInactiveEnemy();
			


				gObj.tag = "EnemyType" + Random.Range(0,3);//Change the max to number of enemies
				gObj.transform.position = tmpPos;
				gObj.SetActive(true);
		}
	}

	//This function adds the weights of different tiles into the sum of weights.

	void getTotalProbabilityWeight()
	{
		float sum = 0;
		sum+=Advanced.normalTiles.probabilityWeight;
		sum+=Advanced.brokenTiles.probabilityWeight;
		sum+=Advanced.oneTimeOnlyTiles.probabilityWeight;
		sum+=Advanced.springTiles.probabilityWeight;
		sum+=Advanced.movingTilesHorizontal.probabilityWeight;
		sum+=Advanced.movingTilesVertical.probabilityWeight;

		totalWeight = sum;
	}

	//getTileBasedOnRandomNumber function simply checks which range the random int (0 to sum of weights) belongs to.

	int getTileBasedOnRandomNumber(int randomNumber)
	{
		if(randomNumber <= Advanced.normalTiles.probabilityWeight)
			return 0;
		else if(randomNumber <= Advanced.normalTiles.probabilityWeight + Advanced.brokenTiles.probabilityWeight)
			return 1;
		else if(randomNumber <= Advanced.normalTiles.probabilityWeight + Advanced.brokenTiles.probabilityWeight + Advanced.oneTimeOnlyTiles.probabilityWeight)
			return 2;
		else if(randomNumber <= Advanced.normalTiles.probabilityWeight + Advanced.brokenTiles.probabilityWeight + Advanced.oneTimeOnlyTiles.probabilityWeight + Advanced.springTiles.probabilityWeight)
			return 3;
		else if(randomNumber <= Advanced.normalTiles.probabilityWeight + Advanced.brokenTiles.probabilityWeight + Advanced.oneTimeOnlyTiles.probabilityWeight + Advanced.springTiles.probabilityWeight + Advanced.movingTilesHorizontal.probabilityWeight)
			return 4;
		else if(randomNumber <= Advanced.normalTiles.probabilityWeight + Advanced.brokenTiles.probabilityWeight + Advanced.oneTimeOnlyTiles.probabilityWeight + Advanced.springTiles.probabilityWeight + Advanced.movingTilesHorizontal.probabilityWeight + Advanced.movingTilesVertical.probabilityWeight)
			return 5;

		return -1;
	}

	//Generate Tile Function creates a tile based on the probability specified in the inspector settings 
	//A random number from 0 to sum of weights is generated and the tile is generated according to the range, the number
	// falls into. For e.g. If normalTile has density = 30, then a number <= 30 will create a normal tile.


	void generateTile()
	{
		GameObject gObj = getInactiveTile();
		//Normal Tile = 0
		//Broken Tile = 1
		//One Time Only = 2
		//Spring Tile = 3
		//Moving Horizontally = 4
		//Moving Vertically = 5

		float rand = Random.Range(0,totalWeight); 
		int randomNumber = getTileBasedOnRandomNumber((int)rand); //A number from 0 to 5 will be generated

		Vector2 tmpPos = new Vector2(Random.Range(-ScreenDistance,ScreenDistance),currentPosition);//Generate a vector with a random x and next y

		switch(randomNumber)
		{
		case 0://Normal Tile
			gObj.transform.position = tmpPos;
			currentPosition += Random.Range(Advanced.normalTiles.minimumHeightBetweenTiles,Advanced.normalTiles.maximumHeightBetweenTiles);
			gObj.tag = "TileType0";
			gObj.SetActive(true);
		break;
		case 1://Broken Tile
			gObj.transform.position = tmpPos;
			currentPosition += Random.Range(Advanced.brokenTiles.minimumHeightBetweenTiles,Advanced.normalTiles.maximumHeightBetweenTiles);
			gObj.tag = "TileType1";
			gObj.SetActive(true);
		break;
		case 2://OneTime Tile
			gObj.transform.position = tmpPos;
			currentPosition += Random.Range(Advanced.oneTimeOnlyTiles.minimumHeightBetweenTiles,Advanced.normalTiles.maximumHeightBetweenTiles);
			gObj.tag = "TileType2";
			gObj.SetActive(true);
		break;
		case 3://Spring Tile
			gObj.transform.position = tmpPos;
			currentPosition += Random.Range(Advanced.springTiles.minimumHeightBetweenTiles,Advanced.springTiles.maximumHeightBetweenTiles);
			gObj.tag = "TileType3";
			gObj.SetActive(true);
		break;
		case 4://Moving Horizontal Tile
			gObj.transform.position = tmpPos;
			currentPosition += Random.Range(Advanced.movingTilesHorizontal.minimumHeightBetweenTiles,Advanced.movingTilesHorizontal.maximumHeightBetweenTiles);
			gObj.tag = "TileType4";
			gObj.SetActive(true);
		break;
		case 5://Moving Vertical Tile
			gObj.transform.position = tmpPos;
			currentPosition += Random.Range(Advanced.movingTilesVertical.minimumHeightBetweenTiles,Advanced.movingTilesVertical.maximumHeightBetweenTiles);
			gObj.tag = "TileType5";
			gObj.SetActive(true);
		break;
		}

	}


	void OnGUI()
	{

		if(!isGamePaused)
		{
			//Show the score of the game is the game is not paused (top left score)
			GUI.Label(new Rect(0,0,100,50),(score.ToString()),scoreLabel);
		}
	}







	//POOLING - OBJECTS ARE NOT DESTROYED BUT ADDED TO A QUEUE AND REUSED

	public void addInactiveTile(GameObject inactiveTile)
	{

		inactiveTile.SetActive(false);
		tilePool.Enqueue(inactiveTile);
		createTile();

	}
	public GameObject getInactiveTile()
	{
		return tilePool.Dequeue();
	}

	public void addInactiveItem(GameObject inactiveItem)
	{
		
		inactiveItem.SetActive(false);
		itemPool.Enqueue(inactiveItem);
		
	}
	public GameObject getInactiveItem()
	{
		return itemPool.Dequeue();
	}

	public void addInactiveEnemy(GameObject inactiveEnemy)
	{
		
		inactiveEnemy.SetActive(false);
		enemyPool.Enqueue(inactiveEnemy);
		
	}
	public GameObject getInactiveEnemy()
	{
		return enemyPool.Dequeue();
	}


	//GENERATING POOL OF OBJECTS AT THE START TO BE REUSED THROUGHOUT THE GAME
	void generateTilePool()
	{	for(int i = 0;i<initialSize; i++)
		{
			GameObject gObj = (GameObject)GameObject.Instantiate(defaultTile,Vector3.zero,Quaternion.identity);
			gObj.SetActive(false);
			gObj.name = "Tile" + i;
			tilePool.Enqueue(gObj);
		}
	}
	
	void generateItemPool()
	{	for(int i = 0;i<initialSize; i++)
		{
			GameObject gObj = (GameObject)GameObject.Instantiate(defaultItem,Vector3.zero,Quaternion.identity);
			gObj.SetActive(false);
			gObj.name = "Item" + i;
			itemPool.Enqueue(gObj);
		}
	}
	
	void generateEnemyPool()
	{	for(int i = 0;i<initialSize; i++)
		{
			GameObject gObj = (GameObject)GameObject.Instantiate(defaultEnemy,Vector3.zero,Quaternion.identity);
			gObj.SetActive(false);
			gObj.name = "Enemy" + i;
			enemyPool.Enqueue(gObj);
		}
	}

}
