using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    private Transform target;
    private Character owner;
    private Character victim;

    protected Vector3 StartPos;
    protected float Distance;
    protected float Speed = 8f;

    [SerializeField] protected Transform Child;
    
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
    public void SetUp(Character character, Transform target, float Distance)
    {
        this.owner = character;
        this.target = target;
        this.Distance = Distance;
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
            Character character = Cache.GetCharacter(other);
            this.victim = character;
            LevelManager.Instance.OnHitEnemy(this.owner, this.victim);
            OnDespawn();
        }
    }
}
