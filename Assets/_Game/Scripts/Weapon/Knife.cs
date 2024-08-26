using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Knife : Bullet
{
    private void Update()
    {
        TF.Translate(TF.forward * Speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(TF.position, StartPos) >= Distance)
        {
            OnDespawn();
        }
    }
}
