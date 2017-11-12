using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

	public GameObject[] objectsToTrigger;
	public LayerMask mask;



	private IInteractable[] objects;

	void Start(){
		objects = new IInteractable[objectsToTrigger.Length];
		for (int i = 0; i < objectsToTrigger.Length; i++) {
			objects [i] = objectsToTrigger [i].GetComponent<IInteractable> ();
		}
	}

	public void ProcessInteracts(){
		
			for (int i = 0; i < objects.Length; i++) {
				objects [i].TriggerAction ();
			}


	}


}
