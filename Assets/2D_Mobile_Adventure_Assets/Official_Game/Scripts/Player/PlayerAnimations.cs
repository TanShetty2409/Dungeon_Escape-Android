using System;
using TMPro;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    Animator _anim;
    Animator _swordAnim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _swordAnim = transform.GetChild(1).GetComponentInChildren<Animator>();
    }

    public void Move(float move)
    {
        // Debug.Log("Animator Move parameter: " + Math.Abs(move));
        _anim.SetFloat("Move", Math.Abs(move));
    }

    public void Jump(bool jumping)
    {
        _anim.SetBool("Jumping", jumping);
    }

    public void Attack()
    {
        _anim.SetTrigger("Attack");
        _swordAnim.SetTrigger("SwordAnimation");
    }
    public void SetFlame(bool isflame)
    {
        _anim.SetBool("isFlame",isflame);
    }
    public void Death()
    {
        _anim.SetTrigger("Death");
    }
}
