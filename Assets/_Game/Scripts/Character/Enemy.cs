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
    public bool IsDestination => Vector3.Distance(Destination, TF.position) <= 0.03f;
    private Vector3 Destination;
    [SerializeField] private NavMeshAgent NavMeshAgent;

    public override void OnInit()
    {
        base.OnInit();
        EnemyStateMachine.ChangeState(EnemyMoveState);


    }
    public override void OnDespawn()
    {
        base.OnDespawn();
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
}
