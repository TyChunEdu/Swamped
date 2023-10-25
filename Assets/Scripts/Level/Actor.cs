using System;
using UnityEngine;

/// <summary>
/// A base class for "sentient" objects in the level (e.g. player, NPCs)
/// </summary>
public abstract class Actor : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rigidBody;
    public SpriteRenderer spriteRenderer;
    public Collider2D weapon;
    public Collider2D characterCollider;

    public float speed = 10f;
    public float jump = 300f;
    public float groundedDistance = 0.1f;
    public bool isHitting;

    private float _verticalVelocity;
    private bool _ableToJump = true;
    private float _jumpingCooldownTimer;
    private const float JumpingCooldown = 0.1f;
    private static float coyoteTime = 0.15f;
    private static float coyoteTimeCounter = coyoteTime;

    protected bool isAirborne = true;

    protected bool FacingRight = true;
    protected bool IsMoving;
    
    private static readonly int Moving = Animator.StringToHash("Moving");
    private static readonly int Midair = Animator.StringToHash("Midair");
    private static readonly int Hitting = Animator.StringToHash("Hitting");

    protected abstract float GetHorizontalMovement();
    protected abstract bool GetJumping();
    protected abstract bool GetHitting();
    

    // Update is called once per frame
    protected void Update()
    {
        var rayPosition = spriteRenderer.bounds.center;
        rayPosition.y -= spriteRenderer.bounds.extents.y + groundedDistance*2;
        var isGrounded = Physics2D.Raycast(rayPosition, -transform.up, 
            groundedDistance);

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        
        // Vertical movement
        if (GetJumping() && (isGrounded || coyoteTimeCounter > 0) && _ableToJump)
        {
            _ableToJump = false;
            coyoteTimeCounter = 0;
            _jumpingCooldownTimer = JumpingCooldown;
            rigidBody.AddForce(transform.up * jump);
        }

        if (!_ableToJump)
        {
            _jumpingCooldownTimer -= Time.deltaTime;
            if (_jumpingCooldownTimer <= 0)
                _ableToJump = true;
        }

        isAirborne = !isGrounded;
        
        animator.SetBool(Midair, !isGrounded);
        
        // Horizontal movement
        var horizontalControl = GetHorizontalMovement();
        var transformRight = transform.right;
        var horizontalMovement = transformRight * (horizontalControl * speed);
        rigidBody.velocity = new Vector2(horizontalMovement.x, rigidBody.velocity.y);
        IsMoving = horizontalMovement.magnitude != 0;
        animator.SetBool(Moving, IsMoving);

        // Make it face the correct direction
        if (horizontalMovement.magnitude != 0 && !isHitting)
        {
            FacingRight = Vector3.Angle(horizontalMovement, transformRight) < 90;
            var newScale = transform.localScale;
            newScale.x = Math.Abs(newScale.x) * (FacingRight ? 1 : -1);
            transform.localScale = newScale;
        }
        
        // Hitting
        animator.SetBool(Hitting, GetHitting());
        isHitting = animator.GetCurrentAnimatorStateInfo(0).IsName("Hitting");
    }
}
