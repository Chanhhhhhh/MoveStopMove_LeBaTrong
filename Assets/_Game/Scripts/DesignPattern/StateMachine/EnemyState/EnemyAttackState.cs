using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : IState<Enemy>
{
    private float attackTime;
    private bool IsAttacked;
   public void EnterState(Enemy owner)
    {
        //Debug.Log("Attack");
        owner.RotateTarget();
        attackTime = Constant.DELAY_TIME_ATTACK;
        IsAttacked = false;
        owner.ChangeAnim(Constant.ATTACK_ANIM_STRING);

    }

    public void Execute(Enemy owner)
    {
        attackTime -= Time.deltaTime;
        if(attackTime < 0 && !IsAttacked)
        {
            IsAttacked = true;         
            owner.Throw();
        }
        if(attackTime < -1f)
        {
            owner.EnemyStateMachine.ChangeState(owner.EnemyMoveState);
        }
    }

    public void ExitState(Enemy owner)
    {

    }


}
