using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class CharacterDriver : MonoBehaviour
{
	public class CharacterState
	{
		public Vector3 position = new Vector3();

		public Vector3 velocity = new Vector3();

		public CharacterState()
		{

		}

		public CharacterState(CharacterState s)
		{
			this.position = s.position;
			this.velocity = s.velocity;
		}

		public CharacterState(Vector3 position, Vector3 velocity)
		{
			this.position = position;

			this.velocity = velocity;
		}
	}

	public float prevmaxSurgeSpeed = 0f; // Forward, backward
	public float prevmaxSwaySpeed = 0f; // Side to side
	public float maxSurgeSpeed = 5f; // Forward, backward
	public float maxSwaySpeed = 5f; // Side to side
	public float jumpHeight = 4f;
    public bool isTouchingJumpSphere = false;


    CharacterController characterController;

	CharacterState currentState = new CharacterState();
	CharacterState previousState = new CharacterState();

	// Use this for initialization
	void Start()
	{
		prevmaxSwaySpeed = maxSwaySpeed;
		prevmaxSurgeSpeed = maxSurgeSpeed;

		this.characterController = GetComponent<CharacterController>();

		this.ResetCharacterDriver();
	}

	[ContextMenu("Reset Character State")]
	void ResetCharacterDriver()
	{
		// Set the transition state
		this.currentState = new CharacterState(transform.position, Vector3.zero);
		this.previousState = this.currentState;
	}

	float t = 0f;
	float dt = 0.01f;
	float currentTime = 0f;
	float accumulator = 0f;

	bool isFirstPhysicsFrame = true;

	// Update is called once per frame
	void Update()
	{
		float frameTime = Time.time - currentTime;
		this.currentTime = Time.time;

		this.accumulator += frameTime;

		while (this.accumulator >= this.dt)
		{
			this.previousState = this.currentState;
			this.currentState = this.MoveUpdate(this.currentState, this.dt);

			Vector3 movementDelta = currentState.position - transform.position;
			this.characterController.Move(movementDelta);
			this.currentState = new CharacterState(transform.position, this.currentState.velocity);

			accumulator -= this.dt;
			this.t += this.dt;
		}

		this.isFirstPhysicsFrame = true;

        //  if (this.characterController.velocity.normalized != Vector3.zero);
        if (Vector3.Distance(this.characterController.velocity.normalized, Vector3.zero) != 0)
        {

        }
        else
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            // AudioSource audio = GetComponent<AudioSource>();
            // audio.Stop();
        }


        //  print("Test vel" + this.characterController.velocity.normalized);
    }

	CharacterState MoveUpdate(CharacterState state, float deltaTime)
	{
		CharacterState currentState = new CharacterState(state);

		if (this.characterController.isGrounded)
		{
			currentState.velocity -= Vector3.Project(currentState.velocity, Physics.gravity.normalized);
		}

		currentState.velocity += Physics.gravity * deltaTime;
        //if (this.characterController.isGrounded && Input.GetButtonDown("Jump") && this.isFirstPhysicsFrame)
        if (this.characterController.isGrounded && isTouchingJumpSphere && Input.GetButtonDown("Jump") && this.isFirstPhysicsFrame)
        {
			currentState.velocity += -1f * Physics.gravity.normalized * this.CalculateJumpVerticalSpeed(this.jumpHeight);
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
        }

		if (this.characterController.isGrounded == false)
		{
			maxSurgeSpeed = 0;
			maxSwaySpeed = 0;

        }
		else
        {
            maxSurgeSpeed = prevmaxSurgeSpeed;
            maxSwaySpeed = prevmaxSwaySpeed;
        }
		float angle = Mathf.Atan2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
		float surgeSpeed = Mathf.Abs(Input.GetAxis("Vertical")) * this.maxSurgeSpeed * Mathf.Sin(angle);
		float swaySpeed = Mathf.Abs(Input.GetAxis("Horizontal")) * this.maxSwaySpeed * Mathf.Cos(angle);

		Vector3 cameraForwardNoY = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
		Vector3 cameraRightNoY = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)).normalized;

		Vector3 movementDelta = (cameraForwardNoY * surgeSpeed + cameraRightNoY * swaySpeed) * deltaTime;

		movementDelta += currentState.velocity * deltaTime;

		currentState.position += movementDelta;
		
		this.isFirstPhysicsFrame = false;

		return currentState;
	}


	float CalculateJumpVerticalSpeed(float targetJumpHeight)
	{
		return Mathf.Sqrt(2f * targetJumpHeight * Physics.gravity.magnitude);
	}


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "JumpSphere")
        {
            isTouchingJumpSphere = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "JumpSphere")
        {
            isTouchingJumpSphere = false;
        }
    }
}