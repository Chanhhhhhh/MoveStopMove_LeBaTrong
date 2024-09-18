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
    [SerializeField] private NavMeshAgent NavMeshAgent;

    private Vector3 Destination;
    public bool IsDestination => Vector3.Distance(Destination, TF.position) <= 0.5f;

    //private void Start()
    //{
    //    OnInit();
    //}
    public override void OnInit()
    {
        CharName = Constant.GetName();
        GetTargetIndicator();
        RandomItemSkin();
        base.OnInit();
        UpLevel(LevelManager.Instance.RandomLevel());
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
        //Debug.Log(Vector3.Distance(Destination, TF.position));
        TimeCountdownAttack -= Time.deltaTime;
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
        ChangeAnim(IsUlti ? Constant.ULTI_ANIM_STRING : Constant.ATTACK_ANIM_STRING);
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

    public void RandomItemSkin()
    {    
        // Pant
        MeshBody.material = DataManager.Instance.GetColor();
        currentPant = DataManager.Instance.pantData.PantDatas[Random.Range(0, DataManager.Instance.pantData.PantDatas.Length)].PantMaterial;
        pantRen.material = currentPant;

        // Hat
        if(currentHat != null)
        {
            Destroy(currentHat.gameObject);
        }
        currentHat = Instantiate(DataManager.Instance.hatData.hatDatas[Random.Range(0, DataManager.Instance.hatData.hatDatas.Length)].HatPrefabs, hatPoint);

        //Shield
        if(currentShield != null)
        {
            Destroy(currentShield.gameObject);
        }
        int count = DataManager.Instance.shieldData.ShieldDatas.Length;
        int rand = Random.Range(0, count + 5);
        if(rand < count)
        {
            currentShield = Instantiate(DataManager.Instance.shieldData.ShieldDatas[rand].ShieldPrefabs, shieldPoint);
        }

        // Weapon
        ChangeWeapon(DataManager.Instance.RandomWeapon());
    }
}
