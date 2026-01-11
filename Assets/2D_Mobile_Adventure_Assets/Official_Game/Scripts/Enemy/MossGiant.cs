using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
    public GameObject gemsPrefab;
    public int Health { get; set; }
    public override void Init()
    {
        base.Init();
        Health = base.health;

    }
    public override void Movement()
    {
        base.Movement();
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
    public void Damage()
    {
        if (isDead == true)
            return;
        Debug.Log("Damage()");
        Health--;
        _anim.SetTrigger("Hit");
        _isHit = true;
        _anim.SetBool("InCombat", true);
        if (Health < 1)
        {
            isDead = true;
            _anim.SetTrigger("Death");
            GameObject diamond = Instantiate(gemsPrefab, transform.position, quaternion.identity);
            diamond.GetComponent<Diamond>().gems = base.gems;

        }
    }
}
