using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitcher : MonoBehaviour {

	public Transform attackTargetObject;
	public GameObject ninja;
	public GameObject oldMan;

	public ParticleSystem transitionEffect;

	private int inRanges = 0;

	public GameObject InLight(){
		inRanges++;
		if (oldMan.activeSelf == false) {
			ninja.SetActive (false);
			oldMan.transform.position = ninja.transform.position;
			oldMan.SetActive (true);
			attackTargetObject.SetParent (oldMan.transform);

			transitionEffect.gameObject.transform.position = oldMan.transform.position;
			transitionEffect.Play ();

			return oldMan;
		}
		return oldMan;
	}

	public GameObject ExitLight(){
		inRanges--;
		if (ninja.activeSelf == false && inRanges == 0) {
			oldMan.SetActive (false);
			ninja.transform.position = oldMan.transform.position;
			ninja.SetActive (true);
			attackTargetObject.SetParent (ninja.transform);

			transitionEffect.gameObject.transform.position = ninja.transform.position;
			transitionEffect.Play ();

			return ninja;
		}
		return ninja;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
