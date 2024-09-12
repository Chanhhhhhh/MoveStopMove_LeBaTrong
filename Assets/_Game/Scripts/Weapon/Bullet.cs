using DG.Tweening;
using UnityEngine;

public class Bullet : GameUnit
{
    private Transform target;
    private Character owner;
    private Character victim;

    protected Vector3 StartPos;
    protected float Distance;
    protected float Speed;

    [SerializeField] protected Transform Child;
    
    public override void OnInit()
    {
        TF.forward = (target.position - TF.position + Vector3.up).normalized;
        StartPos = TF.position;
    }

    public override void OnDespawn()
    {
        owner.EndAttack();
        SimplePool.Despawn(this);
    }
    public void SetUp(Character character, Transform target, float Distance, bool IsUlti)
    {
        TF.localScale = Vector3.one;
        Speed = Constant.SPEED_BULLET;
        if (IsUlti)
        {
            TF.DOScale(Vector3.one * 2, 1f);
            Speed = Constant.SPEED_BULLET * 2;
        }        
        this.owner = character;
        this.target = target;
        this.Distance = Distance;
        OnInit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_PLAYER) && other.gameObject != owner.gameObject)
        {
            LevelManager.Instance.SetLose(this.owner);
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
