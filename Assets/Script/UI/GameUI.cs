using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking.Types;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject hpPrefabs;

    [SerializeField]
    private Transform hpTrans;

    private int playHPTotal;

    private void Start()
    {
        if (GameManage.Instance.isSecond)
            InitPlayHp();
    }

    //��ʼ��Ѫ��

    public void InitPlayHp()
    {
        playHPTotal = GameManage.Instance.secondNpcHp;

        for (int i = 0; i < playHPTotal; i++)
        {
            GameObject go = Instantiate(hpPrefabs, hpTrans);
        }
    }


    //��Ѫ
    public void ReduceHP()
    {
        playHPTotal -= 1;
        hpTrans.GetChild(playHPTotal).GetComponent<HpPrefab>().CloseHPRed();
       
    }

}
