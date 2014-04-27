using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PigeonCoopToolkit.Navmesh2D;

public enum EnemyBehaviourType { Wander, Seek, Idle }

public class Enemy : EnhancedBehaviour {

	public float freezeTime = 5.0f;
	private ScoreManager scoreManager = null;

	/// <summary>
	/// The type of the current behaviour.
	/// </summary>
	EnemyBehaviourType curBehaviourType = EnemyBehaviourType.Idle;

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
		hitPoints = baseHitPoints;
		IsFrozen = false;
		collider2D.enabled = true;
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

	protected override void EnhancedFixedUpdate ()
	{
		base.EnhancedFixedUpdate ();

		if(!IsFrozen && path != null) {
			Body.MovePosition(nextPosition);
		}
	}

	List<Vector2> path;

#region Seek

	void EvaluatePathToSeek() {

		NavMesh2DBehaviour behaviour = NavMesh2D.GetNavMeshObject();
		NavMesh2DNode node = behaviour.ClosestNodeTo(player.position);

		if(node != null) {
			Vector2 nextPos = node.position;
			path = NavMesh2D.GetSmoothedPath(Body.position, nextPos);
		}
	}

	void SeekPlayer() {
		
		if(path == null || path.Count == 0)
		{
			EvaluatePathToSeek();
		}
		
		if(path != null) {
			
			if(Vector2.Distance(Body.position, player.position) < 0.2f) {
				curBehaviourType = EnemyBehaviourType.Idle;
				path = null;
			}
			else if (path.Count != 0) {
				
				nextPosition = Vector2.MoveTowards(Body.position, path[0], maxSpeed * Time.deltaTime);
				
				if(Vector2.Distance(Body.position, path[0]) < 0.01f)
				{
					path.RemoveAt(0);
				}
			}
		}

		if(maxTimeInState.IsOver(Random.Range(5f, 7f), true)) {
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

		if(maxTimeInState.IsOver(Random.Range(5f, 7f), true)) {
			curBehaviourType = EnemyBehaviourType.Seek;
			path = null;
		}
		
		maxTimeInState.UpdateTime(Time.deltaTime);
	}
	
#endregion

#region Idle

	void StayIdle() {

		if(maxTimeInState.IsOver(Random.Range(1f, 3f), true)) {
			curBehaviourType = EnemyBehaviourType.Wander;
			path = null;
		}

		maxTimeInState.UpdateTime(Time.deltaTime);
	}

#endregion

	[SerializeField]
	int baseHitPoints = 3;

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
			hitPoints--;
		}
		else if(col.CompareTag("AreaBullet")) {
			hitPoints = 0;
		}

		if(hitPoints <= 0) {
			StartCoroutine(Death ());
		}
	}

	IEnumerator Unfreeze()
	{
		yield return new WaitForSeconds(freezeTime);
		IsFrozen = false;
	}

	IEnumerator Death() {

		collider2D.enabled = false;
		IsFrozen = true;
		if (!scoreManager)
		{
			scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
		}
		scoreManager.Score += (int)(100 * scoreManager.Combo);
		scoreManager.Combo += 0.1f;
		scoreManager.UpdateScore();

		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		yield return StartCoroutine(spriteRenderer.FadeOut(0.25f));
		EnemySpawner.NumEnemies--;
		this.Recycle();

		yield return null;
	}

	protected override void EnhancedOnDisable ()
	{
		base.EnhancedOnDisable ();
	}
}
