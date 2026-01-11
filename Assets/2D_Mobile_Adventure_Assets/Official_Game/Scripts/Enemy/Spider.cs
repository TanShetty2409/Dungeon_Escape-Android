using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Spider : Enemy, IDamageable
{
    public GameObject acidEffectPrefab;
    public GameObject gemsPrefab;
    public int Health { get; set; }
    public float attackRange = 7.5f;
    float attackCooldown = 2f;
    float _lastAttackTime;
    public override void Init()
    {
        base.Init();
        Health = base.health;

    }
    public override void Update()
    {
        if (isDead == true)
            return;
        float dist = Vector3.Distance(transform.position, _player.transform.position);
        bool canAttack = dist <= attackRange && Time.time >= _lastAttackTime + attackCooldown;
        _anim.SetBool("Attack", canAttack);

    }
    public void Damage()
    {

        if (isDead == true)
            return;
        Health--;
        if (Health < 1)
        {
            isDead = true;
            _anim.SetTrigger("Death");
            GameObject diamond = Instantiate(gemsPrefab, transform.position, quaternion.identity);
            diamond.GetComponent<Diamond>().gems = base.gems;
        }

    }
    public override void Movement()
    {

    }
    public void Attack()
    {
        if (Time.time < _lastAttackTime + attackCooldown)
            return;
        _lastAttackTime = Time.time;
        Instantiate(acidEffectPrefab, transform.position, quaternion.identity);

    }

}
