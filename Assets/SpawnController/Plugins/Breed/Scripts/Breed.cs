using UnityEngine;
using System.Collections;

public class Breed
{
	static public Breed instance = null;
	private ArrayList pools = new ArrayList();

	// returns an instance of the Breed
	public static Breed Instance()
	{
		if ( instance == null )
			instance = new Breed();
		return instance;
	}

	// creates a pool, must be named and the object to pool must be set
	public BreedPool Create(string name, GameObject obj)
	{
		BreedPool pool = new BreedPool(name, obj);
		pools.Add(pool);
		return pool;
	}

	// removes the pool by name
	public void Remove(string name)
	{
		for ( int i = 0; i < pools.Count; i++ )
		{
			BreedPool pool = pools[i] as BreedPool;
			if ( pool.name == name )
			{
				// clear it first
				pool.Clear();

				// remove it from the array
				pools.RemoveAt(i);
			}
		}
	}

	// removes all pools
	public void RemoveAll()
	{
		for ( int i = 0; i < pools.Count; i++ )
			Remove((pools[i] as BreedPool).name);
	}

	// returns the pool by the name, if it exists :P
	public BreedPool Get(string name)
	{
		for ( int i = 0; i < pools.Count; i++ )
		{
			if ( (pools[i] as BreedPool).name == name )
				return pools[i] as BreedPool;
		}
		return null;
	}
}

public class BreedPool
{
	public string name = ""; // the name of the pool
	public GameObject obj = null; // the object we will use to spawn/unspawn
	private Stack available; // keeps track of the available game objects that are ready to spawn
	private ArrayList all; // holds all the game objects spawns

	// constructor
	public BreedPool(string name, GameObject obj)
	{
		this.name = name;
		this.obj = obj;
		available = new Stack();
		all = new ArrayList();
	}

	// spawn the game object, and return it. we need the vector3 and quaternion
	public GameObject Spawn(Vector3 position, Quaternion rotation)
	{
		GameObject go;

		// create it
		if ( available.Count == 0 )
		{
			go = GameObject.Instantiate(obj, position, rotation) as GameObject;
			all.Add(go);
		}
		// use an existing
		else
		{
			go = available.Pop() as GameObject;
			Transform goTrans = go.transform;
			goTrans.position = position;
			goTrans.rotation = rotation;
		}

		// make it active, call the message
		go.SetActiveRecursively(true);
		go.SendMessage("OnSpawn", this, SendMessageOptions.DontRequireReceiver);

		return go;
	}

	// prepopulate, spawn then despawn
	public void Preload(int count)
	{
		GameObject[] gos = new GameObject[count];

		// create the spawn...
		for ( int i = 0; i < count; i++ )
			gos[i] = Spawn(Vector3.zero, Quaternion.identity);

		// ... now unpsawn it!
		for ( int i = 0; i < count; i++ )
			Unspawn(gos[i]);
	}

	// unspawn the game object
	public void Unspawn(GameObject go)
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
	public void UnspawnAll()
	{
		for ( int i = 0; i < all.Count; i++ )
		{
			GameObject go = all[i] as GameObject;
			if ( go != null && go.active )
				Unspawn(go);
		}
	}

	// unspawns all the game objects and clears the pool
	public void Clear()
	{
		UnspawnAll();
		available.Clear();
		all.Clear();
	}

	// returns the number of active objects.
	public int GetActiveCount()
	{
		return all.Count - available.Count;
	}

	// returns the number of available objects.
	public int GetAvailableCount()
	{
		return available.Count;
	}

	// returns the object being used by this pool.
	public GameObject GetObject()
	{
		return obj;
	}
}