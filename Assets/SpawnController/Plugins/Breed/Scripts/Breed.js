#pragma strict
#pragma downcast

class Breed
{
	static private var instance : Breed;
	var pools : BreedPool[] = [];

	// returns an instance of the Breed
	static function Instance() : Breed
	{
		if ( !instance )
			instance = new Breed();
		return instance;
	}

	// creates a pool, must be named and the object to pool must be set
	function Create(name : String, object : GameObject) : BreedPool
	{
		var pool : BreedPool = new BreedPool(name, object);
		pools += [pool];
		return pool;
	}

	// removes the pool by name
	function Remove(name : String)
	{
		for ( var i : int = 0; i < pools.Length; i++ )
		{
			var pool : BreedPool = pools[i] as BreedPool;
			if ( pool.name == name )
			{
				// clear it first
				pool.Clear();

				// remove it from the array
				var _pools : Array = new Array(pools);
				_pools.RemoveAt(i);
				pools = _pools.ToBuiltin(BreedPool);
			}
		}
	}

	// removes all pools
	function RemoveAll()
	{
		for ( var i : int = 0; i < pools.Length; i++ )
			(pools[i] as BreedPool).Clear();

		pools = [];
	}

	// returns the pool by the name, if it exists :P
	function Get(name : String) : BreedPool
	{
		for ( var i : int = 0; i < pools.Length; i++ )
		{
			if ( (pools[i] as BreedPool).name == name )
				return pools[i];
		}
		return null;
	}
}


class BreedPool
{
	var name : String = ""; // the name of the pool
	var object : GameObject = null; // the object we will use to spawn/unspawn
	var initPreload : int = 0;
	private var available : Stack = new Stack(); // keeps track of the available game objects that are ready to spawn
	private var all : ArrayList; // holds all the game objects spawns

	// constructor
	function BreedPool(name : String, object : GameObject)
	{
		this.name = name;
		this.object = object;
		available = new Stack();
		all = new ArrayList();
	}

	// spawn the game object, and return it. we need the vector3 and quaternion
	function Spawn(position : Vector3, rotation : Quaternion) : GameObject
	{
		var go : GameObject;

		// create it
		if ( available.Count == 0 )
		{
			go = GameObject.Instantiate(object, position, rotation) as GameObject;
			all.Add(go);
		}
		// use an existing
		else
		{
			go = available.Pop() as GameObject;
			var goTrans : Transform = go.transform;
			goTrans.position = position;
			goTrans.rotation = rotation;
		}

		// make it active, call the message
		go.SetActiveRecursively(true);
		go.SendMessage("OnSpawn", this, SendMessageOptions.DontRequireReceiver);

		return go;
	}

	// prepopulate, spawn then despawn
	function Preload(count : int)
	{
		var gos : GameObject[] = new GameObject[count];

		// create the spawn...
		for ( var i : int = 0; i < count; i++ )
			gos[i] = Spawn(Vector3.zero, Quaternion.identity);

		// ... now unpsawn it!
		for ( i = 0; i < count; i++ )
			Unspawn(gos[i]);
	}

	// unspawn the game object
	function Unspawn(go : GameObject)
	{
		// only if it doesn't exist
		if ( !available.Contains(go) )
		{
			available.Push(go);
			go.SendMessage("OnUnspawn", SendMessageOptions.DontRequireReceiver);
			go.SetActiveRecursively(false);
		}
	}

	// unspawns all the game objects created by the pool
	function UnspawnAll()
	{
		for ( var i : int = 0; i < all.Count; i++ )
		{
			var go : GameObject = all[i] as GameObject;
			if ( go != null && go.active )
				Unspawn(go);
		}
	}

	// unspawns all the game objects and clears the pool
	function Clear()
	{
		UnspawnAll();
		available.Clear();
		all.Clear();
	}

	// returns the number of active objects.
	function GetActiveCount() : int
	{
		return all.Count - available.Count;
	}

	// returns the number of available objects.
	function GetAvailableCount() : int
	{
		return available.Count;
	}

	// returns the object being used by this pool.
	function GetObject() : GameObject
	{
		return object;
	}

	// returns all game objects instantitated by the pool
	function GetObjects() : GameObject[]
	{
		// return ALL gameobjects
		return _GetObjects(false);
	}
	function GetObjects(active : boolean) : GameObject[]
	{
		// return ONLY the active
		return _GetObjects(active);
	}
	function _GetObjects(active : boolean) : GameObject[]
	{
		var total : int = (active ? all.Count - available.Count : all.Count) - 1;
		var objs : GameObject[] = new GameObject[total + 1];

		for ( var i : int = 0; i < all.Count; i++ )
		{
			var go : GameObject = all[i] as GameObject;
			if ( active && go.active )
				objs[total--] = go;
			else if ( !active )
				objs[total--] = go;
		}

		return objs;
	}
}