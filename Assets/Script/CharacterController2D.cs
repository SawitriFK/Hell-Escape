using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[Header("Movement Parameters")]
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] public LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck = null;							// A position marking where to check if the player is grounded.
	[SerializeField] private Animator anim = null;

	[Header("Jump Parameter")]
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private float fallMultiplier = 2.5f;
	[SerializeField] private float floatMultiplier = 0.5f;

	[Header("Dash Parameters")]
	[SerializeField] private float distance = 5.0f;
	[SerializeField] private Transform raycast = null;							// A position marking where to check if the player is grounded.

	[Header("Enemy Layer")]
	[SerializeField] public LayerMask enemyLayer;

	[Header("Mana Bar")]
	public Bar manaBar;
	[SerializeField] private float maxMana = 0;

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	private bool canDoubleJump;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		manaBar.SetMaxValue(maxMana);
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				canDoubleJump = true;
				
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}

	public void Move(float move, bool jump, bool dash)
	{
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerFight") || anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerFight2"))
		{
			m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
			return;
		}

		if(!m_Grounded)
		{
			if(m_Rigidbody2D.velocity.y < 0)
			{
				m_Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
			}else
			{
				if(Input.GetKey(KeyCode.UpArrow))
				{
					m_Rigidbody2D.velocity += Vector2.up * Physics2D.gravity.y * (floatMultiplier - 1) * Time.deltaTime;
				}
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			if(m_Grounded)
			{
				anim.SetBool("isWalk", true);
			}

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}else if(move == 0)
			{
				anim.SetBool("isWalk", false);
			}
		}
		// If the player should jump...
		if (jump)
		{
			if(m_Grounded)
			{
				// Add a vertical force to the player.
				m_Grounded = false;
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
				anim.SetTrigger("jump");
			}else if(canDoubleJump)
			{
				// Add a vertical force to the player.
				m_Grounded = false;
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce / 1.5f));
				anim.SetTrigger("jump");
				canDoubleJump = false;
			}
		}

		// If the player dash
		if(dash)
		{
			Vector3 targetVelocity = new Vector2(move * 10f, 0f);
			Vector3 dir = targetVelocity.normalized;

			Collider2D adaColl = Physics2D.Raycast(raycast.position, dir, distance, ~enemyLayer).collider;
			
			if(adaColl == null)
			{
				transform.position += dir * distance;
			}
		}
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public bool isGrounded()
	{
		return m_Grounded;
	}
}