using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    public Transform TF;
    public Transform playerTF;

    [SerializeField] Vector3 offset;

    private void LateUpdate()
    {      
        TF.position = Vector3.Lerp(TF.position, playerTF.transform.position + offset, Time.deltaTime * 8f);
    }

}
