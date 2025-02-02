using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpPower;
    private float _xInput;
    private bool isFacingRight = true;

    [Header("Collision Check")]
    [SerializeField] private Transform groundDetector;
    [SerializeField] private float groundDetectorRadius;
    [SerializeField] private LayerMask groundDetectorLayerMask;
    private bool isGroundDetected;

    [SerializeField] private ParticleSystem _moveDustVFX;
    [SerializeField] private ParticleSystem _poofDustVFX;

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
        DetectMoveDust();

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
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x < transform.position.x && isFacingRight)
        {
            Flip();
        }
        else if (mousePos.x > transform.position.x && !isFacingRight)
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
            PlayPoofDustVFX();
        }
    }

    private void DetectMoveDust()
    {
        if (isGroundDetected)
        {
            if (!_moveDustVFX.isPlaying)
            {
                _moveDustVFX.Play();
            }
        }
        else
        {
            if (_moveDustVFX.isPlaying)
            {
                _moveDustVFX.Stop();
            }
        }
    }
    private void PlayPoofDustVFX()
    {
        _poofDustVFX.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundDetector.position, groundDetectorRadius);
    }
}
