using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampBehavior : MonoBehaviour, IInteractable {

	public float swayAmount;
	public float swayFactor;
	public Color onColor, offColor;

	public SpriteRenderer lampSprite;

	private LightSource lightSource;
	private Light spotLight;


	void Start () {
		lightSource = GetComponentInChildren<LightSource> ();
		spotLight = lightSource.spotlight;
		lampSprite.color = onColor;
	}
	

	void FixedUpdate () {
		transform.rotation = Quaternion.Euler((Mathf.Sin(swayFactor * Time.frameCount) * swayAmount)*Vector3.forward);
	}

	public void TriggerAction(){
		TurnOff ();
	}

	public void TurnOff(){
		spotLight.gameObject.SetActive (false);
		lightSource.active = false;
		foreach (GameObject obj in lightSource.objectsInRange) {
			if (obj.CompareTag ("Player")) {
				lightSource.switcher.ExitLight ();
			}
		}
	}
}
