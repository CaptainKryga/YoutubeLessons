using UnityEngine;

namespace Core.Lessons.Code023Events.Scripts
{
	public class Player2D : MonoBehaviour
	{
		[SerializeField] private Rigidbody2D rb;

		[SerializeField] private float moveSpeed = 5f;
		[SerializeField] private float jumpForce = 10f;
		[SerializeField] private LayerMask groundLayer;
		[SerializeField] private Transform groundCheck;
		[SerializeField] private float groundCheckRadius = 0.2f;

		private float horizontalInput;
		private bool isGrounded;

		private void Update()
		{
			horizontalInput = Input.GetAxis("Horizontal");
        
			if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
			{
				Jump();
			}
		}

		private void FixedUpdate()
		{
			GroundCheck();
			Move();
		}

		private void Move()
		{
			rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
		}

		private void Jump()
		{
			rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
		}

		private void GroundCheck()
		{
			isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
		}
	}
}
