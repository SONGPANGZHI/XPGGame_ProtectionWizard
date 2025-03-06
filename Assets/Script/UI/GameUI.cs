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

    //private void Start()
    //{
    //    if (GameManage.Instance.isSecond)
    //        InitPlayHp();
    //}

    private void OnEnable()
    {
        if (GameManage.Instance.isSecond)
            InitPlayHp();
    }
    //初始化血条

    public void InitPlayHp()
    {
        ClearChild();
        playHPTotal = GameManage.Instance.secondNpcHp;
        for (int i = 0; i < playHPTotal; i++)
        {
            GameObject go = Instantiate(hpPrefabs, hpTrans);
        }
    }

    //扣血
    public void ReduceHP()
    {
        playHPTotal -= 1;
        hpTrans.GetChild(playHPTotal).GetComponent<HpPrefab>().CloseHPRed();
    }

    //清除子物体
    public void ClearChild()
    {
        for (int i = 0; i < hpTrans.childCount; i++)
        {
            Destroy(hpTrans.GetChild(i).gameObject);
        }
    }

}
