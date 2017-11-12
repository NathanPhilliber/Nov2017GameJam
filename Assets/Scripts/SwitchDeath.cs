using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDeath : MonoBehaviour, Mortal {

	public Switch mySwitch;

	public void OnDeath(){
		mySwitch.ProcessInteracts ();
	}

	public void OnHit(Vector2 origin){

	}

}
