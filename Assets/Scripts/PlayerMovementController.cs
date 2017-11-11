﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

	public float moveAcceleration;
	public float moveDeceleration;
	public float maxMoveSpeed;
	public float jumpSpeed;
	public float gravity;
	public float maxJumpHeight;

	public int maxJumpsInRow;
	private int currentJumps = 0;

	public float horizontalAirMovementThreshold;

	public float skinWidth = .03f;
	public int horizontalRayCount = 4;
	public int verticalRayCount = 4;
	public LayerMask collisionMask;

	[HideInInspector]
	public float horizontalRaySpacing;
	[HideInInspector]
	public float verticalRaySpacing;

	private BoxCollider2D collider;
	private Vector2 velocity;

	public RaycastOrigins raycastOrigins;

	void Start(){
		collider = GetComponent<BoxCollider2D> ();
		CalculateRaySpacing();
	}

	void Update(){
		if (Input.GetButtonDown ("Jump") && currentJumps < maxJumpsInRow) {
			Jump ();
		}
	}

	void FixedUpdate(){
		MoveUpdate ();
	}

	void Jump(){
		velocity.y = jumpSpeed;
		currentJumps++;
	}

	void MoveUpdate(){

		// Horizontal Movement
		float moveDir = Input.GetAxisRaw("Horizontal");
		if (moveDir == 0) { // Process deceleration on horizontal
			float oldX = velocity.x;
			velocity.x -= (Mathf.Sign (velocity.x) * moveDeceleration);
			if (Mathf.Sign (velocity.x) != Mathf.Sign (oldX)) {
				velocity.x = 0;
			}
		} else { // Process acceleration on horizontal
			//If we switch directions, give extra help to slow down faster
			if (Mathf.Sign (velocity.x) != moveDir) {
				velocity.x += moveDir * moveDeceleration;
			}
			// Move acceleration
			if (Mathf.Abs (velocity.y) < horizontalAirMovementThreshold) {
				velocity.x += moveDir * moveAcceleration;
			}
			//Cap move speed
			if (Mathf.Abs (velocity.x) > maxMoveSpeed) {
				velocity.x = moveDir * maxMoveSpeed;
			}
		}

		// Vertical Movement
		velocity.y -= gravity;
		UpdateRaycastOrigins ();
		ProcessHorizontalCollisions(ref velocity);
		ProcessVerticalCollisions(ref velocity);

		transform.Translate (velocity);
	}

	public void ProcessHorizontalCollisions(ref Vector2 velocity){
		float directionX = Mathf.Sign (velocity.x);
		float rayLength = Mathf.Abs (velocity.x) + skinWidth;

		for (int i = 0; i < horizontalRayCount; i++) {
			Vector2 rayOrigin = (directionX == -1) ? 
				raycastOrigins.bottomLeft : raycastOrigins.bottomRight;

			rayOrigin += Vector2.up * (horizontalRaySpacing * i);
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

			Debug.DrawRay (rayOrigin, Vector2.right * -directionX * rayLength, Color.red);

			if (hit) {
				velocity.x = (hit.distance - skinWidth) * directionX;
			}

		}
	}

	public void ProcessVerticalCollisions(ref Vector2 velocity){
		float directionY = Mathf.Sign (velocity.y);
		float rayLength = Mathf.Abs (velocity.y) + skinWidth;
		for (int i = 0; i < verticalRayCount; i++) {
			Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;

			rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

			Debug.DrawRay (rayOrigin, Vector2.up * directionY * rayLength, Color.red);

			if (hit) {
				if (velocity.y < 0) {
					currentJumps = 0;
				}
				velocity.y = (hit.distance - skinWidth) * directionY;
			}

		}
			
	}

	public void UpdateRaycastOrigins(){
		Bounds bounds = collider.bounds;
		bounds.Expand(skinWidth * -2);

		raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
	}

	void CalculateRaySpacing(){
		Bounds bounds = collider.bounds;
		bounds.Expand(skinWidth * -2);

		horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
		verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}

	public struct RaycastOrigins{
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}

}
