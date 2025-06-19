using UnityEngine;
using UnityEngine.InputSystem;

public class RoombaController : MonoBehaviour
{
	[SerializeField] private float speed = 5f;
	[SerializeField] private float rotationSpeed = 180f;

	private PlayerInput playerInput;
	private InputAction moveAction;
	private Rigidbody2D rb;

	private void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		moveAction = playerInput.actions["Move"]; // Must match your Input Action map name
		rb = GetComponent<Rigidbody2D>();
		rb.gravityScale = 0f;
		rb.freezeRotation = true;
	}

	private void FixedUpdate()
	{
		Vector2 moveInput = moveAction.ReadValue<Vector2>();
		float rotationInput = moveInput.x; // Left/right input 
		float forwardInput = moveInput.y;  // Forward/backward input 

		// Rotate
		float rotationAmount = rotationInput * rotationSpeed * Time.fixedDeltaTime;
		transform.Rotate(0f, 0f, -rotationAmount);

		// Move in the facing direction (local right in 2D)
		Vector2 moveDirection = transform.right * forwardInput * speed;
		rb.linearVelocity = moveDirection;
	}
}
