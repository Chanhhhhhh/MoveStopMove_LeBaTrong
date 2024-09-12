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
    public GameObject giftbox;
    private float RadiusMap = 40f;
    private List<Character> enemyList = new List<Character>();


    public Player player;
    public int AmountEnemy = 10;
    public int MaxEnemy = 4;
    public int countEnemy;

    private bool IsEnd;
    public void OnInit()
    {
        IsEnd = false;
        countEnemy = 0;
        player.OnInit();
        player.TurnOnCircle();
        for (int i = 0;i < MaxEnemy;i++)
        {
            SpawnEnemy();
        }
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
        if(enemyList.Count < MaxEnemy)
        {
            countEnemy++;
            Vector3 SpawnPos = GetRandomPoint(Vector3.zero , RadiusMap); 
            Enemy newEnemy = SimplePool.Spawn<Enemy>(PoolType.Enemy, SpawnPos, Quaternion.identity);
            enemyList.Add(newEnemy);
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
        GameManager.ChangeState(GameState.Win);
        SimplePool.CollectAll();
        player.OnInit();
        player.ChangeAnim(Constant.WIN_ANIM_STRING);
    }

    public void SetLose(Character killer)
    {
        if(IsEnd) { return; }
        IsEnd = true;
        GameManager.ChangeState(GameState.Lose);
        UIManager.Instance.GetUI<LoseUI>().SetResult(GetAlive(), killer.CharName);
        player.OnDespawn();
    }
    internal int RandomLevel()
    {
        if(player.Level <= 2)
        { 
            return Random.Range(0, 3);
        }
        return Random.Range(player.Level - 2, player.Level + 2);
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
}
