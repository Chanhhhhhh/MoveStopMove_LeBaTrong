using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Zone : MonoBehaviour
{
    private NavMeshSurface meshSurface;
    public int AmountEnemies;
    public int MaxEnemies;
    public float MapRadius;

    public void OnInit()
    {
        meshSurface = GetComponent<NavMeshSurface>();
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(meshSurface.navMeshData);
    }
}
