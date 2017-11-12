using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitRange : MonoBehaviour {

	public LayerMask hitMask;

	public float damage;
	public int attackFrames;
	private int timeLeft;

	public Animator animator;



	void OnEnable(){
		timeLeft = attackFrames;

	}

	void OnTriggerEnter2D(Collider2D other){
		if (hitMask == (hitMask | (1 << other.gameObject.layer))) {
			other.GetComponent<Health> ().ChangeHealth (-damage, animator.gameObject.transform.position);

		}

	}

	void FixedUpdate(){
		if (--timeLeft <= 0) {
			animator.SetInteger ("playerState", 0);
			gameObject.SetActive (false);

		}
	}

}
