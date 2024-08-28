using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{

    #region  EnemyState Variable
    public IState<Enemy> EnemyIdleState = new EnemyIdleState();
    public IState<Enemy> EnemyAttackState = new EnemyAttackState();
    public IState<Enemy> EnemyMoveState = new EnemyMoveState();
    public IState<Enemy> EnemyDeadState = new EnemyDeadState();
    #endregion

    public StateMachine<Enemy> EnemyStateMachine;

    [SerializeField] private GameObject focus;
    public bool IsDestination => Vector3.Distance(Destination, TF.position) <= 0.03f;
    private Vector3 Destination;
    [SerializeField] private NavMeshAgent NavMeshAgent;

    public override void OnInit()

    {
        CharName = Constant.GetName();
        GetTargetIndicator();
        base.OnInit();    
        EnemyStateMachine.ChangeState(EnemyMoveState);


    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        TargetIndicator.OnDespawn();
        EnemyStateMachine.ChangeState(EnemyDeadState);

    }

    private void Update()
    {
        EnemyStateMachine.ExecuteState();
        
    }

    public void SetDestination(Vector3 Pos)
    {
        Destination = Pos;
        Destination.y = 0;
        NavMeshAgent.SetDestination(Destination);
    }

    public override void Attack()
    {

    }

    public override void GetTargetIndicator()
    {
        TargetIndicator = SimplePool.Spawn<TargetIndicator>(PoolType.Indicator);
        TargetIndicator.Target = IndicatorOffset;
        TargetIndicator.textName.text = CharName;
        TargetIndicator.OnInit();
    }

    public void SetActiveFocus(bool IsFocus)
    {

    }
}
