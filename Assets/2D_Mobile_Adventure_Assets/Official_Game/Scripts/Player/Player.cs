using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    private Rigidbody2D _rigid;
    [SerializeField]
    private float _jumpForce = 2.0f;
    private bool jumpReset = false;
    private float _speed = 3.5f;
    private bool _grounded = false;
    private bool isDead = false;
    private Vector2 moveInput;
    private bool jumpPressed;
    private bool attackPressed;
    public int diamonds = 0;
    public AudioClip JumpClip;
    public AudioClip SwordClip;
    public AudioClip gemClip;
    public AudioClip deathClip;
    public AudioClip flameSwordClip;

    AudioSource _AudioSrc;
    SpriteRenderer _spriteRend;
    PlayerAnimations _playerAnim;
    SpriteRenderer _spriteRendAnim;


    public int Health { get; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var pi = GetComponent<PlayerInput>();
        pi.SwitchCurrentControlScheme("Gamepad", Gamepad.current);
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponentInChildren<PlayerAnimations>();
        _spriteRend = GetComponentInChildren<SpriteRenderer>();
        _spriteRendAnim = transform.GetChild(1).GetComponentInChildren<SpriteRenderer>();
        _AudioSrc = GetComponent<AudioSource>();
        Health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == true)
            return;
        Movement();
        Attack();
        jumpPressed = false;
        attackPressed = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            jumpPressed = true;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            attackPressed = true;
    }

    public void resumeInput()
    {
        var pi = GetComponent<PlayerInput>();

        pi.DeactivateInput();
        if (Gamepad.current != null)
        {
            pi.SwitchCurrentControlScheme("Gamepad", Gamepad.current);
        }
        pi.ActivateInput();

        moveInput = Vector2.zero;
        jumpPressed = false;
        attackPressed = false;
    }


    void Movement()
    {
        float move = moveInput.x;
        _grounded = isGrounded();
        if (move > 0)
        {
            _spriteRend.flipX = false;
            _spriteRendAnim.flipX = false;
            _spriteRendAnim.flipY = false;
            Vector3 newPos = _spriteRendAnim.transform.localPosition;
            newPos.x = 1.01f;
            _spriteRendAnim.transform.localPosition = newPos;

        }
        else if (move < 0)
        {
            _spriteRend.flipX = true;
            // _spriteRendAnim.flipX = true;
            _spriteRendAnim.flipY = true;
            Vector3 newPos = _spriteRendAnim.transform.localPosition;
            newPos.x = -1.01f;
            _spriteRendAnim.transform.localPosition = newPos;

        }

        if (jumpPressed && isGrounded())
        {
            Debug.Log("Jump");
            _rigid.linearVelocity = new Vector2(_rigid.linearVelocity.x, _jumpForce);
            _rigid.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            if (JumpClip != null)
            {
                _AudioSrc.PlayOneShot(JumpClip);
            }
            StartCoroutine(resetJump());
            _playerAnim.Jump(true);
        }

        _rigid.linearVelocity = new Vector2(move * _speed, _rigid.linearVelocity.y);

        if (_playerAnim != null)
        {
            _playerAnim.Move(move);
            Debug.Log(move);

        }
    }

    bool isGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 0.75f, 1 << 6);
        if (hitInfo.collider != null)
        {
            if (jumpReset == false)
            {
                _playerAnim.Jump(false);
                return true;
            }
        }
        return false;
    }

    void Attack()
    {
        if (!attackPressed || !isGrounded())
            return;

        bool hasFlame = GameManager.Instance.hasFlameSword;
        _playerAnim.SetFlame(hasFlame);
        _playerAnim.Attack();

        if (hasFlame)
        {
            if (flameSwordClip != null)
            {
                _AudioSrc.PlayOneShot(flameSwordClip);
            }
        }
        else
        {
            if (SwordClip != null)
            {
                _AudioSrc.PlayOneShot(SwordClip);
            }
        }
    }
    IEnumerator resetJump()
    {
        jumpReset = true;
        yield return new WaitForSeconds(0.1f);
        jumpReset = false;
    }
    public void Damage()
    {
        if (Health < 1)
        {
            return;
        }
        Debug.Log("Player : Damage");
        Health--;
        UIManager.Instance.UpdateLives(Health);
        if (Health < 1)
        {
            if (deathClip != null)
            {
                _AudioSrc.PlayOneShot(deathClip);
            }
            _playerAnim.Death();
            isDead = true;
        }
    }
    public void AddGems(int amount)
    {
        diamonds += amount;
        if (gemClip != null)
        {
            _AudioSrc.PlayOneShot(gemClip);
        }
        UIManager.Instance.UpdateGemCount(diamonds);
    }
    void Awake()
    {
        if (Gamepad.current == null)
        {
            InputSystem.AddDevice<Gamepad>();
        }
    }
}
