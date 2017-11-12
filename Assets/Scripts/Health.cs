using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public float maxHealth;
	public float health;

	public void ChangeHealth(float pts){
		health += pts;
		if (health > maxHealth) {
			health = maxHealth;
		}
		if (health < 0) {
			health = 0;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
