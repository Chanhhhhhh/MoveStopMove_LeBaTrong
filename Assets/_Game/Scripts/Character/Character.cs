using System;
using System.Linq;
using UnityEngine;

public abstract class Character : GameUnit
{
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask CharacterLayer;
    [SerializeField] private Collider col;
    [SerializeField] protected Transform IndicatorOffset;
    [SerializeField] protected Transform weaponPoint;

    [SerializeField] protected Renderer MeshBody;

    
    protected float Speed;
    protected bool IsWeapon;
    protected bool IsUlti;
    protected TargetIndicator TargetIndicator;
    protected float currentScale;
    protected Weapon weapon;
    protected PoolType weaponType;
    protected Character Target;
    protected float rangeAttack;
    protected float UltiRangedAttack;
    protected int level;
    public int Level => level;
    private string currentAnim;


    public string CharName;
    public int DeadScore;
    public GameObject currentHat;
    public Material currentPant;
    public GameObject currentShield;
    public Transform hatPoint;
    public Renderer pantRen;
    public Transform shieldPoint;



    public override void OnInit()
    {
        IsUlti = false;
        IsWeapon = true;
        col.enabled = true;
        Speed = Constant.SPEED_DEFAULT;
        rangeAttack = Constant.RANGE_ATTACK_DEFAULT;
        level = 0;
        Target = null;
    }

    public abstract void Attack();
    public abstract void GetTargetIndicator();
    public void ChangeWeapon(PoolType weaponType)
    {
        foreach(Transform child in weaponPoint)
        {
            Destroy(child.gameObject);
        }
        this.weaponType = weaponType;
        this.weapon = DataManager.Instance.GetWeapon(this.weaponType);
        this.weapon.transform.SetParent(weaponPoint);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        weapon.transform.localScale = Vector3.one;

    }

    public override void OnDespawn()
    {
        
    }


    public void ChangeAnim(string animName)
    {
        if(animName != currentAnim)
        {
            animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }

    public virtual void EndAttack()
    {
        this.IsWeapon = true;
        weapon.OnChild();
        if (IsUlti)
        {
            IsUlti = false;
        }       
    }

    public void ColliderState(bool state)
    {
        this.col.enabled = state;
    }
    public void Throw()
    {
        weapon.OffChild();
        Bullet newBullet = SimplePool.Spawn<Bullet>(weaponType, TF.position + Vector3.up + TF.forward, TF.rotation);
        if(newBullet != null )
        {
            newBullet.SetUp(this, this.Target.TF, ChooseRangeAttack(), IsUlti);
        }
    }



    public bool CheckTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(TF.position, ChooseRangeAttack(), CharacterLayer);
        colliders = colliders.OrderBy(x => Vector3.Distance(x.bounds.center, TF.position)).ToArray();
        if(colliders.Length > 1 )
        {
            this.Target = Cache.GetCharacter(colliders[1]);
        }
        return colliders.Length > 1;
    }
    public void RotateTarget()
    {
        Vector3 targetPos = Target.TF.position;
        Vector3 targetAngle = targetPos - TF.position;
        float targetAngleY = Mathf.Atan2(targetAngle.x, targetAngle.z) * Mathf.Rad2Deg;
        TF.rotation = Quaternion.Euler(0f, targetAngleY, 0f);
        Quaternion targetRotation = Quaternion.Euler(0f, targetAngleY, 0f);
        TF.rotation = Quaternion.Slerp(TF.rotation, targetRotation, Time.deltaTime * 100f);
    }

    public virtual void UpLevel(int score)
    {
        level += score;
        TargetIndicator.SetLevel(level);
        ScoreRate scoreRate = LevelManager.Instance.GetScoreRate(level);
        currentScale = scoreRate.Scale;
        DeadScore = scoreRate.deadScore;
        rangeAttack = Constant.RANGE_ATTACK_DEFAULT*currentScale;
        this.TF.localScale = Vector3.one * currentScale;
    }

    internal virtual void BuffUlti()
    {
        if (IsUlti)
        {
            return;
        }
        IsUlti = true;
        UltiRangedAttack = rangeAttack * 1.5f;

    }

    public float ChooseRangeAttack()
    {
        return IsUlti ? UltiRangedAttack : rangeAttack;
    }
}
