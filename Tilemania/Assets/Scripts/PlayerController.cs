using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _climbSpeed;

    [SerializeField] private bool isGrounded = false;
    private bool isAlive = true;

    [SerializeField] private Vector2 deathKick = new Vector2();
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _ladderMask;
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private LayerMask _hazardsMask;
    [SerializeField] private Rigidbody2D _body;
    [SerializeField] private Animator _animator;
    [SerializeField] private CapsuleCollider2D _capsuleCollider;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private WinLoseGame winLoseGame;

    void Start()
    {

    }

    void Update()
    {
        if (!isAlive) { return; }

        Movement();
        FlipPlayer();
        Jump();
        ClimbLadder();
        Die();
    }

    private void Movement()
    {
        float inputX = Input.GetAxis("Horizontal");

        Vector2 playerVelocity = new Vector2(inputX * _speed, _body.velocity.y);

        _body.velocity = playerVelocity;

        bool Xspeed = Mathf.Abs(_body.velocity.x) > Mathf.Epsilon;
        _animator.SetBool("isWalking", Xspeed);
        
    }

    private void Jump() 
    {
       

        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
            Vector2 jumpVelocity = new Vector2(0f, _jumpForce);
            _body.velocity += jumpVelocity;
            isGrounded = false;
            _animator.SetTrigger("isJumping");
        }
        else if (_boxCollider.IsTouchingLayers(_groundMask) && !isGrounded)
        {
            isGrounded = true;
        }
    }

    private void FlipPlayer() 
    {
        bool Xspeed = Mathf.Abs(_body.velocity.x) > Mathf.Epsilon;

        if (Xspeed) 
        {
            transform.localScale = new Vector2(Mathf.Sign(_body.velocity.x), 1f);
        }
    }

    private void ClimbLadder() 
    {
        if (!_capsuleCollider.IsTouchingLayers(_ladderMask))
        {
            _animator.SetBool("isClimbing", false);
            _body.gravityScale = 1;
            return;
        }

        Debug.LogError("!!!!!!!!!!!!!!!!!");
        float inputY = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(_body.velocity.x, inputY * _climbSpeed);
        _body.velocity = climbVelocity;
        _body.gravityScale = 0;

        bool Yspeed = Mathf.Abs(_body.velocity.y) > Mathf.Epsilon;
        _animator.SetBool("isClimbing", Yspeed);
    }

    private void Die() 
    {
        if (_capsuleCollider.IsTouchingLayers(_enemyMask) || _boxCollider.IsTouchingLayers(_hazardsMask) || _capsuleCollider
            .IsTouchingLayers(_hazardsMask))
        {
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isClimbing", false);
            _animator.SetTrigger("isDead");
            _body.velocity = deathKick;
            isAlive = false;
            winLoseGame.LoseGame();
        }
    }


}
