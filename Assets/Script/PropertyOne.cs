using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyOne : MonoBehaviour
{
    public bool canAddTime;
    [SerializeField]
    private PlayerManager countdownTimer;
    public float AddTimeCount=10;
    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // 如果碰撞的对象标签是"Sphere"（你可以根据实际情况更改）
        if (other.gameObject.CompareTag("Player"))
        {
            countdownTimer = other.gameObject.GetComponent<PlayerManager>();
            // 增加计时器上的时间
            if (canAddTime)
            {
                countdownTimer.AddTime(AddTimeCount);//加时
            }
            else
            {
                countdownTimer.AddTime(-AddTimeCount);//减时
            }
            
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
