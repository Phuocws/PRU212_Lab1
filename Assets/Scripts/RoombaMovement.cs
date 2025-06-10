using System.Collections;
using UnityEngine;

public class RoombaMovement : MonoBehaviour
{
	public float moveSpeed = 2f;
	public float rotationPauseTime = 0.3f; // Time before turning
	public float rotationDuration = 0.5f;  // Time to rotate to new direction
	public float collisionCooldown = 0.5f;

	private Rigidbody2D rb;
	private Vector2 moveDirection;
	private float lastCollisionTime = -10f;
	private bool isRotating = false;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.gravityScale = 0;
		rb.freezeRotation = true;

		float randomAngle = Random.Range(0f, 360f);
		transform.rotation = Quaternion.Euler(0f, 0f, randomAngle);
		moveDirection = transform.right;
	}

	void FixedUpdate()
	{
		if (isRotating) return;

		Vector2 newPosition = rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime;
		rb.MovePosition(newPosition);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (Time.time - lastCollisionTime < collisionCooldown || isRotating)
			return;

		lastCollisionTime = Time.time;
		StartCoroutine(RotateAfterPause(collision.contacts[0].normal));
	}

	private IEnumerator RotateAfterPause(Vector2 normal)
	{
		isRotating = true;

		// Step 1: Jerk back immediately
		Vector2 jerkBack = -moveDirection * 0.2f; // Tune the 0.2f for jerk distance
		rb.MovePosition(rb.position + jerkBack);

		// Step 2: Wait after jerk (simulate processing time)
		yield return new WaitForSeconds(rotationPauseTime);

		// Step 3: Calculate reflected direction
		Vector2 reflectedDirection = Vector2.Reflect(moveDirection, normal).normalized;

		// Optional: Add slight randomness to new direction
		float randomOffset = Random.Range(-30f, 30f);
		reflectedDirection = Quaternion.Euler(0, 0, randomOffset) * reflectedDirection;

		// Step 4: Smooth rotation
		float startAngle = transform.eulerAngles.z;
		float targetAngle = Mathf.Atan2(reflectedDirection.y, reflectedDirection.x) * Mathf.Rad2Deg;

		// Normalize to avoid 360° jump
		if (targetAngle - startAngle > 180f) targetAngle -= 360f;
		if (startAngle - targetAngle > 180f) startAngle -= 360f;

		float elapsed = 0f;
		while (elapsed < rotationDuration)
		{
			float t = elapsed / rotationDuration;
			float angle = Mathf.Lerp(startAngle, targetAngle, t);
			transform.rotation = Quaternion.Euler(0f, 0f, angle);
			elapsed += Time.deltaTime;
			yield return null;
		}

		transform.rotation = Quaternion.Euler(0f, 0f, targetAngle);
		moveDirection = new Vector2(Mathf.Cos(targetAngle * Mathf.Deg2Rad), Mathf.Sin(targetAngle * Mathf.Deg2Rad)).normalized;

		isRotating = false;
	}
}