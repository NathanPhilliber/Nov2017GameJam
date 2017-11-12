using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour, Mortal {

	public void OnDeath(){
		Destroy (gameObject);
	}

	public void OnHit(){
		
	}
}
