using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Bullet
{
    private void Update()
    {
        TF.Translate(TF.forward*Speed*Time.deltaTime, Space.World);
        Child.Rotate(Vector3.up * -6, Space.Self);
        if(Vector3.Distance(TF.position, StartPos) >= Distance)
        {
            OnDespawn();
        }
    }
}
