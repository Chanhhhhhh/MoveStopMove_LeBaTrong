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
    [SerializeField] protected GameObject FocusImg;

    protected float Speed;
    protected bool IsWeapon;
    protected bool IsUlti;
    protected TargetIndicator TargetIndicator;
    protected float currentScale = 1;
    protected Weapon weapon;
    protected PoolType weaponType;
    protected float rangeAttack;
    protected float UltiRangedAttack;
    protected int level;
    public int Level => level;
    protected float TimeCountdownAttack;
    private string currentAnim;
    public Character Target;

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
        TimeCountdownAttack = 0f;
    }

    public abstract void Attack();
    public void GetTargetIndicator()
    {
        TargetIndicator = SimplePool.Spawn<TargetIndicator>(PoolType.Indicator);
        TargetIndicator.Target = IndicatorOffset;
        TargetIndicator.textName.text = CharName;
        TargetIndicator.OnInit();
    }
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

    public void ChangeWeapon(int weaponIndex)
    {
        foreach (Transform child in weaponPoint)
        {
            Destroy(child.gameObject);
        }
        this.weaponType = DataManager.Instance.weapons[weaponIndex].weaponType;
        this.weapon = Instantiate(DataManager.Instance.weapons[weaponIndex]);
        this.weapon.transform.SetParent(weaponPoint);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        weapon.transform.localScale = Vector3.one;
    }

    public override void OnDespawn()
    {
        SoundManager.Instance.PlaySoundClip(SoundType.Dead, this.TF.position);
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
    public void SetActiveFocus(bool IsFocus)
    {
        FocusImg.SetActive(IsFocus);
    }
    public void Throw()
    {
        TimeCountdownAttack = Constant.TIME_COOLDOWN;
        weapon.OffChild();
        Bullet newBullet = SimplePool.Spawn<Bullet>(weaponType, TF.position + Vector3.up + TF.forward, TF.rotation);
        if(newBullet != null )
        {
            SoundManager.Instance.PlaySoundClip(SoundType.WeaponThrow, this.TF.position);
            newBullet.SetUp(this, this.Target.TF, ChooseRangeAttack(),currentScale, IsUlti);
        }
    }



    public bool CheckTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(TF.position, ChooseRangeAttack(), CharacterLayer);
        colliders = colliders.OrderBy(x => Vector3.Distance(x.transform.position, TF.position)).ToArray();
        if(colliders.Length > 1 )
        {
            Character newTarget = Cache.GetCharacter(colliders[1]);
            if (this is Player)
            {              
                if(newTarget != null && newTarget !=  this.Target) 
                {
                    Target?.SetActiveFocus(false);
                    this.Target = newTarget;
                    this.Target.SetActiveFocus(true);
                }
            }
            else
            {
                this.Target = newTarget;
            }          
        }
        else
        {
            if(this is Player)
            {
                Target?.SetActiveFocus(false);
            }
            this.Target = null;         
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
        if (this is Player && scoreRate.Scale > currentScale)
        {
            SoundManager.Instance.PlaySoundClip(SoundType.SizeUp);
        }
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
