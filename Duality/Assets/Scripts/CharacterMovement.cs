using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement : MonoBehaviour {

	#region Player Movement

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
	[Range(0, 1)] [SerializeField] float _crouchSpeed = 0.36f;
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

	public Transform PlayerFeet { get { return playerFeet; } }

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

			//update arm with player direction
			Vector3 localScale = Vector3.one;
			if (_horizontal < 0) {
				//flip player arm based on movement
				localScale.x = -0.04f;
				localScale.y = playerArm.localScale.y;
			} else if (_horizontal > 0) {
				localScale.x = +0.04f;
				localScale.y = playerArm.localScale.y;
			} else {
				localScale.x = playerArm.localScale.x;
				localScale.y = playerArm.localScale.y;
			}

			playerArm.localScale = localScale;

			Vector3 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			Vector3 aimDirection = (mouse_position - transform.position).normalized;
			float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
			playerArm.eulerAngles = new Vector3(0, 0, angle);

			if (angle > 90) {
				playerArm.eulerAngles = new Vector3(0, 0, 90);

			} else if (angle < -90) {
				playerArm.eulerAngles = new Vector3(0, 0, -90);
			}
		}
	}

	#endregion
}