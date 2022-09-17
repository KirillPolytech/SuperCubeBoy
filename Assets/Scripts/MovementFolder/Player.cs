using UnityEngine;
namespace MovementFolder
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class Player : MonoBehaviour
    {
        Rigidbody Rb;

        private Animator PlayerAnimator;
        [SerializeField] AnimationCurve Curve;

        public bool IsIdle = false;
        public bool IsMoving = false;
        public bool IsJumped = false;
        public bool IsWallSliding = false;
        public bool IsSprint = false;

        public bool IsGrounded = false;
        public bool IsDead = false;

        public bool IsInputJumpDown = false;
        public bool IsInputWallSlidingDown = false;
        public bool IsInputShift = false;

        float AxisX;
        Vector3 MoveDirection;
        public float Speed = 15f;

        public float MaxIsGroundedRay = 0.51f;
        Ray IsGroundedRay;

        [Header("Movement")]
        // Movement.
        public float AirSpeed = 50f;
        // Jump.
        [Header("Jump")]
        public float JumpForce = 250f;
        // Sliding.
        [Header("Sliding")]
        Ray SlideRay, SlideRay2;
        RaycastHit Hit;
        public float MaxCollisionRayDistance;
        public float WallSlidingForce = 15f;
        public float WallJump = 250f;
        public float WallJumpUp = 15f;
        // Sprint.
        [Header("Sprint")]
        public float SprintSpeed = 15f;
        // Gravity.
        public float Gravity = 9.81f;
        void Start()
        {
            Rb = GetComponent<Rigidbody>();
            PlayerAnimator = GetComponent<Animator>();
        }
        private void Update()
        {
            if (Input.GetButtonDown("Jump") && IsGrounded)
            {
                IsInputJumpDown = true;
            }
            if (Input.GetButtonDown("Jump") && IsWallSliding)
            {
                IsInputWallSlidingDown = true;
            }
            if (Input.GetButton("Fire3"))
            {
                IsInputShift = true;
            }
            else
            {
                IsInputShift = false;
            }
        }
        private void FixedUpdate()
        {
            Rb.AddForce(Vector3.down * Gravity);

            AxisX = Input.GetAxis("Horizontal");
            MoveDirection = new(AxisX, 0, 0);
            MoveDirection = MoveDirection.normalized;

            IsGroundedRay = new(transform.position, Vector3.down);
            if (Physics.Raycast(IsGroundedRay, MaxIsGroundedRay))
            {
                IsGrounded = true;
            }
            else
            {
                IsGrounded = false;
            }

            Idle();
            Sprint();
            PlayerMovement();
            Jump();
            WallSliding();

            Debug.DrawRay(transform.position, Vector3.down * MaxIsGroundedRay, Color.green);
            Debug.DrawRay(transform.position, MoveDirection, Color.red);
        }
        public void Idle() 
        {
            if (Rb.velocity == Vector3.zero)
            {
                IsIdle = true;
            }
            else { IsIdle = false; }
        }

        public void PlayerMovement()
        {
            if (AxisX != 0 && IsGrounded == true && Rb.velocity.magnitude <= 675 )
            {
                Rb.velocity = MoveDirection * Speed;
                IsMoving = true;
            }
            else if (IsJumped)
            {
                Rb.AddForce(MoveDirection * AirSpeed);
                IsMoving = true;
            }
            else
            {
                Rb.velocity = Vector3.zero;
                IsMoving = false;
            }
        }
        public void Jump()
        {
            // Correct.
            // Если луч касается земли и нажата Space, то прыгаем. 
            if (IsGrounded && IsInputJumpDown) // IsGrounded && Input.GetButton("Jump") && JumpTimer >= JumpDelay
            {
                Rb.AddForce((Vector3.up + MoveDirection) * JumpForce);
                IsJumped = true;
                IsInputJumpDown = false;
            }
            else if ( !IsGrounded)
            {
                IsJumped = true;
            }
            else if (IsGrounded)
            {
                IsJumped = false;
            }
        }

        public void WallSliding()
        {
            SlideRay = new(transform.position + new Vector3(0, 0.5f, 0), MoveDirection);
            if (Physics.Raycast(SlideRay, out Hit, MaxCollisionRayDistance))
            {
                Rb.AddForce(Vector3.down * WallSlidingForce);
                if (IsInputWallSlidingDown) // JumpTimer >= JumpDelay
                {
                    Rb.AddForce((-MoveDirection * WallJump + Vector3.up * WallJumpUp) );
                    IsInputWallSlidingDown = false;
                }
                IsWallSliding = true;
            }
            else if (!Physics.Raycast(SlideRay, out Hit, MaxCollisionRayDistance))
            { IsWallSliding = false; }
        }

        public void Sprint()
        {
            if (IsInputShift)
            {
                MoveDirection *= SprintSpeed;
                IsSprint = true;
            }
            else
            {
                IsSprint = false;
            }
        }
        void OnCollisionEnter(Collision collision)
        {
            /*
            if (collision.collider.gameObject.CompareTag("Enemy"))
            {
                IsDead = true;
            }
            */
        }

    }
}

/*
 * 
 * Rigidbody Rb;

        private Animator PlayerAnimator;

        public static bool IsIdle = false;
        public static bool IsMoving = false;
        public static bool IsJumped = false;
        public static bool IsWallSliding = false;
        public static bool IsSprint = false;

        public static bool IsGrounded = false;

        [HideInInspector ]public static float AxisX;
        [HideInInspector] public static Vector3 MoveDirection;
        [SerializeField] protected float Speed = 15f;

        public static float MaxIsGroundedRay = 0.51f;
        Ray IsGroundedRay;

        public static int JumpTimer = 0;
        public static int JumpDelay = 20;

        // Movement.
        public float AirSpeed = 50f;
        //
        // Jump.
        public float JumpForce = 250f;
        //
        // Sliding.
        Ray SlideRay, SlideRay2;
        RaycastHit Hit;
        public float MaxCollisionRayDistance;
        public float WallSlidingForce = 15f;
        public float WallJump = 250f;
        public float WallJumpUp = 15f;
        //
        // Sprint.
        public float SprintSpeed = 15f;
        public float MaxIsGroundedRayDistance = 0.6f;
        //
 * */

/*
if (AxisX != 0 && !IsJumped && Input.GetButton("Fire3"))
{
    Rb.velocity = MoveDirection * SprintSpeed;
    IsSprint = true;
}
else if (IsJumped == true && Input.GetButton("Fire3"))
{
    Rb.AddForce(MoveDirection * SprintSpeed);
    IsSprint = true;
}
else
{
    IsSprint = false;
}
*/

/*
if (AxisX != 0 && IsGrounded == true) 
{
    Rb.velocity = MoveDirection * Speed;
    Debug.Log("Usual");

    IsMoving = true;
}
else if (IsJumped == true)
{
    Rb.AddForce(MoveDirection * AirSpeed);
    Debug.Log("Air");

    IsMoving = true;
}
else if (Rb.velocity.magnitude == 0 && IsGrounded)
{
    Debug.Log("None");
    IsMoving = false;
}
*/