using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Importamos SceneManager para reiniciar el nivel

public class PlayerController : MonoBehaviour
{
    public float longIdleTime = 5f;
    public float speed = 2.5f;
    public float jumpForce = 2.5f;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private float _longIdleTimer;
    private Vector2 _movement;
    private bool _facingRight = true;
    private bool _isGrounded;
    private bool _isAttacking;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isAttacking == false) 
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            _movement = new Vector2(horizontalInput, 0f);

            if (horizontalInput < 0f && _facingRight == true) {
                Flip();
            } else if (horizontalInput > 0f && _facingRight == false) {
                Flip();
            }
        }

        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && _isGrounded == true && _isAttacking == false) {
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Fire1") && _isGrounded == true && _isAttacking == false) {
            _movement = Vector2.zero;
            _rigidbody.linearVelocity = Vector2.zero;
            _animator.SetTrigger("Attack");
        }
    }

    void FixedUpdate()
    {
        if (_isAttacking == false) {
            float horizontalVelocity = _movement.normalized.x * speed;
            _rigidbody.linearVelocity = new Vector2(horizontalVelocity, _rigidbody.linearVelocity.y);
        }
    }

    void LateUpdate()
    {
        _animator.SetBool("Idle", _movement == Vector2.zero);

        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
            _isAttacking = true;
        } else {
            _isAttacking = false;
        }

        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle")) {
            _longIdleTimer += Time.deltaTime;
            if (_longIdleTimer >= longIdleTime) {
                _animator.SetTrigger("LongIdle");
            }
        } else {
            _longIdleTimer = 0f;
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX *= -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameOverManager.Instance.ShowGameOver();
            gameObject.SetActive(false); // Desactiva al jugador
        }
    }


    void GameOver()
    {
        GameOverManager.Instance.ShowGameOver();
        gameObject.SetActive(false);
    }
}


