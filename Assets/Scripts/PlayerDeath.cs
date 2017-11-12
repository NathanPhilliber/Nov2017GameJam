using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour, Mortal {

	public UnityStandardAssets._2D.Camera2DFollow camera;

	public void OnDeath(){
		//Destroy (gameObject);
		print("PLAYER DIED");
	}

	public void OnHit(Vector2 origin){
		print ("PLAYER HIT");
		camera.Shake ();
	}
}
