using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftBox : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > 0)
        {
            transform.position += Vector3.down * 2 * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Constant.TAG_PLAYER)|| other.CompareTag(Constant.TAG_ENEMY))
        {
            Character character = Cache.GetCharacter(other);
            character.BuffUlti();
            LevelManager.Instance.StartCoroutine(LevelManager.Instance.GiftBoxCoroutine());
            this.gameObject.SetActive(false);
        }
    }
}
