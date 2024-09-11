using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private CircleAttack circleAttack;

    public override void OnInit()
    {
        GetTargetIndicator();
        base.OnInit();
        UpLevel(0);
        circleAttack.ClearCircle();
        this.TF.position = Vector3.zero;
        ChangeAnim(Constant.IDLE_ANIM_STRING);
        
    }
    private void Update()
    {
        if (!GameManager.IsState(GameState.GamePlay))
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            CounterTime.OnCancel();
            IsWeapon = true;
        }
        
        if (Input.GetMouseButton(0) && JoystickControl.direct != Vector3.zero)
        {
            CounterTime.OnCancel();
            ChangeAnim(Constant.RUN_ANIM_STRING);
            rb.MovePosition(rb.position + JoystickControl.direct * Speed *Time.deltaTime);
            TF.position = rb.position;
            TF.forward = JoystickControl.direct;
        }
        else
        {           
            CounterTime.OnExcute();
        }
        if(Input.GetMouseButtonUp(0))
        {
            ChangeAnim(Constant.IDLE_ANIM_STRING);
        }

        if (!Input.GetMouseButton(0))
        {
            if (!CheckTarget())
            {
                return;
            }
            if (IsWeapon && Target != null )
            {
                ChangeAnim(Constant.IDLE_ANIM_STRING);
                RotateTarget();
                Attack();
                ChangeAnim(Constant.ATTACK_ANIM_STRING);
            }


        }
    }

    public override void Attack()
    {
        IsWeapon = false;
        CounterTime.OnStart(Throw, Constant.DELAY_TIME_ATTACK);
    }

    public override void GetTargetIndicator()
    {
        TargetIndicator = SimplePool.Spawn<TargetIndicator>(PoolType.Indicator);
        TargetIndicator.Target = IndicatorOffset;
        TargetIndicator.textName.text = "You";
        TargetIndicator.OnInit();
    }

    public override void UpLevel(int score)
    {
        base.UpLevel(score);
        Cache.MainCamera.DOOrthoSize(9.5f * currentScale, 1f);
    }

    public void TurnOnCircle()
    {
        circleAttack.DrawCircle(rangeAttack);
    }





}
