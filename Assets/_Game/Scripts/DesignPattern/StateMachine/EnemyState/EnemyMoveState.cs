using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : IState<Enemy>
{
    public void EnterState(Enemy owner)
    {
        owner.ChangeAnim(Constant.RUN_ANIM_STRING);
        owner.SetDestination(LevelManager.Instance.GetRandomPoint(owner.TF.position, 10f*(SaveManager.Instance.Zone+1)));
    }

    public void Execute(Enemy owner)
    {
        if (owner.IsDestination)
        {
            owner.EnemyStateMachine.ChangeState(owner.EnemyIdleState);
        }
    }

    public void ExitState(Enemy owner)
    {

    }
}
