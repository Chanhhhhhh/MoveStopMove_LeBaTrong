using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Character : GameUnit
{
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask CharacterLayer;

    [SerializeField] protected float Speed = 10f;
    protected bool IsWeapon;

    private string currentAnim;
    private CounterTime counterTime = new CounterTime();
    public CounterTime CounterTime => counterTime;

     
    public Transform weaponPoint;
    public Weapon weapon;
    public PoolType weaponType;
    public Character Target;
    public Renderer Mesh;

    private void Start()
    {
        OnInit();
    }
    public override void OnInit()
    {
        IsWeapon = true;
        Mesh.material = DataManager.Instance.GetColor();
        ChangeWeapon();

    }

    public abstract void Attack();
    public void ChangeWeapon()
    {
        foreach(Transform child in weaponPoint)
        {
            Destroy(child.gameObject);
        }
        this.weaponType = (PoolType)UnityEngine.Random.Range(1, 6);
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

    public void WeaponState(bool isWeapon)
    {
        this.IsWeapon = isWeapon;
    }
    public void Throw()
    {
        Debug.Log("Throw");
        weapon.OffChild();
        Bullet newBullet = SimplePool.Spawn<Bullet>(weaponType, TF.position + Vector3.up + TF.forward, TF.rotation);
        if(newBullet != null )
        {
            newBullet.SetUp(this, this.Target.TF);
        }
    }

    public bool CheckTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(TF.position, 15f, CharacterLayer);
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
        TF.rotation = Quaternion.Slerp(TF.rotation, targetRotation, Time.deltaTime * 50f);
    }
}
