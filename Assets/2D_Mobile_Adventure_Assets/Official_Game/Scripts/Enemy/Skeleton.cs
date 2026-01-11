using Unity.Mathematics;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
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
