using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSecondPassManage : MonoBehaviour
{
    public PlayerManager playerManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnlockTheSecondPass()//�����ڶ��ض�
    {
        UnlockInitialize();
        //�ڱ��ػ��������ط���¼��JSON��,���㿪ʼʱ�ж��Ƿ��Ѿ������ڶ���
    }

    public void UnlockInitialize()//�ڶ��س�ʼ��
    {

    }

    //public void SetDifficultyToScore()//����ֽ��������Ѷ�
    //{
    //    if (playerManager.TotalScore>=0&& playerManager.TotalScore<100)
    //    {
    //        //if (one) return;
    //        //������
    //        //one=true;
    //    }
    //    else if (playerManager.TotalScore >= 100 && playerManager.TotalScore < 200)
    //    {
    //        //if (two) return;
    //        //����
    //        //two=true;
    //    }
    //    else if (playerManager.TotalScore >= 200 && playerManager.TotalScore < 300)
    //    {
    //        //if (three) return;
    //        //����+����
    //        //three=true;
    //    }
    //    else if (playerManager.TotalScore >= 300 && playerManager.TotalScore < 400)
    //    {
    //        //if (four) return;
    //        //����+����+Բ
    //        //four=true;
    //    }
    //}
    public void SetDefault()//����
    {
        //One=false;
        //two=false;
        //three=false;
        //four=false;
    }
}
