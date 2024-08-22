using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    private Transform target;
    private Character owner;
    private Character victim;
    protected Vector3 StartPos;

    [SerializeField] protected Transform Child;
    [SerializeField] protected float Speed = 5f;
    public override void OnInit()
    {
        TF.forward = (target.position - TF.position + Vector3.up).normalized;
        StartPos = TF.position;
    }

    public override void OnDespawn()
    {
        owner.WeaponState(true);
        SimplePool.Despawn(this);
    }

    


    public void SetUp(Character character, Transform target)
    {
        this.owner = character;
        this.target = target;
        OnInit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_PLAYER) && other.gameObject != owner.gameObject)
        {
            OnDespawn();
        }

        if(other.CompareTag(Constant.TAG_ENEMY) && other.gameObject != owner.gameObject)
        {
            OnDespawn();
        }
    }
}
