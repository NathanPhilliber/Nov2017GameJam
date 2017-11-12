using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour, Mortal {

	public EnemyMovementController enemyController;
	public Vector2 knockbackAmount;

	public void OnDeath(){
		Destroy (gameObject);
	}

	public void OnHit(Vector2 origin){
		enemyController.velocity.x = -Mathf.Sign(origin.x - gameObject.transform.position.x) * knockbackAmount.x;
		enemyController.velocity.y = knockbackAmount.y;
		enemyController.stopMovingFrames = 30;
	}
}
