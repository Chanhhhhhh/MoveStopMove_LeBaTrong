using DG.Tweening;
using UnityEngine;

public class Bullet : GameUnit
{
    private Vector3 target;
    private Character owner;
    private Character victim;
    private bool IsUlti;

    protected Vector3 StartPos;
    protected float Distance;
    protected float Speed;


    [SerializeField] protected Transform Child;
    
    public override void OnInit()
    {
        owner = null;
        victim = null;
    }

    public override void OnDespawn()
    {
        if(owner != null)
        {
            owner.EndAttack();

        }
        SimplePool.Despawn(this);
    }
    public void SetUp(Character character, Vector3 target, float Distance,float scale, bool IsUlti)
    {
        OnInit();
        TF.localScale = Vector3.one*scale;
        Speed = Constant.SPEED_BULLET;
        this.IsUlti = IsUlti;
        if (IsUlti)
        {
            TF.DOScale(Vector3.one * 4f, 1f);
            Speed = Constant.SPEED_BULLET * 2.5f;
        }        
        this.owner = character;
        this.target = target;
        this.Distance = Distance;
        TF.forward = (target - TF.position + Vector3.up).normalized;
        StartPos = TF.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_PLAYER) && other.gameObject != owner.gameObject)
        {
            Vibration.Vibrate(200);
            LevelManager.Instance.OnHitPlayer(this.owner);
            if (!IsUlti)
            {
                OnDespawn();
            }
                     
        }

        if(other.CompareTag(Constant.TAG_ENEMY) && other.gameObject != owner.gameObject)
        {           
            Character character = Cache.GetCharacter(other);
            this.victim = character;
            LevelManager.Instance.OnHitEnemy(this.owner, this.victim);
            if (!IsUlti)
            {
                OnDespawn();
            }
        }
    }
}
