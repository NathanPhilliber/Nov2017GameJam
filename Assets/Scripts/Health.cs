using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mortal))]
public class Health : MonoBehaviour {

	public float maxHealth;
	public float health;

	private Mortal deathAction;

	public void ChangeHealth(float pts){
		health += pts;
		deathAction.OnHit ();
		if (health > maxHealth) {
			health = maxHealth;
		}
		if (health <= 0) {
			health = 0;
			deathAction.OnDeath ();
		}
	}

	// Use this for initialization
	void Start () {
		deathAction = GetComponent<Mortal> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public interface Mortal{
	void OnDeath();
	void OnHit();
}