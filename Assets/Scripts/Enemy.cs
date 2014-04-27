using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PigeonCoopToolkit.Navmesh2D;

public enum EnemyBehaviourType { Wander, Seek, Idle, Attack, Death }

public class Enemy : EnhancedBehaviour {
	
	public float freezeTime = 5.0f;
	public GameObject frozenPrefab;
	private ScoreManager scoreManager = null;
	private LevelManager levelManager = null;
	
	/// <summary>
	/// The type of the current behaviour.
	/// </summary>
	EnemyBehaviourType curBehaviourType = EnemyBehaviourType.Idle;

	public EnemyBehaviourType CurBehaviourType {
		get {
			return curBehaviourType;
		}
	}
	
	/// <summary>
	/// Aqui a gente armazena
	/// a posiçao do heroi.
	/// </summary>
	[SerializeField]
	Transform player;
	
	public Transform Player {
		get {
			return player;
		}
		set {
			player = value;
		}
	}
	
	[SerializeField]
	Rigidbody2D body;
	
	public Rigidbody2D Body {
		get {
			if(!body) 
				body = rigidbody2D;
			return body;
		}
		set {
			body = value;
		}
	}
	
	/// <summary>
	/// Se isso estiver habilitado, o 
	/// personagem esta parado.
	/// </summary>
	bool isFrozen;
	
	public bool IsFrozen {
		get {
			return isFrozen;
		}
		set {
			isFrozen = value;
			if (isFrozen)
			{
				GameObject frozen = (GameObject)Instantiate(frozenPrefab, transform.position, Quaternion.identity);
				frozen.transform.parent = gameObject.transform;
				StartCoroutine("Unfreeze");
			}
		}
	}
	
	[SerializeField]
	float maxSpeed = 2f;
	
	/// <summary>
	/// The timer to evaluate path.
	/// </summary>
	Timer maxTimeInState = new Timer();
	
	protected override void EnhancedOnEnable ()
	{
		base.EnhancedOnEnable ();
		curBehaviourType = EnemyBehaviourType.Idle;
		hitPoints = baseHitPoints;
		IsFrozen = false;
		collider2D.enabled = true;
		MySpriteRenderer.color = Color.white;
		isHurt = false;
		path = null;
		mirrored = false;
		transform.right = Vector3.right;
	}
	
	protected override void EnhancedUpdate ()
	{
		base.EnhancedUpdate ();
		
		switch(curBehaviourType) {

			case EnemyBehaviourType.Wander:
				WalkRandomly();
				break;
			case EnemyBehaviourType.Idle:
				StayIdle();
				break;
			case EnemyBehaviourType.Attack:
				Attack();
				break;
			default:
				SeekPlayer();
				break;
		}
		
		gameObject.name = curBehaviourType.ToString();
	}
	
	/// <summary>
	/// The next position
	/// of the enemy.
	/// </summary>
	Vector3 nextPosition;

	bool mirrored = false;

	protected override void EnhancedFixedUpdate ()
	{
		base.EnhancedFixedUpdate ();
		
		if(!IsFrozen && path != null) {

			if(Body.position.x < nextPosition.x && !mirrored) {
				mirrored = true;
				transform.right = Vector3.left;
			}
			else if (Body.position.x > nextPosition.x && mirrored) {
				mirrored = false;
				transform.right = Vector3.right;
			}
			Body.MovePosition(nextPosition);
		}
	}

	protected override void EnhancedLateUpdate ()
	{
		base.EnhancedLateUpdate ();
		MySpriteRenderer.sortingOrder = Mathf.FloorToInt(-Body.position.y);
	}
	
	List<Vector2> path;
	
	#region Seek
	
	void EvaluatePathToSeek() {
		path = NavMesh2D.GetSmoothedPath(Body.position, player.position);
	}

	[SerializeField]
	Vector2 seekTimeInterval = new Vector2(5f, 7f);

	void SeekPlayer() {
		
		if(path == null || path.Count == 0)
		{
			EvaluatePathToSeek();
		}
		
		if(path != null) {
			
			if(Vector2.Distance(Body.position, player.position) < 0.2f) {
				curBehaviourType = EnemyBehaviourType.Attack;
				path = null;
				maxTimeInState.Reset();
			}
			else if (path.Count != 0) {
				
				nextPosition = Vector2.MoveTowards(Body.position, path[0], maxSpeed * Time.deltaTime);
				
				if(Vector2.Distance(Body.position, path[0]) < 0.01f)
				{
					path.RemoveAt(0);
				}
			}
		}

		if(maxTimeInState.IsOver(Random.Range(seekTimeInterval.x, seekTimeInterval.y), true)) {
			curBehaviourType = EnemyBehaviourType.Idle;
			path = null;
		}
		
		maxTimeInState.UpdateTime(Time.deltaTime);
	}
	
	#endregion
	
	#region Wander Behaviour
	
