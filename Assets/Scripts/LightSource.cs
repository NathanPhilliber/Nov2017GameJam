using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour {

	public float rayLength;

	public LayerMask collisionMask;

	[HideInInspector]
	public float rangeMin;
	[HideInInspector]
	public float rangeMax;

	public Light spotlight;

	public List<GameObject> objectsInRange = new List<GameObject>();

	private PlayerSwitcher switcher;

	void Start () {
		rangeMin = -(spotlight.spotAngle * .82f) / 100;
		rangeMax = -rangeMin;
		switcher = Camera.main.GetComponent<PlayerSwitcher> ();
			
	}

	void Update () {

		RayCheck ();
		PrecisionCheckInRange ();
			
	}

	void PrecisionCheckInRange(){
		List<GameObject> removeAfter = new List<GameObject> ();
		foreach (var obj in objectsInRange) {
			Vector2 objVector = new Vector2 (obj.transform.position.x - transform.position.x, obj.transform.position.y - transform.position.y);
			Debug.DrawRay (transform.position, objVector, Color.green);
			float angle = Mathf.Deg2Rad*Vector2.Angle (objVector, Vector2.down);

			if (angle < rangeMin || angle > rangeMax) {
				removeAfter.Add (obj);
			}
		}
		for (int i = removeAfter.Count - 1; i >= 0; i--) {
			if (removeAfter[i].CompareTag ("Player")) {
				switcher.Switch ();
			}
			objectsInRange.Remove(removeAfter[i]);

		}
	}

	void RayCheck(){
		Vector2 minVector = new Vector2 (Mathf.Cos (rangeMin - Mathf.PI / 2), Mathf.Sin (rangeMin - Mathf.PI / 2));
		Vector2 maxVector = new Vector2 (Mathf.Cos (rangeMax - Mathf.PI / 2), Mathf.Sin (rangeMax - Mathf.PI / 2));

		Debug.DrawRay (transform.position, minVector*rayLength, Color.yellow);
		Debug.DrawRay (transform.position, maxVector*rayLength, Color.yellow);

		RaycastHit2D hitMin = Physics2D.Raycast (transform.position, minVector, rayLength, collisionMask);
		RaycastHit2D hitMax = Physics2D.Raycast (transform.position, maxVector, rayLength, collisionMask);

		if (hitMin || hitMax) {
			GameObject obj = hitMin ? hitMin.transform.gameObject : hitMax.transform.gameObject;
			if (objectsInRange.Contains (obj) == false) {
				
				if (obj.CompareTag ("Player")) {
					objectsInRange.Add (switcher.Switch ());
				} else {
					objectsInRange.Add (obj);
				}
			}
		}
	}
}
