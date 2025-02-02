using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;

    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _jumpPower;
    private float _xInput;
    private bool isFacingRight = true;

    [Header("Collision Check")]
    [SerializeField]
    private Transform groundDetector;
    [SerializeField]
    private float groundDetectorRadius;
    [SerializeField]
    private LayerMask groundDetectorLayerMask;
    private bool isGroundDetected;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        AnimationController();
        CollisionChecks();
        FlipController();

        // Horizontal Movement
        _xInput = Input.GetAxisRaw("Horizontal");
        Movement();

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void AnimationController()
    {
        _animator.SetFloat("xVelocity", _rb.velocity.x);
        _animator.SetFloat("yVelocity", _rb.velocity.y);
        _animator.SetBool("isGrounded", isGroundDetected);
    }

    private void Movement()
    {
        _rb.velocity = new Vector2(_xInput * _moveSpeed, _rb.velocity.y);
    }

    private void FlipController()
    {
        if (_rb.velocity.x < 0 && isFacingRight)
        {
            Flip();
        }
        else if (_rb.velocity.x > 0 && !isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    private void CollisionChecks()
    {
        isGroundDetected = Physics2D.OverlapCircle(groundDetector.position, groundDetectorRadius, groundDetectorLayerMask);
    }

    private void Jump()
    {
        if (isGroundDetected)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundDetector.position, groundDetectorRadius);
    }
}
