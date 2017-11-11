using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitcher : MonoBehaviour {

	public Transform attackTargetObject;
	public GameObject ninja;
	public GameObject oldMan;

	public GameObject Switch(){
		if (ninja.activeSelf) {
			ninja.SetActive (false);
			oldMan.transform.position = ninja.transform.position;
			oldMan.SetActive (true);
			attackTargetObject.SetParent (oldMan.transform);
			return oldMan;
		} else {
			oldMan.SetActive (false);
			ninja.transform.position = oldMan.transform.position;
			ninja.SetActive (true);
			attackTargetObject.SetParent (ninja.transform);
			return ninja;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
