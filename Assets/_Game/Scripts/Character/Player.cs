using UnityEngine;

public class Player : Character
{
    private CounterTime counterTime = new CounterTime();
    public CounterTime CounterTime => counterTime;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private CircleAttack circleAttack;

    public override void OnInit()
    {     

        base.OnInit();
        circleAttack.ClearCircle();
        this.TF.position = Vector3.zero;
        ChangeAnim(Constant.IDLE_ANIM_STRING);
        counterTime.OnCancel();
        
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        ColliderState(false);
        ChangeAnim(Constant.DIE_ANIM_STRING);
    }
    private void Update()
    {
        TimeCountdownAttack -= Time.deltaTime;
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
            if (IsWeapon && Target != null && TimeCountdownAttack <= 0)
            {
                if(Vector3.Distance(Target.TF.position, TF.position) > rangeAttack)
                {
                    Target = null;
                    return;
                }
                ChangeAnim(Constant.IDLE_ANIM_STRING);
                RotateTarget();
                Attack();                
            }


        }
    }

    public override void Attack()
    {
        ChangeAnim(IsUlti ? Constant.ULTI_ANIM_STRING : Constant.ATTACK_ANIM_STRING);
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

    public void GetSaveItem()
    {
        OnInit();
        ChangeWeapon(SaveManager.Instance.WeaponStates.currentItem);
        int pant = SaveManager.Instance.PantStates.currentItem;
        if(pant >= 0)
        {
            currentPant = DataManager.Instance.pantData.PantDatas[pant].PantMaterial;
            pantRen.material = currentPant;
        }
        int hat = SaveManager.Instance.HatStates.currentItem;
        if(hat >= 0)
        {
            currentHat = Instantiate(DataManager.Instance.hatData.hatDatas[hat].HatPrefabs, hatPoint);
        }
        int shield = SaveManager.Instance.ShieldStates.currentItem;
        if(shield >= 0)
        {
            currentShield = Instantiate(DataManager.Instance.shieldData.ShieldDatas[shield].ShieldPrefabs, shieldPoint);

        }
    }
    public override void UpLevel(int score)
    {
        base.UpLevel(score);
        Camerafollow.Instance.ScaleCamera(currentScale);
    }

    public void TurnOnCircle()
    {
        if (IsUlti)
        {
            circleAttack.DrawCircle(UltiRangedAttack);
            return;
        }
        circleAttack.DrawCircle(rangeAttack);
    }

    internal override void BuffUlti()
    {
        base.BuffUlti();
        TurnOnCircle();
    }

    public override void EndAttack()
    {
        base.EndAttack();
        TurnOnCircle();
    }



}
