using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement : MonoBehaviour {

	#region Jason

	//Serializable Components
	[Header("Cache Components")]
	[SerializeField] Rigidbody2D playerRB;
	[SerializeField] Animator playerAnim;
	[SerializeField] SpriteRenderer playerSR;
	[SerializeField] Transform playerFeet;
	[SerializeField] Transform playerHead;
	[SerializeField] Collider2D m_CrouchDisableCollider;

	//Serializable private fields
	[Header("Movement")]
	[SerializeField] float _speed = 5f;
	[Range(0, 1)][SerializeField] float _crouchSpeed = 0.36f;
	[Header("Jump")]
	[SerializeField] float _jumpVelocity = 10f;
	[SerializeField] int _maxJumps = 2;
	[SerializeField] float _downPull = 0.1f;
	[SerializeField] float _maxJumpDuration = 0.1f;

	//private fields
	Vector2 _startPosition;
	int _jumpsRemaining;
	bool isGrounded = true;
	bool isCrouching = false;
	float _fallTimer;
	float _jumpTimer;
	float _horizontal;

	//you can avoid an if entirely with
	//bool walking = horizontal != 0; - horizontal != 0 would be true, so anything other than that would be false
	//bool flipX = horizontal < 0; - horizontal < 0 would be true, so anything other than that would be false

	// Start is called before the first frame update
	void Start() {
		_startPosition = transform.position;
		_jumpsRemaining = _maxJumps;
	}

	// Update is called once per frame
	void Update() {

		UpdatAnimator();

		CalculateIsGrounded();
		CalculateIsCrouching();

		// Get the Input Movement from the player
		_horizontal = Input.GetAxis($"Horizontal") * _speed;
		UpdateHorizontalMovement();

		PerformJumpingCalculations();
		PerformCrouchingCalculations();
	}

	void PerformJumpingCalculations() {

		//JUMP GOING UP
		//Jump and Double Jump
		if (Input.GetButtonDown($"Jump") && _jumpsRemaining > 0) {

			_jumpsRemaining--;
			Debug.Log($"Jumps Remaining: {_jumpsRemaining}");
			playerRB.velocity = new Vector2(playerRB.velocity.x, _jumpVelocity);
			//playerRB.AddForce(Vector2.up * _jumpForce);
			_fallTimer = 0f;
			_jumpTimer = 0f;

		} //Hold Jump
		  else if (Input.GetButton($"Jump") && _jumpTimer <= _maxJumpDuration) {
			playerRB.velocity = new Vector2(playerRB.velocity.x, _jumpVelocity);
			_fallTimer = 0;
		}

		//we don't wanna increment this in the if because that will just add a few milliseconds in and we will get a third jump on hold
		//we don't care if the jump timer keeps incrementing as long as it is reset when we start the jump
		_jumpTimer += Time.deltaTime;

		// JUMP COMING DOWN
		//Checking and applying motion for grounding the player after the jumping
		//have been falling!!
		if (isGrounded && _fallTimer > 0) {

			_fallTimer = 0f;
			_jumpsRemaining = _maxJumps;

		} else {

			_fallTimer += Time.deltaTime;
			var downForce = _downPull * _fallTimer * _fallTimer;
			playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y - downForce);
		}
	}

	void PerformCrouchingCalculations() {

		if (Input.GetButton("Crouch") && isGrounded) {
			isCrouching = true;
			// Reduce the speed by the crouchSpeed multiplier
			_speed *= _crouchSpeed;

			// Disable one of the colliders when crouching
			if (m_CrouchDisableCollider != null)
				m_CrouchDisableCollider.enabled = false;
		} else {

			isCrouching = false;
			// Reduce the speed by the crouchSpeed multiplier
			_speed = 5f; ;

			// Enable the collider when not crouching
			if (m_CrouchDisableCollider != null)
				m_CrouchDisableCollider.enabled = true;
		}
	}

	void UpdateHorizontalMovement() {

		//Move Player based on movement
		playerRB.velocity = new Vector2(_horizontal, playerRB.velocity.y);
		//Debug.Log($"velocity = {playerRB.velocity}");
	}

	void UpdatAnimator() {
		// Set the animation of player based on movement
		playerAnim.SetBool("isJumping", !isGrounded);
		playerAnim.SetBool("isCrouching", isCrouching);

		//playerAnim.SetBool("isWalking", _horizontal != 0);
		if (_horizontal != 0) {
			//flip player model based on movement
			playerSR.flipX = _horizontal < 0;
		}
			
	}

	void CalculateIsGrounded() {
		//Check if feet are on the Ground with OverlapCircle Raycast
		var hit = Physics2D.OverlapCircle(playerFeet.position, 0.1f, LayerMask.GetMask("Ground"));
		isGrounded = hit != null;
	}

	void CalculateIsCrouching() {
		//Check if head is hitting anything with OverlapCircle Raycast
		var hit = Physics2D.OverlapCircle(playerHead.position, 0.1f, LayerMask.GetMask("Ground"));
		isCrouching = hit != null;
	}

	internal void ResetToStart() {
		playerRB.position = _startPosition;
	}

	internal void TeleportTo(Vector3 position) {
		playerRB.position = position;
		playerRB.velocity = Vector2.zero;
	}

	public void LookAtMouse(Transform playerArm, Transform Torchlight, bool torch) {

		//only work if torch is on
		if (torch) {

			Vector3 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			Vector3 aimDirection = (mouse_position - transform.position).normalized;
			float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
			playerArm.eulerAngles = new Vector3(0, 0, angle);

			Vector3 localScale = Vector3.one;
			if (angle > 90 || angle < -90) {
				localScale.x = playerArm.localScale.x;
				localScale.y = -0.03f;
				Torchlight.localEulerAngles = new Vector3(0, 0, -Torchlight.localEulerAngles.z);
			} else {
				localScale.x = playerArm.localScale.x;
				localScale.y = +0.03f;
				Torchlight.localEulerAngles = new Vector3(0, 0, Torchlight.localEulerAngles.z);
			}

			playerArm.localScale = localScale;
        }
	}

	#endregion

	/*
	#region Brackeys

	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Transform m_Model;
	[SerializeField] private Transform m_Arm;
	[SerializeField] private Transform m_Torch;
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
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

	public bool GetCurrentCrouch { get { return m_currCrouch; } set { m_currCrouch = value; } }

	private void Awake() {
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
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
			m_Arm.eulerAngles = new Vector3(0, 0, angle);

			Vector3 localScale = Vector3.one;
			if(angle > 90 || angle < -90) {
				localScale.x = m_Arm.localScale.x;
				localScale.y = -0.03f;
				//m_Torch.eulerAngles = new Vector3(0, 0, -m_Torch.eulerAngles.z);
			} else {
				localScale.x = m_Arm.localScale.x;
				localScale.y = +0.03f;
				//m_Torch.eulerAngles = new Vector3(0, 0, m_Torch.eulerAngles.z);
			}

			m_Arm.localScale = localScale;
		}
	}
	private void Flip() {
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		//m_SpriteRenderer.flipY = m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = m_Model.localScale;
		theScale.x *= -1;
		m_Model.localScale = theScale;
	}
	#endregion
	*/

}