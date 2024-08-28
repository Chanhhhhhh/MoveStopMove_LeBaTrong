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
    private List<Character> enemyList = new List<Character>();
    public int MaxAmountEnemy = 10;
    public int EnemyAmount = 4;
    public int countEnemy;

    private void Start()
    {
        for (int i = 0;i < EnemyAmount;i++)
        {
            SpawnEnemy();
        }    
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
        }
        owner.UpLevel(victim.DeadScore);
        victim.OnDespawn();
        SpawnEnemy();
        if (enemyList.Count <= 0)
        {
            Debug.Log("Win");
        }

    }
    public void SpawnEnemy()
    {
        if(countEnemy >= MaxAmountEnemy)
        {
            return;
        }
        if(enemyList.Count < EnemyAmount)
        {
            countEnemy++;
            Vector3 SpawnPos = GetRandomPoint(Vector3.zero, 40f); 
            Enemy newEnemy = SimplePool.Spawn<Enemy>(PoolType.Enemy, SpawnPos, Quaternion.identity);
            enemyList.Add(newEnemy);
            newEnemy.OnInit();
        }
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
}
