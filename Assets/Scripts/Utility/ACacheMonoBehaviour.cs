using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACacheMonoBehaviour : MonoBehaviour
{
    private Transform tf;

    public Transform TF
    {
        get
        {
            tf ??= transform;
            return tf;
        }
    }
}
