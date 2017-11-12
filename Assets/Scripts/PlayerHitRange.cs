using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitRange : MonoBehaviour {

	public LayerMask hitMask;

	public float damage;

	void OnTriggerEnter2D(Collider2D other){
		if (hitMask == (hitMask | (1 << other.gameObject.layer))) {
			other.GetComponent<Health> ().ChangeHealth (-damage);

		}

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
