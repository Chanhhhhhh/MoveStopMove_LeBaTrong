using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : IState<Enemy>
{
    private float deadTime;
    public void EnterState(Enemy owner)
    {        
        deadTime = Constant.DELAY_TIME_DEAD;
        owner.SetDestination(owner.TF.position);
        owner.ChangeAnim(Constant.DIE_ANIM_STRING);
        owner.ColliderState(false);
    }

    public void Execute(Enemy owner)
    {
        deadTime -= Time.deltaTime;
        if(deadTime < 0f)
        {
            SimplePool.Despawn(owner);
        }
    }

    public void ExitState(Enemy owner)
    {

    }
}
