using UnityEngine;
using System.Collections;

public class AnimateGrunt : EnhancedBehaviour {

	/// <summary>
	/// The enemy.
	/// </summary>
	Enemy enemy;

	/// <summary>
	/// My animator.
	/// </summary>
	Animator myAnimator;

	protected override void EnhancedOnEnable ()
	{
		base.EnhancedOnEnable ();

		if(!enemy) {
			enemy = GetComponent<Enemy>();
		}

		if(!myAnimator) {
			myAnimator = GetComponent<Animator>();
		}
	}

	protected override void EnhancedUpdate ()
	{
		base.EnhancedUpdate ();

		switch(enemy.CurBehaviourType) {
			case EnemyBehaviourType.Idle:
				myAnimator.CrossFade("idle", 0f);
				break;
			case EnemyBehaviourType.Attack:
				myAnimator.CrossFade("attack", 0f);
				break;
			case EnemyBehaviourType.Seek:
			case EnemyBehaviourType.Wander:
				myAnimator.CrossFade("walk", 0f);
				break;
			default:
				myAnimator.CrossFade("death", 0f);
				break;
		}
	}
}
