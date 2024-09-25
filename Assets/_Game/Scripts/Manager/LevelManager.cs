using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public struct ScoreRate
{
   public int minLevel;
   public float Scale;
   public int deadScore;


    public ScoreRate(int minLevel, float scale, int deadScore)
    {
        this.minLevel = minLevel;
        this.Scale = scale;
        this.deadScore = deadScore;
    }
}
public class LevelManager : Singleton<LevelManager>
{
    private List<ScoreRate> ScoreRates = new List<ScoreRate>()
    {
        new ScoreRate(0,1f,1),
        new ScoreRate(2,1.1f,2),
        new ScoreRate(5,1.3f,3),
        new ScoreRate(9,1.5f,4),
        new ScoreRate(15, 1.7f,5),
        new ScoreRate(18, 2f,6),
        new ScoreRate(22, 2.2f,7),
    };
    [SerializeField] private GameObject[] giftBoxes;
    [SerializeField] private Zone[] zones;
    private Zone currentZone;
    private GameObject giftbox;
    private float RadiusMap = 50f;
    private List<Character> enemyList = new List<Character>();
    private int CoinBonus;
    private Character Killer;
    private bool IsEnd;
    private bool isRevived;
    private int AmountEnemy;
    private int MaxEnemy;
    private int countEnemy;

    public int PriceRevive = 200;
    public int BestRank;
    public Transform TargetIndicatorContent;
    public Player player;


    
    public void CreateZone(int zoneIndex)
    {
        if(currentZone != null)
        {
            Destroy(currentZone.gameObject);
        }
        currentZone = Instantiate(zones[zoneIndex], Vector3.zero, Quaternion.identity).GetComponent<Zone>();
        currentZone.OnInit();
        AmountEnemy = currentZone.AmountEnemies;
        MaxEnemy = currentZone.MaxEnemies;
        RadiusMap = currentZone.MapRadius;
    }
    public void OnInit()
    {
        IsEnd = false;
        isRevived = false;
        countEnemy = 0;
        CoinBonus = 0;
        Killer = null;

        //// enemy
        for (int i = 0; i < MaxEnemy; i++)
        {
            SpawnEnemy();
        }

        // player
        player.OnInit();
        player.GetTargetIndicator();
        player.UpLevel(0);
        player.TurnOnCircle();

        SetAliveUI();
        SpawnGiftBox();
    }

    public Vector3 GetRandomPoint(Vector3 center, float radius)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * radius;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomPoint, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return center;
    }


    public void OnHitEnemy(Character owner, Character victim)
    {
        if (owner.CompareTag(Constant.TAG_PLAYER))
        {
            CoinBonus++;
            Vibration.Vibrate(200);
        }
        SoundManager.Instance.PlaySoundClip(SoundType.WeaponHit, victim.TF.position);
        if (enemyList.Contains(victim))
        {
            enemyList.Remove(victim);
            SetAliveUI();
        }
        owner.UpLevel(victim.DeadScore);
        victim.OnDespawn();
        SpawnEnemy();
        if (enemyList.Count <= 0)
        {
            if (IsEnd)
            {
                return;
            }
            IsEnd = true;
            SetWin();
        }

    }
    public void SpawnEnemy()
    {
        if(countEnemy >= AmountEnemy)
        {
            return;
        }

        if (enemyList.Count < MaxEnemy)
        {
            int CountLoop = 0;
            Vector3 SpawnPos;
            Vector3 FarthestPos = player.TF.position;
            float MaxDistance = Vector3.Distance(player.TF.position, FarthestPos);
            do
            {
                CountLoop++;
                SpawnPos = GetRandomPoint(Vector3.zero, RadiusMap);
                float Distance = Vector3.Distance(player.TF.position, SpawnPos);
                if (Distance > MaxDistance)
                {
                    FarthestPos = SpawnPos;
                    MaxDistance = Distance;
                }
            } while (MaxDistance < 10f && CountLoop < 300);
            countEnemy++;
            Enemy newEnemy = SimplePool.Spawn<Enemy>(PoolType.Enemy, FarthestPos, Quaternion.identity);
            enemyList.Add(newEnemy);
            Physics.SyncTransforms();
            newEnemy.OnInit();
        }
        SetAliveUI();
    }

    public ScoreRate GetScoreRate(int level)
    {
        ScoreRate result = ScoreRates[0];
        for(int i = 0;i< ScoreRates.Count;i++)
        {
            if (level >= ScoreRates[i].minLevel)
            {
                result = ScoreRates[i];
            }
            else
            {
                break;
            }
        }
        return result;
    }

    internal void ClearLevel()
    {
        SaveManager.Instance.Coin += CoinBonus;
        if(giftbox != null)
        {
            giftbox.SetActive(false);
        }
        countEnemy = 0;
        enemyList.Clear();
        SimplePool.CollectAll();
        player.OnInit();
    }

    public void SetWin()
    {
        SoundManager.Instance.PlaySoundClip(SoundType.Win);
        GameManager.ChangeState(GameState.Win);
        if(zones.Length - 1 > SaveManager.Instance.Zone)
        {
            SaveManager.Instance.Zone++;
        }
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<WinUI>();
        UIManager.Instance.GetUI<WinUI>().SetTextCoinBonus(CoinBonus);
        SimplePool.CollectAll();
        player.OnInit();
        player.ChangeAnim(Constant.WIN_ANIM_STRING);
    }


    public void OnHitPlayer(Character Killer)
    {
        IsEnd = true;
        this.Killer = Killer;
        player.OnDespawn();
        if (isRevived)
        {
            SetLose();
            return;
        }
        GameManager.ChangeState(GameState.PopUpRevive);
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<ReviveUI>();
    }
    public void SetLose()
    {        
        SoundManager.Instance.PlaySoundClip(SoundType.Lose);
        GameManager.ChangeState(GameState.Lose);
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<LoseUI>();
        int rank = GetAlive();
        if(rank < BestRank)
        {
            BestRank = rank;
        }
        UIManager.Instance.GetUI<LoseUI>().SetResult(BestRank, Killer.CharName);
        UIManager.Instance.GetUI<LoseUI>().SetTextCoinBonus(CoinBonus);
        
    }
    internal int RandomLevel()
    {
        if(player.Level < 5)
        { 
            return Random.Range(0, 3);
        }
        return Random.Range(player.Level - 5, player.Level + 2);
    }

    public void SetAliveUI()
    {
        UIManager.Instance.GetUI<GamePlay>().SetAlive(GetAlive());
    }

    public int GetAlive()
    {
        return AmountEnemy - countEnemy + enemyList.Count + 1;
    }

    public void SpawnGiftBox()
    {
        giftbox = giftBoxes[Random.Range(0, giftBoxes.Length)];
        giftbox.transform.position = GetRandomPoint(Vector3.zero, RadiusMap) + Vector3.up * 10;
        giftbox.gameObject.SetActive(true);
        
    }
    public IEnumerator GiftBoxCoroutine()
    {
        yield return new WaitForSeconds(5f);
        SpawnGiftBox();
    }

    public void SetNamePlayer(string name)
    {
        SaveManager.Instance.NamePlayer = name;
        player.CharName = name;
    }

    internal void Revive()
    {
        IsEnd = false;
        isRevived = true;
        SaveManager.Instance.Coin -= PriceRevive;
        GameManager.Instance.ChangeStateGamePlay();
        player.OnRevive(GetRandomPoint(Vector3.zero, RadiusMap));
    }
}
