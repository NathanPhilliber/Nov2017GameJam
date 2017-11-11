using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour {

	public float rayLength;

	public int numRays = 2;
	private float raySpacing;

	public LayerMask collisionMask;

	[HideInInspector]
	public float rangeMin;
	[HideInInspector]
	public float rangeMax;

	public Light spotlight;

	public List<GameObject> objectsInRange = new List<GameObject>();

	[HideInInspector]
	public PlayerSwitcher switcher;

	public bool active = true;

	void Start () {
		rangeMin = -(spotlight.spotAngle * .82f) / 100;
		rangeMax = -rangeMin;
		switcher = Camera.main.GetComponent<PlayerSwitcher> ();

		float totalAngle = rangeMax - rangeMin;
		raySpacing = totalAngle / numRays;
			
	}

	void Update () {

		if (active) {
			RayCheck ();
			PrecisionCheckInRange ();
		}
			
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
				switcher.ExitLight ();
			}
			objectsInRange.Remove(removeAfter[i]);

		}
	}

	void RayCheck(){

		for (float i = rangeMin; i <= rangeMax; i += raySpacing) {
			Vector2 vector = new Vector2 (Mathf.Cos (i - Mathf.PI / 2), Mathf.Sin (i - Mathf.PI / 2));

			Debug.DrawRay (transform.position, vector*rayLength, Color.yellow);

			RaycastHit2D hit = Physics2D.Raycast (transform.position, vector, rayLength, collisionMask);

			if (hit) {
				GameObject obj = hit.transform.gameObject;
				if (objectsInRange.Contains (obj) == false) {

					if (obj.CompareTag ("Player")) {
						objectsInRange.Add (switcher.InLight ());
					} else {
						objectsInRange.Add (obj);
					}
				}
			}
		}
	}


}
