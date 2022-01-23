/* Original File by Brackeys 
 * 2D Character Controller @ https://github.com/Brackeys/2D-Character-Controller/blob/master/CharacterController2D.cs
 */

using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement : MonoBehaviour {
	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Transform m_Torchlight;
	[SerializeField] private Transform m_TorchAngleOffset;
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private SpriteRenderer m_SpriteRenderer;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	//get current crouch for animation property
	bool m_currCrouch;

	//look at mouse position
	float raycast_depth = 100.0f;
	LayerMask floor_layer_mask;
	string floor_layer_name = "BackWall";

	public bool GetCurrentCrouch { get { return m_currCrouch; } set { m_currCrouch = value; } }

	private void Awake() {
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

    private void Start() {
		floor_layer_mask = LayerMask.GetMask(floor_layer_name);
    }

    private void FixedUpdate() {
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++) {
			if (colliders[i].gameObject != gameObject) {
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, bool crouch, bool jump) {
		// If crouching, check to see if the character can stand up
		if (!crouch) {
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround)) {
				crouch = true;
			}
		}

		m_currCrouch = crouch;

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl) {

			// If crouching
			if (crouch) {
				if (!m_wasCrouching) {
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else {
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching) {
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight) {
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight) {
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump) {
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}

	public void LookAtMouse(bool torch) {

		//only work if torch is on
		if(torch) {
			Vector3 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			Vector3 aimDirection = (mouse_position - transform.position).normalized;
			float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
			m_Torchlight.eulerAngles = new Vector3(0, 0, angle);

			Vector3 localScale = Vector3.one;
			if(angle > 90 || angle < -90) {
				localScale.x = m_Torchlight.localScale.x;
				localScale.y = -1f;
			} else {
				localScale.x = m_Torchlight.localScale.x;
				localScale.y = +1f;
			}

			m_Torchlight.localScale = localScale;

			/*
			Vector3 mouse_position = Input.mousePosition;

			Ray mouse_ray = Camera.main.ScreenPointToRay(mouse_position);
			RaycastHit[] mouse_hits = Physics.RaycastAll(mouse_ray, raycast_depth, floor_layer_mask);

			if (mouse_hits.Length == 1) {
				
				Vector3 hit_position = mouse_hits[0].point;

				Vector3 local_hit_position = m_Torchlight.InverseTransformPoint(hit_position);
				local_hit_position.y = 0;
				Vector3 look_position = m_Torchlight.TransformPoint(local_hit_position);

				//Using Quaternion
				Vector3 look_direction = look_position - m_Torchlight.position;
				m_Torchlight.rotation = Quaternion.LookRotation(look_direction, m_Torchlight.up);
			}
			*/
		}
	}

	private void Flip() {
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		m_SpriteRenderer.flipY = m_FacingRight;
		/*
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		*/
	}


}