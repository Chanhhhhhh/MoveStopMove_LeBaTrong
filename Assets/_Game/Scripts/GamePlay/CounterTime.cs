using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CounterTime 
{
    UnityAction Action;
    float time;

    public void OnStart(UnityAction Action, float time)
    {
        this.Action = Action;
        this.time = time;
    }

    public void OnExcute()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
            if(time < 0)
            {
                OnExit();
            }
        }
    }

    public void OnExit()
    {
        Action?.Invoke();
    }

    public void OnCancel()
    {
        Action = null;
        time = -1;
    }
}
