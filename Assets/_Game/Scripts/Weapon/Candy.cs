using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Candy : Bullet
{
    private void Update()
    {
        TF.Translate(TF.forward * Speed * Time.deltaTime, Space.World);
        Child.Rotate(Vector3.forward*-10, Space.Self);
        if (Vector3.Distance(TF.position, StartPos) >= Distance)
        {
            OnDespawn();
        }
    }
}
