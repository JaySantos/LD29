#pragma strict

private var pool : BreedPool;
private var trans : Transform;
private var rb : Rigidbody;

function Awake()
{
	trans = transform;
	rb = rigidbody;
}

function Update()
{
	// once it falls off, we unspawn it - DON'T Destroy!
	if ( trans.position.y < -10.0 )
		pool.Unspawn(gameObject);
}

// use this instead of "Instantiate" - it will pass the PoolManagerPool
function OnSpawn(pmp : BreedPool)
{
	// we will use this when unspawning, thats why it was passed - so we don't have to type PoolManager.GetPool
	pool = pmp;
	
	// set back to zero
	rb.velocity = Vector3.zero;
	rb.angularVelocity = Vector3.zero;
}

// use this instead of "Destroy"
function OnUnspawn()
{
	// 
}