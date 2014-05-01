using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	public int baseNumberOfEnemies = 50;
	public int baseSpawnNumber = 50;
	public int NumberOfEnemiesIncrement = 25;
	public int SpawnNumberIncrement = 2;
	public int levelNumber = 0;
	public int enemiesOnLevel;
	public int levelSpawnNumber;

	public Sprite level0;
	public Sprite level1;
	public Sprite level2;
	public Sprite cover0;
	public Sprite cover1;
	public Sprite cover2;

	private EnemySpawner enemySpawner = null;
	// Use this for initialization
	void Start () 
	{
	}

	public void BeginLevel()
	{
		enemiesOnLevel = baseNumberOfEnemies + (NumberOfEnemiesIncrement * levelNumber);
		levelSpawnNumber = baseSpawnNumber + (SpawnNumberIncrement * levelNumber);

		//GameObject.Find("Enemy Manager").GetComponent<EnemySpawner>().MaxEnemies = levelSpawnNumber;

		int levelSprite = Random.Range(0,3);
		GameObject scenario = GameObject.Find("Scenario");
		GameObject cover = GameObject.Find("Cover");
		switch (levelSprite)
		{
		case 0:
			scenario.GetComponent<SpriteRenderer>().sprite = level0;
			break;
		case 1:
			scenario.GetComponent<SpriteRenderer>().sprite = level1;
			break;
		case 2:
			scenario.GetComponent<SpriteRenderer>().sprite = level2;
			break;
		}
	}

	public void GoToNextLevel()
	{
		levelNumber++;
	}

	// Update is called once per frame
	void Update () 
	{
		if (enemiesOnLevel <= 0)
		{
			//Level done! 
			if (!enemySpawner)
			{
				enemySpawner = GameObject.Find("Enemy Manager").GetComponent<EnemySpawner>();
			}
			enemySpawner.StopSpawning();
			levelNumber++;
		}
	}
}
