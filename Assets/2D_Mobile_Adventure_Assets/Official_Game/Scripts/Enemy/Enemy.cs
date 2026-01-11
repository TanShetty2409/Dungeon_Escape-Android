using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform Point_A, Point_B;
    protected Vector3 _currentTarget;
    protected Animator _anim;
    protected SpriteRenderer _sprite;
    private bool _isIdle = false;
    protected bool _isHit = false;
    protected bool isDead = false;
    protected Player _player;
    [SerializeField]
    private float idleDuration;


    public virtual void Init()
    {
        _anim = GetComponentInChildren<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _currentTarget = Point_B.position;
        faceTarget();
    }
    void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && _anim.GetBool("InCombat") == false)
            return;
        if (!isDead)
            Movement();
    }
    public virtual void Movement()
    {
        if (Vector3.Distance(transform.position, _currentTarget) < 0.05f && !_isIdle)
        {
            StartCoroutine(IdleAndFlip());
        }
        if (_isHit == false)
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget, speed * Time.deltaTime);

        if (Vector3.Distance(transform.localPosition, _player.transform.localPosition) > 2.0f)
        {
            if (_anim.GetBool("InCombat") == true)
            {
                _anim.SetBool("InCombat", false);
                if (!_isIdle)
                    faceTarget();
            }
            _isHit = false;
        }

        Vector3 direction = _player.transform.localPosition - transform.localPosition;
        if (direction.x > 0 && _anim.GetBool("InCombat") == true)
        {
            _sprite.flipX = false;
        }
        else if (direction.x < 0 && _anim.GetBool("InCombat") == true)
        {
            _sprite.flipX = true;
        }

    }
    IEnumerator IdleAndFlip()
    {
        _isIdle = true;
        _anim.SetTrigger("Idle");

        yield return new WaitForSeconds(idleDuration);
        if (_currentTarget == Point_A.position)
        {
            _currentTarget = Point_B.position;
            _sprite.flipX = false;
        }
        else
        {
            _currentTarget = Point_A.position;
            _sprite.flipX = true;
        }
        faceTarget();

        _isIdle = false;
    }
    protected void faceTarget()
    {
        float dir = _currentTarget.x - transform.position.x;
        if (dir > 0)
            _sprite.flipX = false;
        else if (dir < 0)
            _sprite.flipX = true;
    }
}