	void EvaluatePathToWander() {
		
		NavMesh2DBehaviour behaviour = NavMesh2D.GetNavMeshObject();
		NavMesh2DNode rndNode = behaviour.GetNode(Random.Range(0, behaviour.NavMesh2DNodes.Length));
		path = NavMesh2D.GetSmoothedPath(Body.position, rndNode.position);
	}

	[SerializeField]
	Vector2 wanderTimeInterval = new Vector2(2f, 3f);

	void WalkRandomly() {
		
		if(path == null || path.Count == 0)
		{
			EvaluatePathToWander();
		}
		
		if(path != null) {
			
			if (path.Count != 0) {
				
				nextPosition = Vector2.MoveTowards(Body.position, path[0], maxSpeed * Time.deltaTime);
				
				if(Vector2.Distance(Body.position, path[0]) < 0.01f)
				{
					path.RemoveAt(0);
				}
			}
		}

		if(maxTimeInState.IsOver(Random.Range(wanderTimeInterval.x, wanderTimeInterval.y), true)) {

			curBehaviourType = EnemyBehaviourType.Seek;
			path = null;
		}
		
		maxTimeInState.UpdateTime(Time.deltaTime);
	}

#endregion

#region Idle

	[SerializeField]
	Vector2 idleTimeInterval = new Vector2(0f, 1f);

	void StayIdle() {
		
		if(maxTimeInState.IsOver(Random.Range(idleTimeInterval.x, idleTimeInterval.y), true)) {
			curBehaviourType = EnemyBehaviourType.Wander;
			path = null;
		}
		
		maxTimeInState.UpdateTime(Time.deltaTime);
	}

#endregion

#region Attack

	void Attack() {

		if(maxTimeInState.IsOver(1f, true)) {

			if(Vector2.Distance(Body.position, player.position) >= 0.2f) {
				curBehaviourType = EnemyBehaviourType.Idle;
				path = null;
			}
		}
		
		maxTimeInState.UpdateTime(Time.deltaTime);
	}

#endregion
	
	[SerializeField]
	int baseHitPoints = 1;
	
	/// <summary>
	/// The current hit points.
	/// </summary>
	int hitPoints;
	
	public int HitPoints {
		get {
			return hitPoints;
		}
	}
	
	protected override void EnhancedOnTriggerEnter2D (Collider2D col)
	{
		base.EnhancedOnTriggerEnter2D (col);
		
		if(col.CompareTag("SingleBullet")) {
			if(!isHurt) {
				StartCoroutine(Hurt (1));
			}
		}
		else if(col.CompareTag("AreaBullet")) {
			StartCoroutine(Hurt (baseHitPoints));
		}
	}
	
	IEnumerator Unfreeze()
	{
		yield return new WaitForSeconds(freezeTime);
		IsFrozen = false;
		foreach (Transform t in transform)
		{
			if (t.gameObject.tag == "FrozenFX")
			{
				Destroy(t.gameObject);
			}
		}
	}

	/// <summary>
	/// My sprite renderer.
	/// </summary>
	SpriteRenderer mySpriteRenderer;

	public SpriteRenderer MySpriteRenderer {
		get {
			if(!mySpriteRenderer)
				mySpriteRenderer = GetComponent<SpriteRenderer>();
			return mySpriteRenderer;
		}
	}


	[SerializeField]
	float hurtTime = 0.2f;

	bool isHurt = false;

	IEnumerator Hurt(int damage) {

		isHurt = true;
		hitPoints -= damage;

		if(hitPoints <= 0) {
			StartCoroutine(Death ());
		}
		else {
			yield return StartCoroutine(MySpriteRenderer.ColorTo(Color.red, hurtTime / 2f));
			yield return StartCoroutine(MySpriteRenderer.ColorTo(Color.white, hurtTime / 2f));
			isHurt = false;
		}
	}
	
	public Transform ExplosionPool {
		get;
		set;
	}
	
	IEnumerator Death() {
		
		collider2D.enabled = false;
		IsFrozen = true;

		curBehaviourType = EnemyBehaviourType.Death;

		if (!scoreManager)
		{
			scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		}
		scoreManager.Score += (int)(100 * scoreManager.Combo);
		scoreManager.Combo += 0.1f;
		scoreManager.UpdateScore();

		yield return new WaitForSeconds(0.65f);
//			StartCoroutine(MySpriteRenderer.FadeOut(0.85f));

		EnemySpawner.NumEnemies--;

		if (!levelManager)
		{
			levelManager = GameObject.Find("Game").GetComponent<LevelManager>();
		}

		levelManager.enemiesOnLevel--;
		this.Recycle();
		
		yield return null;
	}

	public void RocketKill()
	{
		StartCoroutine("Death");
	}
	
	protected override void EnhancedOnDisable ()
	{
		base.EnhancedOnDisable ();
	}
}
