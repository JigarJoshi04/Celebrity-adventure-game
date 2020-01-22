using UnityEngine;
using System;

[Serializable]
public class GameSettings {


	//Settings of the game (do not edit here - edit in the GameManager game object)
	[Serializable]
	public class NormalTile{
		public float probabilityWeight;
		public float minimumHeightBetweenTiles;
		public float maximumHeightBetweenTiles;
	}

	[Serializable]
	public class BrokenTile{
		public float probabilityWeight;
		public float minimumHeightBetweenTiles;
		public float maximumHeightBetweenTiles;
	}

	[Serializable]
	public class OneTimeOnly{
		public float probabilityWeight;
		public float minimumHeightBetweenTiles;
		public float maximumHeightBetweenTiles;
	}

	[Serializable]
	public class SpringTile{
		public float probabilityWeight;
		public float jumpForce;
		public float minimumHeightBetweenTiles;
		public float maximumHeightBetweenTiles;
	}

	[Serializable]
	public class MovingTileHorizontal{
		public float probabilityWeight;
		public float speed;
		public float distance;
		public float minimumHeightBetweenTiles;
		public float maximumHeightBetweenTiles;
	}

	[Serializable]
	public class MovingTileVertical{
		public float probabilityWeight;
		public float speed;
		public float distance;
		public float minimumHeightBetweenTiles;
		public float maximumHeightBetweenTiles;
	}

	public NormalTile normalTiles;
	public BrokenTile brokenTiles;
	public OneTimeOnly oneTimeOnlyTiles;
	public SpringTile springTiles;
	public MovingTileHorizontal movingTilesHorizontal;
	public MovingTileVertical movingTilesVertical;

	public GameObject jumpOrangeCollect;
	public GameObject jumpBlueCollect;

	public float itemAppearProbability; //0 to 1
	public float enemyProbability; //0 to 1

}