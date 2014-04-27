#pragma strict

var ballPrefab : GameObject;
var capsulePrefab : GameObject;
var cubePrefab : GameObject;
private var ballPool : BreedPool;
private var capsulePool : BreedPool;
private var cubePool : BreedPool;

function Start()
{
	// create the pools
	Breed.Instance().Create("balls", ballPrefab);
	Breed.Instance().Create("capsules", capsulePrefab);
	Breed.Instance().Create("cubes", cubePrefab);
	
	// get the pool for short hand
	ballPool = Breed.Instance().Get("balls");
	capsulePool = Breed.Instance().Get("capsules");
	cubePool = Breed.Instance().Get("cubes");
	
	// lets preload a few
	ballPool.Preload(20);
	capsulePool.Preload(10);
	cubePool.Preload(5);
}

function OnGUI()
{
	var pos : Vector3 = Vector3(Random.Range(-2.0, 2.0), Random.Range(3.0, 5.0), 0.0);
	
	// spawn the ball
	if ( GUI.Button(Rect(10, 10, 100, 30), "Spawn Ball") ) 
		ballPool.Spawn(pos, Quaternion.identity);
	
	// spawn the capsule
	if ( GUI.Button(Rect(10, 50, 100, 30), "Spawn Capsule") ) 
		capsulePool.Spawn(pos, Quaternion.identity);
		
	// spawn the cube
	if ( GUI.Button(Rect(10, 90, 100, 30), "Spawn Cube") ) 
		cubePool.Spawn(pos, Quaternion.identity);
		
	// spawn all available!
	if ( GUI.Button(Rect(10, 130, 150, 30), "Spawn ALL Available") )
	{
		var ballCount : int = ballPool.GetAvailableCount();
		var capsuleCount : int = capsulePool.GetAvailableCount();
		var cubeCount : int = cubePool.GetAvailableCount();
		
		for ( var i : int = 0; i < ballCount; i++ )
			ballPool.Spawn(Vector3(Random.Range(-3.0, 3.0), Random.Range(1.0, 6.0), 0.0), Quaternion.identity);
		for ( i = 0; i < capsuleCount; i++ )
			capsulePool.Spawn(Vector3(Random.Range(-3.0, 3.0), Random.Range(1.0, 6.0), 0.0), Quaternion.identity);
		for ( i = 0; i < cubeCount; i++ )
			cubePool.Spawn(Vector3(Random.Range(-3.0, 3.0), Random.Range(1.0, 6.0), 0.0), Quaternion.identity);
	}
		
	// details
	GUI.Label(Rect(Screen.width - 250, 10, 250, 40), "Balls: " + ballPool.GetActiveCount() + " Active -- " + ballPool.GetAvailableCount() + " Available");
	GUI.Label(Rect(Screen.width - 250, 50, 250, 40), "Capsules: " + capsulePool.GetActiveCount() + " Active -- " + capsulePool.GetAvailableCount() + " Available");
	GUI.Label(Rect(Screen.width - 250, 90, 250, 40), "Cubes: " + cubePool.GetActiveCount() + " Active -- " + cubePool.GetAvailableCount() + " Available");
}