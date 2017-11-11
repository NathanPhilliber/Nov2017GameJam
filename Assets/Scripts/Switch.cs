using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

	public IInteractable[] objectsToTrigger;
	public LayerMask mask;

	public void ProcessInteracts(GameObject other){
		if (mask == (mask | (1 << other.layer))) {
			for (int i = 0; i < objectsToTrigger.Length; i++) {
				objectsToTrigger [i].TriggerAction ();
			}

		}
	}
}
