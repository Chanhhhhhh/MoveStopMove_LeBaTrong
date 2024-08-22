using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Knife : Bullet
{
    private void Update()
    {
        TF.Translate(TF.forward * Speed * Time.deltaTime, Space.World);
        Child.Rotate(Vector3.right * -10, Space.Self);
        if (Vector3.Distance(TF.position, StartPos) >= 5f)
        {
            OnDespawn();
        }
    }
}
