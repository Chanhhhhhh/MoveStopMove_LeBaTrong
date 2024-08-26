using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
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
        victim.OnDespawn();
        if(enemyList.Count <= 0)
        {
            Debug.Log("Win");
        }
        else
        {
            Invoke(nameof(SpawnEnemy), 2f);
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
}
