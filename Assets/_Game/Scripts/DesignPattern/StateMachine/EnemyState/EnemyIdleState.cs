using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState<Enemy>
{
    private float IdleTime;
    public void EnterState(Enemy owner)
    {
        //Debug.Log("idle");
        owner.ChangeAnim(Constant.IDLE_ANIM_STRING);
        owner.SetDestination(owner.TF.position);
        IdleTime = Random.Range(2f, 3f);
    }

    public void Execute(Enemy owner)
    {
        IdleTime -= Time.deltaTime;
        if (owner.CheckTarget())
        {
            if(owner.Target != null)
            {
               owner.EnemyStateMachine.ChangeState(owner.EnemyAttackState);
            }
        }
        if (IdleTime < 0)
        {
            owner.EnemyStateMachine.ChangeState(owner.EnemyMoveState);
        }
    }

    public void ExitState(Enemy owner)
    {

    }
}
