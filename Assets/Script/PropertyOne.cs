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
        // �����ײ�Ķ����ǩ��"Sphere"������Ը���ʵ��������ģ�
        if (other.gameObject.CompareTag("Player"))
        {
            countdownTimer = other.gameObject.GetComponent<PlayerManager>();
            // ���Ӽ�ʱ���ϵ�ʱ��
            if (canAddTime)
            {
                countdownTimer.AddTime(AddTimeCount);//��ʱ
            }
            else
            {
                countdownTimer.AddTime(-AddTimeCount);//��ʱ
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
