using UnityEngine;
using System.Collections;

public class GameManager : EnhancedBehaviour 
{
	public EnemySpawner enemySpawner;

	protected override void EnhancedOnEnable ()
	{
		enemySpawner = GameObject.Find("Enemy Manager").GetComponent<EnemySpawner>();
	}
	public void StartGame()
	{
		enemySpawner.StartSpawning();
	}
}
