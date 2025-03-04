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

    public void UnlockTheSecondPass()//解锁第二关度
    {
        UnlockInitialize();
        //在本地或是其他地方记录（JSON）,方便开始时判断是否已经解锁第二关
    }

    public void UnlockInitialize()//第二关初始化
    {

    }

    //public void SetDifficultyToScore()//随积分解锁更高难度
    //{
    //    if (playerManager.TotalScore>=0&& playerManager.TotalScore<100)
    //    {
    //        //if (one) return;
    //        //单三角
    //        //one=true;
    //    }
    //    else if (playerManager.TotalScore >= 100 && playerManager.TotalScore < 200)
    //    {
    //        //if (two) return;
    //        //正方
    //        //two=true;
    //    }
    //    else if (playerManager.TotalScore >= 200 && playerManager.TotalScore < 300)
    //    {
    //        //if (three) return;
    //        //三角+正方
    //        //three=true;
    //    }
    //    else if (playerManager.TotalScore >= 300 && playerManager.TotalScore < 400)
    //    {
    //        //if (four) return;
    //        //三角+正方+圆
    //        //four=true;
    //    }
    //}
    public void SetDefault()//重置
    {
        //One=false;
        //two=false;
        //three=false;
        //four=false;
    }
}
