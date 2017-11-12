using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitRange : MonoBehaviour {
	public LayerMask hitMask;

	public float damage;

	public int hitCooldown;
	private int hitcool;

	void OnTriggerStay2D(Collider2D other){
		if (hitcool <= 0 && hitMask == (hitMask | (1 << other.gameObject.layer))) {
			other.GetComponent<Health> ().ChangeHealth (-damage, Vector2.zero);
			hitcool = hitCooldown;
		}
		if (hitcool > 0) {
			hitcool--;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
